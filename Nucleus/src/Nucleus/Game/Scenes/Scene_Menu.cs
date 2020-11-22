using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nucleus
{
    public class Scene_Menu : Nucleus_Scene
    {
        private Nucleus_Button _button;
        private Nucleus_Sprite _runner;

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Scene_Menu(MainGame pGame) : base(pGame)
        {}

        public override void Load()
        {
            //**********************
            //* Sample - UI Button *
            //**********************
            _button = new Nucleus_Button(spriteManager, mainGame.Content.Load<Texture2D>("_Images\\button"), false);
            _button.SetPosition(100, 100);
            // option 1 : connect to Delegates
            _button.OnClickHandler = onClickPlay;
            _button.OnClick_TextHandler = onClickPlay_Text;
            // option 2 : connect to Events
            //_button.OnClick_EventHandler += onClickPlay;
            spriteManager.Add(_button);

            //****************************
            //* Sample - Animated sprite *
            //****************************
            _runner = new Player(spriteManager, mainGame.Content.Load<Texture2D>("_Images\\herosheet"), true);
            _runner.AddAnimation("run", new int[] {0,1,2,3,4,5,6,7}, 20, 20, 1f /12f, true);
            _runner.LoadAnimation("run");
            _runner.Scale = 4;
            _runner.SetPosition(Nucleus_Utils.ScreenWidth/2 - _runner.Texture.Width/2, Nucleus_Utils.ScreenHeight - (_runner.Texture.Height * _runner.Scale));
            spriteManager.Add(_runner);

            //Nucleus_MusicManager.PlayMusicWithFade(0);
            Nucleus_MusicManager.PlayMusic("cool");

            base.Load();
        }

        public override void UnLoad()
        {
            Nucleus_MusicManager.StopMusic();

            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Load next scene
            if (Nucleus_Inputs.KeyboardKeyHit(Keys.Space, true) || Nucleus_Inputs.GamepadButtonHit(PlayerIndex.One, Buttons.A, true))
                mainGame.GameState.SelectScene(Nucleus_GameState.SceneType.Gameplay);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.DrawString(Nucleus_AssetManager.MainFont, "This is the Menu scene", new Vector2(10,10), Color.White);

            base.Draw(gameTime);
        }

        //*---------------------------------------------------------------------------------
        //* DELEGATES / EVENTS 
        //*---------------------------------------------------------------------------------

        public void onClickPlay(Nucleus_Button pSender)
        {
            mainGame.GameState.SelectScene(Nucleus_GameState.SceneType.Gameplay);
        }

        public void onClickPlay_Text(string pAction)
        {
            switch (pAction)
            {
                case "play" :
                    mainGame.GameState.SelectScene(Nucleus_GameState.SceneType.Gameplay);
                    break;
                default:
                    break;
            }
        }
    }
}