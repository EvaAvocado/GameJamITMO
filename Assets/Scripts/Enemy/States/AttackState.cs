using UnityEngine;

public class AttackState : IState
{
    private EnemyAi _enemyAi;
    private Animator _animator;
    private Transform _playerTransform;
    private float _currentSpeed;

    public AttackState(EnemyAi enemyAi, Animator animator, Transform playerTransform)
    {
        _enemyAi = enemyAi;
        _animator = animator;
        _playerTransform = playerTransform;
    }

    public void OnEnter()
    {
        _currentSpeed = _enemyAi.Speed;
        _enemyAi.SetSpeed(_currentSpeed * 1.5f);

        UpdateAnimationSpeed();
    }

    public void Tick()
    {
        if (IsPlayerInSight()) // Проверка, находится ли игрок в поле зрения врага
        {
            MoveTowardsPlayer(); // Логика передвижения врага за игроком

            float distanceToPlayer = Vector3.Distance(_enemyAi.transform.position, _playerTransform.position);

            if (distanceToPlayer < 1f) // Проверка, если расстояние между врагом и игроком маленькое
            {
                //_animator.SetTrigger(HashAnimation.CatchPlayer); // Проигрывание анимации ловли игрока
                // Дополнительная логика для обработки ловли игрока
            }
        }
    }

    public void OnExit()
    {
    }

    private bool IsPlayerInSight()
    {
        Vector3 enemyPosition = _enemyAi.transform.position;
        Vector3 playerPosition = _playerTransform.position;
    
        Vector3 direction = playerPosition - enemyPosition;
        float distance = direction.magnitude;

        // Проверка на препятствие между врагом и игроком
        if (Physics.Raycast(enemyPosition, direction, out RaycastHit hit, distance))
        {
            // Проверка, что объект, с которым пересекся луч, является игроком
            Player component;

            if (hit.collider.TryGetComponent(out component) != null)
            {
                // Игрок находится в поле зрения
                return true;
            }
        }

        // Игрок не находится в поле зрения
        return false;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 playerPosition = _playerTransform.position;
        Vector3 enemyPosition = _enemyAi.transform.position;

        Vector3 direction = (playerPosition - enemyPosition).normalized;
        Vector3 newPosition = enemyPosition + direction * _enemyAi.Speed * Time.deltaTime;

        _enemyAi.transform.position = newPosition;

        Flip(!(direction.x > 0));
    }

    private void Flip(bool shouldFlip)
    {
        Vector3 scale = _enemyAi.transform.localScale;
        scale.x = shouldFlip ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        _enemyAi.transform.localScale = scale;
    }

    private void UpdateAnimationSpeed() => _animator.SetFloat(HashAnimation.Speed, _enemyAi.Speed);
}