using CrowEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Diagnostics;


namespace ninja_metrovania
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<ScreenEnum, Screen> screens;
        private Screen currentScreen;
        private ScreenEnum nextScreen;
        private bool newScreenFocused;
        const int VIRTUAL_WIDTH = 480;
        const int VIRTUAL_HEIGHT = 270; // Aspect ratio of 16:9

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ResourceManager.manager = Content;
            screens = new Dictionary<ScreenEnum, Screen>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Window.AllowUserResizing = true;

            screens.Add(ScreenEnum.Test, new TestScreen(ScreenEnum.Test));
            currentScreen = screens[ScreenEnum.Test];

            ResourceManager.RegisterFont("Fonts/DefaultFont", "default");
            ResourceManager.RegisterFont("Fonts/DefaultSmallFont", "defaultSmall");
            ResourceManager.RegisterFont("Fonts/DefaultLargeFont", "defaultLarge");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].Initialize(GraphicsDevice, _graphics, Window);
            }

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].LoadContent();
                screens[screen].SetupGameObjects();
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Diplays FPS
            /*            Debug.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
            */

            if (newScreenFocused)
            {
                currentScreen.OnScreenFocus();
                newScreenFocused = false;
            }

            nextScreen = currentScreen.Update(gameTime);

            if (screens[nextScreen] != currentScreen)
            {
                currentScreen.OnScreenDefocus();
                newScreenFocused = true;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentScreen.Draw(gameTime);

            currentScreen = screens[nextScreen];

            base.Draw(gameTime);
        }
    }
}