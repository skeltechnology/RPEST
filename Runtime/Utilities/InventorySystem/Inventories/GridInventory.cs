using SkelTech.RPEST.Utilities.Structures;

using System;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Inventory that has a visual organization of the items.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class GridInventory<T>  where T : GridItemData {
        #region Events
        /// <summary>
        /// Called when the inventory is updated.
        /// This includes operations that add move and remove items.
        /// </summary>
        public event EventHandler OnUpdateInventory;
        #endregion

        #region Properties
        /// <summary>
        /// Number of rows that the inventory grid has.
        /// </summary>
        public int Rows { get { return this.grid.Rows; }}

        /// <summary>
        /// Number of columns that the inventory grid has.
        /// </summary>
        public int Columns { get { return this.grid.Columns; }}
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the inventory grid.
        /// </summary>
        protected Matrix<GridItem<T>> grid;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="rows">Number of rows that the inventory grid has.</param>
        /// <param name="columns">Number of columns that the inventory grid has.</param>
        public GridInventory(int rows, int columns) {
            this.grid = new Matrix<GridItem<T>>(rows, columns, null);
        }
        #endregion

        #region Getters
        // TODO: GET ITEM BY ID OR NAME
        // TODO: GET FREE POSITION

        /// <summary>
        /// Gets the item in the given position.
        /// </summary>
        /// <param name="position">Position of the item.</param>
        /// <returns>Item in the given position or <c>null</c> if it does not exist.</returns>
        public GridItem<T> GetItem(IntPosition position) {
            if (this.grid.IsValidPosition(position)) {
                return this.grid[position.Row, position.Column];
            }
            return null;
        }

        /// <summary>
        /// Gets the number of total slots of the inventory grid.
        /// </summary>
        /// <returns>Number of total slots of the inventory grid.</returns>
        public int GetTotalSlots() {
            return this.Rows * this.Columns;
        }

        /// <summary>
        /// Gets the number of free (unoccupied) slots of the inventory grid.
        /// </summary>
        /// <returns></returns>
        public int GetFreeSlots() {
            int count = 0;
            for (int i = 0; i < this.Rows; ++i) {
                for (int j = 0; j < this.Columns; ++j) {
                    if (this.grid[i, j] == null)
                        ++count;
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the number of occupied slots of the inventory grid.
        /// </summary>
        /// <returns>Number of occupied slots of the inventory grid.</returns>
        public int GetOccupiedSlots() {
            return this.GetTotalSlots() - this.GetFreeSlots();
        }
        #endregion

        #region Setters
        /// <summary>
        /// Adds one unity of the given item data to the inventory at the specified position.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="position">Position where the item will be placed.</param>
        /// <returns>Boolean indicating if the item was added.</returns>
        public bool AddItem(T itemData, IntPosition position) {
            return this.AddItem(itemData, position, 1);
        }

        /// <summary>
        /// Adds the given item data to the inventory at the specified position, according to the given amount of items.
        /// The items are only added if the entire amount of items can be added.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        /// <param name="position">Position where the item will be placed.</param>
        /// <param name="amount">Amount of items to be added.</param>
        /// <returns>Boolean indicating if the item was added.</returns>
        public bool AddItem(T itemData, IntPosition position, int amount) {
            if (itemData == null || position == null || amount < 0) return false;
            if (!this.grid.IsValidPosition(position)) return false;

            GridItem<T> item = this.GetItem(position);

            if (item == null) {  // Empty cell
                if (!this.CheckAmountCondition(itemData, amount)) return false;

                item = new GridItem<T>(itemData, position, amount);
                if (!this.AddItemToGrid(item)) return false;
            } else {  // Occupied cell
                if (!item.ItemData.Equals(itemData)) return false;  // Can't increase a different item
                if (!this.CheckAmountCondition(itemData, item.Count + amount)) return false;  // Overflows the maximum count

                item.Increment(amount);
            }

            this.UpdateEventHandlers();
            return true;
        }

        /// <summary>
        /// Moves the given existing item to the specified position.
        /// </summary>
        /// <param name="item">Existing item.</param>
        /// <param name="newPosition">New position of the item.</param>
        /// <returns>Boolean indicating if the item was moved.</returns>
        public bool MoveItem(GridItem<T> item, IntPosition newPosition) {
            GridItem<T> newItem = new GridItem<T>(item.ItemData, newPosition, item.Count);
            if (!this.AddItemToGrid(newItem)) return false;

            this.RemoveItemFromGrid(item);
            return true;
        }

        /// <summary>
        /// Removes one unity of the item at the given position from the inventory.
        /// </summary>
        /// <param name="position">Position of the item.</param>
        /// <returns>Reference to the item information.</returns>
        public GridItem<T> RemoveItem(IntPosition position) {
            return this.RemoveItem(position, 1);
        }

        /// <summary>
        /// Removes the item at the given position from the inventory, according to the given amount of items.
        /// </summary>
        /// <param name="position">Position of the item.</param>
        /// <param name="amount">Amount of items to be removed.</param>
        /// <returns>Reference to the item information.</returns>
        public GridItem<T> RemoveItem(IntPosition position, int amount) {
            if (position == null || amount < 0) return null;
            if (!this.grid.IsValidPosition(position)) return null;

            GridItem<T> item = this.GetItem(position);

            if (item != null) {
                item.Decrement(amount);
                if (item.Count == 0)
                    this.RemoveItemFromGrid(item);

                this.UpdateEventHandlers();
                return item;
            }
            return null;
        }

        /// <summary>
        /// Removes all the items from the inventory.
        /// </summary>
        public void Clear() {
            this.grid.Fill(null);
            this.UpdateEventHandlers();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Adds the given item to the grid.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        /// <returns>Boolean indicating if the item was added.</returns>
        private bool AddItemToGrid(GridItem<T> item) {
            if (!this.CanBePlaced(item, item.Position)) return false;

            return this.PlaceItem(item, item);
        }

        /// <summary>
        /// Removes the given item from the grid.
        /// </summary>
        /// <param name="item">Item to be removed.</param>
        /// <returns>Boolean indicating if the item was removed.</returns>
        private bool RemoveItemFromGrid(GridItem<T> item) {
            return this.PlaceItem(item, null);
        }

        /// <summary>
        /// Places the given item in inventory with the given value.
        /// </summary>
        /// <param name="item">Item that contains the position and size.</param>
        /// <param name="value">Value to be placed in the inventory.</param>
        /// <returns>Boolean indicating if the item was added.</returns>
        private bool PlaceItem(GridItem<T> item, GridItem<T> value) {
            if (!this.CheckInsideGrid(item, item.Position)) return false;

            for (int i = 0; i < item.ItemData.VerticalSize; ++i) {
                for (int j = 0; j < item.ItemData.HorizontalSize; ++j) {
                    this.grid[item.Position.Row + i, item.Position.Column + j] = value;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the given item can be placed at the given position.
        /// It only can be placed if there are no other items at that positions.
        /// </summary>
        /// <param name="item">Item to be checked.</param>
        /// <param name="position">Position to be checked.</param>
        /// <returns>Boolean indicating if the given item can be placed at the given position.</returns>
        private bool CanBePlaced(GridItem<T> item, IntPosition position) {
            for (int i = 0; i < item.ItemData.VerticalSize; ++i) {
                for (int j = 0; j < item.ItemData.HorizontalSize; ++j) {
                    if (this.grid[position.Row + i, position.Column + j] != null) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the given item, when placed in the given position, fits inside the grid.
        /// </summary>
        /// <param name="item">Item to be checked.</param>
        /// <param name="position">Position to be checked.</param>
        /// <returns>Boolean indicating if the given item, when placed in the given position, fits inside the grid.</returns>
        private bool CheckInsideGrid(GridItem<T> item, IntPosition position) {
            IntPosition currentPosition = new IntPosition();
            for (int i = 0; i < item.ItemData.VerticalSize; ++i) {
                currentPosition.Row = position.Row + i;
                for (int j = 0; j < item.ItemData.HorizontalSize; ++j) {
                    currentPosition.Column = position.Column + j;
                    if (!this.grid.IsValidPosition(currentPosition)) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the amount condition is valid.
        /// </summary>
        /// <param name="itemData">Item to be checked.</param>
        /// <param name="amount">Amount of items.</param>
        /// <returns>Boolean indicating if the amount condition is valid.</returns>
        private bool CheckAmountCondition(T itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }
        
        /// <summary>
        /// Updates the event handlers.
        /// </summary>
        private void UpdateEventHandlers() {
            this.OnUpdateInventory?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
