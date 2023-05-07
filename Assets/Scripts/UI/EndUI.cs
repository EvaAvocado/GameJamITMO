using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _endMenu;

    public void OpenEndMenu()
    {
        if (!_player.hasSecondLife)
        {
           _scoreText.text = "Вы набрали " + _scoreUI.Score + " очков";
            _endMenu.SetActive(true);
            _player.gameObject.SetActive(false); 
        }
    }

    public void OpenEndMenuFromTimer()
    {
        _scoreText.text = "Вы набрали " + _scoreUI.Score + " очков";
        _endMenu.SetActive(true);
        _player.gameObject.SetActive(false); 
    }

    //public void 
}
