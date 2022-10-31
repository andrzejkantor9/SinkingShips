using System;

using UnityEngine;

namespace SinkingShips.Combat
{
    public interface ITwoSidedShooter
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        void Inject(Func<bool> onShootLeft, Func<bool> onShootRight);
        void ShootLeft();
        void ShootRight();
    }
}
