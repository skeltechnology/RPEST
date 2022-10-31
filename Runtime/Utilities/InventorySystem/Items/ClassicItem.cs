using System;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Wrapper for <c>ClassicItemData</c> that contain essential information for the <c>ClassicInventory</c>.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class ClassicItem<T> where T : ClassicItemData {
        #region Properties
        /// <summary>
        /// Reference to the <c>ItemData</c>.
        /// </summary>
        public T ItemData { get; protected set; }

        /// <summary>
        /// Amount of stashed items.
        /// </summary>
        public int Count { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class. Amount is set to zero.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        public ClassicItem(T itemData) : this(itemData, 0) {}

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        /// <param name="count">Amount of items.</param>
        public ClassicItem(T itemData, int count) {
            this.ItemData = itemData;
            this.SetCount(count);
        }
        #endregion

        #region Setters
        /// <summary>
        /// Sets the amount of items to the given value.
        /// </summary>
        /// <param name="value">New amount of items.</param>
        public void SetCount(int value) {
            if (this.ItemData.MaximumCount > 0)
                value = Math.Min(value, this.ItemData.MaximumCount);
            
            this.Count = Math.Max(value, 0);
        }

        /// <summary>
        /// Increments the current amount of items.
        /// </summary>
        /// <returns>New amount of items.</returns>
        public int Increment() {
            return this.Increment(1);
        }

        /// <summary>
        /// Increments the current amount of items by the given amount.
        /// </summary>
        /// <param name="amount">Amount of items to be incremented.</param>
        /// <returns>New amount of items.</returns>
        public int Increment(int amount) {
            this.SetCount(this.Count + amount);
            return this.Count;
        }

        /// <summary>
        /// Decrements the current amount of items.
        /// </summary>
        /// <returns>New amount of items.</returns>
        public int Decrement() {
            return this.Decrement(1);
        }

        /// <summary>
        /// Decrements the current amount of items by the given amount.
        /// </summary>
        /// <param name="amount">Amount of items to be decremented.</param>
        /// <returns>New amount of items.</returns>
        public int Decrement(int amount) {
            this.SetCount(this.Count - amount);
            return this.Count;
        }
        #endregion
    }
}
