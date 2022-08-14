using SkelTech.RPEST.World.Elements.Objects;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    /// <summary>
    /// Class for managing <c>Trigger</c>s.
    /// </summary>
    public class TriggerDatabase : WorldDatabase<Trigger> {
        #region Getters
        /// <summary>
        /// Gets the trigger (if any) at the correspondent position.
        /// If there are two or more triggers at that position, only the first one is returned.
        /// </summary>
        /// <param name="globalPosition">Position of the trigger (global coordinates).</param>
        /// <returns>Trigger at the correspondet position. <c>null</c> if there isn't one.</returns>
        public Trigger GetTrigger(Vector3 globalPosition) {
            return GetFirst(this.database, (trigger) => {
                return trigger.GetWorldObject().Intersects(globalPosition);
            });
        }

        // TODO: DOCUMENTATION
        public ICollection<Trigger> GetTriggers(Vector3 globalPosition) {
            return GetAll(this.database, (trigger) => {
                return trigger.GetWorldObject().Intersects(globalPosition);
            });
        }
        #endregion
    }
}
