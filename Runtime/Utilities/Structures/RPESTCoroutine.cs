using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION
    public enum RPESTCoroutineStatus { Created, Running, Paused, Finished, Canceled }

    public class RPESTCoroutine : Pausable{
        // TODO: EVENTS
        #region Properties
        public RPESTCoroutineStatus Status { get; private set; }
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
        public void Start(MonoBehaviour caller) {
            if (caller == null) throw new Exception("Caller argument can not be null");
            if (this.Status != RPESTCoroutineStatus.Created) throw new Exception("Coroutine was already started before");

            this.Status = RPESTCoroutineStatus.Running;
            caller.StartCoroutine(this.CoroutineWrapper());
        }

        public void Stop() {
            if (this.Status == RPESTCoroutineStatus.Created) throw new Exception("Coroutine was not started");
            if (this.Status == RPESTCoroutineStatus.Finished || 
                this.Status == RPESTCoroutineStatus.Canceled) throw new Exception("Coroutine has already terminated");
            
            this.Status = RPESTCoroutineStatus.Canceled;
        }

        public void Pause() {
            if (this.Status != RPESTCoroutineStatus.Running) throw new Exception("Coroutine needs to be running, in order to be paused");

            this.Status = RPESTCoroutineStatus.Paused;
        }

        public void Play() {
            if (this.Status != RPESTCoroutineStatus.Paused) throw new Exception("Coroutine needs to be paused, in order to be played");

            this.Status = RPESTCoroutineStatus.Running;
        }
        #endregion

        #region Helpers
        private IEnumerator CoroutineWrapper() {
            while (true) {
                if (this.Status == RPESTCoroutineStatus.Paused) {
                    yield return null;
                } else if (this.Status == RPESTCoroutineStatus.Running) {
                    if (this.routine.MoveNext()) yield return this.routine.Current;
                    else this.Status = RPESTCoroutineStatus.Finished;
                } else {  // Finished or cancelled
                    break;
                }
            }
            // TODO: NOTIFY EVENT
        }
        #endregion
    }
}
