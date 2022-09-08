using SkelTech.RPEST.Utilities.Structures;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class GridInventory<T>  where T : GridItemData {
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
                this.grid[position.Row, position.Column] = item;
            } else {  // Occupied cell
                if (!item.ItemData.Equals(itemData)) return false;  // Can't increase a different item
                if (!this.CheckAmountCondition(itemData, item.Count + amount)) return false;  // Overflows the maximum count

                item.Increment(amount);
            }

            return true;
        }

        public bool SwapItems(IntPosition position1, IntPosition position2) {
            return this.grid.Swap(position1, position2);
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
                    this.ClearItem(item);
                return item;
            }
            return null;
        }

        public void Clear() {
            this.grid.Fill(null);
        }
        #endregion

        #region Helpers
        private bool CheckAmountCondition(T itemData, int amount) {
            return amount <= itemData.MaximumCount;
        }

        private void ClearItem(GridItem<T> item) {
            this.grid[item.Position.Row, item.Position.Column] = null;
        }
        #endregion
    }
}
