using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input.Keys {
    public abstract class KeyInputController : InputController<KeyCode> {
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
        public void SetKeys(KeyCode[] keys) {
            foreach (KeyCode key in keys)
                this.SetKey(key);
        }

        public void SetKey(KeyCode key) {
            if (!this.listenedKeys.Contains(key))
                this.listenedKeys.Add(key);
        }

        public void RemoveKeys(KeyCode[] keys) {
            foreach (KeyCode key in keys)
                this.RemoveKey(key);
        }

        public void RemoveKey(KeyCode key) {
            this.listenedKeys.Remove(key);
        }
        #endregion
    }
}
