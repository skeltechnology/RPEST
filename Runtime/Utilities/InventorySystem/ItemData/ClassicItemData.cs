using UnityEngine;

namespace SkelTech.RPEST.Utilities.InventorySystem {
    /// <summary>
    /// <c>ScriptableObject</c> that contains information of a classic item.
    /// </summary>
    public class ClassicItemData : ScriptableObject {
        #region Properties
        /// <summary>
        /// Id of the item data. Must be unique.
        /// </summary>
        public int Id { get { return this.id; }}

        /// <summary>
        /// Name of the item.
        /// </summary>
        public string ItemName { get { return this.itemName; }}

        /// <summary>
        /// Maximum amount of items that can be stashed.
        /// </summary>
        public int MaximumCount { get { return this.maximumCount; }}
        #endregion

        #region Fields
        /// <summary>
        /// Id of the item data. Must be unique.
        /// </summary>
        [SerializeField] private int id;

        /// <summary>
        /// Name of the item.
        /// </summary>
        [SerializeField] private string itemName;

        /// <summary>
        /// Maximum amount of items that can be stashed.
        /// </summary>
        [SerializeField, Min(1)] private int maximumCount = 999999999;
        #endregion

        #region Helpers
        /// <summary>
        /// Checks if the given item data has the same id.
        /// </summary>
        /// <param name="itemData">Other item data.</param>
        /// <returns>Boolean indicating if the given item data has the same id.</returns>
        public bool Equals(ClassicItemData itemData) {
            return this.Id == itemData.Id;
        }
        #endregion
    }
}
