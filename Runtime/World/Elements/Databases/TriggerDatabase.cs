using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class TriggerDatabase : WorldDatabase<Trigger> {
        #region Getters
        public Trigger GetTrigger(Vector3 globalPosition) {
            foreach (Trigger trigger in this.database) {
                if (trigger.GetWorldObject().Intersects(globalPosition))
                    return trigger;
            }
            return null;
        }
        #endregion
    }
}
