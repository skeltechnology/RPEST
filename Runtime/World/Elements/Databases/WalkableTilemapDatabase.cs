using SkelTech.RPEST.World.Elements;
using SkelTech.RPEST.World.Elements.Objects;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class WalkableTilemapDatabase : WorldDatabase<WalkableTilemap> {
        #region Setters
        public override void Add(WalkableTilemap walkableTilemap) {
            walkableTilemap.gameObject.SetActive(false);
            this.database.Add(walkableTilemap);
        }
        #endregion
    }
}
