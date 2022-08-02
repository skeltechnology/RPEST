using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Pathfinding {
    /// <summary>
    /// Model used to store information of a path.
    /// </summary>
    public class Path {
        #region Fields
        /// <summary>
        /// Collection of positions of the path.
        /// </summary>
        private ICollection<Vector3> positions;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public Path() {
            this.positions = new LinkedList<Vector3>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the initial position of the path.
        /// </summary>
        /// <returns>Initial position of the path.</returns>
        public Vector3 GetInitialPosition() {
            IEnumerator<Vector3> position = this.positions.GetEnumerator();
            position.MoveNext();
            return position.Current;
        }

        /// <summary>
        /// Gets the collection of positions of the path.
        /// </summary>
        /// <returns></returns>
        public ICollection<Vector3> GetPositions() {
            return this.positions;
        }

        /// <summary>
        /// Gets the collection of directions of the path.
        /// </summary>
        /// <returns>Collection of directions of the path.</returns>
        public ICollection<Vector3Int> GetDirections() {
            ICollection<Vector3Int> directions = new LinkedList<Vector3Int>();

            if (this.positions.Count > 1) {
                Vector3 previous;
                IEnumerator<Vector3> position = this.positions.GetEnumerator();
                position.MoveNext();
                previous = position.Current;
                
                while (position.MoveNext()) {
                    directions.Add(Vector3Int.RoundToInt(position.Current - previous));
                    previous = position.Current;
                }
            }

            return directions;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Adds a position to the path.
        /// </summary>
        /// <param name="position">Position to be added.</param>
        public void AddPosition(Vector3 position) {
            this.positions.Add(position);
        }
        #endregion
    }
}
