using System;
using UnityEngine;

namespace SinkingShips.Helpers
{
    public static class EnumHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        public static string GetName<T>(T enumValue) where T : Enum
        {
            //not sure if this is needed
            UnityEngine.Assertions.Assert.IsTrue(typeof(T).IsEnum, $"{typeof(T)} is not an enum");

            return Enum.GetName(typeof(T), enumValue);
        }

        public static T[] EnumGetAllValues<T>() where T : Enum
        {
            T[] allEnumValues = (T[])Enum.GetValues(typeof(T));
            return allEnumValues;
        }

        #region Private & Protected
        #endregion
    }
}
