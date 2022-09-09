using System;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION

    public class Matrix<T> {
        #region Properties
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public T this[int i, int j] {
            get { return this.data[i, j]; }
            set { this.data[i, j] = value; }
        }
        #endregion

        #region Fields
        private T[,] data;
        #endregion

        #region Constructors
        public Matrix(int size) : this(size, size) {}

        public Matrix(int rows, int columns) : this(rows, columns, default) {}

        public Matrix(int rows, int columns, T defaultValue) {
            if (rows < 1) {
                throw new Exception("Invalid rows size");
            } else if (columns < 1) {
                throw new Exception("Invalid columns size");
            }

            this.Rows = rows;
            this.Columns = columns;
            this.data = new T[rows, columns];
            
            this.Fill(defaultValue);
        }

        public Matrix(T[,] data) {
            this.Rows = data.GetLength(0);
            this.Columns = data.GetLength(1);
            this.data = data;
        }
        #endregion

        #region Helpers
        public void Fill(T value) {
            for (int i = 0; i < this.Rows; ++i) {
                for (int j = 0; j < this.Columns; ++j) {
                    this.data[i, j] = value;
                }
            }
        }

        public bool Swap(IntPosition position1, IntPosition position2) {
            if (this.IsValidPosition(position1) && this.IsValidPosition(position2)) {
                T temp = this.data[position1.Row, position1.Column];
                this.data[position1.Row, position1.Column] = this.data[position2.Row, position2.Column];
                this.data[position2.Row, position2.Column] = temp;
                return true;
            }
            return false;
        }

        public bool IsValidPosition(IntPosition position) {
            if (position.Row < 0 || position.Row >= this.Rows) return false;
            if (position.Column < 0 || position.Column >= this.Columns) return false;
            return true;
        }
        #endregion
    }
}
