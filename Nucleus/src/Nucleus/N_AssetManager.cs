using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nucleus
{
    public class Nucleus_AssetManager
    {
        public static SpriteFont MainFont { get; private set; }

        /// <summary>
        /// Assets loading
        ///     Monogame : call this method in the LoadContent()
        /// </summary>
        public static void Load(ContentManager pContent)
        {
            MainFont = pContent.Load<SpriteFont>("MainFont");

            // Load sounds and music
            Nucleus_MusicManager.AddMusic("_Musics\\cool");
            Nucleus_MusicManager.AddMusic("_Musics\\techno");
            Nucleus_MusicManager.AddSoundEffect("_Sounds\\explode");
        }
    }
}