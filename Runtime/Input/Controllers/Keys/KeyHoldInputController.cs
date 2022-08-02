using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyHold inputs.
    /// </summary>
    public class KeyHoldInputController : KeyInputController {
        // TODO: create dict<KeyCode, bool> to check if key is currently active
        // and linked list with orderer current keys. Have serialize field to 
        // indicate if should prioritize latest or latest keys.
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKey(key);
        }
        #endregion
    }
}
