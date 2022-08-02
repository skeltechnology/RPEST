using System;

namespace SkelTech.RPEST.Utilities.Search {
    /// <summary>
    /// Wrapper class that contains additional information of a state.
    /// </summary>
    /// <typeparam name="T">Search state type.</typeparam>
    public class SearchState<T> : IComparable<SearchState<T>> {
        #region Properties
        /// <summary>
        /// Priority of the state.
        /// The lower the priority, the sooner the state is visited.
        /// </summary>
        public int Priority { get { return Cost + Heuristic; } }

        /// <summary>
        /// Cost of the state.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Prediction of the remaining cost to reach the final state.
        /// </summary>
        public int Heuristic { get; set; }

        /// <summary>
        /// Reference to the previous search state.
        /// </summary>
        public SearchState<T> Previous { get; set; }

        /// <summary>
        /// Reference to the state.
        /// </summary>
        public T State { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="state">State.</param>
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
        /// <summary>
        /// Resets the search state.
        /// </summary>
        public void Reset() {
            this.Cost = 0;
            this.Heuristic = 0;
            this.Previous = null;
        }
        #endregion
    }
}
