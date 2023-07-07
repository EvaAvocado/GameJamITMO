using UnityEngine;

public class PatrollingState : IState
{
    private Floor[] _floors;
    private Animator _animator;
    private EnemyAi _enemyAi;
    private FloorController _floorController;
    
    private int _currentPatrolIndex;
    private float _currentSpeed;
    private int _currentFloorIndex;
    private float _possibleError = 0.2f;
    private float _timeSinceFloorChange = 0f;
    private Vector3 _currentDestination;
    private float _floorChangeInterval = 10f;

    public PatrollingState(EnemyAi enemyAi, Animator animator, FloorController floorController, Floor[] floors)
    {
        _enemyAi = enemyAi;
        _animator = animator;
        _floors = floors;
        _floorController = floorController;
    }
    
    public void OnEnter()
    {
        UpdateAnimationSpeed();

        _timeSinceFloorChange = 0f;
        _currentFloorIndex = _floorController.GetCurrentFloorIndex();
        _currentPatrolIndex = Random.Range(0, _floors[_currentFloorIndex].PatrolPoints.Length);
        _currentDestination = _floors[_currentFloorIndex].PatrolPoints[_currentPatrolIndex].position;
    }

    public void Tick()
    {
        _timeSinceFloorChange += Time.deltaTime;
        _enemyAi.CalculateElapsedTime();
        UpdateAnimationSpeed();
        MoveToDestination();
        CheckReachedDestination();
        
        if (_timeSinceFloorChange >= _floorChangeInterval)
        {
            Debug.Log("класс PatrollingState метод Tick его if");
            _floorController.CheckNearStaircase();
            
            if (_floorController._isNearStaircase)
            {
                _floorController.ChangeFloor();
            
                _currentFloorIndex = _floorController.GetCurrentFloorIndex();
                _currentPatrolIndex = Random.Range(0, _floors[_currentFloorIndex].PatrolPoints.Length);
                _currentDestination = _floors[_currentFloorIndex].PatrolPoints[_currentPatrolIndex].position;
                _timeSinceFloorChange = 0f;
            }
        }
    }

    public void OnExit()
    {
        _enemyAi.ResetTimeElapsed();
    }

    private void UpdateAnimationSpeed() => _animator.SetFloat(HashAnimation.Speed, _enemyAi.Speed);

    private void MoveToDestination()
    {
        Vector3 position = _enemyAi.transform.position;
        
        UpdateAnimationSpeed();
        
        Vector3 direction = (_currentDestination - position).normalized;
        position += direction * (_enemyAi.Speed * Time.deltaTime);
        _enemyAi.transform.position = position;
        
        Flip(!(direction.x > 0));
    }

    private void CheckReachedDestination()
    {
        float distance = Vector3.Distance(_enemyAi.transform.position, _currentDestination);
        
        if (distance <= _possibleError)
        {
            _currentPatrolIndex = Random.Range(0, _floors[_currentFloorIndex].PatrolPoints.Length);
            _currentDestination = _floors[_currentFloorIndex].PatrolPoints[_currentPatrolIndex].position;
        }
    }
    
    private void Flip(bool shouldFlip)
    {
        Vector3 scale = _enemyAi.transform.localScale;
        scale.x = shouldFlip ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        _enemyAi.transform.localScale = scale;
    }
}