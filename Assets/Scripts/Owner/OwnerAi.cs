using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OwnerAi : Owner
{
    [SerializeField] private Player _target;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private Ladder _ladder;
    [SerializeField] private FirstFloor _firstFloor;
    [SerializeField] private SecondFloor _secondFloor;
    [SerializeField] private ThirdFloor _thirdFloor;
    //[SerializeField] private Transform[] _movePoints;

    [SerializeField] private float _minIdleTime = 1f;
    [SerializeField] private float _maxIdleTime = 3f;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _patrolDuration = 20f;

    private Rigidbody2D _rigidbody;
    private int _randomPoint1;
    private int _randomPoint2;
    private int _randomPoint3;
    private float _currentTime = 0;
    private float _idleTime = 0f;
    private bool _isDormantStateOff = false;
    private bool _isTurnedRight = true;

    private Coroutine _coroutine;
    private int _currentPointIndex;

    public enum FlorPoints
    {
        firstFlorPoints,
        secondFlorPoints,
        thirdFlorPoints
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Patrol());
        _randomPoint1 = Random.Range(0, _firstFloor.MovePoints.Length);
        _randomPoint2 = Random.Range(0, _secondFloor.MovePoints.Length);
        _randomPoint3 = Random.Range(0, _thirdFloor.MovePoints.Length);

        CurrentSpeed = Speed;
        _cooldown.Reset();
    }

    private void Update()
    {
        if (_cooldown.IsReady)
        {
            isCanMoveToAnotherFloor = true;
        }
    }

    public void IdleState()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _currentTime = 0f;
        //Debug.Log("TestIdleStatecs, EnterBehavior");
        _coroutine = StartCoroutine(Idle());
    }

    public void PatrollingState()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Patrol());

        //IdleState();
    }

    private IEnumerator Idle()
    {
        _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
        yield return new WaitForSeconds(_idleTime);
        Flip();
        _isDormantStateOff = true;
        yield return new WaitForSeconds(_idleTime);
        PatrollingState();
        //if (!TryGetComponent(out Player player))
        //Debug.Log("TestIdleStatecs, Idle");
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
            }

            _currentTime += Time.deltaTime;

            if (currentFloor == CurrentFloor.First)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionFirstFloor, Speed * Time.deltaTime);
            else if (currentFloor == CurrentFloor.Second)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionSecondFloor, Speed * Time.deltaTime);
            else if (currentFloor == CurrentFloor.Third)
                transform.position = Vector2.MoveTowards(transform.position, targetPositionThirdFloor, Speed * Time.deltaTime);

            //if (isCanMoveToAnotherFloor)
            //{
            //    _ladder.MoveToAnotherFloorForNpc();
            //}
            {
                // направляемся к текущей цели (точке патруля)
                //Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                //_rigidbody.MovePosition(newPosition);
                //_rigidbody.velocity = (targetPosition - transform.position).normalized * Speed;
            }

            yield return null;
        }

        // если время патрулирования закончилось, переходим в режим ожидания
        IdleState();
    }

    private void Flip()
    {
        //Debug.Log("TestIdleStatecs, Flip");

        _isTurnedRight = !_isTurnedRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

