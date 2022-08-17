using UnityEngine;

namespace SkelTech.RPEST.Utilities.Inventories {
    public class WeightedItemData : ItemData {
        #region Properties
        public float Weight { get { return this.weight; }}
        #endregion

        #region Fields
        [SerializeField] private float weight;
        #endregion
    }
}
