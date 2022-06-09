using System;

namespace SkelTech.RPEST.Utilities.Search {
    public class SearchState<T> : IComparable<SearchState<T>> {
        #region Properties
        public int Priority { get { return Cost + Heuristic; } }  // Lower priority integers are explored first
        public int Cost { get; set; }
        public int Heuristic { get; set; }

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
            if (this.Priority < other.Priority) return -1;
            if (this.Priority > other.Priority) return 1;
            return 0;
        }
        #endregion

        #region Initialization
        public void Reset() {
            this.Cost = 0;
            this.Heuristic = 0;
            this.Previous = null;
        }
        #endregion
    }
}
