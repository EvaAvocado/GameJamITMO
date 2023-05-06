using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    
    private PlaySoundsComponent[] _playSoundsComponents;
    private float _musicVolume;
    private float _soundVolume;

    private void Awake()
    {
        _playSoundsComponents = FindObjectsOfType<PlaySoundsComponent>();
    }

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            
        }
        else
        {
            _musicVolume = 0.5f;
        }

        _musicSlider.value = _musicVolume;

        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            _soundVolume = PlayerPrefs.GetFloat("SoundsVolume");
        }
        else
        {
            _soundVolume = 0.5f;
        }
        
        _soundsSlider.value = _soundVolume;
    }

    public void ChangeVolumeForMusic(float volume)
    {
        if (_playSoundsComponents == null)
        {
            _playSoundsComponents = FindObjectsOfType<PlaySoundsComponent>();
        }
        
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
        if (_playSoundsComponents == null)
        {
            _playSoundsComponents = FindObjectsOfType<PlaySoundsComponent>();
        }
        
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