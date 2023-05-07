using System;
using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _endMenu;

    private PlaySoundsComponent _playSoundsComponent;
    public PlaySoundsComponent _mainMusic;

    private void Awake()
    {
        _playSoundsComponent = GetComponent<PlaySoundsComponent>();
    }

    public void OpenEndMenu()
    {
        if (!_player.hasSecondLife)
        {
           _scoreText.text = "Вы набрали " + _scoreUI.Score + " очков";
            _endMenu.SetActive(true);
            _player.gameObject.SetActive(false); 
        }
        else
        {
            _mainMusic.Mute();
            _playSoundsComponent.Play("Death");
        }
    }

    public void OpenEndMenuFromTimer()
    {
        _mainMusic.Mute();
        _playSoundsComponent.Play("Death");
        _scoreText.text = "Вы набрали " + _scoreUI.Score + " очков";
        _endMenu.SetActive(true);
        _player.gameObject.SetActive(false); 
    }

    //public void 
}
