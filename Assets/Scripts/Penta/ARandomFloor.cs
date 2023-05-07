using System;
using UnityEngine;
using UnityEngine.UI;

public class ARandomFloor : Amulet
{
    [SerializeField] private Player _player;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private Slider _slider;
    [SerializeField] private TimerComponent _timer;
    
    private void OnEnable()
    {
        _timer.TimeFloat = TimeSpan.FromSeconds(_cooldown.ValueOfCooldown).Seconds;
    }

    private void Update()
    {
        UpdateTimer();
    }

    protected override void UpdateTimer()
    {
        _slider.value = _timer.TimeBeforeApplyAction;
        if (_cooldown.IsReady)
        { 
            Reset();
        } 
    }

    public override void SetEffect()
    {
        _player.isCanToRandomFloor = true;
    }

    public override void Reset()
    {
        _player.isCanToRandomFloor = false;
        
        _slider.maxValue = _cooldown.ValueOfCooldown;
        _timer.Reset();
        _timer.DoAction = false;
        _cooldown.Reset();

        amuletManager.ResetCurrentAmulet();
    }


}