using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Nucleus
{
    public class Nucleus_Inputs
    {
        private static KeyboardState _oldKeyboardState;
        private static KeyboardState _newKeyboardState;
        private static GamePadState _oldGamepadState;
        private static GamePadState _newGamepadState;
        //private static bool _isMouseHover;
        private static MouseState _oldMouseState;
        private static MouseState _newMouseState;

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        /// <summary>
        /// Input initialization
        ///     Monogame : call this method in the Initialize()
        /// </summary>
        public static void Initialize()
        {
            _oldKeyboardState = Keyboard.GetState();
            _oldGamepadState = GamePad.GetState(PlayerIndex.One);
            //_oldMouseState = Mouse.GetState();
        }

        //*---------------------------------------------------------------------------------
        //* KEYBOARD
        //*---------------------------------------------------------------------------------

#region KEYBOARD

        /// <summary>
        /// Check if a key has been hit on the keyboard
        /// </summary>
        /// <param name="pKey">Key name</param>
        /// <param name="pPreventMultipleStrikes">(default = true) Prevent the key to have multiple calls before its release</param>
        /// <returns>True if the key is valid</returns>
        public static bool KeyboardKeyHit(Keys pKey, bool pPreventMultipleStrikes = false)
        {
            bool result;

            _newKeyboardState = Keyboard.GetState();

            // Return true while the player hit the key
            if (!pPreventMultipleStrikes && _newKeyboardState.IsKeyDown(pKey))
            {
                result = true;
            }
            // Return true once
            else
            {
                // To check for only 1 keystroke (if the player let his finger on the key down)
                result = _newKeyboardState.IsKeyDown(pKey) && !_oldKeyboardState.IsKeyDown(pKey);
                _oldKeyboardState = _newKeyboardState;
            }

            return result;
        }

#endregion

        //*---------------------------------------------------------------------------------
        //* GAMEPAD
        //*---------------------------------------------------------------------------------

#region GAMEPAD

        /// <summary>
        /// Check if a button has been hit on the gamepad
        /// </summary>
        /// <param name="pGamepadID">Id of the gamepad to check</param>
        /// <param name="pButton">Button name</param>
        /// <param name="pPreventMultipleStrikes">(default = false) Prevent the button to have multiple calls before its release</param>
        /// <returns>True if the button is valid</returns>
        public static bool GamepadButtonHit(PlayerIndex pGamepadID, Buttons pButton, bool pPreventMultipleStrikes = false)
        {
            bool result = false;

            if (IsGamePadConnected(pGamepadID))
            {
                _newGamepadState = GamePad.GetState(pGamepadID, GamePadDeadZone.IndependentAxes);

                // Return true while the player hit the button
                if (!pPreventMultipleStrikes && _newGamepadState.IsButtonDown(pButton))
                {
                    result = true;
                }
                // Return true once
                else
                {
                    // To check for only 1 hit (if the player let his finger on the button down)
                    result = _newGamepadState.IsButtonDown(pButton) && !_oldGamepadState.IsButtonDown(pButton);
                    _oldGamepadState = _newGamepadState;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if a specific gamepad is connected
        /// </summary>
        /// <param name="pGamepadID">The gamepad ID</param>
        /// <returns>True if the gamepad is connected</returns>
        private static bool IsGamePadConnected(PlayerIndex pGamepadID)
        {
            // Get information about the 1st pad
            GamePadCapabilities capabilities = GamePad.GetCapabilities(pGamepadID);
            return capabilities.IsConnected;
        }

#endregion

        //*---------------------------------------------------------------------------------
        //* MOUSE
        //*---------------------------------------------------------------------------------

#region MOUSE
/*
        /// <summary>
        /// Check if a button has been hit on the mouse
        /// </summary>
        /// <param name="pButton">Button name</param>
        /// <param name="pPreventMultipleStrikes">(default = true) Prevent the button to have multiple calls before its release</param>
        /// <returns>True if the button is valid</returns>
        public static bool MouseButtonHit(ButtonState pButton, bool pPreventMultipleStrikes = true)
        {
            bool result = false;

            _newMouseState = Mouse.GetState();

            // Return true while the player hit the button
            if (!pPreventMultipleStrikes && (_newMouseState.LeftButton == pButton || _newMouseState.MiddleButton == pButton || _newMouseState.RightButton == pButton))
            {
                result = true;
            }
            // Return true once
            else
            {
                // To check for only 1 hit (if the player let his finger on the button down)
                result = _newGamepadState.IsButtonDown(pButton) && !_oldGamepadState.IsButtonDown(pButton);
                _oldGamepadState = _newGamepadState;
            }

            return result;
        }
*/
#endregion
    }
}