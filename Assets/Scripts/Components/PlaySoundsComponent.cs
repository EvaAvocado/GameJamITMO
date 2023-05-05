using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public Key key;

    public enum Key
    {
        Music,
        Sound
    }

    private void Start()
    {
        if (key == Key.Music)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                _source.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
            else
            {
                _source.volume = 1;
            }
        }

        else if (key == Key.Sound)
        {
            if (PlayerPrefs.HasKey("SoundsVolume"))
            {
                _source.volume = PlayerPrefs.GetFloat("SoundsVolume");
            }
            else
            {
                _source.volume = 1;
            }
        }
    }


    public void Play(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.id != id) continue;

            _source.PlayOneShot(audioData.clip);
            break;
        }
    }

    public void SetVolume(float volume)
    {
        _source.volume = volume;
    }

    public void Mute()
    {
        _source.mute = true;
    }


    public void UnMute()
    {
        _source.mute = false;
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string id => _id;
        public AudioClip clip => _clip;
    }
}