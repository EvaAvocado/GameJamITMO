using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OwnerAi : Owner
{
    [SerializeField] private Player _target;
    [SerializeField] private Transform[] _movePoints;

    [SerializeField] private float _minIdleTime = 1f;
    [SerializeField] private float _maxIdleTime = 3f;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _patrolDuration = 20f;

    private TestPatrollingStatecs _patrollingStatecs;

    private Rigidbody2D _rigidbody;
    private int _randomPoint;
    private float _currentTime = 0;
    private float _idleTime = 0f;
    private bool _isDormantStateOff = false;
    private bool _isTurnedRight = true;

    private Coroutine _coroutine;
    private int _currentPointIndex;

    private void Awake()
    {
        _patrollingStatecs = GetComponent<TestPatrollingStatecs>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Patrol());
        _randomPoint = Random.Range(0, _movePoints.Length);
        CurrentSpeed = Speed;
    }

    private void Update()
    {
        //_currentTime += Time.deltaTime;

        //if (_isDormantStateOff)
        //{
        //    _patrollingStatecs.EnterBehavior();
        //}
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
        Vector3 targetPosition = _movePoints[_currentPointIndex].position;

        while (_currentTime <= _patrolDuration)
        {
            // проверяем, достигли ли мы текущей цели (точки патруля)
            if (transform.position == targetPosition)
            {
                // выбираем следующую точку патруля
                _randomPoint = Random.Range(0, _movePoints.Length);
                targetPosition = _movePoints[_randomPoint].position;
            }

            _currentTime += Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, CurrentSpeed * Time.deltaTime);

            // направляемся к текущей цели (точке патруля)
            //Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            //_rigidbody.MovePosition(newPosition);
            //_rigidbody.velocity = (targetPosition - transform.position).normalized * Speed;

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

