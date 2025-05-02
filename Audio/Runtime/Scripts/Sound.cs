using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace Clubhouse.Tools.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop = false;
        public bool multipleInstance = false;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-3f, 3f)] public float pitch = 1f;
        [HideInInspector] public List<AudioSource> sources = new List<AudioSource>();
        [HideInInspector] public AudioMixerGroup audioMixerGroup;

        public bool IsPlaying => sources.Count(s => s.isPlaying) > 0;

        public void OnStart()
        {
            sources = new List<AudioSource>();
        }
        public bool IsPlayingAtSource(AudioSource a_audioSource) => sources.Contains(a_audioSource) && a_audioSource.isPlaying;
        public bool StopSoundAtSource(AudioSource a_audioSource)
        {
            if (!IsPlayingAtSource(a_audioSource)) return false;
            a_audioSource.Stop();
            sources.Remove(a_audioSource);
            return true;
        }

        public void StopSoundAtAllSources()
        {
            foreach (AudioSource source in sources)
            {
                if (source != null && source.isPlaying)
                {
                    source.Stop();
                }
            }
            sources.Clear();
        }

        public void ResetSound()
        {
            StopSoundAtAllSources();
        }
    }
}