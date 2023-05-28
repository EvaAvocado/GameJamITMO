using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private OwnerAi _ownerAi;

    private float _timeElapsed; // Время, прошедшее с начала Idle
    private float _idleDuration = 3f; // Длительность одного цикла Idle
    private bool _isFacingRight; // Флаг, указывающий на необходимость поворота врага
    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _ownerAi.GetSpeed(); 
        _animator.SetFloat(HashAnimation.Speed, _currentSpeed);
        _timeElapsed = 0;
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= _idleDuration)
        {
            // _animator.SetBool("IsIdle", true);
            _timeElapsed = 0;
            _isFacingRight = !_isFacingRight;
            Flip();
        }
    }
    
    private void Flip()
    {
        // Изменяем направление взгляда врага
        Vector3 scale = _ownerAi.transform.localScale;
        scale.x *= -1;
        _ownerAi.transform.localScale = scale;
    }
}
