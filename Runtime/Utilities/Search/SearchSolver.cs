using SkelTech.RPEST.Utilities.Structures;

using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.Search {
    /// <summary>
    /// Base class for a search solver.
    /// </summary>
    /// <typeparam name="T">Search state type.</typeparam>
    public abstract class SearchSolver<T> {
        #region Getters
        /// <summary>
        /// Gets the cumulative cost of the given search state.
        /// This method is called after Previous is updated.
        /// </summary>
        /// <param name="state">Search state.</param>
        /// <returns>Cost of the search state.</returns>
        protected abstract int Cost(SearchState<T> state);

        /// <summary>
        /// Gets the heuristic cost (prediction of the remaining cost to reach the final state).
        /// This method is called after Previous is updated.
        /// </summary>
        /// <param name="state">Search state.</param>
        /// <returns>Heuristic cost of the search state.</returns>
        protected abstract int Heuristic(SearchState<T> state);

        /// <summary>
        /// Gets the neighbor states of the given state.
        /// </summary>
        /// <param name="state">State.</param>
        /// <returns>Neighbors of the state.</returns>
        protected abstract ICollection<T> Neighbors(T state);

        /// <summary>
        /// Indicates of the given state is final.
        /// </summary>
        /// <param name="state">State.</param>
        /// <returns>Boolean indicating of the given state is final.</returns>
        protected abstract bool IsFinal(T state);
        #endregion

        #region Operations
        /// <summary>
        /// Solves the search problem.
        /// </summary>
        /// <param name="initialState">Initial state of the problem.</param>
        /// <param name="maxIterations">Maximum number of iterations that the algorithm will go through.</param>
        /// <returns>Array of visited states that solved the problem.</returns>
        public virtual T[] Solve(T initialState, int maxIterations) {
            PriorityQueue<SearchState<T>> queue = new PriorityQueue<SearchState<T>>(100);

            SearchState<T> start = new SearchState<T>(initialState);
            queue.Enqueue(start);

            while (queue.Count > 0 && maxIterations > 0) {
                --maxIterations;
                SearchState<T> current = queue.Dequeue();

                if (this.IsFinal(current.State)) {
                    return GetResult(current);
                }

                ICollection<T> neighbors = this.Neighbors(current.State);

                foreach (T neighbor in neighbors) {
                    SearchState<T> searchState = new SearchState<T>(neighbor);

                    searchState.Previous = current;
                    
                    searchState.Cost = this.Cost(searchState);
                    searchState.Heuristic = this.Heuristic(searchState);

                    queue.Enqueue(searchState);
                }
            }
            return null;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper method that gets the visited states that were necessary to reach the given search state.
        /// </summary>
        /// <param name="searchState">Search state.</param>
        /// <returns>Array of visited states that were necessary to reach the given search state.</returns>
        private static T[] GetResult(SearchState<T> searchState) {
            Stack<T> result = new Stack<T>();
            while (searchState != null) {
                result.Push(searchState.State);
                searchState = searchState.Previous;
            }
            return result.ToArray();
        }
        #endregion
    }
}
