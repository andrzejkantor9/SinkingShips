using System;

using UnityEngine;

namespace SinkingShips.Input
{
    public interface IInputProviderGameplay
    {
        public bool IsMoving { get; }
        public bool IsAttackingLeft { get; }
        public bool IsAttackingRight { get; }

        public Vector2 MovementValue { get; }
        ////////////////////////////////////////////////////////////////////////////////////////////////

        event Action onMove;
        event Action onAttackLeft;
        event Action onAttackRight;

        event Action<Vector2> onMoveChanged;
        event Action<bool> onAttackLeftChanged;
        event Action<bool> onAttackRightChanged;
    }
}
