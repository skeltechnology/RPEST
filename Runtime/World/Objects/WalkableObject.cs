using SkelTech.RPEST.Pathfinding;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Objects {
    public class WalkableObject : WorldObject {
        #region Properties
        public bool IsMoving { get; private set; }
        public bool IsRunning { get { return this.isRunning; } set { this.isRunning = this.canRun && value; } }

        public float WalkingSpeed { get { return this.walkingSpeed; } set { this.walkingSpeed = value; } }
        public float RunningSpeed { get { return this.runningSpeed; } set { this.runningSpeed = value; } }
        public float Speed { get {
            if (this.IsMoving) {
                return this.IsRunning ? this.RunningSpeed : this.WalkingSpeed;
            }
            return 0f;
        } }
        #endregion

        #region Fields
        [SerializeField] private WalkableTilemap walkable;
        [SerializeField] private float walkingSpeed = 4f;
        [SerializeField] private bool canRun = true;
        [SerializeField] private float runningSpeed = 6.5f;

        private float cellDistance;
        private Queue<Vector3Int> directionsQueue;
        private bool isRunning = false;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();

            this.directionsQueue = new Queue<Vector3Int>();
            this.IsMoving = false;
        }
        #endregion

        #region Operators
        public void MoveUp() {
            this.Move(Vector3Int.up);
        }

        public void MoveDown() {
            this.Move(Vector3Int.down);
        }

        public void MoveLeft() {
            this.Move(Vector3Int.left);
        }

        public void MoveRight() {
            this.Move(Vector3Int.right);
        }

        private void Move(Vector3Int direction) {
            if (!this.IsMoving) {
                if (this.walkable.IsWalkable(this.transform.localPosition + direction)) {  // Small optimization
                    this.directionsQueue.Enqueue(direction);
                    StartCoroutine(this.MoveQueuedDirections());
                }
            } else {
                if (this.directionsQueue.Count < 1 && this.cellDistance > this.world.GetGrid().cellSize.x * 0.85f) {
                    this.directionsQueue.Enqueue(direction);
                }
            }
        }

        public void MoveTo(Vector3 position) {
            if (!this.IsMoving) {
                Path path = this.walkable.FindShortestPath(this.transform.localPosition, position, 1000);
                if (path != null && path.GetPositions().Count > 1) {
                    foreach (Vector3Int direction in path.GetDirections()) {
                        this.directionsQueue.Enqueue(direction);
                    }
                    StartCoroutine(MoveQueuedDirections());
                }
            }
        }

        public void StopMoving() {
            if (this.IsMoving)
                this.directionsQueue.Clear();
        }
        #endregion

        #region Coroutines
        private IEnumerator MoveQueuedDirections() {
            this.IsMoving = true;

            Vector3 finalPosition;
            float missingDelta = 0f;
            while (this.directionsQueue.Count > 0) {
                finalPosition = this.transform.localPosition + this.directionsQueue.Dequeue();
                if (this.walkable.IsWalkable(finalPosition)) {
                    this.cellDistance = missingDelta;
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, finalPosition, missingDelta);
                    missingDelta = 0f;

                    float delta = 0;
                    Vector3 currentPosition = finalPosition;
                    while ((finalPosition - this.transform.localPosition).sqrMagnitude > Mathf.Epsilon) {
                        delta = Mathf.Abs(Time.deltaTime * this.Speed);
                        currentPosition = this.transform.localPosition;
                        this.transform.localPosition = Vector3.MoveTowards(currentPosition, finalPosition, delta);
                        this.cellDistance += delta;
                        yield return null;
                    }
                    this.transform.localPosition = finalPosition;
                    missingDelta = delta - (this.transform.localPosition - currentPosition).magnitude;
                } else {
                    this.directionsQueue.Clear();
                }
            }

            this.IsMoving = false;
        }
        #endregion
    }
}
