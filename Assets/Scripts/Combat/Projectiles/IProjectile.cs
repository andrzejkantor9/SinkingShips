using System;

using UnityEngine;

namespace SinkingShips.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void Inject(Action value, float minimumLifetime, float projectileSpeed);
    }
}
