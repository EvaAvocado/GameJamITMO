using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AmuletManager : MonoBehaviour
{
    [SerializeField] private Amulet[] _amulets;
    [SerializeField] private AmuletState _currentAmuletState;
    [SerializeField] private Amulet _currentAmulet;

    [SerializeField] private Thing[] _things;

    /*[Header("MorePoints Amulet")] [SerializeField]
    private Cooldown _cooldownMorePoints;

    [SerializeField] private bool _isCanBeMorePoints = true;
    [SerializeField] private Slider _sliderMorePoints;
    [SerializeField] private TimerComponent _timerMorePoints;*/

    public enum AmuletState
    {
        Nothing,
        MorePoints,
        LessClicks,
        SecondLife,
        MoreTime,
        Uselessness,
        RandomRoom
    }
    
    public Thing[] Things
    {
        get { return _things; }
        set { _things = value; }
    }

    public Amulet CurrentAmulet
    {
        get { return _currentAmulet; }
        set { _currentAmulet = value; }
    }

    /*private void Awake()
    {
        _timerMorePoints.TimeFloat = TimeSpan.FromSeconds(_cooldownMorePoints.ValueOfCooldown).Seconds;
    }*/

    /*private void Update()
    {
        if (_currentAmuletState == AmuletState.MorePoints)
        {
           if (_currentAmulet != null) _sliderMorePoints.value = _timerMorePoints.TimeBeforeApplyAction;
           
            if (_cooldownMorePoints.IsReady && !_isCanBeMorePoints)
            { 
                ResetMorePoints();
                ResetCurrentAmulet();
            } 
        }
        
    }*/

    private void ApplyEffect()
    {
        if (_currentAmuletState == AmuletState.Nothing) return;
        _currentAmulet.SetEffect();
        
        /*switch (_currentAmuletState)
        {
            case AmuletState.MorePoints:
                _currentAmulet.SetEffect();
                break;
            case AmuletState.LessClicks:
                //
                break;
            case AmuletState.SecondLife:
                //
                break;
            case AmuletState.MoreTime:
                //
                break;
            case AmuletState.Uselessness:
                //
                break;
            case AmuletState.RandomRoom:
                //
                break;
        }*/
    }

    /*private void SetMorePointsEffect()
    {
        if (_isCanBeMorePoints)
        {
            foreach (var thing in _things)
            {
                thing.currentPoints = thing.points * 2;
            }
            
            _timerMorePoints.DoAction = true;
            _isCanBeMorePoints = false;
        }
    }*/

    public void ResetCurrentAmulet()
    {
        _currentAmuletState = AmuletState.Nothing;
        _currentAmulet = null;
    }

    public void SpawnNewRandomAmulet()
    {
        foreach (var amulet in _amulets)
        {
            amulet.Reset();
        }

        ChooseCurrentAmulet();
        SpawnCurrentAmulet();
    }

    private void SpawnCurrentAmulet()
    {
        foreach (var amulet in _amulets)
        {
            if (amulet.amuletState == _currentAmuletState)
            {
                amulet.gameObject.SetActive(true);
                _currentAmulet = amulet;

                ApplyEffect();
            }
            else
            {
                amulet.gameObject.SetActive(false);
            }
        }
    }

    private void ChooseCurrentAmulet()
    {
        var random = Random.Range(0, 101);

        if (IsInRange(random, 0, 12))
        {
            _currentAmuletState = AmuletState.MorePoints;
        }
        else if (IsInRange(random, 13, 26))
        {
            _currentAmuletState = AmuletState.LessClicks;
        }
        else if (IsInRange(random, 27, 39))
        {
            _currentAmuletState = AmuletState.SecondLife;
        }
        else if (IsInRange(random, 40, 52))
        {
            _currentAmuletState = AmuletState.MoreTime;
        }
        else if (IsInRange(random, 41, 87))
        {
            _currentAmuletState = AmuletState.Uselessness;
        }
        else if (IsInRange(random, 88, 100))
        {
            _currentAmuletState = AmuletState.RandomRoom;
        }
    }

    private bool IsInRange(int value, int v1, int v2)
    {
        return Enumerable.Range(v1, v2).Contains(value);
    }

    /*private void ResetMorePoints()
    {
        foreach (var thing in _things)
        {
            thing.currentPoints = thing.points;
        }

        if (_currentAmulet != null)
        {
            _currentAmulet.gameObject.SetActive(false);
        }
        
        _currentAmulet = null;
        _sliderMorePoints.maxValue = _cooldownMorePoints.ValueOfCooldown;
        _timerMorePoints.Reset();
        _timerMorePoints.DoAction = false;
        _cooldownMorePoints.Reset();
        _isCanBeMorePoints = true;
    }*/
}