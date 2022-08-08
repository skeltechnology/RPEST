using System;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    [Serializable]
    public class Direction {
        [Serializable] public enum DirectionEnum { Up, Left, Down, Right };

        #region Properties
        public static Direction Up { get; } = new Direction(DirectionEnum.Up);
        public static Direction Left { get; } = new Direction(DirectionEnum.Left);
        public static Direction Down { get; } = new Direction(DirectionEnum.Down);
        public static Direction Right { get; } = new Direction(DirectionEnum.Right);
        #endregion

        #region Fields
        [SerializeField] private DirectionEnum direction;
        #endregion

        #region Constructors
        private Direction(DirectionEnum direction) {
            this.direction = direction;
        }
        #endregion

        #region Operators
        public static bool operator ==(Direction d1, Direction d2) {
            return d1.direction == d2.direction;
        }

        public static bool operator !=(Direction d1, Direction d2) {
            return d1.direction != d2.direction;
        }

        public override bool Equals(object obj) {
            if (obj == null || this.GetType() != obj.GetType() || !(obj is Direction))
                return false;

            return this == (obj as Direction);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        #endregion

        #region Convertion
        // TODO: DOCUMENTATION
        public Vector3Int ToVector3Int() {
            switch (this.direction) {
                case DirectionEnum.Up: return Vector3Int.up;
                case DirectionEnum.Left: return Vector3Int.left;
                case DirectionEnum.Down: return Vector3Int.down;
                case DirectionEnum.Right: return Vector3Int.right;
                default: return Vector3Int.zero;
            }
        }

        public static Direction FromVector3Int(Vector3Int direction){
            if (direction == Vector3Int.up) return Direction.Up;
            else if (direction == Vector3Int.left) return Direction.Left;
            else if (direction == Vector3Int.down) return Direction.Down;
            else if (direction == Vector3Int.right) return Direction.Right;
            return null;
        }

        public static Direction FromInt(int enumValueIndex) {
            switch ((Direction.DirectionEnum) enumValueIndex) {
                case Direction.DirectionEnum.Up: return Direction.Up;
                case Direction.DirectionEnum.Left: return Direction.Left;
                case Direction.DirectionEnum.Down: return Direction.Down;
                case Direction.DirectionEnum.Right: return Direction.Right;
                default: return null;
            }
        }
        #endregion
    }
}
