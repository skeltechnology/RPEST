namespace SkelTech.RPEST.Pathfinding.Search {
    /// <summary>
    /// Class that represents a Cell in Pathfinding.
    /// </summary>
    public class Cell {
        #region Properties
        /// <summary>
        /// Row of the cell.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Column of the cell.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Boolean indicating if the cell was already visited.
        /// </summary>
        public bool Visited { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the cell.
        /// </summary>
        /// <param name="row">Row of the cell.</param>
        /// <param name="column">Column of the cell.</param>
        public Cell(int row, int column) {
            this.Row = row;
            this.Column = column;
            this.Reset();
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Resets the dynamic properties of the class.
        /// </summary>
        public void Reset() {
            this.Visited = false;
        }
        #endregion

        /// <summary>
        /// Represents the class information with a string.
        /// </summary>
        /// <returns>String with the class information.</returns>
        public override string ToString() {
            return Row.ToString() + " " + Column.ToString();
        }
    }
}
