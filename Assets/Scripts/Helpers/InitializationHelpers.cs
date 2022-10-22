using System;
using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Helpers
{
    public static class InitializationHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region GetComponents
        /// <summary>
        /// Gets component if not set in inspector and asserts not null or default.
        /// </summary>
        /// <param name="componentRoot">root of component</param>
        /// <param name="componentName">copy variable name as string here for detailed assert message</param>
        /// <returns>returns unchanged or get component value if null or default</returns>
        public static T GetComponentIfEmpty<T>(T component, GameObject componentRoot, string componentName)
        {
            if (!IsNullOrDefault(component))
                return component;

            component = componentRoot.GetComponent<T>();
            CustomLogger.AssertTrue(!IsNullOrDefault(component), $"{componentName} not found on gameObject",
                componentRoot);

            return component;
        }

        /// <summary>
        /// Gets components if not set in inspector and asserts list having more than 0 elements.
        /// </summary>
        /// <param name="componentsRoot">root of desired components</param>
        /// <param name="componentsName">copy variable name as string here for detailed assert message</param>
        /// <returns>returns unchanged or get component value if list contains 0 elements</returns>
        public static List<T> GetComponentsIfEmpty<T>(List<T> components, GameObject componentsRoot, 
            string componentsName)
        {
            if (!componentsRoot)
            {
                CustomLogger.LogError("componentsRoot is null", componentsRoot);
                return components;
            }
            else if (components.Count != 0)
            {
                return components;
            }

            T[] newComponents = componentsRoot.GetComponents<T>();
            foreach(T component in newComponents)
            {
                components.Add(component);
            }

            CustomLogger.AssertTrue(components.Count != 0, $"{componentsName} not found on {componentsRoot.name}",
                componentsRoot);

            return components;
        }
        #endregion

        #region Private & Protected
        private static bool IsNullOrDefault<T>(T component)
        {
            return component == null || component.Equals(default(T));
        }
        #endregion
    }
}
