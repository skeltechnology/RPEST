using System;

namespace SkelTech.RPEST.Utilities.Inventories {
    public interface Inventory<A, B> where A : Item<B> where B : ItemData {
        #region Getters
        public Item<B> GetItem(Type itemDataType);
        public Item<B> GetItem(int id);
        public Item<B> GetItem(string itemName);
        #endregion

        #region Setters
        public bool AddItem(B item);
        public Item<B> RemoveItem(Type itemDataType);
        public Item<B> RemoveItem(int id);
        public Item<B> RemoveItem(string itemName);
        #endregion
    }
}
