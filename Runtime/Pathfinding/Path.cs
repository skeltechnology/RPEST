using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Pathfinding {
    public class Path {
        #region Fields
        private ICollection<Vector2Int> positions;
        #endregion

        #region Constructors
        public Path() {
            this.positions = new LinkedList<Vector2Int>();
        }
        #endregion

        #region Getters
        public Vector2Int GetInitialPosition() {
            IEnumerator<Vector2Int> position = this.positions.GetEnumerator();
            position.MoveNext();
            return position.Current;
        }

        public ICollection<Vector2Int> GetPositions() {
            return this.positions;
        }

        public ICollection<Vector2Int> GetDirections() {
            ICollection<Vector2Int> directions = new LinkedList<Vector2Int>();

            if (this.positions.Count > 1) {
                Vector2Int previous;
                IEnumerator<Vector2Int> position = this.positions.GetEnumerator();
                position.MoveNext();
                previous = position.Current;
                
                while (position.MoveNext()) {
                    directions.Add(position.Current - previous);
                    previous = position.Current;
                }
            }

            return directions;
        }
        #endregion

        #region Setters
        public void AddPosition(Vector2Int position) {
            this.positions.Add(position);
        }
        #endregion
    }
}
