using UnityEngine;

namespace SkelTech.RPEST.Utilities.Inventories {
    public class ItemData : ScriptableObject {
        #region Properties
        public int Id { get { return this.id; }}
        public string ItemName { get { return this.itemName; }}
        #endregion

        #region Fields
        [SerializeField] private int id;
        [SerializeField] private string itemName;
        #endregion
    }
}
