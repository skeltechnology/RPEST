using System;

namespace SkelTech.RPEST.Input.Controllers {
    /// <summary>
    /// Model responsible for storing information of a listener.
    /// </summary>
    public struct InputListenerData<T> {
        #region Properties
        /// <summary>
        /// Reference to the listener.
        /// </summary>
        public object Listener { get; }

        /// <summary>
        /// Callback of the listener.
        /// </summary>
        /// <value></value>
        public Action Callback { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="listener">Listener object.</param>
        /// <param name="callback">Callback of the listener.</param>
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
