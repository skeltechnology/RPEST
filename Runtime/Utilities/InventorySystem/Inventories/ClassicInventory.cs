namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class ClassicInventory<T> : MapInventory<ClassicItem<T>, T> where T : ClassicItemData {
        #region Constructors
        public ClassicInventory(int maximumUniqueItemsCount) : base(maximumUniqueItemsCount) {}

        #endregion
        #region Helpers
        protected override ClassicItem<T> CreateItem(T itemData, int amount) {
            return new ClassicItem<T>(itemData, amount);
        }
        #endregion
    }
}
