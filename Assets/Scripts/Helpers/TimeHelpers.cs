using System;
using System.Collections;

using UnityEngine;

namespace SinkingShips.Helpers
{
    public static class TimeHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Delays
        public static IEnumerator DisableAfterCondition(Func<bool> waitCondition, Action callback)
        {
            yield return new WaitWhile(waitCondition);
            callback?.Invoke();
        }

        public static IEnumerator DoAfterSeconds(float seconds, Action callback, bool scaledTime = true)
        {
            if (scaledTime)
            {
                yield return new WaitForSeconds(seconds);
            }
            else
            {
                yield return new WaitForSecondsRealtime(seconds);
            }

            callback?.Invoke();
        }
        #endregion
    }
}
