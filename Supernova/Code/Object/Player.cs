using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Supernova.Code.World;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public static class Player {

        private static readonly Vector2 drawPosition = new Vector2(360, 400);
        private static Vector2 dimensions = new Vector2(48, 48);

        private static Vector2 position = new Vector2(0, 0);
        private static Vector2 velocity = new Vector2(.00001f, (float) Math.PI * 3 / 2);

        private static float angle = (float) Math.PI * 3 / 2;


        private static Texture2D sprite = SpriteManager.GetTexture("PLAYER");
        private static float timer = 0f;
        
        public static Vector2 GetDrawPosition() {
            return drawPosition;
        }

        public static Vector2 GetPosition() {
            return position;
        }

        public static Vector2 GetVelocity() {
            return velocity;
        }

        public static float GetXVelocity() {
            return velocity.X;
        }

        public static float GetYVelocity() {
            return velocity.Y;
        }

        public static float GetAngle() {
            return angle;
        }

        public static void SetAngle(float angle) {
            Player.angle = angle;
        }

        public static void addToVelocity(float amount, float angle) {

            float CVX = (float) (velocity.X * Math.Cos(velocity.Y));
            float CVY = (float) (velocity.X * Math.Sin(velocity.Y));

            float NVX = (float)(amount * Math.Cos(angle));
            float NVY = (float)(amount * Math.Sin(angle));

            velocity.X = (float) Math.Sqrt(Math.Pow(CVX + NVX, 2) + Math.Pow(CVY + NVY, 2));

            if (CVX + NVX > 0 && CVY + NVY > 0)
                velocity.Y = (float)Math.Atan((CVY + NVY)/(CVX + NVX));

            else if (CVX + NVX < 0 && CVY + NVY > 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + Math.PI);

            else if (CVX + NVX < 0 && CVY + NVY < 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + Math.PI);

            else if (CVX + NVX > 0 && CVY + NVY < 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + 2 * Math.PI);

        }
        
        private static void UpdateHealth() {
            
        }
        
        private static void CheckCollision() {
            
        }

        private static bool IsCollisionAsteroid(Asteroid asteroid) {
            
            return IsCollisionBody(asteroid.GetX(), asteroid.GetY(), asteroid.GetRadius());
        }

        private static bool IsCollisionPlanet(Planet planet) {
            
            return IsCollisionBody(planet.GetX(), planet.GetY(), planet.GetRadius());
        }
        
        private static bool IsCollisionBody(double bodyX, double bodyY, double bodyRadius)
        {

            double pointX1 = drawPosition.X,
                pointY1 = drawPosition.Y - dimensions.Y / 2,
                pointX2 = drawPosition.X - dimensions.X / 2,
                pointY2 = drawPosition.Y + dimensions.Y / 2,
                pointX3 = drawPosition.X + dimensions.X / 2,
                pointY3 = drawPosition.Y + dimensions.Y / 2;
            return isCollisionLineCircle(pointX1, pointY1, pointX2, pointY2, bodyX, bodyY, bodyRadius)
                   || isCollisionLineCircle(pointX1, pointY1, pointX3, pointY3, bodyX, bodyY, bodyRadius);
        }

        private static bool isCollisionLineCircle(double lineX1, double lineY1, double lineX2, double lineY2, double circleX,
            double circleY, double radius) {

            double lineLen = distance(lineX1, lineY1, lineX2, lineY2);
            double dotProduct = ((circleX - lineX1) * (lineX2 - lineX1) + (circleY - lineY1) * (lineY2 - lineY1)) /
                                Math.Pow(lineLen, 2);
            double closestX = dotProduct * (lineX2 - lineX1) + lineX1;
            double closestY = dotProduct * (lineY2 - lineY1) + lineY1;
            double distLineClosest1 = distance(lineX1, lineY1, closestX, closestY);
            double distLineClosest2 = distance(lineX2, lineY2, closestX, closestY);
            if (Math.Abs(distLineClosest1 + distLineClosest2 - lineLen) > 0.5)
                return false;
            double distCircleClosest = distance(circleX, circleY, closestX, closestY);
            return distCircleClosest < radius;
        }

        private static double distance(double x1, double y1, double x2, double y2) {
            
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static void Tick() {

            Vector2 gravity = WorldManager.getGravityEffects(position);

            addToVelocity(gravity.X, gravity.Y);


            velocity.X = Math.Min(velocity.X, 10);
            velocity.X = Math.Max(velocity.X, -10);

            if (velocity.Y < 0)
                velocity.Y = (float) Math.PI * 2 + velocity.Y;

            velocity.Y = (float) (velocity.Y % (Math.PI * 2));

            if (angle < 0)
                angle = (float) Math.PI * 2 + angle;

            angle = (float) (angle % (Math.PI * 2));

            position.X += (float) (velocity.X * Math.Cos(velocity.Y));
            position.Y += (float) (velocity.X * Math.Sin(velocity.Y));

            Camera.SetX(-position.X);
            Camera.SetY(-position.Y);

            KeyboardState keyboardState = Keyboard.GetState();
            timer += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && timer > 100) {
                Shoot();
                timer = 0f;
            }
        }
        
        
        private static void Shoot() {
            
            Bullet bullet = new Bullet(position, angle);
        }


        public static void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(sprite, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (drawPosition.X - dimensions.X / 2)), (int)(Camera.GetHeightScalar() * (drawPosition.Y - dimensions.Y / 2)), (int)(Camera.GetWidthScalar() * (dimensions.X)), (int)(Camera.GetHeightScalar() * dimensions.Y)), Color.White);


        }
    }
}
