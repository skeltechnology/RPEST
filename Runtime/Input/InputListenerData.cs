using System;

namespace SkelTech.RPEST.Input {
    public struct InputListenerData<T> {
        #region Properties
        public object Listener { get; }
        public Action Callback { get; }
        #endregion

        #region Constructors
        public InputListenerData(object listener, Action callback) {
            this.Listener = listener;
            this.Callback = callback;
        }
        #endregion

        #region Operators
        public override bool Equals(object obj) {            
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }
            
            InputListenerData<T> other = (InputListenerData<T>) obj; 
            return this.Listener == other.Listener && this.Callback == other.Callback;
        }
        
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        #endregion
    }
}
