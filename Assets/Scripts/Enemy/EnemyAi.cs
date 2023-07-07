    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyAi : MonoBehaviour
    {
        [SerializeField] private Floor[] _floors;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _idleDuration = 3f;
        [SerializeField] private float _patrolDuration = 10f;
        
        private readonly StateMachine _stateMachine = new StateMachine();
        private float _timeElapsed = 0f;
        private Animator _animator;
        private FloorController _floorController;

        public float Speed => _speed;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _floorController = GetComponent<FloorController>();

            var idleState = new IdleState(this, _animator);
            var patrollingState = new PatrollingState(this, _animator, _floorController, _floors);
    
            _stateMachine.AddTransition(idleState, patrollingState, () => _timeElapsed >= _idleDuration);
            _stateMachine.AddTransition(patrollingState, idleState, () => _timeElapsed >= _patrolDuration);
    
            _stateMachine.SetState(patrollingState);
        }

        private void Update() => _stateMachine.Tick();

        public float GetSpeed() => _speed;

        public void CalculateElapsedTime() => _timeElapsed += Time.deltaTime;

        public float ResetTimeElapsed() => _timeElapsed = 0;
        
        public float SetSpeed(float newSpeed) => _speed = newSpeed;

        //private bool ShouldStartPatrolling() => _timeElapsed >= _idleDuration; 

        //private bool ShouldStopPatrolling() => _timeElapsed >= _patrolDuration;
    }