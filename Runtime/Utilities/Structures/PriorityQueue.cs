using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.Structures {
    public class PriorityQueue<T> where T : IComparable<T>{
        #region Properties
        public int Count {get { return this.data.Count; }}
        #endregion

        #region Fields
        private List<T> data;
        #endregion

        #region Constructors
        public PriorityQueue() : this(20) {}

        public PriorityQueue(int capacity) {
            this.data = new List<T>(capacity);
        }
        #endregion

        #region Operators
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

        public T Peek() {
            if (this.data.Count <= 0)throw new InvalidOperationException("Priority Queue is empty.");
            return this.data[0];
        }
        #endregion

        #region Helpers
        private void SwapItems(int idx1, int idx2) {
            T temp = this.data[idx1];
            this.data[idx1] = this.data[idx2];
            this.data[idx2] = temp;
        }
        #endregion
    }
}
