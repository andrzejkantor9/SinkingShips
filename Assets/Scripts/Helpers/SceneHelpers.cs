using UnityEngine;

namespace SinkingShips.Helpers
{
    public static class SceneHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Hierarchy
        public static string GetFullGameObjectPath(Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }
            return path;
        }
        #endregion

        #region Private & Protected
        #endregion
    }
}
