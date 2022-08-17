using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class ItemData : ScriptableObject {
        #region Properties
        public int Id { get { return this.id; }}
        public string ItemName { get { return this.itemName; }}
        public int MaximumCount { get { return this.maximumCount; }}
        #endregion

        #region Fields
        [SerializeField] private int id;
        [SerializeField] private string itemName;
        [SerializeField] private int maximumCount;
        #endregion
    }
}
