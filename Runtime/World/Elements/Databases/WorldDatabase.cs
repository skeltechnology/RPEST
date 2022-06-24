using System.Collections.Generic;

namespace SkelTech.RPEST.World.Database {
    public class WorldDatabase<T> {
        #region Fields
        protected ICollection<T> database;
        #endregion

        #region Constructors
        public WorldDatabase() : this(new LinkedList<T>()) {}

        public WorldDatabase(ICollection<T> database) {
            this.database = database;
        }
        #endregion

        #region Setters
        public virtual void Add(T item) {
            this.database.Add(item);
        }

        public virtual void Add(T[] items) {
            foreach (T item in items)
                this.Add(item);
        }
        
        public virtual void Remove(T item) {
            this.database.Remove(item);
        }
        #endregion
    }
}
