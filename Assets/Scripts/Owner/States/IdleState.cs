using UnityEngine;

public class IdleState : IState
{
    private float _idleDuration = 3f; // Длительность одного цикла Idle
    private bool _isFacingRight; // Флаг, указывающий на необходимость поворота врага
    
    private Animator _animator;
    private EnemyAi _enemyAi;
    private float _currentSpeed;

    public IdleState(EnemyAi enemyAi, Animator animator,float idleDuration)
    {
        _enemyAi = enemyAi;
        _animator = animator;
        _idleDuration = idleDuration;
    }

    public void OnEnter()
    {
        _currentSpeed = 0;
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }

    public void Tick()
    {
        _enemyAi.CalculateElapsedTime();
        //походу надо поменять _timeElapsed на внутренюю переменную
        if (!(_enemyAi._timeElapsed >= _idleDuration)) return;

        _isFacingRight = !_isFacingRight;
        Flip();
    }
    
    public void OnExit()
    {
        _enemyAi._timeElapsed = 0f;
    }

    private void Flip()
    {
        // Изменяем направление взгляда врага
        Vector3 scale = _enemyAi.transform.localScale;
        scale.x *= -1;
        _enemyAi.transform.localScale = scale;
    }

    private void UpdateAnimationSpeed()
    {
        _currentSpeed = _enemyAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }
}