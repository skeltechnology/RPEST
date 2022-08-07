using System;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    [Serializable]
    public class Direction {
        public enum DirectionEnum { Up, Left, Down, Right };

        #region Properties
        public static Direction Up { get { return new Direction(DirectionEnum.Up); }}
        public static Direction Left { get { return new Direction(DirectionEnum.Left); }}
        public static Direction Down { get { return new Direction(DirectionEnum.Down); }}
        public static Direction Right { get { return new Direction(DirectionEnum.Right); }}
        #endregion

        #region Fields
        private DirectionEnum direction;
        #endregion

        #region Constructors
        private Direction(DirectionEnum direction) {
            this.direction = direction;
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
        #endregion
    }
}
