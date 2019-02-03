using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using static Dna.DI;


namespace Dna
{
    /// <summary>
    /// Handles reading/writing and querying the file system
    /// </summary>
    public class BaseFileManager : IFileManager
    {


        /// <summary>
        /// Writes all text to a file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="text">Text to write to the file</param>
        /// <param name="append">Flag indicating whether to append or overwrite a file</param>
        /// <returns>Task for Continuation</returns>
        public async Task WriteTextToFileAsync(string text, string path, bool append = false)
        {
            //TODO: Add exception catching

            path = NormalizePath(path);
            path = ResolvePath(path);


            //path = path.Replace("/", "\\").Trim();

            //Lock the task
            await AsyncAwaiter.AwaitAsync(nameof(BaseFileManager) + path, async () =>
             {
                 //Run the synchronous file access as a new task
                 await TaskManager.Run(() =>
                 {
                     //Write the log message to file
                     using (var fileStream = (TextWriter)new StreamWriter(File.Open(path, append ? FileMode.Append : FileMode.Create)))
                     {
                         fileStream.Write(text);
                     }
                 });
             });
        }

        /// <summary>
        /// Normalizes a file path based on operating system
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        public string NormalizePath(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return path?.Replace('/', '\\').Trim();            
            else
                return path?.Replace('\\', '/').Trim();
        }


        /// <summary>
        /// Resolves a path to an absolute path
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        public string ResolvePath(string path)
        {
            return Path.GetFullPath(path);
        }
    }
}
