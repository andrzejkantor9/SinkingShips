using System;
using UnityEngine;

namespace SinkingShips.Helpers
{
    public static class EnumHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        public static string GetName<T1>(T1 enumValue) where T1 : Enum
        {
            //not sure if this is needed
            UnityEngine.Assertions.Assert.IsTrue(typeof(T1).IsEnum, $"{typeof(T1)} is not an enum");

            return Enum.GetName(typeof(T1), enumValue);
        }

        public static T1[] EnumGetAllValues<T1>() where T1 : Enum
        {
            T1[] allEnumValues = (T1[])Enum.GetValues(typeof(T1));
            return allEnumValues;
        }
    }
}
