using UnityEngine;

namespace SinkingShips.UI
{
    public abstract class HealthDisplay : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        protected abstract void UpdateDisplay(float healthPercentage);
    }
}
