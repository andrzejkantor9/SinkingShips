using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Statistics
{
    public abstract class Health : ConsumableStatistic
    {
        protected Affiliation _affiliation;

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        public bool Damage(float amount, Affiliation hitAffiliation)
        {
            if(_affiliation != hitAffiliation && !IsDepleted)
            {
                Reduce(amount);
                return true;
            }

            return false;
        }

        public bool Heal(float amount, Affiliation unitAffiliation)
        {
            if (_affiliation == unitAffiliation && !IsFull)
            {
                Increase(amount);
                return true;
            }

            return false;
        }
        #endregion
    }
}
