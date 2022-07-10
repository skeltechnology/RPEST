using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyDown inputs.
    /// </summary>
    public class KeyDownInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyDown(key);
        }
        #endregion
    }
}
