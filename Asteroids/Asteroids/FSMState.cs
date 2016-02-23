using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public abstract class FSMState
    {
        FSMStateEnum state;
        public float activation;
        protected FSMAIControl control;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public abstract void Update(GameTime GameTime);
        public virtual void Init() { activation = 0.0f; }
        public abstract float CalculateActivation();

        public virtual void CheckLowerBound(float lbound = 0.0f) { if (activation < lbound) activation = lbound; }
        public virtual void CheckUpperBound(float ubound = 1.0f) { if (activation > ubound) activation = ubound; }
        public virtual void CheckBounds(float lb = 0.0f, float ub = 1.0f) { CheckLowerBound(lb); CheckUpperBound(ub); }
    }
}
