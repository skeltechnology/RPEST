using SkelTech.RPEST.Pathfinding;

using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class WalkableObject : MonoBehaviour {
        #region Properties
        public bool IsMoving { get; private set; }
        public float Speed { get { return this.speed; } set { this.speed = value; } } // TODO: >= 0 ?
        #endregion

        #region Fields
        [SerializeField] private Tilemap walkable;
        [SerializeField] private float speed = 1;

        private World world;
        private Pathfinder pathfinder;
        #endregion

        #region Unity
        private void Awake() {
            // TODO: MAKE POSTION ON XX.5 , Must be done in super class WorldObject
            this.IsMoving = false;

            this.world = GameObject.Find("World").GetComponent<World>(); // TODO: IN FUTURE, HAVE SETTER AND WORLD WOULD PERFORM DEPTH SEARCH

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
            if (!this.IsMoving)
                StartCoroutine(MoveOneCell(this.transform.localPosition + direction));
        }
        #endregion

        #region Coroutines
        private IEnumerator MoveOneCell(Vector3 finalPosition) {
            this.IsMoving = true;

            yield return StartCoroutine(MoveToCoroutine(finalPosition));

            this.IsMoving = false;
        }

        // private IEnumerator MovePath() {} // TODO

        // TODO: CAN BE STATIC AND IN UTILS
        private IEnumerator MoveToCoroutine(Vector3 finalPosition) {
            while ((finalPosition - this.transform.localPosition).sqrMagnitude > Mathf.Epsilon) {
                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, finalPosition, Time.deltaTime * this.speed);
                yield return null;
            }
            //this.transform.localPosition = finalPosition;
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
    }
}
