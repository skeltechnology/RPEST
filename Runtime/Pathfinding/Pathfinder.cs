using SkelTech.RPEST.Pathfinding.Search;

using System.Collections.Generic;

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
        public Path FindShortestPath(Vector3Int startGridPosition, Vector3Int endGridPosition, ICollection<Vector3Int> obstacles, int maxIterations) {
            if (!this.IsValidPath(startGridPosition, endGridPosition)) return null;

            this.ResetGrid();
            this.InitializeObstacles(obstacles);
            this.solver.FinalState = this.grid[endGridPosition.y, endGridPosition.x];
            Cell[] result = this.solver.solve(this.grid[startGridPosition.y, startGridPosition.x], maxIterations);
            if (result == null) return null;

            Path path = new Path();
            foreach (Cell cell in result) {
                path.AddPosition(new Vector3(cell.Column, cell.Row, 0));
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

        private void InitializeObstacles(ICollection<Vector3Int> obstacles) {
            if (obstacles == null) return;
            
            Cell cell;
            foreach (Vector3Int obstacle in obstacles) {
                cell = this.grid[obstacle.y, obstacle.x];
                if (cell != null)
                    cell.Visited = true;
            }
        }
        #endregion

        #region Helpers
        private bool IsValidPath(Vector3Int start, Vector3Int end) {
            if (start == null || end == null) return false;
            if (!this.solver.IsValidPosition(start.y, start.x)) return false;
            if (!this.solver.IsValidPosition(end.y, end.x)) return false;
            return true;
        }
        #endregion
    }
}
