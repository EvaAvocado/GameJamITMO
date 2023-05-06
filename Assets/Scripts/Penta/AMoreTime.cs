using UnityEngine;

public class AMoreTime : Amulet
{
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private float _secondsToAdd;
    
    
    public override void SetEffect()
    {
        _timerUI.Timer.TimeBeforeApplyAction += _secondsToAdd;
    }

    public override void Reset()
    {
        
    }


}