using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : IState
{
    private readonly float _minIdleDuration;
    private readonly float _maxIdleDuration;
    private float _idleTimer;
    private bool _isTurnedRight = false;

    public void OnEnter()
    {
        _idleTimer = Random.Range(_minIdleDuration, _maxIdleDuration);
        _isTurnedRight = false;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}