using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmuletManager : MonoBehaviour
{
    [SerializeField] private Amulet[] _amulets;
    [SerializeField] private AmuletState _currentAmulet;
    
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

    private void Update()
    {
        if (_currentAmulet == AmuletState.Nothing) return;
        
        switch (_currentAmulet)
        {
            case AmuletManager.AmuletState.MorePoints:
                //
                break;
            case AmuletManager.AmuletState.LessClicks:
                //
                break;
            case AmuletManager.AmuletState.SecondLife:
                //
                break;
            case AmuletManager.AmuletState.MoreTime:
                //
                break;
            case AmuletManager.AmuletState.Uselessness:
                //
                break;
            case AmuletManager.AmuletState.RandomRoom:
                //
                break;
        }
    }
    
    //private void 

    public void SpawnRandomAmulet()
    {
        ChooseCurrentAmulet();
        SpawnCurrentAmulet();
    }

    private void SpawnCurrentAmulet()
    {
        foreach (var amulet in _amulets)
        {
            if (amulet.amuletState == _currentAmulet)
            {
                amulet.gameObject.SetActive(true);
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
        print(random);
        
        if (IsInRange(random, 0, 12))
        {
            _currentAmulet = AmuletState.MorePoints;
        } else if (IsInRange(random, 13, 26))
        {
            _currentAmulet = AmuletState.LessClicks;
        } else if (IsInRange(random, 27, 39))
        {
            _currentAmulet = AmuletState.SecondLife;
        } else if (IsInRange(random, 40, 52))
        {
            _currentAmulet = AmuletState.MoreTime;
        } else if (IsInRange(random, 41, 87))
        {
            _currentAmulet = AmuletState.Uselessness;
        } else if (IsInRange(random, 88, 100))
        {
            _currentAmulet = AmuletState.RandomRoom;
        }
    }
    
    private bool IsInRange(int value, int v1, int v2)
    {
        return Enumerable.Range(v1,v2).Contains(value);
    }
}
