using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _score;

    public void ChangeScore(int value)
    {
        _score += value;
        _text.text = _score.ToString();
    }
}
