using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input {
    public abstract class InputController<T> : MonoBehaviour {
        #region Fields
        private Dictionary<T, LinkedList<InputListenerData<T>>> listeners;
        #endregion

        #region Unity
        private void Awake() {
            this.listeners = new Dictionary<T, LinkedList<InputListenerData<T>>>();
        }

        private void Update() {
            ICollection<InputListenerData<T>> inputListeners;
            foreach (T input in this.GetInputEvents()) {
                if (this.listeners.ContainsKey(input)) {
                    inputListeners = this.listeners[input];
                    
                    foreach (InputListenerData<T> data in inputListeners) {
                        data.Callback.Invoke(input);
                    }
                }
            }
        }
        #endregion

        #region Getters
        protected abstract ICollection<T> GetInputEvents();
        #endregion

        #region Operators
        public void SetListener(object listener, T input, Action<T> callback) {
            LinkedList<InputListenerData<T>> inputListeners = SecureGetValue(this.listeners, input);

            InputListenerData<T> data = new InputListenerData<T>(listener, callback);
            inputListeners.AddLast(data);
        }
        // TODO: SET LIST OF INPUTS
        // TODO: RemoveCallback
        // TODO: RemoveListener
        #endregion

        #region Helpers
        private static B SecureGetValue<A, B>(Dictionary<A, B> dict, A key) where B : new() {
            B value;
            if (!dict.TryGetValue(key, out value)) {
                value = new B();
                dict.Add(key, value);
            }
            return value;
        }
        #endregion
    }
}
