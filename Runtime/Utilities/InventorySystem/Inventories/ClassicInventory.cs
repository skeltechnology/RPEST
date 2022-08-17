namespace SkelTech.RPEST.Utilities.Inventories {
    public class ClassicInventory<T> : MapInventory<ClassicItem<T>, T> where T : ClassicItemData {
        #region Helpers
        protected override ClassicItem<T> CreateItem(T itemData, int amount) {
            return new ClassicItem<T>(itemData, amount);
        }

        protected override bool NewItemCondition(T itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }

        protected override bool IncrementItemCondition(ClassicItem<T> item, int amount) {
            return (item.Count + amount) <= item.ItemData.MaximumCount;
        }

        protected override void ClearItem(ClassicItem<T> item) {
            this.items.Remove(item.ItemData.Id);
        }
        #endregion
    }
}
