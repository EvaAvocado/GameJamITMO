using UnityEngine;

public class TestPatrollingStatecs : MonoBehaviour
{
    //private TestStateMachine _stateMachine;

    //private void Start()
    //{
    //    _idleStatecs = GetComponent<TestIdleStatecs>();
    //}

    //private void Update()
    //{
    //    if ()
    //    EnterBehavior();
    //}

    public void EnterBehavior()
    {
        Debug.Log("TestPatrollingStatecs, EnterBehavior");

        Debug.Log("Patrolling state activated");
    }
}
