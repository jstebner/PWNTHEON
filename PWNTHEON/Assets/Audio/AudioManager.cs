using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup sfxMixer;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;


    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVol = PlayerPrefs.GetFloat("SfxVolume");
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
        OnMusicSliderValueChange(musicVol);
        OnSfxSliderValueChange(sfxVol);
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicMixer.audioMixer.SetFloat("Music Volume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void OnSfxSliderValueChange(float value)
    {
        musicMixer.audioMixer.SetFloat("SFX Volume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("SfxVolume", value);
        PlayerPrefs.Save();
    }
}