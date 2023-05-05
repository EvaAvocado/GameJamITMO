using System;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private PlaySoundsComponent[] _playSoundsComponents;
    private float _musicVolume;
    private float _soundVolume;

    private void Awake()
    {
        _playSoundsComponents = FindObjectsOfType<PlaySoundsComponent>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            _musicVolume = 1;
        }

        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            _soundVolume = PlayerPrefs.GetFloat("SoundsVolume");
        }
        else
        {
            _soundVolume = 1;
        }
    }

    public void ChangeVolumeForMusic(float volume)
    {
        _musicVolume = volume;

        foreach (var component in _playSoundsComponents)
        {
            if (component.key == PlaySoundsComponent.Key.Music)
            {
                component.SetVolume(_musicVolume);
            }
        }
    }

    public void ChangeVolumeForSounds(float volume)
    {
        _soundVolume = volume;

        foreach (var component in _playSoundsComponents)
        {
            if (component.key == PlaySoundsComponent.Key.Sound)
            {
                component.SetVolume(_soundVolume);
            }
        }
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        PlayerPrefs.SetFloat("SoundsVolume", _soundVolume);
    }
}