using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;
using SinkingShips.Effects;
using System;
using UnityEditor.Build.Content;

namespace SinkingShips.Control
{
    public class BasicDeathHandler : DeathHandlerBase
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private GameObject _movementEffectsRoot;

        private List<EffectPlayer> _deathEffects = new List<EffectPlayer>();
        #endregion

        #region States
        private Coroutine _dyingCoroutine;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        protected void Awake()
        {
            CustomLogger.AssertNotNull(_movementEffectsRoot, "_movementEffectsRoot", this);

            _deathEffects = InitializationHelpers.GetComponentsIfEmpty
                (_deathEffects, _movementEffectsRoot, "_movementEffectsPlayers");
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public override void Die()
        {
            if (_dyingCoroutine != null)
                return;

            PlayEffects();
            _dyingCoroutine = StartCoroutine(TimeHelpers.WaitUntilFalse(() => IsAnyEffectPlaying(), FinalizeDeath));

            CustomLogger.Log($"Start dying:{gameObject.name}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }
        #endregion

        #region Events & Statics
        private bool IsAnyEffectPlaying()
        {
            foreach (EffectPlayer effect in _deathEffects)
            {
                if (effect.IsPlaying())
                    return true;
            }
            return false;
        }
        #endregion

        #region Private
        private void PlayEffects()
        {
            foreach(EffectPlayer effect in _deathEffects)
            {
                effect.Play();
            }
        }

        private void FinalizeDeath()
        {
            _dyingCoroutine = null;
            CustomLogger.Log($"Finalize death on:{gameObject.name}", this, 
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);

            Destroy(gameObject);
        }
        #endregion
    }
}
