﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XTron;

namespace WindowSystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WindowDemo : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        WindowManager windowManager;
        private Camera camera;
        Window closeWindow;

        String[] apps = { "Recorder", "Browser", "Sound", "Gallery", "Video" , "Music", "Health", "Weather","Download","Mobile"  };

        private readonly string dummyText = "Lorem ipsum dolor sit amet, et his phaedrum intellegat, \n" +
            "ut dicta propriae ullamcorper eos. Movet putent constituam est id. Solet nonumy duo id,\n " +
            "docendi constituam duo no, dicta movet dissentiet pro at. Est te intellegat delicatissimi,\n" +
            " vel no iudico quando intellegebat.\n" +
            "Mutat audire sed te, sale dolor tacimates pro cu, ut nam elit mandamus oportere.\n" +
            "An sed etiam antiopam.Ea dicunt quaeque conceptam quo.Accusata interpretaris mei eu, agam impedit vis ad. \n" +
            "Qui eu audiam scriptorem, utamur pertinax ocurreret et nam.Ad usu natum interesset, et nulla scripserit ius.\n";

        public WindowDemo()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;
            int w = 1920;// GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int h = 1080;// GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = w;
            graphics.PreferredBackBufferHeight = h;
            graphics.ApplyChanges();
            // graphics.ToggleFullScreen();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChanged;
            IsMouseVisible = true;
            camera = new Camera();
        }

        private void WindowSizeChanged(object sender, EventArgs e)
        {
            if (windowManager != null)
            {
                windowManager.Resized();
            }
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
            windowManager = new WindowManager(graphics, Content, spriteBatch);

            Button icon = new Button(Content, spriteBatch, new Vector2(32, 32), new Vector2(64, 64));
            icon.Text = "Editor";
            icon.ControlClicked += HandleButtonClick;
            icon.Icon = Content.Load<Texture2D>("Icons/044-memo");
            windowManager.AddChild(icon);

            Button appsButton = new Button(Content, spriteBatch, new Vector2(128, 32), new Vector2(64, 64));
            appsButton.Text = "Apps";
            appsButton.ControlClicked += HandleAppsButtonClick;
            appsButton.Icon = Content.Load<Texture2D>("Icons/034-favourites");
            windowManager.AddChild(appsButton);

            Button settingsButton = new Button(Content, spriteBatch, new Vector2(224, 32), new Vector2(64, 64));
            settingsButton.Text = "Settings";
            settingsButton.ControlClicked += HandleSettingsButtonClick;
            settingsButton.Icon = Content.Load<Texture2D>("Icons/051-settings");
            windowManager.AddChild(settingsButton);


            // TODO: use this.Content to load your game content here
        }

        private void HandleSettingsButtonClick(object sender, EventArgs e)
        {
            Window settingsWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));
            settingsWindow.Title = "Settings";

            Label general = new Label(Content, spriteBatch, new Vector2(32, 32),"General");
            settingsWindow.AddChild(general);

            CheckBox saveSettingsBox = new CheckBox(Content, spriteBatch, new Vector2(32, 64), "Save settings on exit");
            settingsWindow.AddChild(saveSettingsBox);

            CheckBox restoreLayoui = new CheckBox(Content, spriteBatch, new Vector2(32, 96), "Restore layout");
            settingsWindow.AddChild(restoreLayoui);

        }

        private void HandleAppsButtonClick(object sender, EventArgs e)
        {
            Window appsWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));
            appsWindow.Title = "Applications";

            int index = 20;
            int column = 0;
            int row = 0;

            foreach(String name in apps)
            {
                Button appsButton = new Button(Content, spriteBatch, new Vector2(32+column*96, 64 + row * 96), new Vector2(64, 64));
                appsButton.Text = apps[column];
                
                appsButton.Icon = Content.Load<Texture2D>("Icons/0"+ index +"-"+apps[index-20].ToLower());
                appsWindow.AddChild(appsButton);
                index++;

                if (column < 5) { 
                    column++;
                }
                else
                {
                    column = 0;
                    row++;
                }
                               
            }
            
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {            
            closeWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));
            closeWindow.Title = "Editor";

            Button b1 = new Button(Content, spriteBatch, new Vector2(30, 30), new Vector2(150, 50));
            b1.Text = "Close";
            closeWindow.AddChild(b1);
            b1.ControlClicked += HandleCloseButtonClick;

            TextEditorControl te = new TextEditorControl(Content, spriteBatch, new Vector2(10, 90), new Vector2(950, 600));
            closeWindow.AddChild(te);
            te.Text = dummyText;

        }

        private void HandleCloseButtonClick(object sender, EventArgs e)
        {
            windowManager.Close(closeWindow);

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

            /*
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Q))
            {
                camera.AdjustZoom(-0.01f);
            }
            else if (state.IsKeyDown(Keys.E))
            {
                camera.AdjustZoom(0.01f);
            }


            else if (state.IsKeyDown(Keys.W))
            {
                camera.MoveCamera(new Vector2(0, -10f));
            }
            else if (state.IsKeyDown(Keys.S))
            {
                camera.MoveCamera(new Vector2(0, 10f));
            }
            else if (state.IsKeyDown(Keys.A))
            {
                camera.MoveCamera(new Vector2(-10f, 0));
            }
            else if (state.IsKeyDown(Keys.D))
            {
                camera.MoveCamera(new Vector2(10f,0));
            }
            else if (state.IsKeyDown(Keys.N))
            {
                camera.MoveCamera(new Vector2(0, 0));
                camera.Position = new Vector2(0, 0);
                camera.Zoom = 1.0f;
            }

            windowManager.Zoom = camera.Zoom;
            */
            base.Update(gameTime);
            windowManager.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            // spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                     null, null, graphics.GraphicsDevice.RasterizerState);
            // spriteBatch.Begin();   

            windowManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
