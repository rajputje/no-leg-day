using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    [SerializeField] private AudioMixerGroup audioMixerGroup = null;
    [SerializeField] private Sound[] sounds = null;

    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.outputAudioMixerGroup = audioMixerGroup;
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    public void PlaySound(AudioClipNames clipName)
    {
        Sound sound = GetSound(clipName);
        if(sound == null)
        {
            Debug.LogWarning($"Audio with the name {clipName} not found");
            return;
        }
        sound.Source.Play();
    }

    private Sound GetSound(AudioClipNames clipName)
    {
        return (from s in sounds
                where s.ClipName == clipName
                select s).First();
    }

    public void PlaySoundIfNotPlaying(AudioClipNames clipName)
    {
        Sound sound = GetSound(clipName);
        if (sound == null)
        {
            Debug.LogWarning($"Audio with the name {clipName} not found");
            return;
        }
        if (sound.Source.isPlaying)
        {
            return;
        }
        sound.Source.Play();
    }

    public void StopSound(AudioClipNames clipName)
    {
        Sound sound = GetSound(clipName);
        sound.Source.Stop();
    }
}
