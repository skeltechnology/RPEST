using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.Inventories {
    public class ClassicInventory<T> : Inventory<T> where T : ClassicItemData {
        #region Fields
        private Dictionary<int, Item<T>> items;
        #endregion

        #region Getters
        public Item<T> GetItem(int id) {
            return this.items.GetValueOrDefault(id, null);
        }

        public Item<T> GetItem(Type itemDataType) {
            if (itemDataType == null) return null;

            foreach (KeyValuePair<int, Item<T>> pair in this.items) {
                if (itemDataType.Equals(pair.Value.ItemData.GetType())) {
                    return pair.Value;
                }
            }
            return null;
        }

        public Item<T> GetItem(string itemName) {
            if (itemName == null) return null;

            foreach (KeyValuePair<int, Item<T>> pair in this.items) {
                if (itemName.Equals(pair.Value.ItemData.ItemName)) {
                    return pair.Value;
                }
            }
            return null;
        }

        public ICollection<Item<T>> GetItems() {
            return new List<Item<T>>(this.items.Values);
        }
        #endregion

        #region Setters
        public bool AddItem(T itemData) {
            return this.AddItem(itemData, 1);
        }

        public bool AddItem(T itemData, int amount) {
            if (itemData == null || amount <= 0) return false;

            Item<T> item = this.GetItem(itemData.Id);
            
            if (item == null) {
                item = new Item<T>(itemData, amount);
                return this.items.TryAdd(itemData.Id, item);
            } else {
                if (item.Count >= itemData.MaximumCount) return false;
                item.Increment(amount);
                return true;
            }
        }

        public Item<T> RemoveItem(int id) {
            return this.RemoveItem(id, 1);
        }
        public Item<T> RemoveItem(int id, int amount) {
            if (amount <= 0) return null;

            Item<T> item = this.GetItem(id);
            if (item != null) {
                item.Decrement(amount);
                if (item.Count == 0)
                    this.items.Remove(id);
                return item;
            }

            return null;
        }

        public void RemoveItems() {
            this.items.Clear();
        }
        #endregion
    }
}
