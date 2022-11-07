using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Helpers
{
    public static class UIHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        public static void SetCursor(bool visible, UnityEngine.CursorLockMode cursorLockMode)
        {
            CustomLogger.Log($"cursor set active: {visible.ToString()}", 
                LogCategory.Menus, LogFrequency.Rare, LogDetails.Basic);
            Cursor.lockState = cursorLockMode;
            Cursor.visible = visible;
#if UNITY_EDITOR
            // UnityEngine.Cursor.visible = visible;
            // UnityEngine.Cursor.lockState = cursorLockMode;
            // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#endif
        }
    }
}
