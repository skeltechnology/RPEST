namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class ClassicItem<T> : Item<T> where T : ClassicItemData {
        #region Constructors
        public ClassicItem(T itemData) : base(itemData) {}
        public ClassicItem(T itemData, int count) : base(itemData, count) {}
        #endregion
    }
}
