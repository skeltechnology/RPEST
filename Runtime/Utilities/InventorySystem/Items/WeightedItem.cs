namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Wrapper for <c>WeightedItemData</c> that contain essential information for the <c>WeightedInventory</c>.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class WeightedItem<T> : ClassicItem<T> where T : WeightedItemData {
        #region Constructors
        /// <summary>
        /// Constructor of the class. Amount is set to zero.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        public WeightedItem(T itemData) : this(itemData, 0) {}

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        /// <param name="count">Amount of items.</param>
        public WeightedItem(T itemData, int count) : base(itemData, count) {}
        #endregion

        #region Getters
        /// <summary>
        /// Gets the total weight of all the stashed items.
        /// </summary>
        /// <returns>Total weight of all the stashed items.</returns>
        public float GetTotalWeight() {
            return this.Count * this.ItemData.Weight;
        }
        #endregion
    }
}
