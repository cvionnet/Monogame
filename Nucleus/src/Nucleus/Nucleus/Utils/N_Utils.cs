using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nucleus
{
    public class Nucleus_Utils
    {
        public const bool DEBUG_MODE = true;
        public static Texture2D Debug_Texture { get; private set; }

        public static float GAME_SPEED = 60.0f;

        public const float ASPECT_RATIO = 1.0f;

        private static MainGame _mainGame;

        public static Random RandGen { get; private set; }

        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }
        public static Rectangle VirtualScreen { get; private set; }      // Size of the game window

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public static void Initialize(MainGame pGame)
        {
            _mainGame = pGame;

            RandomForceSeed(DateTime.Now.Millisecond);

            ScreenWidth = _mainGame.GraphicsDevice.Viewport.Width;
            ScreenHeight = _mainGame.GraphicsDevice.Viewport.Height;

            VirtualScreen = _mainGame.Window.ClientBounds;

            // Create a texture to display in debug mode (see https://community.monogame.net/t/whats-the-simplest-way-to-draw-a-rectangular-outline-without-generating-the-texture/7818/3)
            Debug_Texture = new Texture2D(_mainGame.GraphicsDevice, 1, 1);
            Debug_Texture.SetData(new Color[] { Color.Red });
        }

        //*---------------------------------------------------------------------------------
        //* COLLISIONS
        //*---------------------------------------------------------------------------------

        /// <summary>
        /// Test a collision between 2 rectangles  ( = collide box)
        /// </summary>
        /// <param name="p1">First rectangle</param>
        /// <param name="p2">Second rectangle</param>
        /// <returns>True if objects collide</returns>
        public static bool CollideByBox(Nucleus_Sprite p1, Nucleus_Sprite p2)
        {
            return p1.CollideBox.Intersects(p2.CollideBox);
        }

        //*---------------------------------------------------------------------------------
        //* RANDOM
        //*---------------------------------------------------------------------------------

        /// <summary>
        /// Set a specific seed to generate random numbers
        /// </summary>
        /// <param name="pSeed">The seed to use</param>
        public static void RandomForceSeed(int pSeed)
        {
            RandGen = new Random(pSeed);
        }

        /// <summary>
        /// Generate a random float value between 2 values
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>A random value</returns>
        public static float RandomFloat(float min, float max) {
            float result;

            // To avoid to get a 0 as result (ie : random between -1 and 1 can generate 0)
            do
            {
                result = ((float) RandGen.NextDouble() * (max - min)) + min;
            } while (result == 0);

            return result;
        }

        /// <summary>
        /// Generate a random int value between 2 values
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>A random value</returns>
        public static float RandomInt(int min, int max) {
            int result;

            // To avoid to get a 0 as result (ie : random between -1 and 1 can generate 0)
            do
            {
                result = RandGen.Next(min, max + 1);
            } while (result == 0);

            return result;
        }
    }
}