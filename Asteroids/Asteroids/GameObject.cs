using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public abstract class GameObject
    {

        public Vector2 position;
        public Rectangle sourceRect;
        public float size;
        public Vector2 currentVelocity;

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public float Angle(Vector2 from, Vector2 to)
        {
            return (float)Math.Atan2(from.X - to.X, to.Y - from.Y);
        }
    }
}
