using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class MoveState : FuSMState
    {

        public MoveState(FuSMAIControl control) 
        {
            this.control = control;
        }

        public override void Update(GameTime GameTime) 
        {
            GameObject nearestBall = control.nearestObj;
            Ship ship = Game1.controlShip;
            Vector2 deltaPos = nearestBall.position - ship.position;
            Vector2 targetPos = nearestBall.position;

            float dotVelocity = 0.0f;
            Vector2.Dot(ref ship.currentVelocity, ref nearestBall.currentVelocity, out dotVelocity);

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
                float combinedSpeed = ((deltaPos * control.maxSpeed) + nearestBall.currentVelocity).Length();
                float predictionTime = deltaPos.Length() / combinedSpeed;
                targetPos = nearestBall.position + (nearestBall.currentVelocity * predictionTime);
                deltaPos = (targetPos - ship.position) * 0.02f; //Number makes sure the movement is made at proper speed.
            }

            ship.ChangeDirection(deltaPos * activation);

        }

        public override void Init() 
        {

        }

        public override float CalculateActivation()
        {
            if (control.nearestObj == null)
                activation = 0.0f;
            else
                activation = (control.nearestObjDist - Globals.VisionDistance) / Globals.VisionDistance;
                

            CheckBounds();
            return activation;
        }

    }
}
