using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{

    public enum FSMStateEnum { none, idleBestState, checkPathState, moveState }

    class FSMachine
    {
        List<FSMState> states;
        List<FSMState> activated;
        float highestTotal = 0.0f;

        public FSMachine()
        {
            states = new List<FSMState>();
            activated = new List<FSMState>();
        }


        public void UpdateMachine(GameTime gameTime)
        {
            //don't do anything if you have no states
            if (states.Count == 0)
                return;

            //check for activations, and then update
            activated.Clear();
            List<FSMState> nonActiveStates = new List<FSMState>();
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].CalculateActivation() > 0)
                    activated.Add(states[i]);
                else
                    nonActiveStates.Add(states[i]);
            }

            //Exit all non active states for cleanup
            if (nonActiveStates.Count != 0)
            {
                for (int i = 0; i < nonActiveStates.Count; i++)
                    nonActiveStates[i].Exit();
            }

            //Update all activated states
            if (activated.Count != 0)
            {
                for (int i = 0; i < activated.Count; i++)
                    activated[i].Update(gameTime);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "max total " + highestTotal, new Vector2(30, 10), Color.White);
            float tempHighest = 0.0f;
            for (int i = 0; i < states.Count; i++)
            {
                tempHighest += states[i].activation;
                spriteBatch.DrawString(Game1.font, "state" + i + ": " + states[i].activation.ToString(), new Vector2(30, 30 + 20 * i), Color.White);
            }
            if (tempHighest > highestTotal)
            {
                highestTotal = tempHighest;
            }


        }

        public void AddState(FSMState newState)
        {
            states.Add(newState);
        }

        public void SetDefaultState(FSMState state) 
        {
            //defaultState = state; 
        }

        void SetGoalID(FSMStateEnum goal) 
        {
            //goalStateID = goal; 
        }

        public bool IsActive(FSMState state)
        {
            if (activated.Count != 0)
            {
                for (int i = 0; i < activated.Count; i++)
                    if (activated[i] == state)
                        return true;
            }
            return false;
        }


        public void Reset()
        {
            for (int i = 0; i < states.Count; i++)
            {
                states[i].Exit();
                states[i].Init();
            }

        }


    }
}
