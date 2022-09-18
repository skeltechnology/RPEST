using System;
using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION
    public enum RPESTCoroutineStatus { Created, Running, Paused, Finished, Canceled }

    public class RPESTCoroutine : CustomYieldInstruction{
        #region Events
        public event EventHandler OnCoroutineFinished;
        #endregion

        #region Properties
        public RPESTCoroutineStatus Status { get; private set; }

        public override bool keepWaiting { get { return this.Status == RPESTCoroutineStatus.Running; }}
        #endregion

        #region Fields
        private IEnumerator routine;
        #endregion

        #region Constructors
        public RPESTCoroutine(IEnumerator routine) {
            if (routine == null) throw new Exception("Routine can not be null");

            this.Status = RPESTCoroutineStatus.Created;
            this.routine = routine;
        }
        #endregion

        #region Operators
        public bool Start(MonoBehaviour caller) {
            if (caller == null) throw new Exception("Caller argument can not be null");
            if (this.Status != RPESTCoroutineStatus.Created) return false;

            this.Status = RPESTCoroutineStatus.Running;
            caller.StartCoroutine(this.CoroutineWrapper());
            return true;
        }

        public bool Stop() {
            if (this.Status == RPESTCoroutineStatus.Finished || 
                this.Status == RPESTCoroutineStatus.Canceled) return false;
            
            this.Status = RPESTCoroutineStatus.Canceled;
            return true;
        }

        public bool Pause() {
            if (this.Status != RPESTCoroutineStatus.Running) return false;

            this.Status = RPESTCoroutineStatus.Paused;
            return true;
        }

        public bool Play() {
            if (this.Status != RPESTCoroutineStatus.Paused) return false;

            this.Status = RPESTCoroutineStatus.Running;
            return true;
        }
        #endregion

        #region Helpers
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
