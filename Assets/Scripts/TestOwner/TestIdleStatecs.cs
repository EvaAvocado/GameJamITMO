using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestIdleStatecs : MonoBehaviour
{
    //[SerializeField] private Player _target;
    [SerializeField] private float _minIdleTime = 1f;
    [SerializeField] private float _maxIdleTime = 3f;

    private TestPatrollingStatecs _patrollingStatecs;

    private bool _isDormantStateOff = false;
    private float _idleTime = 0f;
    private bool _isTurnedRight = true;

    private Coroutine _coroutine;

    private void Awake()
    {
        _patrollingStatecs = GetComponent<TestPatrollingStatecs>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Idle());
    }

    private void Update()
    {
        if (_isDormantStateOff)
        {
            _patrollingStatecs.EnterBehavior();
        }
    }

    public void EnterBehavior()
    {
        Debug.Log("TestIdleStatecs, EnterBehavior");
        _coroutine = StartCoroutine(Idle());
    }

    //public void ExitBehavior()
    //{
    //    if (_coroutine != null)
    //        StopCoroutine(_coroutine);

    //    if (_coroutine == null)
    //        _stateMachine.EnterBehavior<TestPatrollingStatecs>();
    //}

    private IEnumerator Idle()
    {
        _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
        yield return new WaitForSeconds(_idleTime);
        Flip();
        _isDormantStateOff = true;
        //if (!TryGetComponent(out Player player))
        Debug.Log("TestIdleStatecs, Idle");
    }

    private void Flip()
    {
        Debug.Log("TestIdleStatecs, Flip");

        _isTurnedRight = !_isTurnedRight;
        transform.Rotate(0f, 180f, 0f);
    }
}