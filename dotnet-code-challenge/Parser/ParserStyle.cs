using System.IO;

namespace BeteasyTest
{
    public class ParserStyle
    {
        /// <summary>
        /// Gets the file parser style.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>FileParser object</returns>
        public FileParser GetFileParser(string filePath)
        {
            FileParser fileParser = null;
            var extension = Path.GetExtension(filePath);

            if (extension == ".xml")
            {
                fileParser = new XmlFileParser();
            }
            else if (extension == ".json")
            {
                fileParser = new JsonFileParser();

            }
            return fileParser;           
        }
    }
}
