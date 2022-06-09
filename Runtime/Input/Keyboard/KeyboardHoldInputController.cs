using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input.Keyboard {
    public class KeyboardHoldInputController : KeyboardInputController {
        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return UnityEngine.Input.GetKey(key);
        }
        #endregion
    }
}
