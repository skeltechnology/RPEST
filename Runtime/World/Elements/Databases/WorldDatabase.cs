using System;
using System.Collections.Generic;

namespace SkelTech.RPEST.World.Database {
    /// <summary>
    /// Generic class for managing a world database.
    /// </summary>
    /// <typeparam name="T">Database type.</typeparam>
    public class WorldDatabase<T> {
        #region Fields
        /// <summary>
        /// Collection of database elements.
        /// </summary>
        protected ICollection<T> database;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public WorldDatabase() : this(new LinkedList<T>()) {}

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="database">Collection of database elements.</param>
        public WorldDatabase(ICollection<T> database) {
            this.database = database;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Adds an element to the databse.
        /// </summary>
        /// <param name="item">Element to be added</param>
        public virtual void Add(T item) {
            this.database.Add(item);
        }

        /// <summary>
        /// Adds the given elements to the database.
        /// </summary>
        /// <param name="items">Array of elements.</param>
        public virtual void Add(T[] items) {
            foreach (T item in items)
                this.Add(item);
        }
        
        /// <summary>
        /// Removes the given element from the database.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item) {
            this.database.Remove(item);
        }
        #endregion

        #region Helpers
        protected static A GetFirst<A>(ICollection<A> collection, Func<A, bool> evaluator) where A : class {
            foreach (A element in collection) {
                if (evaluator.Invoke(element))
                    return element;
            }
            return null;
        }

        protected static ICollection<A> GetAll<A>(ICollection<A> collection, Func<A, bool> evaluator) where A : class {
            ICollection<A> result = new LinkedList<A>();

            foreach (A element in collection) {
                if (evaluator.Invoke(element))
                    result.Add(element);
            }
            return result;
        }
        #endregion
    }
}
