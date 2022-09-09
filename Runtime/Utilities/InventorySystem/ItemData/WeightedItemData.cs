using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class WeightedItemData : ClassicItemData {
        #region Properties
        public float Weight { get { return this.weight; }}
        #endregion

        #region Fields
        [SerializeField, Min(0)] private float weight;
        #endregion
    }
}
