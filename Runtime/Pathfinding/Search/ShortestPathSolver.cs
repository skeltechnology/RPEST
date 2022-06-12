using SkelTech.RPEST.Utilities.Search;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Pathfinding.Search {
    public class ShortestPathSolver : SearchSolver<Cell> {
        #region Properties
        public Cell FinalState { get; set; }
        #endregion

        #region Fields
        private readonly Cell[,] grid;
        private int rows, columns;
        #endregion

        #region Constructors
        public ShortestPathSolver(Cell[,] grid) {
            this.grid = grid;
            this.rows = grid.GetLength(0);
            this.columns = grid.GetLength(1);
        }
        #endregion

        #region Getters
        protected override int Cost(SearchState<Cell> state) {
            return state.Previous.Cost + 1;
        }

        protected override int Heuristic(SearchState<Cell> state) {
            return Mathf.Abs(state.State.Row - this.FinalState.Row) + Mathf.Abs(state.State.Column - this.FinalState.Column);
        }

        protected override ICollection<Cell> Neighbors(Cell state) {
            ICollection<Cell> neighbors = new LinkedList<Cell>();
            int[] offsets = {-1, 1};

            int i = state.Row, j = state.Column;
            Cell cell;
            foreach (int offset in offsets) {
                if (this.IsValidPosition(i + offset, j)) {
                    cell = this.grid[i + offset, j];
                    if (!cell.Visited) {
                        cell.Visited = true;
                        neighbors.Add(cell);
                    }
                }
                if (this.IsValidPosition(i, j + offset)) {
                    cell = this.grid[i, j + offset];
                    if (!cell.Visited) {
                        cell.Visited = true;
                        neighbors.Add(cell);
                    }
                }
            }
            return neighbors;
        }
        
        protected override bool IsFinal(Cell state) {
            return state == this.FinalState;
        }
        #endregion

        #region Operators
        public override Cell[] solve(Cell initialState, int maxIterations) {
            if (initialState == null || this.FinalState == null) return null;
            initialState.Visited = true;
            return base.solve(initialState, maxIterations);
        }
        #endregion

        #region Helpers
        public bool IsValidPosition(int i, int j) {
            if(i >= 0 && i < this.rows && j >= 0 && j < this.columns) {
                return this.grid[i, j] != null;
            }
            return false;
        }
        #endregion
    }
}
