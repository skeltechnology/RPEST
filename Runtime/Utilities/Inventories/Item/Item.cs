namespace SkelTech.RPEST.Utilities.Inventories {
    public class Item<T> where T : ItemData {
        #region Properties
        public T ItemData { get; private set; }
        public int Count { get; private set; }
        #endregion

        #region Constructors
        public Item(T itemData) : this(itemData, 0) {}

        public Item(T itemData, int count) {
            this.ItemData = itemData;
            this.Count = count;
        }
        #endregion
    }
}
