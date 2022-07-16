using SkelTech.RPEST.World.Elements;

namespace SkelTech.RPEST.World.Database {
    /// <summary>
    /// Class for managing <c>WalkableTilemap</c>s.
    /// </summary>
    public class WalkableTilemapDatabase : WorldDatabase<WalkableTilemap> {
        #region Setters
        public override void Add(WalkableTilemap walkableTilemap) {
            walkableTilemap.gameObject.SetActive(false);
            this.database.Add(walkableTilemap);
        }
        #endregion
    }
}
