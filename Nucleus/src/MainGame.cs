using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nucleus
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;

        public Nucleus_GameState GameState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Load scene manager
            GameState = new Nucleus_GameState(this);

            Nucleus_Inputs.Initialize();
            Nucleus_Utils.Initialize(this);
            Nucleus_MusicManager.Initialize(this);

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load assets manager
            Nucleus_AssetManager.Load(Content);

            // Load first scene
            GameState.SelectScene(Nucleus_GameState.SceneType.Menu);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameState.CurrentScene?.Update(gameTime);
            Nucleus_MusicManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Nucleus_Utils.ASPECT_RATIO));
            GameState.CurrentScene?.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
