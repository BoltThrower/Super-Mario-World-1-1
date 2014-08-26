using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperMario.Commands.GameStateCommands;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SuperMario : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IController playerOneController;
        private IController playerTwoController;
        private Background background;
        private Camera camera;

        public SuperMario()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = GameValues.ScreenHeight;
            graphics.PreferredBackBufferWidth = GameValues.ScreenWidth;

            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            AnimatedSpriteFactory.Instance.content = Content;
            SoundManager.Instance.content = Content;
            HUD.Instance.Content = Content;
            GameStateMachine.Instance.Graphics = graphics;

            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                //playerTwoController = new GamePadController(this, );
            }
            //else
            //{
            playerOneController = new KeyboardController(this, Mario.Instance);
            //}
            camera = new Camera();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SoundManager.Instance.LoadSounds();
            HUD.Instance.LoadFonts();
            GameStateMachine.Instance.GameFont = HUD.Instance.HudFont;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            HUD.Instance.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / GameValues.InitialHUDPositionOffset.X, graphics.GraphicsDevice.Viewport.Height / GameValues.InitialHUDPositionOffset.Y);
            background = new Background(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            // TODO: use this.Content to load your game content here
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
            GameStateMachine.Instance.Update(gameTime);
            camera.Update(gameTime);
            playerOneController.Update();

            if (!Level.Instance.IsOnePlayer)
            {
                //playerTwoController.Update();
            }

            background.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            if (GameStateMachine.Instance.DrawBackground)
            {
                background.Draw(spriteBatch, gameTime);
            }

            GameStateMachine.Instance.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
            
        }
    }
}
