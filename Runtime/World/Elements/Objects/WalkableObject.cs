using SkelTech.RPEST.Pathfinding;
using SkelTech.RPEST.Utilities.Structures;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a walkable object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    public class WalkableObject : ColliderObject {
        #region Events
        /// <summary>
        /// Called when the object starts moving to other cell.
        /// </summary>
        public event EventHandler OnStartedMovement;

        /// <summary>
        /// Called when the object finishes moving to other cell.
        /// </summary>
        public event EventHandler OnFinishedMovement;

        /// <summary>
        /// Called every frame while the object is moving, passing as parameter the percentage of walking.
        /// </summary>
        public event EventHandler<float> OnUpdateMovement;

        /// <summary>
        /// Called when the object changes its direction.
        /// </summary>
        public event EventHandler<Direction> OnUpdateDirection;
        #endregion

        #region Properties
        /// <summary>
        /// Indicates if the object is currently moving.
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Indicates if the object is currently running.
        /// </summary>
        public bool IsRunning { get { return this.isRunning; } set { this.isRunning = this.canRun && value; } }

        /// <summary>
        /// Speed at which the object moves when it is walking.
        /// </summary>
        public float WalkingSpeed { get { return this.walkingSpeed; } set { this.walkingSpeed = value; } }

        /// <summary>
        /// Speed at which the object moves when it is running.
        /// </summary>
        /// <value></value>
        public float RunningSpeed { get { return this.runningSpeed; } set { this.runningSpeed = value; } }

        /// <summary>
        /// Current speed of the object.
        /// </summary>
        public float Speed { get {
            if (this.IsMoving) {
                return this.IsRunning ? this.RunningSpeed : this.WalkingSpeed;
            }
            return 0f;
        } }
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the <c>WalkableTilemap</c> the object is moving in.
        /// </summary>
        [SerializeField] private WalkableTilemap walkable;

        /// <summary>
        /// Indicates if the object can move.
        /// </summary>
        [SerializeField] private bool canMove = true;

        /// <summary>
        /// Speed at which the object moves when it is walking.
        /// </summary>
        [SerializeField] private float walkingSpeed = 4f;

        /// <summary>
        /// Indicates if the object can run.
        /// </summary>
        [SerializeField] private bool canRun = true;

        /// <summary>
        /// Speed at which the object moves when it is running.
        /// </summary>
        [SerializeField] private float runningSpeed = 6.5f;

        /// <summary>
        /// Distance traveled during the current movement.
        /// It is reseted each time the object moves to a different cell.
        /// </summary>
        private float cellDistance;

        /// <summary>
        /// Current direction of the object.
        /// </summary>
        [SerializeField] protected Direction direction = Direction.Down;

        /// <summary>
        /// Queue of directions that the object will travel.
        /// </summary>
        private Queue<Direction> directionsQueue;

        /// <summary>
        /// Indicates if the object is currently running.
        /// </summary>
        private bool isRunning = false;
        #endregion

        #region Unity
        protected virtual void Awake() {
            this.directionsQueue = new Queue<Direction>();
            this.IsMoving = false;
        }

        protected virtual void Start() {
            this.OnUpdateDirection?.Invoke(this, this.direction);
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the reference to the <c>WalkableTilemap</c> the object is moving in.
        /// </summary>
        /// <returns>Reference to the <c>WalkableTilemap</c> the object is moving in.</returns>
        public WalkableTilemap GetWalkableTilemap() {
            return this.walkable;
        }

        /// <summary>
        /// Gets the current direction of the object.
        /// </summary>
        /// <returns>Current direction of the object.</returns>
        public Direction GetDirection() {
            return this.direction;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Changes if the object can move.
        /// </summary>
        /// <param name="canMove">Boolean indicating if the object can move.</param>
        public void SetCanMove(bool canMove) {
            this.canMove = canMove;
        }

        /// <summary>
        /// Changes if the object can run.
        /// </summary>
        /// <param name="canRun">Boolean indicating if the object can run.</param>
        public void SetCanRun(bool canRun) {
            this.canRun = canRun;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Moves the object to the cell above.
        /// </summary>
        public void MoveUp() {
            this.Move(Direction.Up);
        }

        /// <summary>
        /// Moves the object to left cell.
        /// </summary>
        public void MoveLeft() {
            this.Move(Direction.Left);
        }

        /// <summary>
        /// Moves the object to the cell bellow.
        /// </summary>
        public void MoveDown() {
            this.Move(Direction.Down);
        }

        /// <summary>
        /// Moves the object to the right cell.
        /// </summary>
        public void MoveRight() {
            this.Move(Direction.Right);
        }

        /// <summary>
        /// Moves the object in the given direction.
        /// </summary>
        /// <param name="direction">Direction in which the object will be moved one.</param>
        private void Move(Direction direction) {
            if (this.canMove) {
                if (!this.IsMoving) {
                    if (this.CanMoveTo(this.transform.localPosition + direction.ToVector3Int())) {  // Small optimization
                        this.directionsQueue.Enqueue(direction);
                        StartCoroutine(this.MoveQueuedDirections());
                    } else {
                        this.UpdateDirection(direction);
                    }
                } else {
                    if (this.directionsQueue.Count < 1 && this.cellDistance > this.world.GetGrid().cellSize.x * 0.85f) {
                        this.directionsQueue.Enqueue(direction);
                    }
                }
            }
        }

        /// <summary>
        /// Moves the object to the given position.
        /// </summary>
        /// <param name="position">Destination position.</param>
        public void MoveTo(Vector3 position) {
            this.MoveTo(Vector3Int.FloorToInt(position));
        }

        /// <summary>
        /// Moves the object to the given position.
        /// </summary>
        /// <param name="position">Destination position.</param>
        public void MoveTo(Vector3Int position) {
            if (this.canMove && !this.IsMoving) {
                Path path = this.walkable.FindShortestPath(
                    Vector3Int.FloorToInt(this.transform.localPosition), 
                    position,
                    this.HasCollision()
                );
                if (path != null && path.GetPositions().Count > 1) {
                    foreach (Direction direction in path.GetDirections()) {
                        this.directionsQueue.Enqueue(direction);
                    }
                    StartCoroutine(MoveQueuedDirections());
                }
            }
        }

        /// <summary>
        /// Deletes any following movement instructions.
        /// If the object is currently moving, it will not stop until it finalizes the current movement.
        /// </summary>
        public void StopMoving() {
            if (this.IsMoving)
                this.directionsQueue.Clear();
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Helper coroutine to moce the object until the queue is empty.
        /// </summary>
        private IEnumerator MoveQueuedDirections() {
            this.IsMoving = true;

            Vector3 finalPosition;
            float missingDelta = 0f;
            while (this.directionsQueue.Count > 0) {
                this.UpdateDirection(this.directionsQueue.Dequeue());
                finalPosition = this.transform.localPosition + this.direction.ToVector3Int();
                if (this.CanMoveTo(finalPosition)) {
                    this.OnStartedMovement?.Invoke(this, EventArgs.Empty);
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

                        this.OnUpdateMovement?.Invoke(this, this.cellDistance / this.world.GetGrid().cellSize.x);

                        yield return null;
                    }
                    this.transform.localPosition = finalPosition;
                    missingDelta = delta - (this.transform.localPosition - currentPosition).magnitude;
                    this.OnFinishedMovement?.Invoke(this, EventArgs.Empty);
                } else {
                    this.directionsQueue.Clear();
                }
            }

            this.IsMoving = false;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper method that indicates if the object can potentially move to the given position.
        /// The object can potentially move to a given position if that position is walkable and has no collider on it, in case this object has collision enabled.
        /// </summary>
        /// <param name="position">Position to move to.</param>
        /// <returns></returns>
        public bool CanMoveTo(Vector3 position) {
            position = this.LocalToWorld(position);
            if (this.HasCollision()) {
                if (this.world.ColliderObjectDatabase.HasCollider(position))
                    return false;
            }

            return this.walkable.IsWalkable(position);
        }

        /// <summary>
        /// Updates the current direction of the object.
        /// </summary>
        /// <param name="direction">New direction.</param>
        public void UpdateDirection(Direction direction) {
            if (!this.direction.Equals(direction)) {
                this.direction = direction;
                this.OnUpdateDirection?.Invoke(this, direction);
            }
        }
        #endregion
    }
}
