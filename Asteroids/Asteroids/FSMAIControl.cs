using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class FSMAIControl
    {
        FSMachine fsm;
        public bool willCollide = false;
        public float safetyRadius = 15.0f;
        public GameObject nearestObj = null;
        public int maxSpeed = 3;
        public float nearestObjDist;

        public FSMAIControl()
        {

            nearestObjDist = 0.0f;

            fsm = new FSMachine();
            fsm.AddState(new EvadeState(this));
            fsm.AddState(new MoveState(this));
            fsm.AddState(new AttackState(this));
            fsm.Reset();

        }


        public void Update(GameTime gameTime)
        {

            UpdatePerceptions(gameTime);
            fsm.UpdateMachine(gameTime);
        }


        private void UpdatePerceptions(GameTime gameTime)
        {
            if (willCollide)
            {
                safetyRadius = 50.0f;
            }
            else
            {
                safetyRadius = 50.0f;
            }

            //store closest asteroid and powerup
            nearestObj = null;
            //m_nearestPowerup  = NULL;
            nearestObj = Game1.getNearestObject(Game1.controlShip);
            //if(Game1.controlShip.GetShotLevel() < Globals.MAX_SHOT_LEVEL)
            //    m_nearestPowerup  = Game.GetClosestGameObj(m_ship,GameObj::OBJ_POWERUP);
            
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

            ////powerup near determination
            //if(m_nearestPowerup)
            //    m_nearestPowerupDist = m_nearestPowerup->m_position.Distance(m_ship->m_position); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(Game1.font, "BestLane: "+bestlane.ToString(), new Vector2(30, 30), Color.Black);
            //spriteBatch.DrawString(Game1.font, "CurrentState: " + fsm.currentState.GetID().ToString(), new Vector2(30, 50), Color.Black);
            fsm.Draw(spriteBatch);
        }

    }
}
