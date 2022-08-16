using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.Inventories {
    public interface Inventory<T> where T : ItemData {
        #region Getters
        public Item<T> GetItem(int id);
        public Item<T> GetItem(Type itemDataType);
        public Item<T> GetItem(string itemName);
        public ICollection<Item<T>> GetItems();
        #endregion

        #region Setters
        public bool AddItem(T item);
        public bool AddItem(T itemData, int amount);
        public Item<T> RemoveItem(int id);
        public Item<T> RemoveItem(int id, int amount);
        public void RemoveItems();
        #endregion
    }
}
