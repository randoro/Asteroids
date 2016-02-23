using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Ball : GameObject
    {
        float directionRadian;

        public Ball(Vector2 position)
        {
            size = 50;
            currentVelocity = new Vector2(0, 0);
            this.position = position;
            sourceRect = new Rectangle((int)position.X, (int)position.Y, 28, 28);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!(currentVelocity.X == 0))
                position.X += currentVelocity.X;

            if (!(currentVelocity.Y == 0))
                position.Y += currentVelocity.Y;
            //position += currentVelocity * Vector2.Normalize(directionVect); //* new Vector2((float)Math.Cos(directionRadian), (float)Math.Sin(directionRadian));
            sourceRect.X = (int)position.X;
            sourceRect.Y = (int)position.Y;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.ship, position, new Rectangle(0, 0, 28, 28), Color.White, directionRadian, new Vector2(14, 14), 1.0f, SpriteEffects.None, 1.0f);

        }

    }
}
