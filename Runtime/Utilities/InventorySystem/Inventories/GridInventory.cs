using SkelTech.RPEST.Utilities.Structures;

using System;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class GridInventory<T>  where T : GridItemData {
        #region Events
        public event EventHandler OnUpdateInventory;
        #endregion

        #region Properties
        public int Rows { get { return this.grid.Rows; }}
        public int Columns { get { return this.grid.Columns; }}
        #endregion

        #region Fields
        protected Matrix<GridItem<T>> grid;
        #endregion

        #region Constructors
        public GridInventory(int rows, int columns) {
            this.grid = new Matrix<GridItem<T>>(rows, columns, null);
        }
        #endregion

        #region Getters
        public GridItem<T> GetItem(IntPosition position) {
            if (this.grid.IsValidPosition(position)) {
                return this.grid[position.Row, position.Column];
            }
            return null;
        }

        public int GetTotalSlots() {
            return this.Rows * this.Columns;
        }

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

        public int GetOccupiedSlots() {
            return this.GetTotalSlots() - this.GetFreeSlots();
        }
        #endregion

        #region Setters
        public bool AddItem(T itemData, IntPosition position) {
            return this.AddItem(itemData, position, 1);
        }

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

        public bool MoveItem(GridItem<T> item, IntPosition newPosition) {
            GridItem<T> newItem = new GridItem<T>(item.ItemData, newPosition, item.Count);
            if (!this.AddItemToGrid(newItem)) return false;

            this.RemoveItemFromGrid(item);
            return true;
        }

        public GridItem<T> RemoveItem(IntPosition position) {
            return this.RemoveItem(position, 1);
        }

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

        public void Clear() {
            this.grid.Fill(null);
            this.UpdateEventHandlers();
        }
        #endregion

        #region Helpers
        private bool AddItemToGrid(GridItem<T> item) {
            if (!this.CanBePlaced(item, item.Position)) return false;

            return this.PlaceItem(item, item);
        }

        private void RemoveItemFromGrid(GridItem<T> item) {
            this.PlaceItem(item, null);
        }

        private bool PlaceItem(GridItem<T> item, GridItem<T> value) {
            if (!this.CheckInsideGrid(item, item.Position)) return false;

            for (int i = 0; i < item.ItemData.VerticalSize; ++i) {
                for (int j = 0; j < item.ItemData.HorizontalSize; ++j) {
                    this.grid[item.Position.Row + i, item.Position.Column + j] = value;
                }
            }
            return true;
        }

        private bool CanBePlaced(GridItem<T> item, IntPosition position) {
            for (int i = 0; i < item.ItemData.VerticalSize; ++i) {
                for (int j = 0; j < item.ItemData.HorizontalSize; ++j) {
                    if (this.grid[position.Row + i, position.Column + j] != null) return false;
                }
            }
            return true;
        }

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

        private bool CheckAmountCondition(T itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }
        
        private void UpdateEventHandlers() {
            this.OnUpdateInventory?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
