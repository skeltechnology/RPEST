using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Class that implements an unordered inventory system with a dictionary, using key-value pairs.
    /// </summary>
    /// <typeparam name="A">Item type.</typeparam>
    /// <typeparam name="B">Item data type.</typeparam>
    public abstract class MapInventory<A, B> where A : ClassicItem<B> where B : ClassicItemData {
        #region Events
        /// <summary>
        /// Called when the inventory is updated.
        /// This includes operations that add and remove items.
        /// </summary>
        public event EventHandler OnUpdateInventory;
        #endregion

        #region Properties
        /// <summary>
        /// Number of maximum unique items that the inventory can carry.
        /// </summary>
        public int MaximumUniqueItemsCount { get; private set; }

        /// <summary>
        /// Number of unique items that the inventory carries.
        /// </summary>
        /// <value></value>
        public int UniqueItemsCount { get { return this.items.Count; }}
        #endregion

        #region Fields
        /// <summary>
        /// Map that stores the inventory items.
        /// </summary>
        protected Dictionary<int, A> items = new Dictionary<int, A>();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="maximumUniqueItemsCount">Number of maximum unique items that the inventory can carry.</param>
        public MapInventory(int maximumUniqueItemsCount) {
            this.MaximumUniqueItemsCount = maximumUniqueItemsCount;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the item with the specified if.
        /// </summary>
        /// <param name="id">Id of the item.</param>
        /// <returns>Item with the specified id or <c>null</c> if it does not exist.</returns>
        public A GetItem(int id) {
            return this.items.GetValueOrDefault(id, null);
        }

        /// <summary>
        /// Gets the item with the specified name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Item with the specified name or <c>null</c> if it does not exist.</returns>
        public A GetItem(string itemName) {
            if (itemName == null) return null;

            foreach (KeyValuePair<int, A> pair in this.items) {
                if (itemName.Equals(pair.Value.ItemData.ItemName)) {
                    return pair.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a collection with all the inventory items.
        /// </summary>
        /// <returns>Collection with all the inventory items.</returns>
        public ICollection<A> GetItems() {
            return this.items.Values;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Adds one unity of the given item data to the inventory.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <returns>Boolean indicating if the item was added.</returns>
        public bool AddItem(B itemData) {
            return this.AddItem(itemData, 1);
        }

        /// <summary>
        /// Adds the given item data to the inventory, according to the given amount of items.
        /// The items are only added if the entire amount of items can be added.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="amount">Amount of items to be added.</param>
        /// <returns>Boolean indicating if the items were added.</returns>
        public bool AddItem(B itemData, int amount) {
            if (itemData == null || amount <= 0) return false;

            A item = this.GetItem(itemData.Id);
            
            if (item == null) {
                if (!this.NewItemCondition(itemData, amount)) return false;

                item = this.CreateItem(itemData, amount);
                bool added = this.items.TryAdd(itemData.Id, item);
                if (!added) return false;
            } else {
                if (!this.IncrementItemCondition(item, amount)) return false;

                item.Increment(amount);
            }
            this.UpdateEventHandlers();
            return true;
        }

        /// <summary>
        /// Removes one unity of the item with the given id from the inventory.
        /// </summary>
        /// <param name="id">Id of the item.</param>
        /// <returns>Reference to the item information.</returns>
        public A RemoveItem(int id) {
            return this.RemoveItem(id, 1);
        }
        
        /// <summary>
        /// Removes the item with the given id from the inventory, according to the given amount of items.
        /// </summary>
        /// <param name="id">Id of the item.</param>
        /// <param name="amount">Amount of items to be removed.</param>
        /// <returns>Reference to the item information.</returns>
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

        /// <summary>
        /// Removes all the items from the inventory.
        /// </summary>
        public void Clear() {
            this.items.Clear();
            this.UpdateEventHandlers();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper method to instantiate an inventory item.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="amount">Amount of items.</param>
        /// <returns>Instantiated inventory item.</returns>
        protected abstract A CreateItem(B itemData, int amount);
        
        /// <summary>
        /// Helper method to verify if a new item can be instantiated.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="amount">Amount of items.</param>
        /// <returns>Boolean indicating if a new item can be instantiated.</returns>
        protected virtual bool NewItemCondition(B itemData, int amount) {
            return this.UniqueItemsCount < this.MaximumUniqueItemsCount && amount <= itemData.MaximumCount;
        }

        /// <summary>
        /// Helper method to verify if an item count can be incremented.
        /// </summary>
        /// <param name="item">Item reference.</param>
        /// <param name="amount">Amount of items.</param>
        /// <returns>Boolean indicating if an item count can be incremented.</returns>
        protected virtual bool IncrementItemCondition(A item, int amount) {
            return item.Count + amount <= item.ItemData.MaximumCount;
        }

        /// <summary>
        /// Helper method that deletes an item (erases its reference) from the inventory.
        /// </summary>
        /// <param name="item">Item to be deleted.</param>
        private void ClearItem(A item) {
            this.items.Remove(item.ItemData.Id);
        }

        /// <summary>
        /// Helper method that notifies the event handlers of the <c>OnUpdateInventory</c> event.
        /// </summary>
        private void UpdateEventHandlers() {
            this.OnUpdateInventory?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
