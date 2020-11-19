using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nucleus
{
    public class Nucleus_Sprite
    {
        public Texture2D Texture { get; }
        public Rectangle CollideBox { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Origin { get; private set; }
        public float Angle { get; set; }
        public float Scale { get; set; } = 1.0f;
        public SpriteEffects Effect { get; set; }

        public float VX { get; private set; } = 0;
        public float VY { get; private set; } = 0;

        //** Animation **
        private List<Nucleus_Animation> _lstAnimations;
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        private Nucleus_Animation _currentAnimation;
        private int _currentFrame;
        private float _timer;

        public bool IsCentered
        {
            get => _IsCentered;
            set
            {
                SetOrigin();
                _IsCentered = value;
            }
        }
        private bool _IsCentered;
        public bool IsVisible { get; set; }
        public bool IsToDelete { get; set; }

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Nucleus_Sprite(Texture2D pTexture, bool pIsAnimated)
        {
            Texture = pTexture;
            Effect = SpriteEffects.None;

            _lstAnimations = new List<Nucleus_Animation>();
            // If the sprite is not animated, automatically create a 1 frame animation
            if (!pIsAnimated)
            {
                AddAnimation("one_frame", new int[] { 0 }, 0, 0, 0);
                LoadAnimation("one_frame");
            }

            IsToDelete = false;
            IsVisible = true;
            IsCentered = false;

            _currentFrame = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            Move(gameTime);
            Update_Animation(gameTime);
            UpdateCollideBox();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Nucleus_Utils.DEBUG_MODE)
                spriteBatch.Draw(Nucleus_Utils.Debug_Texture, new Rectangle(CollideBox.X, CollideBox.Y, CollideBox.Width, CollideBox.Height), Color.Red);

            Draw_Animation(spriteBatch, gameTime);
        }

        /// <summary>
        /// Set the origin on the center of the texture or at the up-left corner (default)
        /// </summary>
        private void SetOrigin()
        {
            if (IsCentered)
                Origin = new Vector2(FrameWidth / 2, FrameHeight / 2);
            else
                Origin = Vector2.Zero;
        }

        //*---------------------------------------------------------------------------------
        //* COLLISION
        //*---------------------------------------------------------------------------------

        #region COLLISION

        /// <summary>
        /// Action when the sprite is touched by another sprite
        /// </summary>
        /// <param name="pBy">The sprite that touched</param>
        public virtual void TouchedBy(Nucleus_Sprite pBy)
        { }

        private void UpdateCollideBox()
        {
            // Update the collide box
            if (_IsCentered)
                CollideBox = new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y),
                                           (int)(FrameWidth * Scale), (int)(FrameHeight * Scale));
            else
                CollideBox = new Rectangle((int)Position.X, (int)Position.Y,
                                           (int)(FrameWidth * Scale), (int)(FrameHeight * Scale));
        }

        #endregion

        //*---------------------------------------------------------------------------------
        //* MOVEMENT
        //*---------------------------------------------------------------------------------

        #region MOVEMENT

        /// <summary>
        /// Set the position of a sprite
        /// </summary>
        /// <param name="pX">X</param>
        /// <param name="pY">Y</param>
        public void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }

        /// <summary>
        /// Move the sprite using the velocity
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            Position = new Vector2(Position.X + VX * Nucleus_Utils.GAME_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds,
                                   Position.Y + VY * Nucleus_Utils.GAME_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        #endregion

        //*---------------------------------------------------------------------------------
        //* VELOCITY
        //*---------------------------------------------------------------------------------

        #region VELOCITY

        /// <summary>
        /// Initialize velocity
        /// </summary>
        /// <param name="pVX">X velocity</param>
        /// <param name="pVY">Y velocity</param>
        public void Init_Velocity(float pVX, float pVY)
        {
            VX = pVX;
            VY = pVY;
        }

        /// <summary>
        /// To change X and Y velocity
        /// </summary>
        /// <param name="pVX">New X velocity</param>
        /// <param name="pVY">New Y velocity</param>
        public void Increment_Velocity(float pVX, float pVY)
        {
            VX += pVX;
            VY += pVY;
        }

        /// <summary>
        /// To change X and Y velocity and add a maximum speed
        /// </summary>
        /// <param name="pVX">New X velocity</param>
        /// <param name="pVY">New Y velocity</param>
        /// <param name="pVMax">Maximum speed</param>
        public void Increment_Velocity(float pVX, float pVY, float pVMax)
        {
            VX += pVX;
            VY += pVY;

            // Force a velocity to a maximum speed
            if (Math.Abs(VX) > pVMax) VX = pVMax * Math.Sign(pVX);
            if (Math.Abs(VY) > pVMax) VY = pVMax * Math.Sign(pVY);
        }

        /// <summary>
        /// Invert VX velocity
        /// </summary>
        public void Invert_Velocity_VX()
        {
            VX *= -1;
        }

        /// <summary>
        /// Invert VY velocity
        /// </summary>
        public void Invert_Velocity_VY()
        {
            VY *= -1;
        }

        #endregion

        //*---------------------------------------------------------------------------------
        //* ANIMATION
        //*---------------------------------------------------------------------------------

        #region ANIMATION

        /// <summary>
        /// Add an animation into a list of animations
        /// </summary>
        /// <param name="pName">Name of the animation</param>
        /// <param name="pFrames">An array with the ID of each frame in the animation</param>
        /// <param name="pFrameDuration">How much time to display each frame</param>
        /// <param name="pIsLooping">To make the animation looping</param>
        public void AddAnimation(string pName, int[] pFrames, int pFrameWidth, int pFrameHeight, float pFrameDuration = 1f/12f, bool pIsLooping = true)
        {
            FrameWidth = pFrameWidth == 0 ? Texture.Width : pFrameWidth;        // if no size as parameter, take the size of the texture
            FrameHeight = pFrameHeight == 0 ? Texture.Height : pFrameHeight;
            SetOrigin();

            Nucleus_Animation animation = new Nucleus_Animation(pName, pFrames, pFrameDuration, pIsLooping);
            _lstAnimations.Add(animation);
        }

        /// <summary>
        /// Load the selected animation
        /// </summary>
        /// <param name="pName">Name of the animation</param>
        public void LoadAnimation(string pName)
        {
            foreach (Nucleus_Animation animation in _lstAnimations)
            {
                if (animation.Name == pName)
                {
                    _currentAnimation = animation;
                    _currentFrame = 0;
                    _currentAnimation.IsFinished = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Update each frame of the animation
        /// </summary>
        /// <param name="gameTime"></param>
        private void Update_Animation(GameTime gameTime)
        {
            // Exit conditions
            if (_currentAnimation == null) return;
            if (_currentAnimation.FrameDuration == 0) return;
            if (_currentAnimation.IsFinished) return;

            // Frame by frame animation
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > _currentAnimation.FrameDuration)
            {
                _currentFrame++;
                if (_currentFrame >= _currentAnimation.Frames.Length)
                {
                    if (_currentAnimation.IsLooping)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame--;
                        _currentAnimation.IsFinished = true;
                    }
                }
                _timer = 0;
            }
        }

        private void Draw_Animation(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsVisible) return;

            // Generate a virtual rectangle around the frame to draw 
            Rectangle source = new Rectangle(_currentAnimation.Frames[_currentFrame] * FrameWidth, 0, FrameWidth, FrameHeight);

            // Draw the virtual rectangle in the texture
            spriteBatch.Draw(Texture, Position, source, Color.White, Angle, Origin, Scale, Effect, 0);
        }

        #endregion

    }
}