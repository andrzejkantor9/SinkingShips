using UnityEngine;

using SinkingShips.Helpers;

namespace SinkingShips.Control
{
    public class GameManager : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnApplicationFocus(bool hasFocus)
        {
            UIHelpers.SetCursor(false, CursorLockMode.Locked);
        }
    }
}
