using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Pathfinding {
    public class Path {
        #region Fields
        private ICollection<Vector3> positions;
        #endregion

        #region Constructors
        public Path() {
            this.positions = new LinkedList<Vector3>();
        }
        #endregion

        #region Getters
        public Vector3 GetInitialPosition() {
            IEnumerator<Vector3> position = this.positions.GetEnumerator();
            position.MoveNext();
            return position.Current;
        }

        public ICollection<Vector3> GetPositions() {
            return this.positions;
        }

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
        public void AddPosition(Vector3 position) {
            this.positions.Add(position);
        }
        #endregion
    }
}
