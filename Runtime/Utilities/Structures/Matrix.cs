using System;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Model used store and manipulate a two-dimensional array of values.
    /// </summary>
    /// <typeparam name="T">Matrix elements type.</typeparam>
    public class Matrix<T> {
        #region Properties
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Accesses the element at the given position.
        /// </summary>
        public T this[int i, int j] {
            get { return this.data[i, j]; }
            set { this.data[i, j] = value; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Two-dimensional array that stores the matrix elements.
        /// </summary>
        private T[,] data;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class that creates a square matrix.
        /// </summary>
        /// <param name="size">Size of the square matrix sides.</param>
        public Matrix(int size) : this(size, size) {}

        /// <summary>
        /// Constructor of the class that initializes the matrix with the default value of the element type.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        public Matrix(int rows, int columns) : this(rows, columns, default) {}

        /// <summary>
        /// Constructor of the class that initializes the matrix with the given value.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="defaultValue">Default value of the elements.</param>
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

        /// <summary>
        /// Constructor that initializes the class with an array.
        /// </summary>
        /// <param name="data">Array of values.</param>
        public Matrix(T[,] data) {
            this.Rows = data.GetLength(0);
            this.Columns = data.GetLength(1);
            this.data = data;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Fills the entire matrix with the specified value.
        /// </summary>
        /// <param name="value">Value with which the array will be filled.</param>
        public void Fill(T value) {
            for (int i = 0; i < this.Rows; ++i) {
                for (int j = 0; j < this.Columns; ++j) {
                    this.data[i, j] = value;
                }
            }
        }

        /// <summary>
        /// Swaps two elements.
        /// </summary>
        /// <param name="position1">Position of the first element.</param>
        /// <param name="position2">Position of the second element.</param>
        /// <returns>Boolean indicating if the swap was successful.</returns>
        public bool Swap(IntPosition position1, IntPosition position2) {
            if (this.IsValidPosition(position1) && this.IsValidPosition(position2)) {
                T temp = this.data[position1.Row, position1.Column];
                this.data[position1.Row, position1.Column] = this.data[position2.Row, position2.Column];
                this.data[position2.Row, position2.Column] = temp;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Indicates if the given position is valid.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Boolean indicating if the given position is valid.</returns>
        public bool IsValidPosition(IntPosition position) {
            if (position.Row < 0 || position.Row >= this.Rows) return false;
            if (position.Column < 0 || position.Column >= this.Columns) return false;
            return true;
        }
        #endregion
    }
}
