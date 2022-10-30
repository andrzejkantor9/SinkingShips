using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Debug;
using SinkingShips.Utils;

namespace SinkingShips.Combat.ShootingStates
{
    public class ShootingStateMachine : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]

        private CallbacksConfig _callbacksConfig;
        private ShootingConfig _shootingConfig;
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
        public class CallbacksConfig
        {
            public Func<Projectile> GetProjectile { get; private set; }
            public Action<Projectile> OnReleaseObject { get; private set; }
            public Func<bool> ShouldShoot { get; private set; }

            /// <summary>
            /// </summary>
            /// <param name="onreleaseObject">Action to be performed after projectile is no longer needed.</param>
            public CallbacksConfig(Func<Projectile> getProjectile, Action<Projectile> onreleaseObject, 
                Func<bool> shouldShoot)
            {
                GetProjectile = getProjectile;
                OnReleaseObject = onreleaseObject;
                ShouldShoot = shouldShoot;
            }
        }

        public class ShootingConfig
        {
            public readonly float _projectileMinimumLifetime;
            public readonly float _timeBetweenAttacks;
            public readonly float _impulseStrength;

            public ShootingConfig(float timeBetweenAttacks, float impulseStrength, float projectileMinimumLifetime)
            {
                _projectileMinimumLifetime = projectileMinimumLifetime;
                _timeBetweenAttacks = timeBetweenAttacks;
                _impulseStrength = impulseStrength;
            }
        }
        #endregion

        #region Transitions & StateMachine
        private ShootingState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private static List<Transition> EmptyTransitions = new List<Transition>(0);

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

        private Func<bool> _shouldShoot;
        private Func<bool> HasReloaded()
        {
            return () => _reloading.ReloadingTime > _shootingConfig._timeBetweenAttacks;
        }
        private Func<bool> HasShot()
        {
            return _callbacksConfig.ShouldShoot;
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
        public void Inject(CallbacksConfig callbacksConfig, ShootingConfig shootingConfig)
        {
            _callbacksConfig = callbacksConfig;
            _shootingConfig = shootingConfig;
            SetupStates();
        }
        //refactor
            //proper state transitions - inject, setup states, check each state contents
            //get component<rigidbody> in shooting and pool - ugly
            //call abstract shoot method?
            //decouple everything from shooting?
            //logs
            //___
            //simultaneous and rigidbody shooter separate?
            //abstract out state machine?
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupStates()
        {
            _ready = new Ready();
            _reloading = new Reloading(_shootingConfig._timeBetweenAttacks);
            SetupShootingState();

            AddTransition(_ready, _shooting, _shouldShoot);
            AddTransition(_shooting, _reloading, HasShot());
            AddTransition(_reloading, _ready, HasReloaded());

            SwitchState(_ready);
        }

        private void SetupShootingState()
        {
            var shootingCallbacks = new Shooting.CallbacksConfig(
                null,
                _callbacksConfig.GetProjectile,
                _callbacksConfig.OnReleaseObject);

            var shootingConfig = new Shooting.ShootingConfig(
                _shootingConfig._impulseStrength,
                _particlesSpawnAndForward,
                _shootingConfig._projectileMinimumLifetime);

            _shooting = new Shooting(shootingCallbacks, shootingConfig);
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
                _currentTransitions = EmptyTransitions;

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
        #endregion
    }
}
