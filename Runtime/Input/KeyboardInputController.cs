using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input {
    public class KeyboardInputController : InputController<KeyCode> {
        #region Fields
        [SerializeField] private List<KeyCode> listenedKeys;
        #endregion

        #region Getters
        protected override ICollection<KeyCode> GetInputEvents() {
            LinkedList<KeyCode> result = new LinkedList<KeyCode>();

            foreach (KeyCode key in this.listenedKeys) {
                if (UnityEngine.Input.GetKeyDown(key)) {
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
