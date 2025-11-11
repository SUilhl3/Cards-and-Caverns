#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace CardsAndCaverns.SoundManager
{
    [CustomEditor(typeof(SoundsSO))]
    public class SoundsSOEditor : Editor
    {
        private void OnEnable()
        {
            ref SoundList[] soundList = ref ((SoundsSO)target).sounds;

            if (soundList == null)
                return;

            string[] names = Enum.GetNames(typeof(SoundType));
            bool differentSize = names.Length != soundList.Length;

            Dictionary<string, SoundList> existingSounds = new();

            if (differentSize)
            {
                for (int i = 0; i < soundList.Length; ++i)
                {
                    existingSounds[soundList[i].name] = soundList[i];
                }
            }

            Array.Resize(ref soundList, names.Length);
            for (int i = 0; i < soundList.Length; i++)
            {
                string currentName = names[i];
                soundList[i].name = currentName;
                if (soundList[i].volume == 0) soundList[i].volume = 1;

                if (differentSize)
                {
                    if (existingSounds.ContainsKey(currentName))
                    {
                        SoundList current = existingSounds[currentName];
                        UpdateElement(ref soundList[i], current.category, current.volume, current.sounds, current.mixer);
                    }
                    else
                    {
                        UpdateElement(ref soundList[i], SoundCategory.SFX, 1, new AudioClip[0], null);
                    }

                    static void UpdateElement(ref SoundList element, SoundCategory category, float volume, AudioClip[] sounds, AudioMixerGroup mixer)
                    {
                        element.category = category;
                        element.volume = volume;
                        element.sounds = sounds;
                        element.mixer = mixer;
                    }
                }
            }
        }
    }
}
#endif
