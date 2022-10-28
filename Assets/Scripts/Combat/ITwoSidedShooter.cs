using System;
using UnityEngine;

namespace SinkingShips.Combat
{
    public interface ITwoSidedShooter
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        void Inject(Func<bool> leftShootingCallback, Func<bool> rihgtShootingCallback);
        void ShootLeft();
        void ShootRight();
    }
}
