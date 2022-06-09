using SkelTech.RPEST.Pathfinding.Search;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.Pathfinding {
    public class Pathfinder {
        #region Fields
        private Cell[,] grid;
        private ShortestPathSolver solver;
        #endregion

        #region Constructors
        public Pathfinder(Tilemap tilemap) {
            this.grid = Pathfinder.GenerateGrid(tilemap);
            this.solver = new ShortestPathSolver(grid);
        }
        #endregion

        #region Operators
        public Path FindShortestPath(Vector2Int startPosition, Vector2Int endPosition, int maxIterations) {
            if (!this.IsValidPath(startPosition, endPosition)) return null;

            this.ResetGrid();
            this.solver.FinalState = this.grid[endPosition.x, endPosition.y];
            Cell[] result = this.solver.solve(this.grid[startPosition.x, startPosition.y], maxIterations);
            if (result == null) return null;

            Path path = new Path();
            foreach (Cell cell in result) {
                path.AddPosition(new Vector2Int(cell.Row, cell.Column));
            }
            return path;
        }
        #endregion

        #region Initialization
        private static Cell[,] GenerateGrid(Tilemap tilemap) {
            tilemap.CompressBounds();

            BoundsInt bounds = tilemap.cellBounds;
            int rows = bounds.size.y, columns = bounds.size.x;
            Cell[,] grid = new Cell[rows, columns];

            for (int y = bounds.yMin, i = 0; i < rows; ++y, ++i) {
                for (int x = bounds.xMin, j = 0; j < columns; ++x, ++j) {
                    if (tilemap.HasTile(new Vector3Int(x, y, 0))) {
                        grid[i, j] = new Cell(i, j);
                    }
                }
            }
            return grid;
        }

        private void ResetGrid() {
            foreach (Cell cell in this.grid) {
                cell?.Reset();
            }
        }
        #endregion

        #region Helpers
        private bool IsValidPath(Vector2Int start, Vector2Int end) {
            if (start == null || end == null) return false;
            if (!this.solver.IsValidPosition(start.x, start.y)) return false;
            if (!this.solver.IsValidPosition(end.x, end.y)) return false;
            return true;
        }
        #endregion
    }
}
