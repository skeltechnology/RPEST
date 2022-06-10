using UnityEngine;

namespace SkelTech.RPEST.Input.Keyboard {
    public class KeyboardDownInputController : KeyboardInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyDown(key);
        }
        #endregion
    }
}
