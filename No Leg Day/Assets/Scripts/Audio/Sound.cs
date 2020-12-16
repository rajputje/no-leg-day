using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClipNames ClipName;

    public AudioClip Clip;

    public bool Loop = false;

    [Range(0, 1)]
    public float Volume = 1;
    [Range(-3, 3)]
    public float Pitch = 1;

    [HideInInspector]
    public AudioSource Source;

}
