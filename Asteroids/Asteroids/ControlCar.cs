using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class Ship
    {
        float directionRadian;
        float currentVelocity;
        public Vector2 position;
        Rectangle sourceRect;

        public Ship(Vector2 position)
        {
            directionRadian = 0f;
            currentVelocity = 0.1f;
            this.position = position;
            sourceRect = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }

        public void Update(GameTime gameTime)
        {
            position += currentVelocity * new Vector2((float)Math.Cos(directionRadian), (float)Math.Sin(directionRadian));
            sourceRect.X = (int)position.X;
            sourceRect.Y = (int)position.Y;

        }


        public void MoveRight()
        {
            directionRadian += 0.1f;
        }

        public void MoveLeft()
        {
            directionRadian -= 0.1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.ship, position, new Rectangle(0, 0, 60, 110), Color.White, directionRadian, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }

    }
}
