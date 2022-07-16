using SkelTech.RPEST.Input.Controllers.Keys;
using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys {
    /// <summary>
    /// <c>MonoBehaviour</c> responsible for handling input event to make the <c>InteractorObject</c> interact.
    /// </summary>
    [RequireComponent(typeof(InteractorObject))]
    public class KeyInteractionInputListener : InputListener {
        #region Fields
        /// <summary>
        /// Reference to the interactor object.
        /// </summary>
        protected InteractorObject interactorObject;

        /// <summary>
        /// Reference to the input controller.
        /// </summary>
        [SerializeField] protected KeyDownInputController inputController;

        /// <summary>
        /// Key associated with the interaction.
        /// </summary>
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
