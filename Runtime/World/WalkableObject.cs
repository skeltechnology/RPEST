using SkelTech.RPEST.Pathfinding;

using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
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
        [SerializeField] private Tilemap walkable;
        [SerializeField] private float walkingSpeed = 4f;
        [SerializeField] private bool canRun = true;
        [SerializeField] private float runningSpeed = 6.5f;

        private float cellDistance;
        private Vector3Int queueDirection;
        private bool isRunning = false;

        private Pathfinder pathfinder;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();

            this.IsMoving = false;
            if (this.world) this.pathfinder = new Pathfinder(this.walkable);
        }
        #endregion

        #region Getters
        public Tilemap GetWalkable() {
            return this.walkable;
        }
        #endregion

        #region Operators
        public Path FindShortestPath(Vector2Int localStartPosition, Vector2Int localEndPosition, int maxIterations) {
            Vector2Int gridStartPosition = this.LocalToGrid(localStartPosition);
            Vector2Int gridEndPosition = this.LocalToGrid(localEndPosition);

            Path gridPath = this.pathfinder.FindShortestPath(gridStartPosition, gridEndPosition, maxIterations);
            Path localPath = new Path();
            foreach (Vector2Int gridPosition in gridPath.GetPositions()) {
                localPath.AddPosition(this.GridToLocal(gridPosition));
            }
            return localPath;
        }

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
                Vector3 finalPosition = this.transform.localPosition + direction;
                if (this.IsWalkable(finalPosition))
                    StartCoroutine(MoveCell(finalPosition));
            } else if (this.cellDistance > this.walkable.layoutGrid.cellSize.x * 0.8f) {
                this.queueDirection = direction;
            }
        }

        public void Move(Path path) {
            if (!this.IsMoving) {
                if (path != null && 
                path.GetPositions().Count > 0 && 
                (path.GetInitialPosition() - Vector2Int.FloorToInt((Vector2) this.transform.localPosition)).sqrMagnitude < Mathf.Epsilon)
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
                this.Move(this.queueDirection);
                this.queueDirection = Vector3Int.zero;
            }
        }

        private IEnumerator MovePath(Path path) {
            this.IsMoving = true;

            Vector3 position;
            foreach (Vector2Int direction in path.GetDirections()) {
                position = this.transform.localPosition + (Vector3Int) direction;
                if (!this.IsWalkable(position)) break;

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

        #region Convertion
        private Vector2Int LocalToGrid(in Vector2Int localPosition) {
            return new Vector2Int(localPosition.y - this.walkable.cellBounds.yMin, localPosition.x - this.walkable.cellBounds.xMin);
        }

        private Vector2Int GridToLocal(in Vector2Int gridPosition) {
            return new Vector2Int(gridPosition.y + this.walkable.cellBounds.xMin, gridPosition.x + this.walkable.cellBounds.yMin);
        }
        #endregion

        #region Helpers
        private bool IsWalkable(Vector3 position) {
            Vector3Int floorPosition = Vector3Int.FloorToInt(position);  // TODO: CHECK IF NEEDS CONVERTION
            return this.IsWalkable(floorPosition);
        }

        private bool IsWalkable(Vector3Int position) {
            Vector3Int floorPosition = Vector3Int.FloorToInt(position);
            bool hasTile = this.walkable.HasTile(floorPosition);
            bool hasObstacle = false;  // TODO: CHECK FOR OBSTACLE
            return hasTile && !hasObstacle;
        }
        #endregion
    }
}
