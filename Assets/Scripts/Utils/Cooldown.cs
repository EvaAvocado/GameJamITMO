﻿using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _valueOfCooldown;

    private float _timesUp;
    
    public float ValueOfCooldown
    {
        get { return _valueOfCooldown; }
        set { _valueOfCooldown = value; }
    }

    public void Reset()
    {
        _timesUp = Time.time + _valueOfCooldown;
    }

    public bool IsReady => _timesUp <= Time.time;
}