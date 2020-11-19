using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Nucleus
{
    public class Nucleus_MusicManager
    {
        private static MainGame _mainGame;

        private static List<Song> _lstMusics;
        private static Song _currentSong;
        private static int _currentMusicID;
        private static int _nextMusicID;
        private static bool _isMusicFading;

        private static List<SoundEffect> _lstSoundEffects;
        private static SoundEffect _soundEffectPlayer;

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public static void Initialize(MainGame pGame)
        {
            _mainGame = pGame;

            _lstMusics = new List<Song>();
            _lstSoundEffects = new List<SoundEffect>();

            _currentMusicID = -1;
            _isMusicFading = false;

            SetMusicRepeating(true);
        }

        public static void Update(GameTime gameTime)
        {
            if (_isMusicFading)
            {
                // Change the current music (fadeout)
                if (_currentMusicID != _nextMusicID)
                {
                    MediaPlayer.Volume -= 0.01f;
                    if (MediaPlayer.Volume <= 0)
                    {
                        MediaPlayer.Volume = 0;

                        _currentSong = _lstMusics[_nextMusicID];
                        MediaPlayer.Play(_currentSong);

                        _currentMusicID = _nextMusicID;
                    }
                }
                // Increase volume (fadein)
                else if (_currentMusicID == _nextMusicID && MediaPlayer.Volume < 1)
                {
                    MediaPlayer.Volume += 0.01f;
                }
            }
        }

        //*---------------------------------------------------------------------------------
        //* SOUNDS
        //*---------------------------------------------------------------------------------

        /// <summary>
        /// Add a sound effect in the bank
        /// </summary>
        /// <param name="pFileName">The file name of the sound</param>
        public static void AddSoundEffect(string pFileName)
        {
            _lstSoundEffects.Add(_mainGame.Content.Load<SoundEffect>(pFileName));
        }

        /// <summary>
        /// Play a sound effect
        /// </summary>
        /// <param name="pSoundName">Name of the sound to play</param>
        public static void PlaySound(string pSoundName)
        {
            _soundEffectPlayer = _lstSoundEffects.Find(effect => effect.Name == pSoundName);
            _soundEffectPlayer?.Play();
        }

        //*---------------------------------------------------------------------------------
        //* MUSIC
        //*---------------------------------------------------------------------------------

        /// <summary>
        /// set if the music will loop
        /// </summary>
        /// <param name="pValue">True = music is looping    False = music is played once</param>
        public static void SetMusicRepeating(bool pValue)
        {
            MediaPlayer.IsRepeating = pValue;
            MediaPlayer.Volume = MediaPlayer.IsRepeating ? 1 : 0;
        }

        /// <summary>
        /// Add a music in the bank
        /// </summary>
        /// <param name="pFileName">The file name of the song</param>
        public static void AddMusic(string pFileName)
        {
            _lstMusics.Add(_mainGame.Content.Load<Song>(pFileName));
        }

        /// <summary>
        /// Load the next music to play (fadeout / fadein)
        /// </summary>
        /// <param name="pMusicID">Id of the next music</param>
        public static void PlayMusicWithFade(int pMusicID)
        {
            // "Load" the next music to play
            _isMusicFading = true;
            if (_nextMusicID != pMusicID)
                _nextMusicID = pMusicID;
        }

        /// <summary>
        /// Play a music
        /// </summary>
        /// <param name="pMusicName">Name of the music to play</param>
        public static void PlayMusic(string pMusicName)
        {
            try
            {
                _isMusicFading = false;

                _currentSong = _lstMusics.Find(music => music.Name == pMusicName);
                MediaPlayer.Play(_currentSong);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("PlayMusic - Can't find music");
                Console.WriteLine(ex.Message);
                //throw;
            }
        }

        /// <summary>
        /// Stop to play a music
        /// </summary>
        public static void StopMusic()
        {
            _isMusicFading = false;
            MediaPlayer.Stop();
        }
    }
}