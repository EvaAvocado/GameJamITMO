using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    public Transform[] PatrolPoints => _patrolPoints;
}