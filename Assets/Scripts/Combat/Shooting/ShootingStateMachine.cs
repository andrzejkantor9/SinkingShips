using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Debug;
using SinkingShips.Utils;

namespace SinkingShips.Combat.ShootingStates
{
    public class ShootingStateMachine : ShootingController
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform[] _particlesSpawnAndForward;

        private Ready _ready;
        private Shooting _shooting;
        private Reloading _reloading;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        #region Transitions & StateMachine
        private ShootingState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private static List<Transition> _emptyTransitions = new List<Transition>(0);

        private class Transition
        {
            public Func<bool> Condition { get; }
            public ShootingState FollowingState { get; }

            public Transition(ShootingState followingState, Func<bool> condition)
            {
                FollowingState = followingState;
                Condition = condition;
            }
        }

        private bool _hasShot;
        private bool _hasReloaded;
        private Func<bool> ShouldShoot()
        {
            return _callbacksConfig.ShouldShoot;
        }
        private Func<bool> HasShot()
        {
            return () => _hasShot;
        }
        private Func<bool> HasReloaded()
        {
            return () => _hasReloaded;
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_particlesSpawnAndForward.Length > 0, "_particlesSpawnAndForward are empty", 
                this);
        }

        private void Update()
        {
            Profiler.BeginSample("ShootingStateMachine Update()");

            Transition transition = GetTransition();
            if (transition != null)
                SwitchState(transition.FollowingState);

            _currentState.Update(Time.deltaTime);

            Profiler.EndSample();
        }
        #endregion

        #region Public
        public override void Inject(CallbacksConfig callbacksConfig, ShootingConfig shootingConfig)
        {
            base.Inject(callbacksConfig, shootingConfig);
            SetupStates();
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupStates()
        {
            _ready = new Ready();
            SetupShootingState();
            _reloading = new Reloading(_shootingConfig._timeBetweenAttacks, null, () => SetShot(false));

            AddTransition(_ready, _shooting, ShouldShoot());
            AddTransition(_shooting, _reloading, HasShot());
            AddTransition(_reloading, _ready, HasReloaded());

            SwitchState(_ready);
        }

        private void SetupShootingState()
        {
            var shootingCallbacks = new Shooting.CallbacksConfig(
                _callbacksConfig.GetProjectile, _callbacksConfig.OnReleaseObject);

            var shootingConfig = new Shooting.ShootingConfig(
                _shootingConfig._projectileSpeed,
                _particlesSpawnAndForward,
                _shootingConfig._projectileMinimumLifetime);

            _shooting = new Shooting(shootingCallbacks, shootingConfig, null, () => SetShot(true));
        }

        private void SwitchState(ShootingState state)
        {
            if (state == _currentState)
                return;

            //exit state
            if (_currentState != null)
            {
                _currentState.Exit();
                CustomLogger.Log($"{gameObject.name} exit state: {_currentState.GetType().Name}", this,
                    LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Basic);
            }
            _currentState = state;

            //set current transitions
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = _emptyTransitions;

            //enter state
            CustomLogger.Log($"{gameObject.name} enter state: {_currentState.GetType().Name}", this,
                    LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Basic);
            _currentState.Enter();
        }

        private void AddTransition(ShootingState previousState, ShootingState followingState, Func<bool> condition)
        {
            if (_transitions.TryGetValue(previousState.GetType(), out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[previousState.GetType()] = transitions;
            }

            transitions.Add(new Transition(followingState, condition));
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
            }

            return null;
        }

        private void SetShot(bool hasShot)
        {
            _hasShot = hasShot;
            _hasReloaded = !hasShot;
        }
        #endregion
    }
}
