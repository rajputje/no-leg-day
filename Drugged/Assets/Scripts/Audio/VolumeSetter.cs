using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null;

    private void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("masterVol", 1f);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVol", volume);
    }
}
