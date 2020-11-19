namespace Nucleus
{
    public class Nucleus_GameState
    {
        public enum SceneType
        {
            Menu = 0, Gameplay = 1, Gameover = 2
        }
        public Nucleus_Scene CurrentScene { get; private set; }
        protected MainGame mainGame;

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Nucleus_GameState(MainGame pGame)
        {
            mainGame = pGame;
        }

        /// <summary>
        /// Load a new scene
        /// </summary>
        /// <param name="pType">The scene to load</param>
        public void SelectScene(SceneType pType)
        {
            // If a scene exists, unload it
            if (CurrentScene != null)
            {
                CurrentScene.UnLoad();
                CurrentScene = null;
            }

            switch (pType)
            {
                case SceneType.Menu:
                    CurrentScene = new Scene_Menu(mainGame);
                    break;
                case SceneType.Gameplay:
                    CurrentScene = new Scene_Gameplay(mainGame);
                    break;
                case SceneType.Gameover:
                    CurrentScene = new Scene_Gameover(mainGame);
                    break;
                default:
                    break;
            }

            CurrentScene.Load();
        }
    }
}