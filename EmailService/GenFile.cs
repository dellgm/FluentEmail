using System.IO;

namespace EmailService
{
    public class GenFile
    {
        public string FileName { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public string MediaType { get; set; }

        /// <summary>
        /// Get mediaTypes in MediaTypeNames.Application
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="memoryStream"></param>
        /// <param name="mediaType"></param>
        public GenFile(string fileName, MemoryStream memoryStream, string mediaType)
        {
            FileName = fileName;
            MemoryStream = memoryStream;
            MediaType = mediaType;
        }
    }
}
