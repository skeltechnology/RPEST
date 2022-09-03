using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public interface Inventory<A, B> where A : ClassicItem<B> where B : ClassicItemData {
        #region Getters
        public A GetItem(int id);
        public A GetItem(string itemName);
        public ICollection<A> GetItems();
        #endregion

        #region Setters
        public bool AddItem(B item);
        public bool AddItem(B itemData, int amount);
        public A RemoveItem(int id);
        public A RemoveItem(int id, int amount);
        public void RemoveItems();
        #endregion
    }
}
