using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Debug;
using SinkingShips.Utils;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat.ShootingStates
{
    public class ShootingStateMachine : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]

        private ShootingConfig _shootingConfig;
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform[] _particlesSpawnAndForward;

        private Action _hasShotCallbackInternal;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        private Ready _ready;
        private Shooting _shooting;
        private Reloading _reloading;
        #endregion

        #region States
        private IShootingState _currentState;
        private bool _hasShot;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        private class Transition
        {
            public Func<bool> Condition { get; }
            public IShootingState FollowingState { get; }

            public Transition(IShootingState followingState, Func<bool> condition)
            {
                FollowingState = followingState;
                Condition = condition;
            }
        }

        public struct ShootingConfig
        {

            public ObjectPoolBase<Projectile> ProjectilesObjectPool { get; private set; }

            public Projectile ProjectileScript { get; private set; }
            public float TimeBetweenAttacks { get; private set; }
            public float ImpulseStrength { get; private set; }
            public bool GravityEnabled { get; private set; }
            public float ProjectileMinimumLifetime { get; private set; }

            public ShootingConfig(ObjectPoolBase<Projectile> projectilesObjectPool, Projectile projectileScript,
                float timeBetweenAttacks, float impulseStrength, bool gravityEnabled, float projectileMinimumLifetime)
            {
                ProjectilesObjectPool = projectilesObjectPool;

                ProjectileScript = projectileScript;
                TimeBetweenAttacks = timeBetweenAttacks;
                ImpulseStrength = impulseStrength;
                GravityEnabled = gravityEnabled;
                ProjectileMinimumLifetime = projectileMinimumLifetime;
            }
        }
        #endregion

        #region Transitions
        private Func<bool> _wasShotPerformed;
        private Func<bool> HasReloaded()
        {
            return () => _reloading.ReloadingTime > _shootingConfig.TimeBetweenAttacks;
        }
        private Func<bool> HasShot()
        {
            return () => _hasShot;
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
        public void Inject(ShootingConfig shootingConfig, Func<bool> wasShotPerformed)
        {
            _shootingConfig = shootingConfig;
            _wasShotPerformed = wasShotPerformed;

            SetupStates();
        }
        //refactor
            //proper state transitions
            //call abstract shoot method?
            //decouple everything from shooting?
            //introduce structs that make sense
            //get component<rigidbody> in shooting and pool - ugly
            //logs
            //___
            //simultaneous and rigidbody shooter separate?
            //ITwoSidedShooter interface depends on inject with callbacks
            //abstract out state machine?
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupStates()
        {
            _hasShotCallbackInternal += () => _hasShot = true; 

            _ready = new Ready();
            _reloading = new Reloading(_shootingConfig.TimeBetweenAttacks, () => { });
            SetShootingState();

            AddTransition(_ready, _shooting, _wasShotPerformed);
            AddTransition(_shooting, _reloading, HasShot());
            AddTransition(_reloading, _ready, HasReloaded());

            SwitchState(_ready);
        }

        private void SetShootingState()
        {
            var shootingCallbacks = new Shooting.CallbacksConfig(
                            _hasShotCallbackInternal,
                            () => _hasShot = false,
                            _shootingConfig.ProjectilesObjectPool.GetObject,
                            _shootingConfig.ProjectilesObjectPool.ReleaseObject);

            var shootingConfig = new Shooting.ShootingConfig(
                _shootingConfig.ImpulseStrength,
                _particlesSpawnAndForward,
                _shootingConfig.ProjectileMinimumLifetime);

            _shooting = new Shooting(shootingCallbacks, shootingConfig);
        }

        private void SwitchState(IShootingState state)
        {
            if (state == _currentState)
                return;

            if (_currentState != null)
            {
                _currentState.Exit();
                CustomLogger.Log($"{gameObject.name} exit state: {_currentState.GetType().Name}", this,
                    LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Basic);
            }
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CustomLogger.Log($"{gameObject.name} enter state: {_currentState.GetType().Name}", this,
                    LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Basic);
            _currentState.Enter();
        }

        private void AddTransition(IShootingState previousState, IShootingState followingState, Func<bool> condition)
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
