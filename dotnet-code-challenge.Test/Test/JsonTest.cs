using BeteasyTest;
using System.IO;
using Xunit;

namespace BeteasyTest.Test
{
   public class JsonTest
    {
        [Fact]
        public void GetHorseFromJsonFile()
        {
            var jsonFileParser = new JsonFileParser();
            string path = "TestData/Wolferhampton_Race1.json";
            var horses = jsonFileParser.GetParticipantHorses(path);
            Assert.NotNull(horses);
            Assert.Equal(2, horses.Count);
        }
      
        [Fact]
        public void GetHorseFromJsonFileNotExcetpion()
        {
            var strategy = new XmlFileParser();
            string path = "TestData/xyz.json";
            Assert.Throws<FileNotFoundException>(() => strategy.GetParticipantHorses(path));
        }
    }
}
