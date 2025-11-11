using System;
using UnityEngine;
using UnityEngine.Audio;

namespace CardsAndCaverns.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundsSO SO;
        private static SoundManager instance;

        private AudioSource sfxSource;
        private AudioSource musicSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); 
                sfxSource = GetComponent<AudioSource>();
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
        {
            if (instance == null || instance.SO == null) return;
            SoundList soundList = instance.SO.sounds[(int)sound];
            if (soundList.category != SoundCategory.SFX) return;
            AudioClip[] clips = soundList.sounds;
            if (clips == null || clips.Length == 0) return;

            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            if (source)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                source.Play();
            }
            else
            {
                instance.sfxSource.outputAudioMixerGroup = soundList.mixer;
                instance.sfxSource.PlayOneShot(randomClip, volume * soundList.volume);
            }
        }

        public static void PlayMusic(SoundType music, float volume = 1, bool loop = true)
        {
            if (instance == null || instance.SO == null) return;
            SoundList soundList = instance.SO.sounds[(int)music];
            if (soundList.category != SoundCategory.Music) return;

            AudioClip[] clips = soundList.sounds;
            if (clips == null || clips.Length == 0) return;

            AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            instance.musicSource.outputAudioMixerGroup = soundList.mixer;
            instance.musicSource.clip = clip;
            instance.musicSource.volume = volume * soundList.volume;
            instance.musicSource.loop = loop;
            instance.musicSource.Play();
        }

        public static void StopMusic()
        {
            if (instance == null) return;
            instance.musicSource.Stop();
        }

        public static void FadeToMusic(SoundType newMusic, float duration = 1f)
        {
            if (instance == null) return;
            instance.StartCoroutine(instance.FadeMusicRoutine(newMusic, duration));
        }

        private System.Collections.IEnumerator FadeMusicRoutine(SoundType newMusic, float duration)
        {
            float startVolume = musicSource.volume;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
                yield return null;
            }

            PlayMusic(newMusic);
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(0, 1, t / duration);
                yield return null;
            }
        }
    }

    public enum SoundCategory
    {
        SFX,
        Music
    }

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        public SoundCategory category;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
    }
}
