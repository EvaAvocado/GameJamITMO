using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : Creature
{
    public bool isCanMoveToAnotherFloor;
    private Player _target;

    public Player Target => _target;
}
