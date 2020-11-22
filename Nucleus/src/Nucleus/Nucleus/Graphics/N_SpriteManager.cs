using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Nucleus
{
    public class Nucleus_SpriteManager : ISpriteManager
    {
        public List<Nucleus_Sprite> LstSprites { get; private set; }

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Nucleus_SpriteManager()
        {}

        /// <summary>
        /// Add a new sprite to the Sprite Manager
        /// </summary>
        /// <param name="pSprite">The sprite to add</param>
        /// <param name="pIsAnimated">True if the sprite have an animation</param>
        public void Add(Nucleus_Sprite pSprite)
        {
            if (LstSprites == null) LstSprites = new List<Nucleus_Sprite>();

            Nucleus_Sprite _sprite = pSprite;
            LstSprites.Add(_sprite);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Nucleus_Sprite sprite in LstSprites)
                sprite.Update(gameTime);

            // Clean sprite list from objects to delete
            CleanListSprite();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Nucleus_Sprite sprite in LstSprites)
                sprite.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Delete from the list all objects marked as to be deleted
        /// </summary>
        private void CleanListSprite()
        {
            LstSprites.RemoveAll(item => item.IsToDelete == true);
        }
    }
}
