using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public abstract class MapInventory<A, B> where A : ClassicItem<B> where B : ClassicItemData {
        #region Events
        public event EventHandler OnUpdateInventory;
        #endregion

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
                bool added = this.items.TryAdd(itemData.Id, item);
                if (!added) return false;
            } else {
                if (!this.NewItemCondition(item.ItemData, item.Count + amount)) return false;

                item.Increment(amount);
            }
            this.UpdateEventHandlers();
            return true;
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
                
                this.UpdateEventHandlers();
                return item;
            }
            return null;
        }

        public void Clear() {
            this.items.Clear();
            this.UpdateEventHandlers();
        }
        #endregion

        #region Helpers
        protected abstract A CreateItem(B itemData, int amount);
        
        protected virtual bool NewItemCondition(B itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }

        private void ClearItem(A item) {
            this.items.Remove(item.ItemData.Id);
        }

        private void UpdateEventHandlers() {
            this.OnUpdateInventory?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
