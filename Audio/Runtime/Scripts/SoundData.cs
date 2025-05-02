using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Clubhouse.Tools.Audio
{
    [CreateAssetMenu(fileName = "New Sound Collection", menuName = "Audio/Sound Collection")]
    public class SoundData : ScriptableObject
    {
        public Sound[] sounds;
        private Dictionary<string, Sound> soundDictionary;

        public void Initialize()
        {
            soundDictionary = sounds.ToDictionary(r => r.name, r => r);

            foreach (Sound sound in sounds)
            {
                sound.OnStart();
            }
        }

        public void ResetSoundData()
        {
            foreach (Sound sound in sounds)
            {
                sound?.ResetSound();
            }
        }

        // Helper method to get all sounds
        public Sound[] GetSounds()
        {
            return sounds;
        }

        // Helper method to find a specific sound by name
        public Sound GetSound(string a_soundName)
        {
            // return Array.Find(sounds, sound => sound.name == soundName);
            if (soundDictionary == null) Initialize();
            if (!soundDictionary.ContainsKey(a_soundName))
            {
                Debug.LogError("Sound: " + a_soundName + " not found!");
                return null;
            }
            return soundDictionary[a_soundName];
        }

        public bool IsPlaying(Sound a_sound)
        {
            if (a_sound == null)
            {
                Debug.LogError("Sound is null!");
                return false;
            }
            return a_sound.IsPlaying;
        }

        public bool IsPlayingAtSource(Sound a_sound, AudioSource a_audioSource)
        {
            if (a_audioSource == null || a_sound == null){
                Debug.LogError("Sound or AudioSource is null!");
                return false;
            }
            return a_sound.IsPlayingAtSource(a_audioSource);
        }

        public bool IsPlaying(string a_soundName)
        {
            Sound sound = GetSound(a_soundName);
            return IsPlaying(sound);
        }

        public bool IsPlayingFromRandom(string a_soundName, int a_minInclusive, int a_maxExclusive)
        {
            for (int i = a_minInclusive; i < a_maxExclusive; i++)
            {
                if (IsPlaying(a_soundName + i))
                {
                    return true;
                }
            }
            return false;
        }

        public bool StopSoundAtSource(Sound a_sound, AudioSource a_audioSource)
        {
            if (!IsPlayingAtSource(a_sound, a_audioSource)) return false;
            return a_sound.StopSoundAtSource(a_audioSource);
        }

        public void StopSoundAtAllSources(Sound a_sound)
        {
            if (!IsPlaying(a_sound)) return;
            a_sound.StopSoundAtAllSources();
        }

        private void OnValidate()
        {
            foreach (Sound sound in sounds)
            {
                if(sound.volume == 0) sound.volume = 1;
                if(sound.pitch == 0) sound.pitch = 1;
            }
        }
    }
}