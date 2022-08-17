using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class WeightedInventory<T> : MapInventory<WeightedItem<T>, T> where T : WeightedItemData {
        #region Properties
        public float MaximumTotalWeight { get; private set; }
        public float TotalWeight { get {
            float totalWeight = 0f;
            foreach (KeyValuePair<int, WeightedItem<T>> pair in this.items) {
                totalWeight += pair.Value.GetTotalWeight();
            }
            return totalWeight;
        }}
        #endregion

        #region Constructors
        public WeightedInventory(float maximumTotalWeight) {
            this.MaximumTotalWeight = maximumTotalWeight;
        }
        #endregion

        #region Helpers
        protected override WeightedItem<T> CreateItem(T itemData, int amount) {
            return new WeightedItem<T>(itemData, amount);
        }

        protected override bool NewItemCondition(T itemData, int amount) {
            if (amount <= itemData.MaximumCount) {
                return this.CanAddItem(itemData, amount);
            }
            return false;
        }

        protected override bool IncrementItemCondition(WeightedItem<T> item, int amount) {
            if ((item.Count + amount) <= item.ItemData.MaximumCount) {
                return this.CanAddItem(item.ItemData, amount);
            }
            return false;
        }

        protected override void ClearItem(WeightedItem<T> item) {
            this.items.Remove(item.ItemData.Id);
        }

        private bool CanAddItem(T itemData, int amount) {
            float newTotalWeight = this.TotalWeight + itemData.Weight * amount;
            return newTotalWeight <= this.MaximumTotalWeight;
        }
        #endregion
    }
}
