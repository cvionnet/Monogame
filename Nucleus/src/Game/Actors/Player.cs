using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nucleus
{
    public class Player : Nucleus_Sprite
    {
        private ISpriteManager _spriteManager;
        public int Energy;
        private float _vMax = 5.0f;

        public Player(ISpriteManager pSpriteManager, Texture2D pTexture, bool pIsAnimated) : base(pTexture, pIsAnimated)
        {
            _spriteManager = pSpriteManager;
            Energy = 1000;
        }

        /// <summary>
        /// Action when the sprite is touched by another sprite  (override from Nucleus_Sprite)
        /// </summary>
        /// <param name="pBy">The sprite that touched</param>
        public override void TouchedBy(Nucleus_Sprite pBy)
        {
            if (pBy is Meteor)
                Energy -= 1;

            base.TouchedBy(pBy);
        }

        /// <summary>
        /// To control the player
        /// </summary>
        public void InputControl()
        {
            if (Nucleus_Inputs.KeyboardKeyHit(Keys.Right) || Nucleus_Inputs.GamepadButtonHit(PlayerIndex.One, Buttons.LeftThumbstickRight))
                Increment_Velocity(1f, 0, _vMax);
            if (Nucleus_Inputs.KeyboardKeyHit(Keys.Left) || Nucleus_Inputs.GamepadButtonHit(PlayerIndex.One, Buttons.LeftThumbstickLeft))
                Increment_Velocity(-1f, 0, _vMax);
            if (Nucleus_Inputs.KeyboardKeyHit(Keys.Up) || Nucleus_Inputs.GamepadButtonHit(PlayerIndex.One, Buttons.LeftThumbstickUp))
                Increment_Velocity(0, -1f, _vMax);
            if (Nucleus_Inputs.KeyboardKeyHit(Keys.Down) || Nucleus_Inputs.GamepadButtonHit(PlayerIndex.One, Buttons.LeftThumbstickDown))
                Increment_Velocity(0, 1f, _vMax);
        }
    }
}