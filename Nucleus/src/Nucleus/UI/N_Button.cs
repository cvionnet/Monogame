using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nucleus
{
    //*---------------------------------------------------------------------------------
    //* DELEGATES
    //*---------------------------------------------------------------------------------
    public delegate void OnClickDeleguate(Nucleus_Button pSender);      // In Scene_Menu.cs, need 1 method for each button
    public delegate void OnClickDeleguate_Text(string pAction);     // In Scene_Menu.cs, only 1 method (contains a switch to dispatch the action to do). Note : could replace string by an Enum

    public class Nucleus_Button : Nucleus_Sprite
    {
        private ISpriteManager _spriteManager;

        public bool IsHover { get; private set; }
        private MouseState _oldMouseState;
        private MouseState _newMouseState;

        public OnClickDeleguate OnClickHandler { get; set; }        // Using Delegate
        public event OnClickDeleguate OnClick_EventHandler;         // The same, but using Event
        public OnClickDeleguate_Text OnClick_TextHandler { get; set; }

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public Nucleus_Button(ISpriteManager pSpriteManager, Texture2D pTexture, bool pIsAnimated) : base(pTexture, pIsAnimated)
        {
            _spriteManager = pSpriteManager;
        }

        public override void Update(GameTime gameTime)
        {
            _newMouseState = Mouse.GetState();

            // Check if the mouse is inside the button (Hover)
            if (CollideBox.Contains(_newMouseState.Position.X, _newMouseState.Position.Y))
            {
                if (!IsHover)
                    IsHover = true;
            }
            else
            {
                IsHover = false;
            }

            // Check if mouse button is clicked  (invoke a delegate)
            if (IsHover && _newMouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released)
            {
                OnClickHandler?.Invoke(this);
                OnClick_TextHandler?.Invoke("play");
                OnClick_EventHandler?.Invoke(this);
            }

            _oldMouseState = _newMouseState;

            base.Update(gameTime);
        }
    }
}