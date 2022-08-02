namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Base interface for classes that have a "Select Implementation" structure.
    /// </summary>
    /// <typeparam name="T">Implementation type.</typeparam>
    public interface SelectImplementation<T> {
        #region Setters
        public void AddImplementation(T implementation);
        #endregion
    }
}
