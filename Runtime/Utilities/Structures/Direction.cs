using System;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Model to store and manage two dimensional directions.
    /// </summary>
    [Serializable]
    public class Direction {
        /// <summary>
        /// Enumerator that represents the four two dimensional directions.
        /// </summary>
        [Serializable] public enum DirectionEnum { Up, Left, Down, Right };

        #region Properties
        /// <summary>
        /// Shorthand for up direction.
        /// </summary>
        public static Direction Up { get; } = new Direction(DirectionEnum.Up);

        /// <summary>
        /// Shorthand for left direction.
        /// </summary>
        public static Direction Left { get; } = new Direction(DirectionEnum.Left);

        /// <summary>
        /// Shorthand for down direction.
        /// </summary>
        public static Direction Down { get; } = new Direction(DirectionEnum.Down);

        /// <summary>
        /// Shorthand for right direction.
        /// </summary>
        public static Direction Right { get; } = new Direction(DirectionEnum.Right);
        #endregion

        #region Fields
        /// <summary>
        /// Enumerator representing the direction.
        /// </summary>
        [SerializeField] private DirectionEnum direction;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
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
        /// <summary>
        /// Converts the current direction to the correspondent <c>Vector3Int</c>.
        /// </summary>
        /// <returns>Correspondent <c>Vector3Int</c> direction.</returns>
        public Vector3Int ToVector3Int() {
            switch (this.direction) {
                case DirectionEnum.Up: return Vector3Int.up;
                case DirectionEnum.Left: return Vector3Int.left;
                case DirectionEnum.Down: return Vector3Int.down;
                case DirectionEnum.Right: return Vector3Int.right;
                default: return Vector3Int.zero;
            }
        }

        /// <summary>
        /// Converts the given <c>Vector3Int</c> to a <c>Direction</c>.
        /// </summary>
        /// <param name="direction"><c>Vector3Int</c> representing a direction.</param>
        /// <returns><c>Direction</c> correspondent to the given <c>Vector3Int</c>.</returns>
        public static Direction FromVector3Int(Vector3Int direction) {
            if (direction == Vector3Int.up) return Direction.Up;
            else if (direction == Vector3Int.left) return Direction.Left;
            else if (direction == Vector3Int.down) return Direction.Down;
            else if (direction == Vector3Int.right) return Direction.Right;
            return null;
        }

        /// <summary>
        /// Converts the given enum to a <c>Direction</c>.
        /// </summary>
        /// <param name="directionEnum">Enum representing the direction.</param>
        /// <returns><c>Direction</c> correspondent to the given enum.</returns>
        public static Direction FromEnum(DirectionEnum directionEnum) {
            switch (directionEnum) {
                case Direction.DirectionEnum.Up: return Direction.Up;
                case Direction.DirectionEnum.Left: return Direction.Left;
                case Direction.DirectionEnum.Down: return Direction.Down;
                case Direction.DirectionEnum.Right: return Direction.Right;
                default: return null;
            }
        }

        /// <summary>
        /// Converts the given enum index to a <c>Direction</c>.
        /// </summary>
        /// <param name="enumValueIndex">Index of the enum value.</param>
        /// <returns><c>Direction</c> correspondent to the given enum index.</returns>
        public static Direction FromInt(int enumValueIndex) {
            return FromEnum((Direction.DirectionEnum) enumValueIndex);
        }
        #endregion
    }
}
