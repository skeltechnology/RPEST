namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Inventory that can stash an certain amount of items, but only a fixed number of each.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class ClassicInventory<T> : MapInventory<ClassicItem<T>, T> where T : ClassicItemData {
        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="maximumUniqueItemsCount">Number of maximum unique items that the inventory can carry.</param>
        public ClassicInventory(int maximumUniqueItemsCount) : base(maximumUniqueItemsCount) {}

        #endregion
        #region Helpers
        protected override ClassicItem<T> CreateItem(T itemData, int amount) {
            return new ClassicItem<T>(itemData, amount);
        }
        #endregion
    }
}
