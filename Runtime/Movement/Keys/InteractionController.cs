using SkelTech.RPEST.Input.Keys;
using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Movement.Keys {
    public class InteractionController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] protected KeyDownInputController inputController;
        [SerializeField] private KeyCode interactionKey = KeyCode.Return;
        #endregion

        #region Initialization
        protected override void SetListeners() {
            // TODO: REFACTOR
            this.inputController.SetListener(this, this.interactionKey, ((InteractorObject) this.walkableObject).Interact);
        }

        protected override void RemoveListeners() {
            this.inputController.RemoveListener(this);
        }
        #endregion
    }
}
