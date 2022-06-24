using SkelTech.RPEST.Input.Controllers.Keys;
using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys {
    [RequireComponent(typeof(InteractorObject))]
    public class KeyInteractionInputListener : InputListener {
        #region Fields
        protected InteractorObject interactorObject;
        [SerializeField] protected KeyDownInputController inputController;
        [SerializeField] private KeyCode interactionKey = KeyCode.Return;
        #endregion

        #region Unity
        private void Awake() {
            this.interactorObject = this.GetComponent<InteractorObject>();
        }
        #endregion

        #region Initialization
        protected override void SetListeners() {
            this.inputController.SetListener(this, this.interactionKey, this.interactorObject.Interact);
        }

        protected override void RemoveListeners() {
            this.inputController.RemoveListener(this);
        }
        #endregion
    }
}
