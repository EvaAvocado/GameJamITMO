using System;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TimerComponent _timer;
    [SerializeField] private TMP_Text _text;

    public TimerComponent Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }
    
    private void Update()
    {
        _text.text =  TimeSpan.FromSeconds(_timer.TimeBeforeApplyAction).Minutes + ":" + TimeSpan.FromSeconds(_timer.TimeBeforeApplyAction).Seconds;
    }
}
