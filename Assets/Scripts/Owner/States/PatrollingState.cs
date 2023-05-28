using UnityEngine;

public class PatrollingState : IState
{
    private Transform[] _patrolPoints;
    private Animator _animator;
    private OwnerAi _ownerAi;
    
    private int _currentPatrolIndex;
    private float _currentSpeed;
    private float _possibleError = 0.2f;
    private Vector3 _currentDestination;

    public PatrollingState(OwnerAi ownerAi, Animator animator, Transform[] patrolPoints)
    {
        _ownerAi = ownerAi;
        _animator = animator;
        _patrolPoints = patrolPoints;
    }
    
    public void OnEnter()
    {
        UpdateAnimationSpeed();
        
        _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
        _currentDestination = _patrolPoints[_currentPatrolIndex].position;
    }

    public void Tick()
    {
        MoveToDestination();
        CheckReachedDestination();
    }

    public void OnExit()
    {
        
    }
    
    private void UpdateAnimationSpeed()
    {
        _currentSpeed = _ownerAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }
    
    private void MoveToDestination()
    {
        Vector3 position = _ownerAi.transform.position;
        
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
        
        Vector3 direction = (_currentDestination - position).normalized;
        position += direction * _currentSpeed * Time.deltaTime;
        _ownerAi.transform.position = position;
        
        Flip(direction.x > 0 ? true : false);
    }

    private void CheckReachedDestination()
    {
        float distance = Vector3.Distance(_ownerAi.transform.position, _currentDestination);
        
        if (distance <= _possibleError)
        {
            _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
            _currentDestination = _patrolPoints[_currentPatrolIndex].position;
        }
    }
    
    private void Flip(bool shouldFlip)
    {
        // Изменяем направление взгляда врага
        Vector3 scale = _ownerAi.transform.localScale;
        
        scale.x = shouldFlip ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);

        _ownerAi.transform.localScale = scale;
    }
}