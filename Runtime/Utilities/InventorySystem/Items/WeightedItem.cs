namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class WeightedItem<T> : ClassicItem<T> where T : WeightedItemData {
        #region Constructors
        public WeightedItem(T itemData) : base(itemData) {}
        public WeightedItem(T itemData, int count) : base(itemData, count) {}
        #endregion

        #region Getters
        public float GetTotalWeight() {
            return this.Count * this.ItemData.Weight;
        }
        #endregion
    }
}
