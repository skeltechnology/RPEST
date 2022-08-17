namespace SkelTech.RPEST.Utilities.Inventories {
    public class WeightedItem<T> : Item<T> where T : WeightedItemData {
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
