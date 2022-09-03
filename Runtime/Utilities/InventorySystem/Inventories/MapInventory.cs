using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public abstract class MapInventory<A, B> : Inventory<A, B> where A : ClassicItem<B> where B : ClassicItemData {
        #region Fields
        protected Dictionary<int, A> items = new Dictionary<int, A>();
        #endregion

        #region Getters
        public A GetItem(int id) {
            return this.items.GetValueOrDefault(id, null);
        }

        public A GetItem(string itemName) {
            if (itemName == null) return null;

            foreach (KeyValuePair<int, A> pair in this.items) {
                if (itemName.Equals(pair.Value.ItemData.ItemName)) {
                    return pair.Value;
                }
            }
            return null;
        }

        public ICollection<A> GetItems() {
            return new List<A>(this.items.Values);
        }
        #endregion

        #region Setters
        public bool AddItem(B itemData) {
            return this.AddItem(itemData, 1);
        }

        public bool AddItem(B itemData, int amount) {
            if (itemData == null || amount <= 0) return false;

            A item = this.GetItem(itemData.Id);
            
            if (item == null) {
                if (!this.NewItemCondition(itemData, amount)) return false;

                item = this.CreateItem(itemData, amount);
                return this.items.TryAdd(itemData.Id, item);
            } else {
                if (!this.IncrementItemCondition(item, amount)) return false;

                item.Increment(amount);
                return true;
            }
        }

        public A RemoveItem(int id) {
            return this.RemoveItem(id, 1);
        }
        public A RemoveItem(int id, int amount) {
            if (amount <= 0) return null;

            A item = this.GetItem(id);
            if (item != null) {
                item.Decrement(amount);
                if (item.Count == 0)
                    this.ClearItem(item);
                return item;
            }

            return null;
        }

        public void RemoveItems() {
            this.items.Clear();
        }
        #endregion

        #region Helpers
        protected abstract A CreateItem(B itemData, int amount);

        protected virtual void ClearItem(A item) {
            this.items.Remove(item.ItemData.Id);
        }
        
        protected virtual bool NewItemCondition(B itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }
        protected virtual bool IncrementItemCondition(A item, int amount) {
            return (item.Count + amount) <= item.ItemData.MaximumCount;
        }
        #endregion
    }
}
