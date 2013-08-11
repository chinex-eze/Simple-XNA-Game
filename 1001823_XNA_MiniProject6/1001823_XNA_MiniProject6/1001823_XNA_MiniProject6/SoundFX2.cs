using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace _1001823_XNA_MiniProject6
{
    class SoundFX2 : BasicSoundFX
    {
        protected SoundEffectInstance soundEffectInstance;

        private float volume = 1.0f;
        private float pitch = 0.0f;
        private float pan = 0.0f;

        public SoundFX2() : base()
        { 
        }

        public SoundFX2(ContentManager theContentManager, string soundFile)
            : base(theContentManager, soundFile)
        {
            soundEffectInstance = effect.CreateInstance(); 
        }

        public override void LoadSound(ContentManager theContentManager, string soundFile)
        {
            base.LoadSound(theContentManager, soundFile); 
            soundEffectInstance = effect.CreateInstance(); 
        }

        /***************************CONTROLS***********/
        public override void Play()
        {
            soundEffectInstance.Play();
        }

        public void Pause()
        {
            soundEffectInstance.Pause(); 
        }

        public void Stop()
        {
            soundEffectInstance.Stop(); 
        }

        public bool IsPlaying()
        {
            return (soundEffectInstance.State == SoundState.Playing); 
        }

        public bool Loop
        {
            get { return soundEffectInstance.IsLooped; }
            set { soundEffectInstance.IsLooped = true; } 
        }

        /**volume is increased in rates of 0.2 */
        public static readonly float VOLUMEUP = 0.2f;
        public static readonly float VOLUMEDOWN = -0.2f; 

        public void Volume(float vol)
        {
            volume += vol; 

            if (volume > 1.0f) { volume = 1.0f; }
            else if (volume < 0.0f) { volume = 0.0f; } 

            soundEffectInstance.Volume = volume; 
        }
    }
}
