using System;
using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Enum that represents the status of the coroutine.
    /// </summary>
    public enum RPESTCoroutineStatus { Created, Running, Paused, Finished, Canceled }

    /// <summary>
    /// Class that represents a coroutine.
    /// It expands the functionalities of Unity coroutines.
    /// </summary>
    public class RPESTCoroutine : CustomYieldInstruction, Pausable{
        #region Events
        /// <summary>
        /// Called when the coroutine status is set to <c>Finished</c>.
        /// </summary>
        public event EventHandler OnCoroutineFinished;
        #endregion

        #region Properties
        /// <summary>
        /// Status of the coroutine.
        /// </summary>
        public RPESTCoroutineStatus Status { get; private set; }

        /// <summary>
        /// Boolean that indicates if a <c>yield</c> statement should keep waiting for the coroutine.
        /// </summary>
        public override bool keepWaiting { get { return this.Status == RPESTCoroutineStatus.Running; }}
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the coroutine.
        /// </summary>
        private IEnumerator routine;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="routine">Coroutine that will be executed.</param>
        public RPESTCoroutine(IEnumerator routine) {
            if (routine == null) throw new Exception("Routine can not be null");

            this.Status = RPESTCoroutineStatus.Created;
            this.routine = routine;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Starts the execution of the coroutine.
        /// </summary>
        /// <param name="caller">Reference to the <c>MonoBehaviour</c> that will execute the coroutine.</param>
        /// <returns>Boolean indicating if the coroutine was started.</returns>
        public bool Start(MonoBehaviour caller) {
            if (caller == null) throw new Exception("Caller argument can not be null");
            if (this.Status != RPESTCoroutineStatus.Created) return false;

            this.Status = RPESTCoroutineStatus.Running;
            caller.StartCoroutine(this.CoroutineWrapper());
            return true;
        }

        /// <summary>
        /// Stops (cancels) the execution of the coroutine.
        /// After a coroutine is stopped, it can never be executed again.
        /// </summary>
        /// <returns>Boolean indicating if the coroutine was stopped.</returns>
        public bool Stop() {
            if (this.Status == RPESTCoroutineStatus.Finished || 
                this.Status == RPESTCoroutineStatus.Canceled) return false;
            
            this.Status = RPESTCoroutineStatus.Canceled;
            return true;
        }

        /// <summary>
        /// Pauses the execution of the coroutine.
        /// </summary>
        /// <returns>Boolean indicating if the coroutine was paused.</returns>
        public bool Pause() {
            if (this.Status != RPESTCoroutineStatus.Running) return false;

            this.Status = RPESTCoroutineStatus.Paused;
            return true;
        }

        /// <summary>
        /// Plays (unpauses) the execution of the coroutine.
        /// </summary>
        /// <returns>Boolean indicating if the coroutine was played (unpaused).</returns>
        public bool Play() {
            if (this.Status != RPESTCoroutineStatus.Paused) return false;

            this.Status = RPESTCoroutineStatus.Running;
            return true;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Wrapper that implements the execution logic of the <c>RPESTCoroutine</c>.
        /// </summary>
        private IEnumerator CoroutineWrapper() {
            while (true) {
                if (this.Status == RPESTCoroutineStatus.Paused) {
                    yield return null;
                } else if (this.Status == RPESTCoroutineStatus.Running) {
                    if (this.routine.MoveNext()) {
                        yield return this.routine.Current;
                    }
                    else {
                        this.Status = RPESTCoroutineStatus.Finished;
                        this.OnCoroutineFinished?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                } else {  // Cancelled
                    break;
                }
            }
        }
        #endregion
    }
}
