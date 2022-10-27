using System;
using UnityEngine;

namespace SinkingShips.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void SetPoolReleaseMethod(Action value);
    }
}
