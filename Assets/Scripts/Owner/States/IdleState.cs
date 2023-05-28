using UnityEngine;

public class IdleState : IState
{
    private float _timeElapsed; // Время, прошедшее с начала Idle
    private float _idleDuration; // Длительность одного цикла Idle
    private bool _isFacingRight; // Флаг, указывающий на необходимость поворота врага
    
    private Animator _animator;
    private OwnerAi _ownerAi;
    private float _currentSpeed;

    public IdleState(Animator animator, OwnerAi ownerAi, float idleDuration)
    {
        _animator = animator;
        _ownerAi = ownerAi;
        _idleDuration = idleDuration;
    }

    public void OnEnter()
    {
        //_animator.SetBool(HashAnimation.IsIdle, true);
        UpdateAnimationSpeed();
        
        _timeElapsed = 0;
    }

    public void Tick()
    {
        _timeElapsed += Time.deltaTime;

        if (!(_timeElapsed >= _idleDuration)) return;

        UpdateAnimationSpeed();
        
        _timeElapsed = 0;
        _isFacingRight = !_isFacingRight;
        Flip();
    }
    
    public void OnExit()
    {
        UpdateAnimationSpeed();
        
        _timeElapsed = 0;
    }

    private void Flip()
    {
        // Изменяем направление взгляда врага
        Vector3 scale = _ownerAi.transform.localScale;
        scale.x *= -1;
        _ownerAi.transform.localScale = scale;
    }

    private void UpdateAnimationSpeed()
    {
        _currentSpeed = _ownerAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
    }
}