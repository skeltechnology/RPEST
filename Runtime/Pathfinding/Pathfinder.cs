using SkelTech.RPEST.Pathfinding.Search;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.Pathfinding {
    /// <summary>
    /// Class that manages the pathfinding of a tilemap.
    /// </summary>
    public class Pathfinder {
        #region Fields
        /// <summary>
        /// Grid representation.
        /// </summary>
        private Cell[,] grid;

        /// <summary>
        /// Reference to the shortest path solver.
        /// </summary>
        private ShortestPathSolver solver;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="tilemap">Tilemap to be searched.</param>
        public Pathfinder(Tilemap tilemap) {
            this.grid = Pathfinder.GenerateGrid(tilemap);
            this.solver = new ShortestPathSolver(grid);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Finds the shortest path of the two given positions, taking into account the given obstacles.
        /// </summary>
        /// <param name="startGridPosition">Start position of the path (grid coordinates).</param>
        /// <param name="endGridPosition">End position of the path (grid coordinates).</param>
        /// <param name="obstacles">Collection of positions the represent obstacles (grid coordinates).</param>
        /// <param name="maxIterations">Maximum number of iterations that the algorithm will go through.</param>
        /// <returns>Shortest path.</returns>
        public Path FindShortestPath(Vector3Int startGridPosition, Vector3Int endGridPosition, ICollection<Vector3Int> obstacles, int maxIterations) {
            if (!this.IsValidPath(startGridPosition, endGridPosition)) return null;

            this.ResetGrid();
            this.InitializeObstacles(obstacles);
            this.solver.FinalState = this.grid[endGridPosition.y, endGridPosition.x];
            Cell[] result = this.solver.Solve(this.grid[startGridPosition.y, startGridPosition.x], maxIterations);
            if (result == null) return null;

            Path path = new Path();
            foreach (Cell cell in result) {
                path.AddPosition(new Vector3(cell.Column, cell.Row, 0));
            }
            return path;
        }

        /// <summary>
        /// Finds the shortest path of the two given positions, taking into account the given obstacles.
        /// </summary>
        /// <param name="startGridPosition">Start position of the path (grid coordinates).</param>
        /// <param name="endGridPosition">End position of the path (grid coordinates).</param>
        /// <param name="obstacles">Collection of positions the represent obstacles (grid coordinates).</param>
        /// <param name="maxIterations">Maximum number of iterations that the algorithm will go through.</param>
        /// <param name="callback">Method that will be called when pathfinding is completed.</param>
        /// <returns>Shortest path.</returns>
        public async void FindShortestPathAsync(Vector3Int startGridPosition, Vector3Int endGridPosition, ICollection<Vector3Int> obstacles, int maxIterations, System.Action<Path> callback) {
            await Task.Factory.StartNew(() => {
                Path path = this.FindShortestPath(startGridPosition, endGridPosition, obstacles, maxIterations);
                callback.Invoke(path);
            });
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Generates the grid representation according to the given tilemap.
        /// </summary>
        /// <param name="tilemap">Tilemap that will be used to generate the grid.</param>
        /// <returns>Grid representation of the tilemap.</returns>
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

        /// <summary>
        /// Resets every cell of the grid.
        /// </summary>
        private void ResetGrid() {
            foreach (Cell cell in this.grid) {
                cell?.Reset();
            }
        }

        /// <summary>
        /// Converts the collection of obstacles to the grid representation.
        /// </summary>
        /// <param name="obstacles">Collection of positions that represents the obstacles.</param>
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
        /// <summary>
        /// Indicates if the given path finding is valid (valid start and end positions).
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private bool IsValidPath(Vector3Int start, Vector3Int end) {
            if (start == null || end == null) return false;
            if (!this.solver.IsValidPosition(start.y, start.x)) return false;
            if (!this.solver.IsValidPosition(end.y, end.x)) return false;
            return true;
        }
        #endregion
    }
}
