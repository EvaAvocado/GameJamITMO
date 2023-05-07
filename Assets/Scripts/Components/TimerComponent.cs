using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private bool _doAction = true;
    [SerializeField] private UnityEvent _action;
    public bool test;
    
    private float _timeBeforeApplyAction;

    public bool DoAction
    {
        get { return _doAction; }
        set { _doAction = value; }
    }

    public float TimeFloat
    {
        get { return _time; }
        set { _time = value; }
    }

    public float TimeBeforeApplyAction
    {
        get { return _timeBeforeApplyAction; }
        set { _timeBeforeApplyAction = value; }
    }

    private void Awake()
    {
        _timeBeforeApplyAction = _time;
    }

    private void Update()
    {
        if (_doAction)
        {
            Timer();
        }
    }

    public void SetNegativeDoAction()
    {
        _doAction = !_doAction;
    }
    
    public void Timer()
    {
        _timeBeforeApplyAction -= Time.deltaTime;
        
        if (_timeBeforeApplyAction < 0)
        {
            _timeBeforeApplyAction = _time;
            _action?.Invoke();
        }
    }

    public void Reset()
    {
        _timeBeforeApplyAction = _time;
    }
}