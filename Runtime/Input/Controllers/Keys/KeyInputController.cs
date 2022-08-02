using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Base class for managing KeyCode inputs.
    /// </summary>
    public abstract class KeyInputController : InputController<KeyCode> {
        #region Fields
        /// <summary>
        /// List of <c>KeyCode</c>s that will be polled.
        /// </summary>
        [SerializeField] protected List<KeyCode> listenedKeys;
        #endregion

        #region Getters
        /// <summary>
        /// Checks of the given key is active.
        /// </summary>
        /// <param name="key">Key that will be checked.</param>
        /// <returns>Boolean indicating if the key is active.</returns>
        protected abstract bool IsInputKeyActive(KeyCode key);

        /// <summary>
        /// Gets the collection of keys that will trigger listening callbacks.
        /// </summary>
        /// <returns>Collection of active keys.</returns>
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
        /// <summary>
        /// Adds the given keys to the collection.
        /// </summary>
        /// <param name="keys">List of keys that will be added to the collection.</param>
        public void SetKeys(KeyCode[] keys) {
            foreach (KeyCode key in keys)
                this.SetKey(key);
        }

        /// <summary>
        /// Adds the given key to the collection.
        /// </summary>
        /// <param name="key">Key that will be added to the collection.</param>
        public void SetKey(KeyCode key) {
            if (!this.listenedKeys.Contains(key))
                this.listenedKeys.Add(key);
        }

        /// <summary>
        /// Removes the given keys form the collection.
        /// </summary>
        /// <param name="keys">List of keys that will be removed from the collection.</param>
        public void RemoveKeys(KeyCode[] keys) {
            foreach (KeyCode key in keys)
                this.RemoveKey(key);
        }

        /// <summary>
        /// Removes the given key form the collection.
        /// </summary>
        /// <param name="key">Key that will be removed from the collection.</param>
        public void RemoveKey(KeyCode key) {
            this.listenedKeys.Remove(key);
        }
        #endregion
    }
}
