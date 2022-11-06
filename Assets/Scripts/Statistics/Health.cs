using UnityEngine;

using SinkingShips.Types;
using SinkingShips.Factions;
using SinkingShips.Helpers;

namespace SinkingShips.Statistics
{
    public abstract class Health : ConsumableStatistic
    {
        #region Cache & Constants
        private FactionBase _faction;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        protected virtual void Awake()
        {
            _faction = InitializationHelpers.GetComponentIfEmpty(_faction, gameObject, "_faction");
        }
        #endregion

        #region Public
        public bool Damage(float amount, Affiliation damagerAffiliation)
        {
            if(!_faction.IsAlliedFaction(damagerAffiliation) && !IsDepleted)
            {
                Reduce(amount);
                return true;
            }

            return false;
        }

        public bool Heal(float amount, Affiliation healerAffiliation)
        {
            if (_faction.IsAlliedFaction(healerAffiliation) && !IsFull)
            {
                Increase(amount);
                return true;
            }

            return false;
        }
        #endregion
    }
}
