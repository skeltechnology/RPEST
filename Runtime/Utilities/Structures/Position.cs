using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    public class IntPosition : Position<int> {
        #region Constructors
        /// <summary>
        /// Default class constructor.
        /// Initializes the position of the default values.
        /// </summary>
        public IntPosition() : this(default, default) {}

        /// <summary>
        /// Constructor that initializes the position with the given values.
        /// </summary>
        /// <param name="row">Row of the position.</param>
        /// <param name="column">Column of the position.</param>
        public IntPosition(int row, int column) : base(row, column) {}
        #endregion
    }
    public class FloatPosition : Position<float> {
        #region Constructors
        /// <summary>
        /// Default class constructor.
        /// Initializes the position of the default values.
        /// </summary>
        public FloatPosition() : this(default, default) {}

        /// <summary>
        /// Constructor that initializes the position with the given values.
        /// </summary>
        /// <param name="row">Row of the position.</param>
        /// <param name="column">Column of the position.</param>
        public FloatPosition(float row, float column) : base(row, column) {}
        #endregion
    }

    /// <summary>
    /// Template model for classes that represent positions.
    /// </summary>
    /// <typeparam name="T">Position value type.</typeparam>
    public class Position<T> {
        #region Properties
        /// <summary>
        /// Row of the position.
        /// </summary>
        public T Row { get; set; }

        /// <summary>
        /// Column of the position.
        /// </summary>
        public T Column { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default class constructor.
        /// Initializes the position of the default values.
        /// </summary>
        public Position() : this(default, default) {}

        /// <summary>
        /// Constructor that initializes the position with the given values.
        /// </summary>
        /// <param name="row">Row of the position.</param>
        /// <param name="column">Column of the position.</param>
        public Position(T row, T column) {
            this.Row = row;
            this.Column = column;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper method that creates a new class with the same values.
        /// </summary>
        /// <returns>new class with the same values.</returns>
        public Position<T> Copy() {
            return new Position<T>(this.Row, this.Column);
        }
        #endregion
    }
}
