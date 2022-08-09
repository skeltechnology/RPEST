using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyHold inputs.
    /// </summary>
    public class KeyHoldInputController : KeyInputController {
        #region Fields
        /// <summary>
        /// Boolean indicating if only the latest key is returned.
        /// </summary>
        [SerializeField] private bool latestKeyOnly = false;

        /// <summary>
        /// Linked list with the keys that are currently activated, maintaining its pressing order.
        /// </summary>
        private LinkedList<KeyCode> activeKeys = new LinkedList<KeyCode>();
        #endregion

        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return this.activeKeys.Contains(key);
        }

        protected override ICollection<KeyCode> GetInputEvents() {
            foreach (KeyCode key in this.listenedKeys) {
                if (UnityEngine.Input.GetKeyDown(key)) {  // Key pressed
                    this.activeKeys.AddFirst(key);
                } else if (UnityEngine.Input.GetKeyUp(key)) {  // Key released
                    this.activeKeys.Remove(key);
                }
            }

            if (this.latestKeyOnly && this.activeKeys.Count > 1) {
                LinkedList<KeyCode> result = new LinkedList<KeyCode>();
                result.AddLast(this.activeKeys.First.Value);
                return result;
            }
            return this.activeKeys;
        }
        #endregion
    }
}
