﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Boids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D boidTex;
        Texture2D obstacleTex;
        SteeringBehaviourManager sbm;
        ButtonState lastLeftMouseState;
        ButtonState lastRightMouseState;

        public static Random rnd;
        public static Vector2 windowBounds;
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
            rnd = new Random();
            boidTex = Content.Load<Texture2D>(@"simpleship");
            obstacleTex = Content.Load<Texture2D>(@"obstacleCircle");
            sbm = new SteeringBehaviourManager(boidTex);
            graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 650;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            windowBounds = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
            BuildWallAroundMap();
            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            sbm.Update(gameTime);
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && lastLeftMouseState != ButtonState.Pressed)
            {
                sbm.AddShip(boidTex, Mouse.GetState().Position.ToVector2());
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed && lastRightMouseState != ButtonState.Pressed)
            {
                sbm.AddObstacle(obstacleTex, Mouse.GetState().Position.ToVector2());
            }
            // TODO: Add your update logic here
            lastLeftMouseState = Mouse.GetState().LeftButton;
            lastRightMouseState = Mouse.GetState().RightButton;
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
            sbm.Draw(spriteBatch);
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void BuildWallAroundMap()
        {
            for (int i = 0; i < 1000; i++)
            {
                sbm.AddObstacle(obstacleTex, new Vector2(i, 20));
                sbm.AddObstacle(obstacleTex, new Vector2(i, 600));
            }
            for (int i = 0; i < 650; i++)
            {
                sbm.AddObstacle(obstacleTex, new Vector2(20, i));
                sbm.AddObstacle(obstacleTex, new Vector2(950, i));

            }
            /*Adds 200 boids at the beggining*/
            for (int i = 0; i < 50; i++)
            {
                sbm.AddShip(boidTex, new Vector2(rnd.Next(100, 800), rnd.Next(100, 400)));

            }
        }
    }
}
