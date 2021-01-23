using System;
namespace Supernova.Code.Util {

    /**
     * Generates Open Simplex Noise for Terrain
     * This method / formula was invented by Kurt Spencer
     *
     * @author Aarnav Tale
     * @author Kurt Spencer
     */
    public class NoiseGenerator {

        // Constants (The number is here instead of the operation to reduce unnecessary calculations)
        private static readonly double STRETCH_2D = -0.211324865405187; // (1 / Math.sqrt(2 + 1) -1) / 2
        private static readonly double SQUISH_2D = 0.366025403784439; // (Math.sqrt(2 + 1) - 1) / 2

        private static readonly double NORMAL_2D = 47;
        //Gradients for 2D. They approximate the directions to the
        //vertices of an octagon from the center.
        private static readonly short[] gradients2D = new short[] { 5, 2, 2, 5, -5, 2, -2, 5, 5, -2, 2, -5, -5, -2, -2, -5 };
        private readonly short[] permutation;

        /**
         * Initialzes the Noise Generator using a permutation from a specified 64 Bit seed
         *
         * @param seed 64 Bit Long
         */
        public NoiseGenerator(long seed) {

            permutation = new short[256];
            short[] source = new short[256];

            for (short iteration = 0; iteration < 256; iteration++) {

                source[iteration] = iteration;
            }

            // Simplex Constants (Revamped in Open Simplex)
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;

            // Iterate through all 256 Indices
            for (int index = 255; index >= 0; index--) {

                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int random = (int)((seed + 31) % (index + 1));

                if (random < 0) {

                    random += (index + 1);
                }

                permutation[index] = source[random];
                source[random] = source[index];
            }
        }

        /**
         * Faster Math Floor function
         *
         * @param n Double to Floor
         * @return Floored Value
         */
        private static int fastFloor(double n) {

            int initialX = (int)n;
            return n < initialX ? initialX - 1 : initialX;
        }

        /**
         * Generates 2D Open Simplex Noise at the specified point
         *
         * @param x X Coordinate
         * @param y Y Coordinate
         * @return Noise value
         */
        public double evaluate(double x, double y) {

            // Input Coordinates are inserted into the stretched grid
            double stretchOffset = (x + y) * STRETCH_2D;
            double stretchedX = x + stretchOffset;
            double stretchedY = y + stretchOffset;

            // Fast Floor to get the coordinates of the Stretched Square (Rhombus) origin
            int fastFloorX = fastFloor(stretchedX);
            int fastFloorY = fastFloor(stretchedY);

            // Calculate grid coordinates relative to the Stretched Square (Rhombus)
            double insertX = stretchedX - fastFloorX;
            double insertY = stretchedY - fastFloorY;

            // Sum the inserted value relative calculations
            double insertSum = insertX + insertY;

            // Input Coordinates are inserted into the skewed (squished) grid
            double squishOffset = (fastFloorX + fastFloorY) * SQUISH_2D;
            double xb = fastFloorX + squishOffset;
            double yb = fastFloorY + squishOffset;

            // Positions Relative to the Origin
            double displaceX = x - xb;
            double displaceY = y - yb;

            // Future Use
            double derivativeX, derivativeY, calculateValue = 0;
            int externalX, externalY;

            // (1, 0) Contribution
            double displaceX10 = displaceX - 1 - SQUISH_2D;
            double displaceY10 = displaceY - 0 - SQUISH_2D;
            double attenuation10 = 2 - displaceX10 * displaceX10 - displaceY10 * displaceY10;

            if (attenuation10 > 0) {

                attenuation10 *= attenuation10;
                calculateValue += attenuation10 * attenuation10 * extrapolate(fastFloorX + 1, fastFloorY, displaceX10, displaceY10);
            }

            // (0, 1) Contribution
            double displaceX01 = displaceX - 0 - SQUISH_2D;
            double displaceY01 = displaceY - 1 - SQUISH_2D;
            double attenuation01 = 2 - displaceX01 * displaceX01 - displaceY01 * displaceY01;

            if (attenuation01 > 0) {

                attenuation01 *= attenuation01;
                calculateValue += attenuation01 * attenuation01 * extrapolate(fastFloorX, fastFloorY + 1, displaceX01, displaceY01);
            }

            // (0, 0) 2D Simplex Triangle Calculations
            if (insertSum <= 1) {

                double insertZ = 1 - insertSum;
                if (insertZ > insertX || insertZ > insertY) {

                    if (insertX > insertY) {

                        externalX = fastFloorX + 1;
                        externalY = fastFloorY - 1;
                        derivativeX = displaceX - 1;
                        derivativeY = displaceY + 1;
                    } else {

                        externalX = fastFloorX - 1;
                        externalY = fastFloorY + 1;
                        derivativeX = displaceX + 1;
                        derivativeY = displaceY - 1;
                    }
                } else {

                    externalX = fastFloorX + 1;
                    externalY = fastFloorY + 1;
                    derivativeX = displaceX - 1 - 2 * SQUISH_2D;
                    derivativeY = displaceY - 1 - 2 * SQUISH_2D;
                }
            } else {

                // (1, 1) 2D Simplex Triangle Calculations
                double insertZ = 2 - insertSum;
                if (insertZ < insertX || insertZ < insertY) {

                    if (insertX > insertY) {

                        externalX = fastFloorX + 2;
                        externalY = fastFloorY;
                        derivativeX = displaceX - 2 - 2 * SQUISH_2D;
                        derivativeY = displaceY + 0 - 2 * SQUISH_2D;
                    } else {

                        externalX = fastFloorX;
                        externalY = fastFloorY + 2;
                        derivativeX = displaceX + 0 - 2 * SQUISH_2D;
                        derivativeY = displaceY - 2 - 2 * SQUISH_2D;
                    }
                } else {

                    derivativeX = displaceX;
                    derivativeY = displaceY;
                    externalX = fastFloorX;
                    externalY = fastFloorY;
                }

                fastFloorX += 1;
                fastFloorY += 1;
                displaceX = displaceX - 1 - 2 * SQUISH_2D;
                displaceY = displaceY - 1 - 2 * SQUISH_2D;
            }

            // (0, 0) or (1, 1) Contribution
            double attenuation0011 = 2 - displaceX * displaceX - displaceY * displaceY;
            if (attenuation0011 > 0) {

                attenuation0011 *= attenuation0011;
                calculateValue += attenuation0011 * attenuation0011 * extrapolate(fastFloorX, fastFloorY, displaceX, displaceY);
            }

            // Extra Vertex
            double attenuationExtra = 2 - derivativeX * derivativeX - derivativeY * derivativeY;
            if (attenuationExtra > 0) {

                attenuationExtra *= attenuationExtra;
                calculateValue += attenuationExtra * attenuationExtra * extrapolate(externalX, externalY, derivativeX, derivativeY);
            }

            // Finally return our Value
            return calculateValue / NORMAL_2D;
        }

        /**
         * Extrapolate Data within the missing points
         *
         * @param floorX      Fast Floor calculation for X
         * @param floorY      Fast Floor calculation for Y
         * @param derivativeX Derivative (or Displacement) X Calculation
         * @param derivativeY Derivative (or Displacement) Y Calculation
         * @return Extrapolated Data
         */
        private double extrapolate(int floorX, int floorY, double derivativeX, double derivativeY) {

            int index = permutation[(permutation[floorX & 0xFF] + floorY) & 0xFF] & 0x0E;
            return gradients2D[index] * derivativeX + gradients2D[index + 1] * derivativeY;
        }
    }
}