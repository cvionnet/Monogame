using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nucleus
{
    // Source (willmotil) : https://community.monogame.net/t/a-simple-monogame-fps-display-class/10545
    public class Fps_counter
    {
        private double frames = 0;
        private double updates = 0;
        private double elapsed = 0;
        private double last = 0;
        private double now = 0;
        public double msgFrequency = 1.0f;
        public string msg = "";

        /// <summary>
        /// The msgFrequency here is the reporting time to update the message.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            now = gameTime.TotalGameTime.TotalSeconds;
            elapsed = (double)(now - last);
            if (elapsed > msgFrequency)
            {
                msg = "Fps: " + (Math.Round(frames / elapsed, 4)).ToString() + "\nElapsed time: " + Math.Round(elapsed, 4).ToString() + "\nUpdates: " + updates.ToString() + "\nFrames: " + frames.ToString();
                //Console.WriteLine(msg);
                elapsed = 0;
                frames = 0;
                updates = 0;
                last = now;
            }
            updates++;
        }

        public void DrawFps(SpriteBatch pSpriteBatch, Vector2 pFpsDisplayPosition, float pScale)
        {
            pSpriteBatch.DrawString(Nucleus_AssetManager.MainFont, msg, pFpsDisplayPosition, Color.Red, 0, Vector2.Zero, pScale, 0, 0);
            frames++;
        }
    }
}
