using BeteasyTest;
using System.IO;
using Xunit;

namespace BeteasyTest.Test
{
    public class XmlFileTest
    {
        [Fact]
        public void GetHorseFromXmlFile()
        {
            var xmlFileParser = new XmlFileParser();
            string path = "TestData/Caulfield_Race1.xml";
            var horses = xmlFileParser.GetParticipantHorses(path);
            Assert.NotNull(horses);
            Assert.True(horses.Count > 0);
        }

       
        [Fact]
        public void GetHorseFromXmlFileNotExcetpion()
        {
            var xmlFileParser = new XmlFileParser();
            string path = "TestData/xyz.xml";
            Assert.Throws<FileNotFoundException>(() => xmlFileParser.GetParticipantHorses(path));
        }

    }
}
