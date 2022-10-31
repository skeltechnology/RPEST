using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// <c>ClassicItemData</c> that also contains information of its weight.
    /// </summary>
    public class WeightedItemData : ClassicItemData {
        #region Properties
        /// <summary>
        /// Weight of the item data.
        /// </summary>
        public float Weight { get { return this.weight; }}
        #endregion

        #region Fields
        /// <summary>
        /// Weight of the item data.
        /// </summary>
        [SerializeField, Min(0)] private float weight;
        #endregion
    }
}
