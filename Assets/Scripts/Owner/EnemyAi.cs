    using UnityEngine;

    public class EnemyAi : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _idleDuration = 3f;
        [SerializeField] private float _patrolDuration = 10f;

        public float _timeElapsed = 0f;
        private StateMachine _stateMachine;
        private Animator _animator;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _animator = GetComponent<Animator>();

            var idleState = new IdleState(this, _animator, _idleDuration);
            var patrollingState = new PatrollingState(this, _animator, _patrolPoints, _patrolDuration);
    
            _stateMachine.AddTransition(idleState, patrollingState, ShouldStartPatrolling);
            _stateMachine.AddTransition(patrollingState, idleState, ShouldStopPatrolling);
    
            _stateMachine.SetState(idleState);
            // написать переходы между состояниями и протестировать
        }

        private void Update() => _stateMachine.Tick();

        public float GetSpeed() => _speed;

        public float CalculateElapsedTime() => _timeElapsed += Time.deltaTime;

        private bool ShouldStartPatrolling() => _timeElapsed >= _idleDuration; 

        private bool ShouldStopPatrolling() => _timeElapsed >= _patrolDuration;
    }