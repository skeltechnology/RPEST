using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers {
    /// <summary>
    /// Base class for managing inputs and handling listeners.
    /// </summary>
    /// <typeparam name="T">Input type</typeparam>
    public abstract class InputController<T> : MonoBehaviour {
        #region Fields
        /// <summary>
        /// Collection of listeners.
        /// </summary>
        private Dictionary<T, LinkedList<InputListenerData<T>>> listeners = new Dictionary<T, LinkedList<InputListenerData<T>>>();
        #endregion

        #region Unity
        private void Update() {
            InputListenerData<T>[] inputListeners;
            foreach (T input in this.GetInputEvents()) {
                if (this.listeners.ContainsKey(input)) {
                    inputListeners = this.listeners[input].ToArray();
                    foreach (InputListenerData<T> data in inputListeners) {
                        data.Callback.Invoke();
                    }
                }
            }
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the collection of active input events of the controller.
        /// </summary>
        /// <returns>Collection of active input events of the controller.</returns>
        protected abstract ICollection<T> GetInputEvents();
        #endregion

        #region Operators
        /// <summary>
        /// Adds the given listener with the respective parameters.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="inputList">Array of inputs that trigger the callback.</param>
        /// <param name="callback">Listener callback.</param>
        public void SetListener(object listener, T[] inputList, Action callback) {
            foreach (T input in inputList)
                this.SetListener(listener, input, callback);
        }

        /// <summary>
        /// Adds the given listener with the respective parameters.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="input">Input that triggers the callback.</param>
        /// <param name="callback">Listener callback.</param>
        public void SetListener(object listener, T input, Action callback) {
            LinkedList<InputListenerData<T>> inputListeners = SecureGetValue(this.listeners, input);

            InputListenerData<T> data = new InputListenerData<T>(listener, callback);
            inputListeners.AddLast(data);
        }

        /// <summary>
        /// Removes the given listener.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        public void RemoveListener(object listener) {
            this.RemoveListener(listener, (Action) null);
        }

        /// <summary>
        /// Removes the callback of the given listener that is triggered by the given input.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="input">Input that triggers the callback.</param>
        public void RemoveListener(object listener, T input) {
            this.RemoveListener(listener, input, null);
        }

        /// <summary>
        /// Removes the given callback of the given listener.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="callback">Callback that will be removed.</param>
        public void RemoveListener(object listener, Action callback) {
            foreach (LinkedList<InputListenerData<T>> inputListeners in this.listeners.Values.ToArray()) {
                RemoveAllOccurrences(inputListeners, listener, callback);
            }
        }

        /// <summary>
        /// Removes the given callback of the given listener that is triggered by the given input.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="input">Input that triggers the callback.</param>
        /// <param name="callback">Callback that will be removed.</param>
        public void RemoveListener(object listener, T input, Action callback) {
            if (this.listeners.ContainsKey(input))
                RemoveAllOccurrences(this.listeners[input], listener, callback);
        }
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

        private static void RemoveAllOccurrences(LinkedList<InputListenerData<T>> list, object listener, Action callback) {
            LinkedListNode<InputListenerData<T>> node = list.First, next;
            bool sameListener, sameCallback;
            while (node != null) {
                next = node.Next;
                sameListener = (listener == null || node.Value.Listener == listener);
                sameCallback = (callback == null || node.Value.Callback == callback);
                if (sameListener && sameCallback)
                    list.Remove(node);
                node = next;
            }
        }
        #endregion
    }
}
