using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Data Structure that behaves lika a priority queue.
    /// It is implemented with a <c>List</c>.
    /// </summary>
    /// <typeparam name="T">Queue elements type.</typeparam>
    public class PriorityQueue<T> where T : IComparable<T>{
        #region Properties
        /// <summary>
        /// Number of elements in the queue.
        /// </summary>
        public int Count {get { return this.data.Count; }}
        #endregion

        #region Fields
        /// <summary>
        /// List of the queue elements (representing a tree).
        /// </summary>
        private List<T> data;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public PriorityQueue() : this(20) {}

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="capacity">Initial capacity of the queue.</param>
        public PriorityQueue(int capacity) {
            this.data = new List<T>(capacity);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds an element to the priority queue.
        /// This operation is performed with a time complexity of O(log n).
        /// </summary>
        /// <param name="item">Element to be added.</param>
        public void Enqueue(T item) {
            int parentIndex, childIndex = this.data.Count;

            this.data.Add(item);
            while (childIndex > 0) {
                parentIndex = (childIndex - 1) / 2;
                if (this.data[childIndex].CompareTo(this.data[parentIndex]) >= 0)  // If child is greater than (or equal) to parent, end
                    break;
                this.SwapItems(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        /// <summary>
        /// Removes and returns the first element of the priority queue.
        /// This operation is performed with a time complexity of O(1).
        /// </summary>
        /// <returns>First element of the priority queue.</returns>
        public T Dequeue() {
            if (this.data.Count <= 0)throw new InvalidOperationException("Priority Queue is empty.");

            int lastIndex = this.data.Count - 1;
            T firstItem = this.data[0];
            data[0] = data[lastIndex];
            this.data.RemoveAt(lastIndex);

            --lastIndex;
            int rightChild, childIndex, parentIndex = 0;
            while (true) {
                childIndex = parentIndex * 2 + 1;
                if (childIndex > lastIndex) break;  // Has no child
                rightChild = childIndex + 1;
                if (rightChild <= lastIndex && this.data[rightChild].CompareTo(this.data[childIndex]) < 0)  // If the right child exists and is smaller than the current child
                    childIndex = rightChild;
                if (this.data[parentIndex].CompareTo(this.data[childIndex]) <= 0)  // If the parent is smaller than (or equal to) the current child
                    break;
                this.SwapItems(parentIndex, childIndex);
                parentIndex = childIndex;
            }
            return firstItem;
        }

        /// <summary>
        /// Gets the first element of the priority queue.
        /// This operation is performed with a time complexity of O(1).
        /// </summary>
        /// <returns>First element of the priority queue.</returns>
        public T Peek() {
            if (this.data.Count <= 0)throw new InvalidOperationException("Priority Queue is empty.");
            return this.data[0];
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper method to swap two elements.
        /// </summary>
        /// <param name="idx1">Index of the first element.</param>
        /// <param name="idx2">Index of the second element.</param>
        private void SwapItems(int idx1, int idx2) {
            T temp = this.data[idx1];
            this.data[idx1] = this.data[idx2];
            this.data[idx2] = temp;
        }
        #endregion
    }
}
