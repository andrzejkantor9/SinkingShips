using System;
using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Combat.Projectiles;
using UnityEditorInternal;
using UnityEngine.PlayerLoop;

namespace SinkingShips.Combat.ShootingStates
{
    public class ShootingStateMachine : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform[] _particlesSpawnAndForward;

        private ShootingConfig _shootingConfig;
        private Action _hasShotCallback;

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

            public ProjectilesObjectPool ProjectilesObjectPool { get; private set; }

            public Projectile ProjectileScript { get; private set; }
            public float TimeBetweenAttacks { get; private set; }
            public float ImpulseStrength { get; private set; }
            public bool GravityEnabled { get; private set; }

            public ShootingConfig(ProjectilesObjectPool projectilesObjectPool, Projectile projectileScript,
                float timeBetweenAttacks, float impulseStrength, bool gravityEnabled)
            {
                ProjectilesObjectPool = projectilesObjectPool;

                ProjectileScript = projectileScript;
                TimeBetweenAttacks = timeBetweenAttacks;
                ImpulseStrength = impulseStrength;
                GravityEnabled = gravityEnabled;
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
            Transition transition = GetTransition();
            if (transition != null)
                SwitchState(transition.FollowingState);

            _currentState.Update(Time.deltaTime);
        }
        #endregion

        #region Public
        public void Inject(ShootingConfig shootingConfig, Func<bool> wasShotPerformed, Action hasShotCallback)
        {
            _shootingConfig = shootingConfig;
            _wasShotPerformed = wasShotPerformed;
            _hasShotCallback = hasShotCallback;

            SetupStates();
        }
        //shooting
        //    pass data from config
        //        gravity, impulse, concrete object from object pool
        //        above as struct / class?
        //    enable projectile in object pool
        //    pass projectile prefab from object pool to enable
        //    initialize object pool before switching states?
        //    disable object in object pool when it is out of screen
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupStates()
        {
            _ready = new Ready();
            _shooting = new Shooting(_hasShotCallbackInternal);
            _reloading = new Reloading(this, _shootingConfig.TimeBetweenAttacks);

            AddTransition(_ready, _shooting, _wasShotPerformed);
            AddTransition(_shooting, _reloading, HasShot());
            AddTransition(_reloading, _ready, HasReloaded());

            _hasShotCallbackInternal += () => { _hasShot = true; _hasShotCallback?.Invoke(); };

            SwitchState(_ready);
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
            _hasShot = false;
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
