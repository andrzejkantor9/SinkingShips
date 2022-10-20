using System;

using UnityEngine;

using SinkingShips.Debug;
using System.Collections.Generic;

namespace SinkingShips.Helpers
{
    public static class InitializationHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region GetComponents
        /// <summary>
        /// Gets component if not set in inspector and asserts not null or default
        /// </summary>
        /// <param name="componentOwner">caller "this"</param>
        /// <param name="componentName">copy variable name as string here for detailed assert message</param>
        /// <returns>returns unchanged or get component value if null or default</returns>
        public static T GetComponentIfEmpty<T>(T component, Component componentOwner, string componentName)
        {
            if (!IsNullOrDefault(component))
                return component;

            component = componentOwner.GetComponent<T>();
            CustomLogger.AssertTrue(!IsNullOrDefault(component), $"{componentName} not found on gameObject",
                componentOwner);

            return component;
        }
        #endregion

        #region Private
        private static bool IsNullOrDefault<T>(T component)
        {
            return component == null || component.Equals(default(T));
        }
        #endregion
    }
}
