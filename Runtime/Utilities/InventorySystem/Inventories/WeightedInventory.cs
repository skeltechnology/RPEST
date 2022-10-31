using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Classic inventory that also has a weight limitation.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class WeightedInventory<T> : MapInventory<WeightedItem<T>, T> where T : WeightedItemData {
        #region Properties
        /// <summary>
        /// Maximum amount of weight the inventory can carry.
        /// </summary>
        public float MaximumTotalWeight { get; private set; }

        /// <summary>
        /// Weight that the inventory is currently carrying.
        /// </summary>
        public float TotalWeight { get {
            float totalWeight = 0f;
            foreach (KeyValuePair<int, WeightedItem<T>> pair in this.items) {
                totalWeight += pair.Value.GetTotalWeight();
            }
            return totalWeight;
        }}
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="maximumUniqueItemsCount">Number of maximum unique items that the inventory can carry.</param>
        /// <param name="maximumTotalWeight">Maximum amount of weight the inventory can carry.</param>
        /// <returns></returns>
        public WeightedInventory(int maximumUniqueItemsCount, float maximumTotalWeight) : base(maximumUniqueItemsCount) {
            this.MaximumTotalWeight = maximumTotalWeight;
        }
        #endregion

        #region Helpers
        protected override WeightedItem<T> CreateItem(T itemData, int amount) {
            return new WeightedItem<T>(itemData, amount);
        }

        protected override bool NewItemCondition(T itemData, int amount) {
            return base.NewItemCondition(itemData, amount) && this.CanAddItem(itemData, amount);
        }

        protected override bool IncrementItemCondition(WeightedItem<T> item, int amount) {
            return base.IncrementItemCondition(item, amount) && this.CanAddItem(item.ItemData, amount);
        }

        /// <summary>
        /// Helper method that makes the weight verification.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="amount">Amount of items.</param>
        /// <returns>Boolean indicating if the weight constraint is verified.</returns>
        private bool CanAddItem(T itemData, int amount) {
            float newTotalWeight = this.TotalWeight + itemData.Weight * amount;
            return newTotalWeight <= this.MaximumTotalWeight;
        }
        #endregion
    }
}
