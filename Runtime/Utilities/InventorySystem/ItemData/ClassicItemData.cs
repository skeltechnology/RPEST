using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    public class ClassicItemData : ScriptableObject {
        #region Properties
        public int Id { get { return this.id; }}
        public string ItemName { get { return this.itemName; }}
        public int MaximumCount { get { return this.maximumCount; }}
        #endregion

        #region Fields
        [SerializeField] private int id;
        [SerializeField] private string itemName;
        [SerializeField, Min(1)] private int maximumCount = 999999999;
        #endregion

        #region Helpers
        public bool Equals(ClassicItemData itemData) {
            return this.Id == itemData.Id;
        }
        #endregion
    }
}
