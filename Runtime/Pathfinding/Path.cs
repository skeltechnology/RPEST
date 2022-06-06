using System.Collections;
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
        public ICollection<Vector2Int> GetPositions() {
            return this.positions;
        }
        #endregion

        #region Setters
        public void AddPosition(Vector2Int position) {
            this.positions.Add(position);
        }
        #endregion
    }
}
