using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class FuSMAIControl
    {
        FuSMachine fuzzy;
        public bool willCollide = false;
        public float safetyRadius = 50;
        public GameObject nearestObj = null;
        public int maxSpeed = 3;
        public float nearestObjDist;
        public float nearestCoinDist;

        public FuSMAIControl()
        {

            nearestObjDist = 0.0f;

            fuzzy = new FuSMachine();
            fuzzy.AddState(new EvadeState(this));
            fuzzy.AddState(new MoveState(this));
            fuzzy.Reset();

        }


        public void Update(GameTime gameTime)
        {

            UpdatePerceptions(gameTime);
            fuzzy.UpdateMachine(gameTime);
        }


        private void UpdatePerceptions(GameTime gameTime)
        {
            //store closest asteroid and powerup
            nearestObj = null;
            nearestObj = Game1.getNearestEnemyObject(Game1.controlShip);

            //asteroid collision determination
            willCollide = false;
            if (nearestObj != null)
            {
                Vector2.Distance(ref nearestObj.position, ref Game1.controlShip.position, out nearestObjDist);
                 float adjSafetyRadius = safetyRadius + nearestObj.size;

                //if you're too close,
                //flag a collision
                if (nearestObjDist <= adjSafetyRadius)
                    willCollide = true;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fuzzy.Draw(spriteBatch);
        }

    }
}
