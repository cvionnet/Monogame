using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Nucleus
{
    public class Nucleus_Scene
    {
        protected MainGame mainGame;
        protected ISpriteManager spriteManager;

        private Fps_counter _fps;

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Nucleus_Scene(MainGame pGame)
        {
            mainGame = pGame;
            spriteManager = new Nucleus_SpriteManager();

            if (Nucleus_Utils.DEBUG_MODE)
                _fps = new Fps_counter();
        }

        public virtual void Load()
        {}

        public virtual void UnLoad()
        {}

        public virtual void Update(GameTime gameTime)
        {
            spriteManager.Update(gameTime);

            if (Nucleus_Utils.DEBUG_MODE)
                _fps.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteManager.Draw(mainGame.spriteBatch, gameTime);

            if (Nucleus_Utils.DEBUG_MODE)
            {
                mainGame.spriteBatch.DrawString(Nucleus_AssetManager.MainFont, $"Sprites total : {spriteManager.LstSprites.Count.ToString()}", new Vector2(Nucleus_Utils.ScreenWidth - 100, 10), Color.Red, 0, Vector2.Zero, 0.5f, 0, 0);
                _fps.DrawFps(mainGame.spriteBatch, new Vector2(Nucleus_Utils.ScreenWidth - 100, 30), 0.5f);
            }
        }
    }
}