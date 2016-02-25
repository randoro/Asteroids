using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{

    class FuSMachine
    {
        List<FuSMState> states;
        List<FuSMState> activated;
        float highestTotal = 0.0f;

        public FuSMachine()
        {
            states = new List<FuSMState>();
            activated = new List<FuSMState>();
        }


        public void UpdateMachine(GameTime gameTime)
        {
            //don't do anything if you have no states
            if (states.Count == 0)
                return;

            //check for activations, and then update
            activated.Clear();
            List<FuSMState> nonActiveStates = new List<FuSMState>();
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
            spriteBatch.DrawString(Game1.font, "Max Total: " + highestTotal, new Vector2(30, 10), Color.White);
            float tempHighest = 0.0f;
            
                tempHighest += states[0].activation;
                spriteBatch.DrawString(Game1.font, "EvadeState: " + states[0].activation.ToString(), new Vector2(30, 30 + 25 * 0), Color.White);

                tempHighest += states[1].activation;
                spriteBatch.DrawString(Game1.font, "MoveToState: " + states[1].activation.ToString(), new Vector2(30, 30 + 25 * 1), Color.White);
            

            if (tempHighest > highestTotal)
            {
                highestTotal = tempHighest;
            }


        }

        public void AddState(FuSMState newState)
        {
            states.Add(newState);
        }

        public bool IsActive(FuSMState state)
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
