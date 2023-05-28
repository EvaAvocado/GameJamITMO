using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField]private Animator _animator;

    private int _currentPatrolIndex;
    private float _currentSpeed;
    private Vector3 _currentDestination;

    private void Start()
    {
        _currentSpeed = 2f;
        _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
        _currentDestination = _patrolPoints[_currentPatrolIndex].position;
    }

    private void Update()
    {
        MoveToDestination();
        CheckReachedDestination();
    }

    private void MoveToDestination()
    {
        Vector3 position = transform.position;
        
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
        
        Vector3 direction = (_currentDestination - position).normalized;
        position += direction * _currentSpeed * Time.deltaTime;
        transform.position = position;
        
        if (direction.x > 0)
        {
            Flip(true); // Не отражаем
        }
        else if (direction.x < 0)
        {
            Flip(false); // Отражаем
        }
    }

    private void CheckReachedDestination()
    {
        float distance = Vector3.Distance(transform.position, _currentDestination);
        
        if (distance <= 0.3f)
        {
            _currentPatrolIndex = Random.Range(0, _patrolPoints.Length);
            _currentDestination = _patrolPoints[_currentPatrolIndex].position;
        }
    }
    
    private void Flip(bool shouldFlip)
    {
        // Изменяем направление взгляда врага
        Vector3 scale = transform.localScale;
        if (shouldFlip)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}