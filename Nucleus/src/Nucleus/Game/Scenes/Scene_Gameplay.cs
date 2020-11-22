using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace Nucleus
{
    public class Scene_Gameplay : Nucleus_Scene
    {
        private Player _player;

        public Scene_Gameplay(MainGame pGame) : base(pGame)
        {}

        public override void Load()
        {
            //******************
            //* Sample - Tiles *
            //******************
            Nucleus_Tiles_TMX.Initialize(mainGame.Content.Load<TiledMap>("_Tiled\\map"), mainGame.GraphicsDevice);

            // Initialize the player
            _player = new Player(spriteManager, mainGame.Content.Load<Texture2D>("_Images\\ship"), false);
            _player.SetPosition(Nucleus_Utils.ScreenWidth/2 - _player.Texture.Width/2, Nucleus_Utils.ScreenHeight/2 - _player.Texture.Height/2);
            spriteManager.Add(_player);

            // Initialize meteors
            for (int i = 0; i < 20; i++)
            {
                Meteor m = new Meteor(spriteManager, mainGame.Content.Load<Texture2D>("_Images\\meteor"), false);
                m.SetPosition (Nucleus_Utils.RandomInt(1, Nucleus_Utils.ScreenWidth - m.Texture.Width), Nucleus_Utils.RandomInt(1, Nucleus_Utils.ScreenHeight - m.Texture.Height));
                spriteManager.Add(m);
            }

            //Nucleus_MusicManager.PlayMusicWithFade(1);
            Nucleus_MusicManager.PlayMusic("techno");

            base.Load();
        }

        public override void UnLoad()
        {
            Nucleus_MusicManager.StopMusic();

            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Actions with meteors
            foreach (Nucleus_Sprite sprite in spriteManager.LstSprites)
            {
                if (sprite is Meteor meteor)
                {
                    // Rebound on the borders
                    meteor.ScreenBordersRebound();

                    // Detect collisions. Delete the meteor that touched
                    if (Nucleus_Utils.CollideByBox(meteor, _player))
                    {
                        _player.TouchedBy(meteor);
                        meteor.IsToDelete = true;
                        Nucleus_MusicManager.PlaySound("_Sounds\\explode");

                        // Display the game over screen if player is dead
                        if (_player.Energy <= 0)
                            mainGame.GameState.SelectScene(Nucleus_GameState.SceneType.Gameover);
                    }
                }
            }

            // Move the player
            _player.InputControl();

            // Tiles
            Nucleus_Tiles_TMX.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.DrawString(Nucleus_AssetManager.MainFont, "This is the Gameplay scene", new Vector2(10,10), Color.White);
            mainGame.spriteBatch.DrawString(Nucleus_AssetManager.MainFont, $"Energy = {_player.Energy}", new Vector2(10,30), Color.White);

            // Tiles
            Nucleus_Tiles_TMX.Draw();

            base.Draw(gameTime);
        }
    }
}