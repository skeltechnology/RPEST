using UnityEngine;

namespace SkelTech.RPEST.Input.Keys {
    public class KeyUpInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyUp(key);
        }
        #endregion
    }
}
