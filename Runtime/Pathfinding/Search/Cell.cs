namespace SkelTech.RPEST.Pathfinding.Search {
    public class Cell {
        #region Properties
        public int Row { get; private set; }
        public int Column { get; private set; }

        public bool Visited { get; set; }
        #endregion

        #region Constructors
        public Cell(int row, int column) {
            this.Row = row;
            this.Column = column;
            this.Reset();
        }
        #endregion

        #region Initialization
        public void Reset() {
            this.Visited = false;
        }
        #endregion
    }
}
