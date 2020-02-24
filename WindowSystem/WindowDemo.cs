using System;
using System.IO;
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
        Window editorWindow;
        Window musicPlayerWindow;
        CheckBox fullscreenBox;

        String[] apps = { "Recorder", "Browser", "Sound", "Gallery", "Video" , "Music", "Health", "Weather","Download","Mobile"  };

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
            windowManager =  WindowManager.CreateInstance(graphics, Content, spriteBatch);

            Button icon = new Button(new Vector2(32, 64), new Vector2(64, 64));
            icon.Text = "Editor";
            icon.ControlClicked += HandleButtonClick;
            icon.Icon = Content.Load<Texture2D>("Icons/044-memo");
            windowManager.AddChild(icon);

            Button appsButton = new Button(new Vector2(128, 64), new Vector2(64, 64));
            appsButton.Text = "Apps";
            appsButton.ControlClicked += HandleAppsButtonClick;
            appsButton.Icon = Content.Load<Texture2D>("Icons/034-favourites");
            windowManager.AddChild(appsButton);

            Button settingsButton = new Button(new Vector2(224, 64), new Vector2(64, 64));
            settingsButton.Text = "Settings";
            settingsButton.ControlClicked += HandleSettingsButtonClick;
            settingsButton.Icon = Content.Load<Texture2D>("Icons/051-settings");
            windowManager.AddChild(settingsButton);

            MenuBar menubar = new MenuBar(new Vector2(0, 0), new Vector2(graphics.PreferredBackBufferWidth, 35));
            menubar.AddMenus(new String[]{ "File", "Edit", "Help"});
            windowManager.AddChild(menubar);

            Button exitButton = new Button(new Vector2(0, 35), new Vector2(90, 50));
            exitButton.Text = "Exit";
            exitButton.ShowBorder = false;

            exitButton.ControlClicked += delegate (object sender, EventArgs e)
            {
                Environment.Exit(0);
            };

            menubar.GetChildren()[0].AddChild(exitButton);

            CreateEditor();


            AnalogClock clock = new AnalogClock(new Vector2(1700,200),128);
            clock.Size = new Vector2(256, 256);
            windowManager.AddChild(clock);
                


            // TODO: use this.Content to load your game content here
        }

        private void HandleSettingsButtonClick(object sender, EventArgs e)
        {
            Window settingsWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));
            settingsWindow.Title = "Settings";

            Label general = new Label(new Vector2(32, 32),"General");
            settingsWindow.AddChild(general);

            CheckBox saveSettingsBox = new CheckBox(new Vector2(32, 64), "Save settings on exit");
            settingsWindow.AddChild(saveSettingsBox);

            CheckBox restoreLayout = new CheckBox(new Vector2(32, 96), "Restore layout");
            settingsWindow.AddChild(restoreLayout);

            fullscreenBox = new CheckBox(new Vector2(32, 128), "Fullscreen");
            settingsWindow.AddChild(fullscreenBox);
            fullscreenBox.Checked = false;

            fullscreenBox.ControlClicked += HandleFullscreenClicked;

        }

        private void HandleFullscreenClicked(object sender, EventArgs e)
        {
            if (fullscreenBox.Checked)
            {
                if (!graphics.IsFullScreen)
                {
                    graphics.ToggleFullScreen();
                }
            }
            else
            {
                if (graphics.IsFullScreen)
                {
                    graphics.ToggleFullScreen();
                }
            }

        }

        private void HandleAppsButtonClick(object sender, EventArgs e)
        {
            Window appsWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(800, 400));
            appsWindow.Title = "Applications";

            Panel panel = new Panel(new Vector2(0, 0), new Vector2(1024, 768));
            ScrollPanel sp = new ScrollPanel(new Vector2(0, 25), new Vector2(800, 400), panel);

            appsWindow.AddChild(sp);

            int index = 20;
            int column = 0;
            int row = 0;

            foreach(String name in apps)
            {
                Button appsButton = new Button(new Vector2(32+column*96, 64 + row * 96), new Vector2(64, 64));
                appsButton.Text = apps[column];
                
                if (apps[column] == "Music")
                {
                    appsButton.ControlClicked += HandleMusicButtonClicked;
                }

                appsButton.Icon = Content.Load<Texture2D>("Icons/0"+ index +"-"+apps[index-20].ToLower());
                panel.AddChild(appsButton);
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

        private void HandleMusicButtonClicked(object sender, EventArgs e)
        {
            musicPlayerWindow = windowManager.AddWindow(new Vector2(100, 300), new Vector2(1024, 300));
            musicPlayerWindow.Title = "MusicPlayer";

            Button rewindButton = new Button(new Vector2(32, 32), new Vector2(64 , 64));
            rewindButton.Text = "";
            rewindButton.Icon = Content.Load<Texture2D>("Icons/055-rewind");

            Button playButton = new Button( new Vector2(96, 32), new Vector2(64, 64));
            playButton.Text = "";
            playButton.Icon = Content.Load<Texture2D>("Icons/054-play");

            musicPlayerWindow.AddChild(rewindButton);
            musicPlayerWindow.AddChild(playButton);

            rewindButton.ControlClicked += HandleCloseButtonClick;

        }

        private void CreateEditor()
        {
            editorWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));
            editorWindow.Title = "Editor";

            Button b1 = new Button(new Vector2(30, 30), new Vector2(150, 50));
            b1.Text = "Close";
            editorWindow.AddChild(b1);
            b1.ControlClicked += HandleCloseButtonClick;

            string data = null;

            using (var stream = TitleContainer.OpenStream("loremipsum.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }
            }

            TextEditorControl te = new TextEditorControl(new Vector2(10, 90), new Vector2(1280, 800));
            te.Text = data;

            ScrollPanel sp = new ScrollPanel(new Vector2(10, 90), new Vector2(950, 600), te);

            editorWindow.AddChild(sp);
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {
            CreateEditor();
        }

        private void HandleCloseButtonClick(object sender, EventArgs e)
        {
            windowManager.Close(editorWindow);

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
