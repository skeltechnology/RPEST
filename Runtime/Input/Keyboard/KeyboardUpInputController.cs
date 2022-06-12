using UnityEngine;

namespace SkelTech.RPEST.Input.Keyboard {
    public class KeyboardUpInputController : KeyboardInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyUp(key);
        }
        #endregion
    }
}
