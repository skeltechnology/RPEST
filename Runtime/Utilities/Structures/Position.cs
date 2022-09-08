using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION
    public class IntPosition : Position<int> {}
    public class FloatPosition : Position<float> {}

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
    }
}
