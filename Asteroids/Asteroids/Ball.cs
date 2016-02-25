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

        public Ball(Vector2 position, Vector2 currentVelocity)
        {
            size = 50;
            this.currentVelocity = currentVelocity;
            this.position = position;
            sourceRect = new Rectangle((int)position.X, (int)position.Y, 28, 28);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!(currentVelocity.X == 0))
                position.X += currentVelocity.X;

            if (!(currentVelocity.Y == 0))
                position.Y += currentVelocity.Y;

            sourceRect.X = (int)position.X;
            sourceRect.Y = (int)position.Y;

            directionRadian = Angle(Vector2.Zero, currentVelocity);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.ship, position, new Rectangle(0, 0, 28, 28), Color.Blue, directionRadian, new Vector2(14, 14), 1.0f, SpriteEffects.None, 1.0f);

        }

        

    }
}
