using SkelTech.RPEST.Pathfinding;

using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

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
        private Vector3Int queueDirection;
        private bool isRunning = false;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();

            this.IsMoving = false;
        }
        #endregion

        #region Operators
        public void MoveUp() {
            this.MoveDirection(Vector3Int.up);
        }

        public void MoveDown() {
            this.MoveDirection(Vector3Int.down);
        }

        public void MoveLeft() {
            this.MoveDirection(Vector3Int.left);
        }

        public void MoveRight() {
            this.MoveDirection(Vector3Int.right);
        }

        private void MoveDirection(Vector3Int direction) {
            if (!this.IsMoving) {
                Vector3 finalPosition = this.transform.localPosition + direction;
                if (this.walkable.IsWalkable(finalPosition))
                    StartCoroutine(MoveCell(finalPosition));
            } else if (this.cellDistance > this.walkable.GetTilemap().layoutGrid.cellSize.x * 0.8f) {
                this.queueDirection = direction;
            }
        }

        public void MoveTo(Vector3 position) {
            if (!this.IsMoving) {
                Path path = this.walkable.FindShortestPath(this.transform.localPosition, position, 1000);
                if (path != null && path.GetPositions().Count > 2)
                    StartCoroutine(MovePath(path));
            }
        }
        #endregion

        #region Coroutines
        private IEnumerator MoveCell(Vector3 finalPosition) {
            this.IsMoving = true;

            yield return StartCoroutine(MoveToCoroutine(finalPosition));

            this.IsMoving = false;

            if (this.queueDirection != Vector3Int.zero) {
                this.MoveDirection(this.queueDirection);
                this.queueDirection = Vector3Int.zero;
            }
        }

        private IEnumerator MovePath(Path path) {
            this.IsMoving = true;

            Vector3 position;
            foreach (Vector3Int direction in path.GetDirections()) {
                position = this.transform.localPosition + direction;
                if (!this.walkable.IsWalkable(position)) break;

                yield return StartCoroutine(MoveToCoroutine(position));
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
