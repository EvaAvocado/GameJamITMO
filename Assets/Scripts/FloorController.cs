using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FloorController : MonoBehaviour
{
    [SerializeField] private Transform[] _floors;
    [SerializeField] private float _staircaseDetectionRadius = 0.2f;

    private int _currentFloor = 0;
    public bool _isNearStaircase = false;
    private EnemyAi _enemyAi;

    private void Awake()
    {
        _enemyAi = GetComponent<EnemyAi>();
    }

    private void Start()
    {
        _currentFloor = 0;
    }

    public int GetCurrentFloorIndex()
    {
        return _currentFloor;
    }
    
    public void ChangeFloor()
    {
        Debug.Log("класс FloorController метод ChangeFloor");
        
        // Проверка, что враг находится рядом с лестницей на текущем этаже
        if (_isNearStaircase)
        {
            Debug.Log("класс FloorController метод ChangeFloor его if");

            switch (_currentFloor)
            {
                case 0:
                    _currentFloor = 1;
                    break;
                case 1:
                    int nextFloor = Random.Range(0, _floors.Length);
                    
                    while (nextFloor == 1)
                        nextFloor = Random.Range(0, _floors.Length);
                    
                    _currentFloor = nextFloor;
                    break;
                case 2:
                    _currentFloor = 1;
                    break;
            }
            // Установка новой позиции врага
            _enemyAi.transform.position = _floors[_currentFloor].position;
            _isNearStaircase = false;
        }
    }

    public void CheckNearStaircase()
    {
        Debug.Log("класс FloorController метод CheckNearStaircase");
        
        foreach (Transform floor in _floors)
        {
            float distance = Vector3.Distance(_enemyAi.transform.position, floor.position);

            if (distance <= _staircaseDetectionRadius)
            {
                _isNearStaircase = true;
                return;
            }
        }

        _isNearStaircase = false;
    }
}