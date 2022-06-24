using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    public class KeyHoldInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKey(key);
        }
        #endregion
    }
}
