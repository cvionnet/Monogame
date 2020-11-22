using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nucleus
{
    public class Meteor : Nucleus_Sprite
    {
        private ISpriteManager _spriteManager;
        private float _vMax = 5.0f;

        public Meteor(ISpriteManager pSpriteManager, Texture2D pTexture, bool pIsAnimated) : base(pTexture, pIsAnimated)
        {
            _spriteManager = pSpriteManager;
            Init_Velocity(Nucleus_Utils.RandomFloat(-_vMax, _vMax) / 5, Nucleus_Utils.RandomFloat(_vMax, _vMax) / 5);
        }

        /// <summary>
        /// Make the sprite rebound on the screen borders
        /// </summary>
        public void ScreenBordersRebound()
        {
            if (Math.Abs(VX) > 0 && (CollideBox.X < 0))
            {
                SetPosition(0, CollideBox.Y);
                Invert_Velocity_VX();
            }
            else if (Math.Abs(VX) > 0 && (CollideBox.X + CollideBox.Width > Nucleus_Utils.ScreenWidth))
            {
                SetPosition(Nucleus_Utils.ScreenWidth - CollideBox.Width, Position.Y);
                Invert_Velocity_VX();
            }
            else if (Math.Abs(VY) > 0 && (CollideBox.Y < 0))
            {
                SetPosition(Position.X, 0);
                Invert_Velocity_VY();
            }
            else if (Math.Abs(VY) > 0 && (CollideBox.Y + CollideBox.Height > Nucleus_Utils.ScreenHeight))
            {
                SetPosition(Position.X, Nucleus_Utils.ScreenHeight - CollideBox.Height);
                Invert_Velocity_VY();
            }
        }
    }
}