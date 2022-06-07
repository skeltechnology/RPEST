using SkelTech.RPEST.Utilities.Structures;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.Pathfinding {
    public class Pathfinder {
        #region Fields
        private int rows, columns;
        private Cell[,] grid;
        #endregion

        #region Constructors
        public Pathfinder(Tilemap tilemap) {
            this.GenerateGrid(tilemap);
        }
        #endregion

        #region Pathfinding
        public Path GetPath(Vector2Int startPosition, Vector2Int endPosition, int maxIterations) {
            if (this.IsValidPath(startPosition, endPosition)) {
                Cell start = this.grid[startPosition.x, startPosition.y];
                Cell end = this.grid[endPosition.x, endPosition.y];

                PriorityQueue<Cell> queue = new PriorityQueue<Cell>(100);
                start.visited = true;
                queue.Enqueue(start);

                while (queue.Count > 0 && maxIterations > 0) {
                    --maxIterations;
                    Cell current = queue.Dequeue();
                    if (current == end) {
                        Debug.Log("Iterations: " + (1000 - maxIterations).ToString());
                        return this.GetPath(current);
                    }
                    foreach (Cell neighbor in this.GetNeighbors(current)) {
                        if (!neighbor.visited) {
                            neighbor.visited = true;
                            neighbor.previous = current;
                            neighbor.g = current.g + 1;
                            neighbor.h = CalculateHeuristic(neighbor, end);
                            neighbor.f = neighbor.g + neighbor.h;
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        private Path GetPath(Cell cell) {
            Stack<Cell> stack = new Stack<Cell>();
            Path path = new Path();

            while (cell != null) {
                stack.Push(cell);
                cell = cell.previous;
            }

            foreach (Cell c in stack) {
                path.AddPosition(new Vector2Int(c.i, c.j));
            }
            return path;
        }

        private bool IsValidPath(Vector2Int start, Vector2Int end) {
            if (start == null || end == null) return false;
            if (!this.IsValidPosition(start.x, start.y)) return false;
            if (!this.IsValidPosition(end.x, end.y)) return false;
            return true;
        }

        private ICollection<Cell> GetNeighbors(in Cell cell) {
            ICollection<Cell> neighbors = new LinkedList<Cell>();
            int[] offset = {-1, 1};

            int i = cell.i, j = cell.j;
            foreach (int iOffset in offset) {
                if (this.IsValidPosition(i + iOffset, j))
                    neighbors.Add(this.grid[i + iOffset, j]);
            }
            foreach (int jOffset in offset) {
                if (this.IsValidPosition(i, j + jOffset))
                    neighbors.Add(this.grid[i, j + jOffset]);
            }
            return neighbors;
        }

        private bool IsValidPosition(int i, int j) {
            if(i >= 0 && i < this.rows && j >= 0 && j < this.columns) {
                return this.grid[i, j] != null;
            }
            return false;
        }

        private static int CalculateHeuristic(Cell current, Cell final) {
            return Mathf.Abs(current.i - final.i) + Mathf.Abs(current.j - final.j);
        }
        #endregion

        #region Initialization
        private void GenerateGrid(Tilemap tilemap) {
            tilemap.CompressBounds();

            BoundsInt bounds = tilemap.cellBounds;
            this.rows = bounds.size.y;
            this.columns = bounds.size.x;

            this.grid = new Cell[rows, columns];

            for (int y = bounds.yMin, i = 0; i < rows; ++y, ++i) {
                for (int x = bounds.xMin, j = 0; j < columns; ++x, ++j) {
                    if (tilemap.HasTile(new Vector3Int(x, y, 0))) { // TODO: REPLACE WITH TILEMAP Z
                        grid[i, j] = new Cell(i, j);
                    }
                }
            }
        }
        #endregion
    }
}
