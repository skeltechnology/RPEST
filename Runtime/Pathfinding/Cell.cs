using System;

namespace SkelTech.RPEST.Pathfinding {
    public class Cell : IComparable<Cell> {
        #region Fields
        public int i, j;
        public int f, g, h;
        public bool visited;
        public Cell previous;
        #endregion

        #region Constructors
        public Cell(int i, int j) {
            this.i = i;
            this.j = j;
            this.ResetCell();
        }
        #endregion

        #region Operators
        public int CompareTo(Cell other) {
            if (this.h < other.h) return -1;
            if (this.h == other.h) return 0;
            return 1;
        }
        #endregion

        #region Initialization
        public void ResetCell() {
            this.f = 0;
            this.g = 0;
            this.h = 0;
            this.visited = false;
            this.previous = null;
        }
        #endregion
    }
}