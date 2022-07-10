using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyHold inputs.
    /// </summary>
    public class KeyHoldInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKey(key);
        }
        #endregion
    }
}
