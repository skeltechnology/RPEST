using SkelTech.RPEST.Utilities.Structures;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// Wrapper for <c>GridItemData</c> that contain essential information for the <c>GridInventory</c>.
    /// </summary>
    /// <typeparam name="T">Item data type.</typeparam>
    public class GridItem<T> : ClassicItem<T> where T : GridItemData {
        #region Properties
        /// <summary>
        /// Position of the item.
        /// </summary>
        public IntPosition Position { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class. Amount is set to zero.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        /// <param name="position">Position of the item.</param>
        public GridItem(T itemData, IntPosition position) : this(itemData, position, 0) {}

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="itemData">Reference to the <c>ItemData</c>.</param>
        /// <param name="position">Position of the item.</param>
        /// <param name="count">Amount of items.</param>
        public GridItem(T itemData, IntPosition position, int count) : base(itemData, count) {
            this.Position = position;
        }
        #endregion
    }
}
