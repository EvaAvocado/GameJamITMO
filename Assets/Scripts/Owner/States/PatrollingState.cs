using UnityEngine;

public class PatrollingState : IState
{
    private Transform[] _patrolPoints;
    private Animator _animator;
    private EnemyAi _enemyAi;
    
    private int _currentPatrolIndex;
    private float _currentSpeed;
    private float _patrolDuration = 10f;
    private float _possibleError = 0.2f;
    private Vector3 _currentDestination;

    public PatrollingState(EnemyAi enemyAi, Animator animator, Transform[] patrolPoints, float patrolDuration)
    {
        _enemyAi = enemyAi;
        _animator = animator;
        _patrolPoints = patrolPoints;
        _patrolDuration = patrolDuration;
    }
    
    public void OnEnter()
    {
        UpdateAnimationSpeed();
        
        _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
        _currentDestination = _patrolPoints[_currentPatrolIndex].position;
    }

    public void Tick()
    {
        _enemyAi.CalculateElapsedTime();
        
        MoveToDestination();
        CheckReachedDestination();
    }

    public void OnExit()
    {
        _enemyAi._timeElapsed = 0f;
    }
    
    private void UpdateAnimationSpeed()
    {
        _currentSpeed = _enemyAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }
    
    private void MoveToDestination()
    {
        Vector3 position = _enemyAi.transform.position;
        
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
        
        Vector3 direction = (_currentDestination - position).normalized;
        position += direction * _currentSpeed * Time.deltaTime;
        _enemyAi.transform.position = position;
        
        Flip(!(direction.x > 0));
    }

    private void CheckReachedDestination()
    {
        float distance = Vector3.Distance(_enemyAi.transform.position, _currentDestination);
        
        if (distance <= _possibleError)
        {
            _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
            _currentDestination = _patrolPoints[_currentPatrolIndex].position;
        }
    }
    
    private void Flip(bool shouldFlip)
    {
        Vector3 scale = _enemyAi.transform.localScale;
        scale.x = shouldFlip ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        _enemyAi.transform.localScale = scale;
    }
}