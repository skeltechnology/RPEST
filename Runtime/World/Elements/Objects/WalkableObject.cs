using SkelTech.RPEST.Pathfinding;
using SkelTech.RPEST.Animations.Sprites;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class WalkableObject : ColliderObject {
        #region Properties
        public bool IsMoving { get; private set; }
        public bool IsRunning { get { return this.isRunning; } set { this.isRunning = this.canRun && value; } }

        public bool IsAnimated { get { return this.walkableAnimation != null && this.animator != null; } }

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

        [SerializeField] private SpriteAnimator animator;
        [SerializeField] private WalkableAnimation walkableAnimation;

        [SerializeField] private float walkingSpeed = 4f;
        [SerializeField] private bool canRun = true;
        [SerializeField] private float runningSpeed = 6.5f;

        private float cellDistance;
        protected Vector3Int lastDirection = Vector3Int.down;
        private Queue<Vector3Int> directionsQueue;
        private bool isRunning = false;
        #endregion

        #region Unity
        protected void Awake() {
            this.directionsQueue = new Queue<Vector3Int>();
            this.IsMoving = false;
        }
        #endregion

        #region Getters
        public WalkableTilemap GetWalkableTilemap() {
            return this.walkable;
        }
        #endregion

        #region Operators
        protected virtual void OnStartedMovement() {}
        protected virtual void OnFinishedMovement() {}

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
                if (this.CanMoveTo(this.transform.localPosition + direction)) {  // Small optimization
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

        public void MoveTo(Vector3 position) {
            this.MoveTo(Vector3Int.FloorToInt(position));
        }

        public void MoveTo(Vector3Int position) {
            if (!this.IsMoving) {
                Path path = this.walkable.FindShortestPath(
                    Vector3Int.FloorToInt(this.transform.localPosition), 
                    position,
                    this.HasCollision()
                );
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
                this.UpdateDirection(this.directionsQueue.Dequeue());
                finalPosition = this.transform.localPosition + this.lastDirection;
                if (this.CanMoveTo(finalPosition)) {
                    this.OnStartedMovement();
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

                        // TODO: OPTIMIZATION: ONLY CALCULATE IF IS ANIMATED
                        this.UpdateSprite(this.cellDistance / this.world.GetGrid().cellSize.x);

                        yield return null;
                    }
                    this.transform.localPosition = finalPosition;
                    this.UpdateSprite(0);
                    missingDelta = delta - (this.transform.localPosition - currentPosition).magnitude;
                    this.OnFinishedMovement();
                } else {
                    this.directionsQueue.Clear();
                }
            }

            this.IsMoving = false;
        }
        #endregion

        #region Helpers
        public bool CanMoveTo(Vector3 position) {
            position = this.LocalToWorld(position);
            if (this.HasCollision()) {
                if (this.world.ColliderObjectDatabase.HasCollider(position))
                    return false;
            }

            return this.walkable.IsWalkable(position);
        }

        private void UpdateDirection(Vector3Int direction) {
            this.lastDirection = direction;
            if (this.IsAnimated) {
                SpriteAnimation animation;
                if (direction == Vector3Int.up) animation = this.walkableAnimation.GetUpAnimation();
                else if (direction == Vector3Int.left) animation = this.walkableAnimation.GetLeftAnimation();
                else if (direction == Vector3Int.right) animation = this.walkableAnimation.GetRightAnimation();
                else animation = this.walkableAnimation.GetDownAnimation();

                this.animator.SetAnimation(animation);
                this.animator.UpdateSprite(0);
            }
        }

        private void UpdateSprite(float progress) {
            if (this.IsAnimated)
                this.animator.UpdateSprite(progress);
        }
        #endregion
    }
}
