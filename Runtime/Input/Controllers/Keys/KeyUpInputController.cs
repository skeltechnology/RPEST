using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyUp inputs.
    /// </summary>
    public class KeyUpInputController : KeyInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKeyUp(key);
        }
        #endregion
    }
}
