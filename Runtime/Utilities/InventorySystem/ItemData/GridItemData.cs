using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// <c>ClassicItemData</c> that also contains information of its size.
    /// </summary>
    public class GridItemData : ClassicItemData {
        #region Properties
        /// <summary>
        /// Horizontal size of the item data.
        /// </summary>
        public int HorizontalSize { get { return this.horizontalSize; }}

        /// <summary>
        /// Vertical size of the item data.
        /// </summary>
        public int VerticalSize { get { return this.verticalSize; }}
        #endregion

        #region Fields
        /// <summary>
        /// Horizontal size of the item data.
        /// </summary>
        [SerializeField, Min(1)] private int horizontalSize = 1;

        /// <summary>
        /// Vertical size of the item data.
        /// </summary>
        [SerializeField, Min(1)] private int verticalSize = 1;
        #endregion
    }
}
