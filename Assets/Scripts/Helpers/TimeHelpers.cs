using System;
using System.Collections;

using UnityEngine;

namespace SinkingShips.Helpers
{
    public static class TimeHelpers
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Delays
        /// <summary>
        /// needs to be called from MonoBehaviour via StartCoroutine
        /// </summary>
        /// <param name="waitCondition">do callback after waitCondition is false</param>
        public static IEnumerator WaitUntilFalse(Func<bool> waitCondition, Action callback)
        {
            yield return new WaitWhile(waitCondition);
            callback?.Invoke();
        }

        /// <summary>
        /// needs to be called from MonoBehaviour via StartCoroutine
        /// </summary>
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
