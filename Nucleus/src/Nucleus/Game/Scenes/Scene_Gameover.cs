using Microsoft.Xna.Framework;

namespace Nucleus
{
    public class Scene_Gameover : Nucleus_Scene
    {
        public Scene_Gameover(MainGame pGame) : base(pGame)
        { }

        public override void Load()
        {
            base.Load();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.DrawString(Nucleus_AssetManager.MainFont, "This is the Gameover scene", new Vector2(10,10), Color.White);

            base.Draw(gameTime);
        }
    }
}