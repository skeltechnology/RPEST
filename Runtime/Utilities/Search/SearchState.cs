using System;

namespace SkelTech.RPEST.Utilities.Search {
    public class SearchState<T> : IComparable<SearchState<T>> {
        #region Properties
        public int F { get { return G + H; } }
        public int G { get; set; }
        public int H { get; set; }

        public SearchState<T> Previous { get; set; }

        public T State { get; }
        #endregion

        #region Constructors
        public SearchState(T state) {
            this.State = state;
            this.Reset();
        }
        #endregion

        #region Operators
        public int CompareTo(SearchState<T> other) {
            if (this.F < other.F) return -1;
            if (this.F > other.F) return 1;
            return 0;
        }
        #endregion

        #region Initialization
        public void Reset() {
            this.G = 0;
            this.H = 0;
            this.Previous = null;
        }
        #endregion
    }
}