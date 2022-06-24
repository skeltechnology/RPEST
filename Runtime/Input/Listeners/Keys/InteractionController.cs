using SkelTech.RPEST.Input.Controllers.Keys;
using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys {
    public class InteractionController : WalkableObjectInputListener {
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
