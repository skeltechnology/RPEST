namespace SkelTech.RPEST.Utilities.InventorySystem {
    // TODO: DOCUMENTATION
    public class WeightedItem<T> : ClassicItem<T> where T : WeightedItemData {
        #region Constructors
        public WeightedItem(T itemData) : this(itemData, 0) {}
        public WeightedItem(T itemData, int count) : base(itemData, count) {}
        #endregion

        #region Getters
        public float GetTotalWeight() {
            return this.Count * this.ItemData.Weight;
        }
        #endregion
    }
}
