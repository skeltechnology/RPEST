using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input.Keyboard {
    public abstract class KeyboardInputController : InputController<KeyCode> {
        #region Fields
        [SerializeField] protected List<KeyCode> listenedKeys;
        #endregion

        #region Getters
        protected abstract bool IsInputKeyActive(KeyCode key);

        protected override ICollection<KeyCode> GetInputEvents() {
            LinkedList<KeyCode> result = new LinkedList<KeyCode>();

            foreach (KeyCode key in this.listenedKeys) {
                if (this.IsInputKeyActive(key)) {
                    result.AddLast(key);
                }
            }

            return result;
        }
        #endregion

        #region Setters
        public void SetKey(KeyCode key) {
            this.listenedKeys.Add(key);
        }

        // TODO: ADD LIST OF KEYS
        // TODO: REMOVE
        // TODO: REMOVE LIST OF KEYS
        #endregion
    }
}
