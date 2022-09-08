using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class GridItemData : ClassicItemData {
        #region Properties
        public float HorizontalSize { get { return this.horizontalSize; }}
        public float VerticalSize { get { return this.verticalSize; }}
        #endregion

        #region Fields
        [SerializeField, Min(1)] private int horizontalSize = 1;
        [SerializeField, Min(1)] private int verticalSize = 1;
        #endregion
    }
}
