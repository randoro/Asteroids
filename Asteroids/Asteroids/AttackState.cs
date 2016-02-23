using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class AttackState : FSMState
    {

        public AttackState(FSMAIControl control) 
        {
            this.control = control;
        }

        public override void Update(GameTime GameTime)
        {

        }

        public override float CalculateActivation()
        {
            return 0.0f;
        }
    }
}
