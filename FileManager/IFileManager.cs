using System.Threading.Tasks;

namespace Dna
{
    /// <summary>
    /// Handles reading/writing and querying the file system
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Writes all text to a file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="text">Text to write to the file</param>
        /// <param name="append">Flag indicating whether to append or overwrite a file</param>
        /// <returns>Task for Continuation</returns>
        Task WriteTextToFileAsync(string text, string path, bool append = false);

        /// <summary>
        /// Normalizes a file path based on the current operating system
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string NormalizePath(string path);

        /// <summary>
        /// Resolves a path to an absolute path
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        string ResolvePath(string path);
    }
}
