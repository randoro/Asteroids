using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class Ship : GameObject
    {
        float directionRadian;

        public Ship(Vector2 position)
        {
            directionRadian = 0f;
            size = 50;
            currentVelocity = new Vector2(0, 0);
            this.position = position;
            sourceRect = new Rectangle((int)position.X, (int)position.Y, 28, 28);
        }

        public override void Update(GameTime gameTime)
        {
            CorrectSpeed();

            if (!(currentVelocity.X == 0))
                position.X += currentVelocity.X;

            if (!(currentVelocity.Y == 0))
                position.Y += currentVelocity.Y;

            sourceRect.X = (int)position.X;
            sourceRect.Y = (int)position.Y;

            //directionRadian = Angle(Vector2.Zero, currentVelocity);

            currentVelocity.X = 0;
            currentVelocity.Y = 0;

            

        }

        public void MoveRight()
        {
            directionRadian += 0.1f;
            
        }

        public void MoveLeft()
        {
            directionRadian -= 0.1f;


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.ship, position, new Rectangle(0, 0, 28, 28), Color.White, directionRadian, new Vector2(14, 14), 1.0f, SpriteEffects.None, 1.0f);
           
        }


        public void IncreaseSpeed()
        {
            currentVelocity += 5.5f * new Vector2((float)Math.Cos(directionRadian), (float)Math.Sin(directionRadian));
            CorrectSpeed();
        }

        public void DecreaseSpeed()
        {
            currentVelocity -= 5.5f * new Vector2((float)Math.Cos(directionRadian), (float)Math.Sin(directionRadian));
            CorrectSpeed();
        }

        private void CorrectSpeed()
        {
            if (currentVelocity.X > 3)
                currentVelocity.X = 3.0f;

            if (currentVelocity.Y > 3)
                currentVelocity.Y = 3.0f;

            if (currentVelocity.X < -3)
                currentVelocity.X = -3.0f;

            if (currentVelocity.Y < -3)
                currentVelocity.Y = -3.0f;
        }


        public void ChangeDirection(Vector2 offset)
        {
            currentVelocity += offset;
        }
    }
}
