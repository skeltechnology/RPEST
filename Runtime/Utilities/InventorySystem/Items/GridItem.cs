using SkelTech.RPEST.Utilities.Structures;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    // TODO: DOCUMENTATION
    public class GridItem<T> : ClassicItem<T> where T : GridItemData {
        #region Properties
        public IntPosition Position { get; private set; }
        #endregion

        #region Constructors
        public GridItem(T itemData, IntPosition position) : this(itemData, position, 0) {}
        public GridItem(T itemData, IntPosition position, int count) : base(itemData, count) {
            this.Position = position;
        }
        #endregion
    }
}
