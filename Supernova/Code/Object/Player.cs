using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Supernova.Code.World;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public static class Player {

        private static Vector2 position = new Vector2(360, 225);
        private static Vector2 velocity = new Vector2(.00001f, (float) Math.PI * 3 / 2);

        private static Boolean engine = false;
        
        private static Texture2D sprite = SpriteManager.GetTexture("PLAYER");
        private static Texture2D sprite2 = SpriteManager.GetTexture("PLAYER1");
        private static float timer = 0f;
        
        public static Vector2 DrawPosition { get; } = new Vector2(360, 225);
        
        public static Vector2 Size { get; } = new Vector2(32, 32);
        
        public static float Angle { get; set; } = (float) Math.PI * 3 / 2;

        public static Vector2 GetPosition() {
            return position;
        }
        
        public static float X {
            get { return position.X; }
        }
        
        public static float Y {
            get { return position.Y; }
        }
        
        public static float VX {
            get { return velocity.X; }
        }
        
        public static float VY {
            get { return velocity.Y; }
        }
        
        public static void addToVelocity(float amount, float angle) {

            float CVX = (float) (velocity.X * Math.Cos(velocity.Y));
            float CVY = (float) (velocity.X * Math.Sin(velocity.Y));

            float NVX = (float)(amount * Math.Cos(angle));
            float NVY = (float)(amount * Math.Sin(angle));

            velocity.X = (float) Math.Sqrt(Math.Pow(CVX + NVX, 2) + Math.Pow(CVY + NVY, 2));

            if (CVX + NVX >= 0 && CVY + NVY >= 0)
                velocity.Y = (float)Math.Atan((CVY + NVY)/(CVX + NVX));

            else if (CVX + NVX <= 0 && CVY + NVY >= 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + Math.PI);

            else if (CVX + NVX <= 0 && CVY + NVY <= 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + Math.PI);

            else if (CVX + NVX >= 0 && CVY + NVY <= 0)
                velocity.Y = (float)(Math.Atan((CVY + NVY) / (CVX + NVX)) + 2 * Math.PI);

        }
        
        private static void UpdateHealth() {
            
        }
        
        private static void CheckCollision() {
            
        }

        private static bool IsCollisionAsteroid(Asteroid asteroid) {
            
            return IsCollisionBody(asteroid.X, asteroid.Y, asteroid.Radius);
        }

        private static bool IsCollisionPlanet(Planet planet) {
            
            return IsCollisionBody(planet.X, planet.Y, planet.Radius);
        }
        
        private static bool IsCollisionBody(double bodyX, double bodyY, double bodyRadius)
        {

            double pointX1 = DrawPosition.X,
                pointY1 = DrawPosition.Y - Size.Y / 2,
                pointX2 = DrawPosition.X - Size.X / 2,
                pointY2 = DrawPosition.Y + Size.Y / 2,
                pointX3 = DrawPosition.X + Size.X / 2,
                pointY3 = DrawPosition.Y + Size.Y / 2;
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


            velocity.X = Math.Min(velocity.X, 5);
            velocity.X = Math.Max(velocity.X, -5);

            if (velocity.Y < 0)
                velocity.Y = (float) Math.PI * 2 + velocity.Y;

            velocity.Y = (float) (velocity.Y % (Math.PI * 2));

            if (Angle < 0)
                Angle = (float) Math.PI * 2 + Angle;

            Angle = (float) (Angle % (Math.PI * 2));

            position.X += (float) (velocity.X * Math.Cos(velocity.Y));
            position.Y += (float) (velocity.X * Math.Sin(velocity.Y));

            Camera.SetX(-(position.X - 360));
            Camera.SetY(-(position.Y - 225));

            KeyboardState keyboardState = Keyboard.GetState();
            timer += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && timer > 100) {
                Shoot();
                timer = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                engine = true;
            else
                engine = false;
        }
        
        
        private static void Shoot() {
            
            Bullet bullet = new Bullet(position, Angle);
        }


        public static void Render(SpriteBatch _spriteBatch) {
            
            if (!engine)
                _spriteBatch.Draw(sprite, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (DrawPosition.X - Size.X / 2)), (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Size.Y / 2)), (int)(Camera.GetWidthScalar() * (Size.X)), (int)(Camera.GetHeightScalar() * Size.Y)),null, Color.White, (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2), new Vector2(Size.X / 2 * Camera.GetWidthScalar(), Camera.GetHeightScalar() * (Size.Y / 2 -2)) , SpriteEffects.None, 0f);

            else
                _spriteBatch.Draw(sprite2, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (DrawPosition.X - Size.X / 2)), (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Size.Y / 2)), (int)(Camera.GetWidthScalar() * (Size.X)), (int)(Camera.GetHeightScalar() * Size.Y)), null, Color.White, (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2), new Vector2(Size.X / 2 * Camera.GetWidthScalar(), Camera.GetHeightScalar() * (Size.Y / 2 - 2)), SpriteEffects.None, 0f);

        }
    }
}
