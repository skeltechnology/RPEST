using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    public class KeyDownInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyDown(key);
        }
        #endregion
    }
}
