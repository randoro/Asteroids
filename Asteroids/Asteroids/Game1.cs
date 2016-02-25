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
        FuSMAIControl AIcontrol;
        private bool AIControlled;

        static int spawnCounter = 0;
        static int spawnCounterMax = 5;

        static int despawnCounter = 0;
        static int despawnCounterMax = 400;


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
            graphics.PreferredBackBufferWidth = Globals.windowX;
            graphics.PreferredBackBufferHeight = Globals.windowY;
            graphics.ApplyChanges();
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
            controlShip = new Ship(new Vector2(500, 500));
            AIcontrol = new FuSMAIControl();
            // TODO: use this.Content to load your game content here

            //objList.Add(new Ball(new Vector2(800, 400)));
            //objList[0].currentVelocity = new Vector2(-2.1f, 0.0f);
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

            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                AIControlled = !AIControlled;
                //crashes = 0;
                //roundStart = DateTime.Now;

            }

            SpawnBall();

            ClearBalls();

            if (AIControlled)
            {
                AIcontrol.Update(gameTime);

            }
            else
            {
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
            }

            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].Update(gameTime);

            }

            controlShip.Update(gameTime);

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

            base.Draw(gameTime);
        }


        public static GameObject getNearestEnemyObject(GameObject locationObj)
        {
            if (objList.Count == 0)
                return null;

            int nr = -1;
            float length = float.MaxValue;

            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i] is Ball)
                {
                    float newDist;
                    Vector2.Distance(ref locationObj.position, ref objList[i].position, out newDist);
                    if (newDist < length)
                    {
                        length = newDist;
                        nr = i;
                    }
                }

            }

            return objList[nr];
        }

        public static void SpawnBall()
        {
            spawnCounter++;
            if(spawnCounter > spawnCounterMax) 
            {
                spawnCounter = 0;

                int xory = Globals.rand.Next(0, 2);
                int side = Globals.rand.Next(0, 2);
                int xpos = 0;
                int ypos = 0;

                if (xory == 0)
                {
                    xpos = side * Globals.windowX;
                    ypos = Globals.rand.Next(0, Globals.windowY);
                }
                else
                {
                    xpos = Globals.rand.Next(0, Globals.windowX);
                    ypos = side * Globals.windowY;
                }

                double xSpeed = Globals.rand.NextDouble();
                double ySpeed = Globals.rand.NextDouble();

                float realSpeedX = (float)((xSpeed * 2) - 1.0f);
                float realSpeedY = (float)((ySpeed * 2) - 1.0f);

                objList.Add(new Ball(new Vector2(xpos, ypos), new Vector2(realSpeedX, realSpeedY)));

            }

        }

        public static void ClearBalls()
        {
            despawnCounter++;
            if (despawnCounter > despawnCounterMax)
            {
                despawnCounter = 0;


                for (int i = objList.Count - 1; i > 0; i--)
                {
                    Vector2 pos = objList[i].position;
                    if (pos.X < 0 || pos.X > Globals.windowX || pos.Y < 0 || pos.Y > Globals.windowY)
                    {
                        objList.RemoveAt(i);
                    }

                }
            }
        }
    }
}
