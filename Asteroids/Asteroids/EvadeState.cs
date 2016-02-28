using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class EvadeState : FuSMState
    {

        public EvadeState(FuSMAIControl control) 
        {
            this.control = control;
        }

        

        public override void Update(GameTime GameTime) 
        {
            GameObject asteroid = control.nearestObj;
            Ship ship = Game1.controlShip;
            Vector2 vecBrake = (ship.position - asteroid.position) * 0.02f; //Number makes sure the movement is made at proper speed.
            

            ship.ChangeDirection(vecBrake * activation);
    


        }

        public override float CalculateActivation()
        {
            if (control.nearestObj == null)
                activation = 0.0f;
            else
                activation = 1.0f - (control.nearestObjDist - control.nearestObj.size) / control.safetyRadius;
            CheckBounds();
            return activation;
        }

    }
}
