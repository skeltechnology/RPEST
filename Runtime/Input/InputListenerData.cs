using System;

namespace SkelTech.RPEST.Input {
    public struct InputListenerData<T> {
        public object Listener { get; }
        public Action<T> Callback { get; }

        public InputListenerData(object listener, Action<T> callback) {
            this.Listener = listener;
            this.Callback = callback;
        }
    }
}
