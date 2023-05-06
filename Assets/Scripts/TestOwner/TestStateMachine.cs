//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestStateMachine : MonoBehaviour
//{
//    private Dictionary<Type, ISwitcherState> _allBehaviors;
//    private ISwitcherState _currentBehavior;

//    private void Awake()
//    {
//        _allBehaviors = new Dictionary<Type, ISwitcherState>
//        { 
//            [typeof(TestIdleStatecs)] = GetComponent<TestIdleStatecs>(),
//            [typeof(TestPatrollingStatecs)] = GetComponent<TestPatrollingStatecs>(),
//            //[typeof(CollectState)] = GetComponent<CollectState>(),
//        };

//        Debug.Log("TestStateMachine, Awake");
//    }

//    private void Start()
//    {
//        foreach (var behavior in _allBehaviors)
//        {
//            behavior.Value.Init(this);
//            behavior.Value.ExitBehavior();
//        }

//        _currentBehavior = _allBehaviors[typeof(TestIdleStatecs)];
//        EnterBehavior<TestIdleStatecs>();

//        Debug.Log("TestStateMachine, Start");
//    }

//    public void EnterBehavior<TState>() where TState : ISwitcherState
//    {
//        Debug.Log("TestStateMachine, EnterBehavior");

//        var behavior = _allBehaviors[typeof(TState)];
//        _currentBehavior.ExitBehavior();
//        behavior.EnterBehavior();
//        _currentBehavior = behavior;
//    }
//}