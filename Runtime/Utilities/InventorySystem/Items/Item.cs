using System;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class Item<T> where T : ItemData {
        #region Properties
        public T ItemData { get; protected set; }
        public int Count { get; protected set; }
        #endregion

        #region Constructors
        public Item(T itemData) : this(itemData, 0) {}

        public Item(T itemData, int count) {
            this.ItemData = itemData;
            this.SetCount(count);
        }
        #endregion

        #region Setters
        public void SetCount(int value) {
            if (this.ItemData.MaximumCount > 0)
                value = Math.Min(value, this.ItemData.MaximumCount);
            
            this.Count = Math.Max(value, 0);
        }

        public int Increment() {
            return this.Increment(1);
        }

        public int Increment(int amount) {
            this.SetCount(this.Count + amount);
            return this.Count;
        }

        public int Decrement() {
            return this.Decrement(1);
        }

        public int Decrement(int amount) {
            this.SetCount(this.Count - amount);
            return this.Count;
        }
        #endregion
    }
}
