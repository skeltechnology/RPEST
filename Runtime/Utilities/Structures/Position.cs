using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION
    public class IntPosition : Position<int> {
        #region Constructors
        public IntPosition() : this(default, default) {}

        public IntPosition(int row, int column) : base(row, column) {}
        #endregion
    }
    public class FloatPosition : Position<float> {
        #region Constructors
        public FloatPosition() : this(default, default) {}

        public FloatPosition(float row, float column) : base(row, column) {}
        #endregion
    }

    public class Position<T> {
        #region Properties
        public T Row { get; set; }
        public T Column { get; set; }
        #endregion

        #region Constructors
        public Position() : this(default, default) {}

        public Position(T row, T column) {
            this.Row = row;
            this.Column = column;
        }
        #endregion

        #region Helpers
        public Position<T> Copy() {
            return new Position<T>(this.Row, this.Column);
        }
        #endregion
    }
}
