using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace _1001823_XNA_MiniProject6
{
    class BasicSoundFX
    {
        protected SoundEffect effect; 

        public BasicSoundFX()
        {
        }

        public BasicSoundFX(ContentManager theContentManager, string soundFile)
        {
            effect = theContentManager.Load<SoundEffect>(soundFile);  
        }

        public virtual void LoadSound(ContentManager theContentManager, string soundFile)
        {
            effect = theContentManager.Load<SoundEffect>(soundFile);
        }

        public virtual void Play()
        {
            effect.Play(); 
        } 
    }
}
