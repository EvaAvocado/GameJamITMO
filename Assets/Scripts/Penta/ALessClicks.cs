using System;
using UnityEngine;
using UnityEngine.UI;

public class ALessClicks : Amulet
{
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private bool _isCanBe = true;
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
        if (_cooldown.IsReady && !_isCanBe)
        { 
            Reset();
        } 
    }

    public override void SetEffect()
    {
        if (_isCanBe)
        {
            foreach (var thing in amuletManager.Things)
            {
                thing.currentClickCount = thing.clickCount / 2;
            }
            
            _timer.DoAction = true;
            _isCanBe = false;
        }
    }

    public override void Reset()
    {
        foreach (var thing in amuletManager.Things)
        {
            thing.currentClickCount = thing.clickCount;
        }

        if (amuletManager.CurrentAmulet != null)
        {
            amuletManager.CurrentAmulet.gameObject.SetActive(false);
        }
        
        _slider.maxValue = _cooldown.ValueOfCooldown;
        _timer.Reset();
        _timer.DoAction = false;
        _cooldown.Reset();
        _isCanBe = true;

        amuletManager.ResetCurrentAmulet();
    }
}