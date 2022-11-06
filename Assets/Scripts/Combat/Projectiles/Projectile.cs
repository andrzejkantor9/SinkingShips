using System;

using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        #region Data
        public class InjectConfig
        {
            public InjectConfig(Action releaseCallback, float damagePerHit, float minimumLifetime, 
                Affiliation affiliation)
            {
                ReleaseCallback = releaseCallback;

                _damagePerHit = damagePerHit;
                _minimumLifetime = minimumLifetime;
                _affiliation = affiliation;
            }

            public Action ReleaseCallback { get; }

            public readonly float _damagePerHit;
            public readonly float _minimumLifetime;
            public readonly Affiliation _affiliation;
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void Inject(InjectConfig config);
    }
}
