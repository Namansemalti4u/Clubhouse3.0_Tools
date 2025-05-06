using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using Clubhouse.Helper;
using System.Collections.Generic;
using System.Linq;
using Clubhouse.Tools.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public struct AudioSourceInfo
    {
        public SoundData soundData;
        public Sound sound;
        public bool isLocked;
    }

    public class SoundPlayParams
    {
        public float delay = 0f;
        public bool canOverride = false;
        public bool canPlayMultipleInstance = false;
        public float volume = 0f;
        public float pitch = 0f;
        public bool usingChannelRange = false;
        public int minSfxSourceIndexInclusive = 0, maxSfxSourceIndexExclusive = 0, fallbackSfxSourceIndex = 0;
    }

    [SerializeField] private SoundDataCollection centralSoundDataCollection;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource[] soundEffectSources;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private AudioMixerGroup uiMixerGroup;
    [SerializeField] private Sound uiButtonSound;


    private SoundData gameSoundData;
    private Dictionary<AudioSource, AudioSourceInfo> sourceDictionary;
    private bool isMusicEnabled;
    private bool isSfxEnabled;

    private void Start()
    {
        isMusicEnabled = true;
        isSfxEnabled = true;
        InitializeAudioSources();
    }

    public void SetSoundsInAudioManager(SoundData a_soundData)
    {
        gameSoundData = a_soundData;
    }

    private void InitializeAudioSources()
    {

        backgroundMusicSource.outputAudioMixerGroup = musicMixerGroup;
        backgroundMusicSource.mute = !isMusicEnabled;

        foreach (AudioSource s in soundEffectSources)
        {
            s.outputAudioMixerGroup = sfxMixerGroup;
            s.playOnAwake = false;
            s.mute = !isSfxEnabled;
            s.Stop();
        }

        InitializeSourceDictionary();
        // InitializeUIAudioSource();
    }

    public void InitializeUIAudioSource()
    {
        uiAudioSource.outputAudioMixerGroup = uiMixerGroup;
        uiAudioSource.Stop();
        uiAudioSource.clip = uiButtonSound.clip;
        uiAudioSource.volume = uiButtonSound.volume;
        uiAudioSource.pitch = uiButtonSound.pitch;
        uiAudioSource.loop = false;
        uiAudioSource.playOnAwake = false;
        uiAudioSource.mute = !isSfxEnabled;
        uiButtonSound.sources.Add(uiAudioSource);
    }

    private void InitializeSourceDictionary()
    {
        sourceDictionary = soundEffectSources.ToDictionary(source => source, source => new AudioSourceInfo { sound = null, isLocked = false });
        sourceDictionary[uiAudioSource] = new AudioSourceInfo { sound = uiButtonSound, isLocked = true };
        sourceDictionary[backgroundMusicSource] = new AudioSourceInfo { sound = null, isLocked = true };
    }

    public void SetMusicEnabled(bool a_enabled)
    {
        isMusicEnabled = a_enabled;
        backgroundMusicSource.mute = !isMusicEnabled;
    }

    public void SetSfxEnabled(bool a_enabled)
    {
        isSfxEnabled = a_enabled;
        foreach (AudioSource s in soundEffectSources)
        {
            s.mute = !isSfxEnabled;
        }
    }

    //Todo: Add Lock/Unlock AudioSource functionality + new source generartion istead of fallback

    public void LockAudioChannel(int a_channelIndex, bool a_lock)
    {
        if (a_channelIndex >= soundEffectSources.Length) return;
        AudioSourceInfo info = sourceDictionary[soundEffectSources[a_channelIndex]];
        info.isLocked = a_lock;
        sourceDictionary[soundEffectSources[a_channelIndex]] = info;
    }

    public void Play(string a_soundName, string a_soundCategory = null, SoundPlayParams a_params = null)
    {
        (int sfxSourceIndex, bool canOverrideDueToFallback) = GetSfxAudioSourceIndex(a_params);
        Play(sfxSourceIndex, canOverrideDueToFallback, a_soundName, a_soundCategory, a_params);
    }

    public void Play(int a_sfxSourceId, string a_soundName, string a_soundCategory = null, SoundPlayParams a_params = null)
    {
        Play(a_sfxSourceId, false, a_soundName, a_soundCategory, a_params);
    }

    private void Play(int a_sfxSourceId, bool a_canOverrideDueToFallback, string a_soundName, string a_soundCategory = null, SoundPlayParams a_params = null)
    {
        if (a_sfxSourceId >= soundEffectSources.Length) return;
        AudioSource sfxSource = soundEffectSources[a_sfxSourceId];
        if (sfxSource == null) return;

        if (!(a_canOverrideDueToFallback || (a_params?.canOverride ?? false)) && sfxSource.isPlaying) return;

        SoundData collection = GetSoundData(a_soundCategory);
        Sound sound = collection?.GetSound(a_soundName) ?? null;
        PlaySoundAtSource(sfxSource, sound, collection, a_params);
    }

    public void PlayRandom(string a_soundName, int a_minInclusive, int a_maxExclusive, string a_soundCategory = null, SoundPlayParams a_params = null)
    {
        if (IsPlayingFromRandom(a_soundName, a_minInclusive, a_maxExclusive, a_soundCategory)) return;
        Play(a_soundName + Random.Range(a_minInclusive, a_maxExclusive), a_soundCategory, a_params);
    }

    public bool IsPlaying(string a_soundName, string a_soundCategory = null)
    {
        return GetSoundData(a_soundCategory)?.IsPlaying(a_soundName) ?? false;
    }

    public bool IsPlayingFromRandom(string a_soundName, int a_minInclusive, int a_maxExclusive, string a_soundCategory = null)
    {
        return GetSoundData(a_soundCategory)?.IsPlayingFromRandom(a_soundName, a_minInclusive, a_maxExclusive) ?? false;
    }

    public void Stop(string a_soundName, string a_soundCategory = null)
    {
        SoundData collection = GetSoundData(a_soundCategory);
        collection?.StopSoundAtAllSources(collection.GetSound(a_soundName));
    }

    public void PlayBackgroundMusic(string a_soundName, string a_soundCategory = null, SoundPlayParams a_params = null)
    {
        SoundData collection = GetSoundData(a_soundCategory);
        Sound sound = collection?.GetSound(a_soundName) ?? null;
        PlaySoundAtSource(backgroundMusicSource, sound, collection, a_params);
    }

    public void StopBackgroundMusic()
    {
        StopSoundAtSource(backgroundMusicSource);
    }

    public void StopAllSounds()
    {
        StopBackgroundMusic();
        foreach (AudioSource s in soundEffectSources)
        {
            StopSoundAtSource(s);
        }
    }

    public void ResetGameAudiosystem()
    {
        StopAllSounds();
        gameSoundData?.ResetSoundData();
        centralSoundDataCollection?.ResetSoundCollection();
    }

    public void PlaySoundOnButtonPressed()
    {
        uiAudioSource.Play();
    }

    private int GetFirstUnlockedSfxSourceIndex()
    {
        for (int i = 0; i < soundEffectSources.Length; i++)
        {
            if (!sourceDictionary[soundEffectSources[i]].isLocked) return i;
        }

        // if all are locked, return the first one
        return 0;
    }

    private (int, bool) GetSfxAudioSourceIndex(SoundPlayParams a_params = null)
    {
        int sfxSourceIndex = -1;
        bool canOverrideDueToFallback = false;
        if (a_params?.usingChannelRange ?? false)
        {
            (sfxSourceIndex, canOverrideDueToFallback) = GetChannelIndexForRange(a_params);
        }
        else
        {
            for (int i = 0; i < soundEffectSources.Length; i++)
            {
                if (!soundEffectSources[i].isPlaying && !sourceDictionary[soundEffectSources[i]].isLocked)
                {
                    (sfxSourceIndex, canOverrideDueToFallback) = (i, false);
                    break;
                }
            }
        }

        if (sfxSourceIndex <= 0)
        {
            (sfxSourceIndex, canOverrideDueToFallback) = (GetFirstUnlockedSfxSourceIndex(), true);
        }

        return (sfxSourceIndex, canOverrideDueToFallback);
    }

    private (int, bool) GetChannelIndexForRange(SoundPlayParams a_params)
    {
        if (a_params.minSfxSourceIndexInclusive < 0 || a_params.minSfxSourceIndexInclusive >= soundEffectSources.Length
            || a_params.maxSfxSourceIndexExclusive < 0 || a_params.maxSfxSourceIndexExclusive >= soundEffectSources.Length
            || a_params.minSfxSourceIndexInclusive >= a_params.maxSfxSourceIndexExclusive)
        {
            Debug.LogError("Invalid sfx source index range: " + a_params.minSfxSourceIndexInclusive + " to " + a_params.maxSfxSourceIndexExclusive);
            return (-1, false);
        }

        for (int i = a_params.minSfxSourceIndexInclusive; i < a_params.maxSfxSourceIndexExclusive; i++)
        {
            if (soundEffectSources[i].isPlaying) continue;
            return (i, false);
        }

        if (a_params.fallbackSfxSourceIndex >= 0 && a_params.fallbackSfxSourceIndex < soundEffectSources.Length)
        {
            return (a_params.fallbackSfxSourceIndex, true);
        }

        return (-1, false);
    }

    private SoundData GetSoundData(string a_soundCategory)
    {
        return string.IsNullOrEmpty(a_soundCategory) ? gameSoundData : centralSoundDataCollection.GetCollection(a_soundCategory);
    }

    private void PlaySoundAtSource(AudioSource a_audioSource, Sound a_sound, SoundData a_soundData, SoundPlayParams a_params = null)
    {
        if (a_soundData == null || a_sound == null) return;
        if (!(a_params?.canPlayMultipleInstance ?? false) && a_sound.IsPlaying) return;

        StopSoundAtSource(a_audioSource);
        a_audioSource.clip = a_sound.clip;
        a_audioSource.volume = (a_params?.volume != 0f ? a_params?.volume : a_sound.volume) ?? a_sound.volume;
        a_audioSource.pitch = (a_params?.pitch != 0f ? a_params?.pitch : a_sound.pitch) ?? a_sound.pitch;
        a_audioSource.loop = a_sound.loop;
        a_audioSource.playOnAwake = false;
        a_audioSource.PlayDelayed(a_params?.delay ?? 0f);
        a_sound.sources.Add(a_audioSource);
        sourceDictionary[a_audioSource] = new AudioSourceInfo { sound = a_sound, soundData = a_soundData, isLocked = sourceDictionary[a_audioSource].isLocked };
    }

    private void StopSoundAtSource(AudioSource a_audioSource)
    {
        AudioSourceInfo info = sourceDictionary[a_audioSource];
        if (info.soundData == null || info.sound == null)
        {
            a_audioSource.Stop();
            return;
        }
        bool isStopped = info.soundData.StopSoundAtSource(info.sound, a_audioSource);
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        ResetGameAudiosystem();
    }
}