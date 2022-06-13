using SkelTech.RPEST.Pathfinding;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Objects {
    public class WalkableObject : WorldObject {
        #region Properties
        public bool IsMoving { get; private set; }  // TODO: CAN BE QUEUE.SIZE > 0?
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
            this.AddDirection(Vector3Int.up);
        }

        public void MoveDown() {
            this.AddDirection(Vector3Int.down);
        }

        public void MoveLeft() {
            this.AddDirection(Vector3Int.left);
        }

        public void MoveRight() {
            this.AddDirection(Vector3Int.right);
        }

        private void AddDirection(Vector3Int direction) {
            if (!this.IsMoving) {
                this.directionsQueue.Enqueue(direction);
                StartCoroutine(this.MoveQueuedDirections());
            } else {
                if (this.directionsQueue.Count == 1 && this.cellDistance > this.world.GetGrid().cellSize.x * 0.8f) {
                    this.directionsQueue.Enqueue(direction);
                }
            }
        }

        public void MoveTo(Vector3 position) {
            if (!this.IsMoving) {
                Path path = this.walkable.FindShortestPath(this.transform.localPosition, position, 1000);
                if (path != null && path.GetPositions().Count > 2) {
                    foreach (Vector3Int direction in path.GetDirections()) {
                        this.directionsQueue.Enqueue(direction);
                    }
                    StartCoroutine(MoveQueuedDirections());
                }
            }
        }
        #endregion

        #region Coroutines
        private IEnumerator MoveQueuedDirections() {
            this.IsMoving = true;

            Vector3 finalPosition;
            while (this.directionsQueue.Count > 0) {
                finalPosition = this.transform.localPosition + this.directionsQueue.Dequeue();
                if (this.walkable.IsWalkable(finalPosition))
                    yield return StartCoroutine(MoveToCoroutine(finalPosition));
            }

            this.IsMoving = false;
        }

        // TODO: CAN BE STATIC AND IN UTILS
        private IEnumerator MoveToCoroutine(Vector3 finalPosition) {
            this.cellDistance = 0f;
            float delta;
            while ((finalPosition - this.transform.localPosition).sqrMagnitude > Mathf.Epsilon) {
                delta = Mathf.Abs(Time.deltaTime * this.Speed);
                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, finalPosition, delta);
                this.cellDistance += delta;
                yield return null;
            }
            this.transform.localPosition = finalPosition;
        }
        #endregion
    }
}
