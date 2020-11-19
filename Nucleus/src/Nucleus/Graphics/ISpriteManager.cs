using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nucleus
{
    public interface ISpriteManager
    {
        List<Nucleus_Sprite> LstSprites { get; }

        void Add(Nucleus_Sprite pSprite);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}