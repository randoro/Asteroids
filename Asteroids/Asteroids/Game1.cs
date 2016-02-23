using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D ship;
        public static SpriteFont font;
        public static List<GameObject> objList;
        public static Ship controlShip;
        FSMAIControl AIcontrol;
        private bool AIControlled;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship = Content.Load<Texture2D>("ship");
            font = Content.Load<SpriteFont>("font");
            objList = new List<GameObject>();
            controlShip = new Ship(new Vector2(100, 100));
            AIcontrol = new FSMAIControl();
            // TODO: use this.Content to load your game content here

            objList.Add(new Ball(new Vector2(800, 400)));
            objList[0].currentVelocity = new Vector2(-2.1f, 0.0f);
            //objList.Add(new Ball(new Vector2(300, 300)));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();

            if (KeyMouseReader.KeyPressed(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                controlShip.MoveLeft();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                controlShip.MoveRight();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                controlShip.IncreaseSpeed();

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                controlShip.DecreaseSpeed();

            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                AIControlled = !AIControlled;
                //crashes = 0;
                //roundStart = DateTime.Now;

            }

            if (AIControlled)
            {
                AIcontrol.Update(gameTime);
            }

            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].Update(gameTime);

            }


            controlShip.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].Draw(spriteBatch);

            }
            controlShip.Draw(spriteBatch);
            AIcontrol.Draw(spriteBatch);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public static GameObject getNearestObject(GameObject locationObj)
        {
            if (objList.Count == 0)
                return null;

            int nr = 0;
            float length = float.MaxValue;

            for (int i = 0; i < objList.Count; i++)
            {
                float newDist;
                Vector2.Distance(ref locationObj.position, ref objList[i].position, out newDist);
                if (newDist < length)
                {
                    length = newDist;
                    nr = i;
                }

            }

            return objList[nr];
        }
    }
}
