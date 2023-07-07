using UnityEngine;

public class IdleState : IState
{
    private float _turnDuration = 3f;
    private float _turnElapsed = 0f;
    private bool _isFacingRight;
    
    private Animator _animator;
    private EnemyAi _enemyAi;
    private float _currentSpeed;

    public IdleState(EnemyAi enemyAi, Animator animator)
    {
        _enemyAi = enemyAi;
        _animator = animator;
    }

    public void OnEnter()
    {
        _currentSpeed = 0f;
        _turnElapsed = 0f;
        _enemyAi.ResetTimeElapsed();
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }

    public void Tick()
    {
        _enemyAi.CalculateElapsedTime();
        _turnElapsed += Time.deltaTime;
        //походу надо поменять _timeElapsed на внутренюю переменную
        if (_turnElapsed >= _turnDuration)
        {
            _turnElapsed = 0f;
            _isFacingRight = !_isFacingRight;
            Flip();
        }
    }
    
    public void OnExit()
    {
        _turnElapsed = 0f;
        _enemyAi.ResetTimeElapsed();
    }

    private void Flip()
    {
        // Изменяем направление взгляда врага
        var transform = _enemyAi.transform;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimationSpeed()
    {
        _currentSpeed = _enemyAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }
}