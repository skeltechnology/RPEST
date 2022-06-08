using SkelTech.RPEST.Utilities.Structures;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Utilities.Search {
    public abstract class SearchSolver<T> {
        #region Getters
        protected abstract int Cost(T state);
        protected abstract int Heuristic(T state);
        protected abstract ICollection<T> Neighbors(T state);
        protected abstract bool IsFinal(T state);
        #endregion

        #region Operations
        public virtual T[] solve(T initialState, int maxIterations) {
            PriorityQueue<SearchState<T>> queue = new PriorityQueue<SearchState<T>>(100);

            SearchState<T> start = new SearchState<T>(initialState);
            queue.Enqueue(start);

            while (queue.Count > 0 && maxIterations > 0) {
                --maxIterations;
                SearchState<T> current = queue.Dequeue();

                if (this.IsFinal(current.State)) {
                    Debug.Log("Iterations: " + (1000 - maxIterations).ToString());
                    return GetResult(current);
                }

                ICollection<T> neighbors = this.Neighbors(current.State);

                foreach (T neighbor in neighbors) {
                    SearchState<T> searchState = new SearchState<T>(neighbor);

                    searchState.Previous = current;
                    
                    searchState.G = current.G + this.Cost(neighbor); // TODO: CONFIRM
                    searchState.H = this.Heuristic(neighbor);

                    queue.Enqueue(searchState);
                }
            }
            return null;
        }
        #endregion

        #region Helpers
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
