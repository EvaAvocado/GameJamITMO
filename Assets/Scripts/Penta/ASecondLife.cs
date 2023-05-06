using System;
using UnityEngine;

public class ASecondLife : Amulet
{
    [SerializeField] private Player _player;
    [SerializeField] private  bool _isHasSecondLife;
    
    
    public void SpendSecondLife()
    {
        if (_isHasSecondLife)
        {
            _player.playerState = Player.PlayerState.Default;
            _isHasSecondLife = false;
        }
    }
    
    public override void SetEffect()
    {
        _isHasSecondLife = true;
    }

    public override void Reset()
    {
        _isHasSecondLife = false;
    }


}