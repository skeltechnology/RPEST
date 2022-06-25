using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class InteractableDatabase : WorldDatabase<Interactable> {
        #region Getters
        public Interactable GetInteractable(Vector3 globalPosition) {
            foreach (Interactable interactable in this.database) {
                if (interactable.GetWorldObject().Intersects(globalPosition))
                    return interactable;
            }
            return null;
        }
        #endregion
    }
}
