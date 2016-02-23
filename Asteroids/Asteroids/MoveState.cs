using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class MoveState : FSMState
    {

        public MoveState(FSMAIControl control) 
        {
            this.control = control;
        }

        public override void Update(GameTime GameTime) 
        {
            //turn and then thrust towards closest asteroid
            GameObject asteroid = control.nearestObj;
            Ship ship = Game1.controlShip;
            Vector2 deltaPos = asteroid.position - ship.position;
            Vector2 targetPos = asteroid.position;

            float dotVelocity = 0.0f;
            Vector2.Dot(ref ship.currentVelocity, ref asteroid.currentVelocity, out dotVelocity);

            float tempDot = 0.0f;
            Vector2.Dot(ref deltaPos, ref ship.currentVelocity, out tempDot);
            if ((tempDot < 0) || (dotVelocity > -0.93))//magic number == about 21 degrees
            {
                Vector2 shipVel = ship.currentVelocity;
                Vector2 tempVect = Vector2.Zero;
                if (tempVect != Vector2.Zero)
                {
                    tempVect = Vector2.Normalize(shipVel) * control.maxSpeed;
                }
                shipVel = tempVect;
                float combinedSpeed = ((deltaPos * control.maxSpeed) + asteroid.currentVelocity).Length();
                float predictionTime = deltaPos.Length() / combinedSpeed;
                targetPos = asteroid.position + (asteroid.currentVelocity * predictionTime);
                deltaPos = (targetPos - ship.position) * 0.01f;
            }

            //move there
            ship.ChangeDirection(deltaPos * activation);

            //parent->m_target->m_position = asteroid->m_position;
        }

        public override void Init() 
        {

        }

        public override float CalculateActivation()
        {
            if (control.nearestObj == null)
                activation = 0.0f;
            else
                activation = (control.nearestObjDist - Globals.FU_APPROACH_DIST) / Globals.FU_APPROACH_DIST;
                

            CheckBounds();
            return activation;
        }

    }
}
