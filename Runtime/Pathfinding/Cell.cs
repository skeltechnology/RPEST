using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Pathfinding {
    public class Cell {
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