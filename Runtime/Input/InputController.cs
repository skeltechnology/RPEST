using System;
using System.Linq;
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
        protected abstract ICollection<T> GetInputEvents();
        #endregion

        #region Operators
        public void SetListener(object listener, T[] inputList, Action callback) {
            foreach (T input in inputList)
                this.SetListener(listener, input, callback);
        }

        public void SetListener(object listener, T input, Action callback) {
            LinkedList<InputListenerData<T>> inputListeners = SecureGetValue(this.listeners, input);

            InputListenerData<T> data = new InputListenerData<T>(listener, callback);
            inputListeners.AddLast(data);
        }

        public void RemoveListener(object listener) {
            this.RemoveListener(listener, (Action) null);
        }

        public void RemoveListener(object listener, T input) {
            this.RemoveListener(listener, input, null);
        }

        public void RemoveListener(object listener, Action callback) {
            foreach (LinkedList<InputListenerData<T>> inputListeners in this.listeners.Values.ToArray()) {
                RemoveAllOccurrences(inputListeners, listener, callback);
            }
        }

        public void RemoveListener(object listener, T input, Action callback) {
            if (this.listeners.ContainsKey(input))
                RemoveAllOccurrences(this.listeners[input], listener, callback);
        }
        #endregion

        #region Helpers
        private static B SecureGetValue<A, B>(Dictionary<A, B> dict, A key) where B : new() {
            B value;
            Debug.Log(dict);
            Debug.Log(key);
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
                sameListener = listener == null || node.Value.Listener == listener;
                sameCallback = callback == null || node.Value.Callback == callback;
                if (sameListener && sameCallback)
                    list.Remove(node);
                node = next;
            }
        }
        #endregion
    }
}
