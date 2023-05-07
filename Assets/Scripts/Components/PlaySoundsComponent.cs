using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;
    public Key key;
    [SerializeField] private bool _isSongByPieces;

    private AudioData _currentSound;

    private float _dspSongTime;
    private float _songPosition;

    public enum Key
    {
        Music,
        Sound
    }

    private void Start()
    {
        /*if (_isSongByPieces)
        {
            Play(_sounds[0].id);
        }

        _dspSongTime = (float) AudioSettings.dspTime;
        */

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

    /*private void Update()
    {
        _songPosition = (float) (AudioSettings.dspTime - _dspSongTime);

        if (_isSongByPieces)
        {
            if (!_source.isPlaying)
            {
                for (int i = 0; i < _sounds.Length; i++)
                {
                    if (_sounds[i].id != _currentSound.id) continue;

                    Play(i + 1 >= _sounds.Length ? _sounds[0].id : _sounds[i + 1].id);

                    break;
                }
            }
        }

        print("SongPosition: " + _songPosition);
        print("Length: " + _currentSound.clip.length);
    }*/


    public void PlayRandomSoundComponent()
    {
        bool isNewClip = false;
        int random = -1;

        while (!isNewClip)
        {
            random = Random.Range(0, _sounds.Length);

            if (_currentSound != _sounds[random])
            {
                isNewClip = true;
            }
        }

        Play(_sounds[random].id);
    }

    /*public void PlaySoundAfterPiece(string id)
    {
        bool isCanPlay = false;

        while (!isCanPlay)
        {
            if (_currentSound.clip.length - _songPosition <= 1)
            {
                isCanPlay = true;
                Play(id);
            }
        }
    }*/


    public void Play(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.id != id) continue;

            _currentSound = audioData;
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