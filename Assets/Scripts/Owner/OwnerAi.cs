using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static Creature;
using Random = UnityEngine.Random;

public class OwnerAi : Owner
{
    [SerializeField] private Transform _target;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private Ladder _ladder;
    [SerializeField] private Thing _thing;
    [SerializeField] private BoxCollider2D _currentCollider;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Transform _targetTransform;

    [SerializeField] private FirstFloor _firstFloor;
    [SerializeField] private SecondFloor _secondFloor;
    [SerializeField] private ThirdFloor _thirdFloor;

    [SerializeField] private float _minIdleTime = 1f;
    [SerializeField] private float _maxIdleTime = 3f;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _minPatrolDuration = 15f;
    [SerializeField] private float _maxPatrolDuration = 30f;
    [SerializeField] private float _speedIncrease = 6f;

    private int _randomPoint1;
    private int _randomPoint2;
    private int _randomPoint3;

    private float _patrolDuration;

    private float _currentTime = 0;
    private float _idleTime = 0f;
    private bool _isTurnedRight = true;
    private bool _isAttacked = false;

    private Animator _animator;
    private Coroutine _coroutine;
    private Coroutine _follow;

    public enum FlorPoints
    {
        firstFlorPoints,
        secondFlorPoints,
        thirdFlorPoints
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Patrol());
        _randomPoint1 = Random.Range(0, _firstFloor.MovePoints.Length);
        _randomPoint2 = Random.Range(0, _secondFloor.MovePoints.Length);
        _randomPoint3 = Random.Range(0, _thirdFloor.MovePoints.Length);

        _patrolDuration = Random.Range(_minPatrolDuration, _maxPatrolDuration);

        CurrentSpeed = Speed;
        _cooldown.Reset();
    }

    private void Update()
    {
        if (_cooldown.IsReady)
            isCanMoveToAnotherFloor = true;
    }

    public void IdleState()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _currentTime = 0f;

        _coroutine = StartCoroutine(Idle());
    }

    public void PatrollingState()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _patrolDuration = Random.Range(_minPatrolDuration, _maxPatrolDuration);

        _coroutine = StartCoroutine(Patrol());
    }

    public void DoStealth()
    {
        _isAttacked = true;
        Attack();
    }

    public void NoAttack()
    {
        //меняется анимация с бега на осмотреться

        if (!_isAttacked)
        {
            StopCoroutine(_follow);

            CurrentSpeed = Speed;
            _currentCollider.enabled = true;
            _collider.enabled = false;
            IdleState();
            print("Не атакую!");
        }
    }

    public void Attack()
    {
        //меняется анимация с хотьбы на бег

        if (_isAttacked)
        {
            CurrentSpeed = _speedIncrease;
            _currentCollider.enabled = false;
            _collider.enabled = true;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(Follow());

            _isAttacked = false;
        }
    }

    public void FixThing()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (Thing.ThingState.Broken == _thing._state)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            //включение анимации починки вещи
            _animator.Play("Fix");

            if (stateInfo.normalizedTime >= 1.0f)
                _thing.FixThing();
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        PatrollingState();
    }

    private IEnumerator Follow()
    {
        while (transform.position != _target.position)
        {
            Vector2 currentPosition = transform.position;

            transform.position = Vector2.MoveTowards(transform.position, _target.position, CurrentSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, currentPosition.y, transform.position.z);

            yield return new WaitForSeconds(2f);

            if (transform.position == _target.position)
            {
                //анимация ловли кота
            }
        }
    }

    private IEnumerator Idle()
    {
        _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
        print("перехожу в айдл");
        yield return new WaitForSeconds(_idleTime);

        //Flip();
        PatrollingState();
    }

    private IEnumerator Patrol()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPositionFirstFloor = _firstFloor.MovePoints[_randomPoint1].position;
        Vector3 targetPositionSecondFloor = _secondFloor.MovePoints[_randomPoint2].position;
        Vector3 targetPositionThirdFloor = _thirdFloor.MovePoints[_randomPoint3].position;

        while (_currentTime <= _patrolDuration)
        {
            // проверяем, достигли ли мы текущей цели (точки патруля)
            if (transform.position == targetPositionFirstFloor || transform.position == targetPositionSecondFloor || transform.position == targetPositionThirdFloor)
            {
                // выбираем следующую точку патруля
                _randomPoint1 = Random.Range(0, _firstFloor.MovePoints.Length);
                _randomPoint2 = Random.Range(0, _secondFloor.MovePoints.Length);
                _randomPoint3 = Random.Range(0, _thirdFloor.MovePoints.Length);

                if (currentFloor == CurrentFloor.First)
                    targetPositionFirstFloor = _firstFloor.MovePoints[_randomPoint1].position;
                else if (currentFloor == CurrentFloor.Second)
                    targetPositionSecondFloor = _secondFloor.MovePoints[_randomPoint2].position;
                else if (currentFloor == CurrentFloor.Third)
                    targetPositionThirdFloor = _thirdFloor.MovePoints[_randomPoint3].position;

                //if (transform.position.x > targetPositionFirstFloor.x && _isTurnedRight ||
                //transform.position.x < targetPositionFirstFloor.x && !_isTurnedRight)
                //{
                //    Flip();
                //}

                //if (transform.position.x > targetPositionSecondFloor.x && _isTurnedRight ||
                //transform.position.x < targetPositionSecondFloor.x && !_isTurnedRight)
                //{
                //    Flip();
                //}

                //if (transform.position.x > targetPositionThirdFloor.x && _isTurnedRight ||
                //transform.position.x < targetPositionThirdFloor.x && !_isTurnedRight)
                //{
                //    Flip();
                //}
            }

            _currentTime += Time.deltaTime;

            if (currentFloor == CurrentFloor.First)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionFirstFloor, Speed * Time.deltaTime);
            else if (currentFloor == CurrentFloor.Second)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionSecondFloor, Speed * Time.deltaTime);
            else if (currentFloor == CurrentFloor.Third)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionThirdFloor, Speed * Time.deltaTime);

            yield return null;
        }

        // если время патрулирования закончилось, переходим в режим ожидания
        IdleState();
    }

    private void Flip()
    {
        _isTurnedRight = !_isTurnedRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

