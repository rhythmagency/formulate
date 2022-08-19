namespace Formulate.BackOffice.Utilities
{

    /// <summary>
    /// A contract for creating a utility to get the expected folder icon or the default folder icon based.
    /// </summary>
    public interface IGetFolderIconOrDefault
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="input">The folder icon.</param>
        /// <returns>A <see cref="string"/></returns>
        string GetFolderIcon(TreeRootTypes input);
    }
}
