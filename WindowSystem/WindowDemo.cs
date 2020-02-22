using System;
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

            IsMouseVisible = true;
            camera = new Camera();
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
            windowManager = new WindowManager(Content, spriteBatch);

            Window w1 = windowManager.AddWindow(new Vector2(100, 100), new Vector2(640, 480));

            Button b1 = new Button(Content, spriteBatch, new Vector2(30, 30), new Vector2(150, 50));
            b1.Text = "Add window";
            w1.AddChild(b1);

            b1.ButtonClicked += HandleButtonClick;

            windowManager.AddWindow(new Vector2(200, 200), new Vector2(640, 480));

            Button icon = new Button(Content, spriteBatch, new Vector2(30, 30), new Vector2(64, 64));
            icon.Text = "Icon";
            icon.ButtonClicked += HandleButtonClick;

            windowManager.AddChild(icon);

            // TODO: use this.Content to load your game content here
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {            

            closeWindow = windowManager.AddWindow(new Vector2(300, 300), new Vector2(1024, 768));

            Button b1 = new Button(Content, spriteBatch, new Vector2(30, 30), new Vector2(150, 50));
            b1.Text = "Close";
            closeWindow.AddChild(b1);
            b1.ButtonClicked += HandleCloseButtonClick;

            TextEditorControl te = new TextEditorControl(Content, spriteBatch, new Vector2(30, 90), new Vector2(950, 600));
            closeWindow.AddChild(te);

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
            GraphicsDevice.Clear(Color.LightGray);
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
