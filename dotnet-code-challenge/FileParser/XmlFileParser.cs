using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BeteasyTest
{
    /// <summary>
    /// Xml file parser strategy class
    /// </summary>
    public class XmlFileParser : FileParser
    {
        /// <summary>
        /// Gets the participant horses.
        /// </summary>
        /// <param name="path">The XML file path.</param>
        /// <returns>List of participant horses</returns>
        public override List<ParticipantHorse> GetParticipantHorses(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new List<ParticipantHorse>();
            }
            XmlSerializer xs = new XmlSerializer(typeof(CaulfieldRaceModel));
           
                using (StreamReader reader = new StreamReader(path))
                {
                    var deserialize = (CaulfieldRaceModel)xs.Deserialize(reader);
                    RaceName = GetRaceName(deserialize);

                    var participatingHorses = new List<ParticipantHorse>();
                    //Get list of all the horses in the race.
                    foreach (var horse in GetHorseList(deserialize))
                    {
                        var h = new ParticipantHorse
                        {
                            Id = horse.Number,
                            Name = horse.Name,
                        };
                    participatingHorses.Add(h);
                    }
                    //Get price of each horse in the race.
                    foreach (var horse in GetHorseListWithPrice(deserialize))
                    {
                        var horseWithPrice = participatingHorses.Find(h => h.Id == horse?.Number);
                        horseWithPrice.Price = Convert.ToDouble(horse?.Price);
                    }

                    return participatingHorses;
                }          
           
        }

        /// <summary>
        /// Gets the name of the race.
        /// </summary>
        /// <param name="model">The CaulfieldRaceModel model.</param>
        /// <returns>Name of the race</returns>
        private string GetRaceName(CaulfieldRaceModel model)
        {
            return model.Track.Name;
        }

        /// <summary>
        /// Get the horse list
        /// </summary>
        /// <param name="model">The CaulfieldRaceModel model.</param>
        /// <returns>List of horses</returns>
        private List<Horse> GetHorseList(CaulfieldRaceModel model)
        {
            return model.Races.Race.Horses.HorseList;
        }

        /// <summary>
        /// Get the horse list with price.
        /// </summary>
        /// <param name="model">The CaulfieldRaceModel model.</param>
        /// <returns>List of horses with price</returns>
        private List<HorseWithPrice> GetHorseListWithPrice(CaulfieldRaceModel model)
        {
            return model.Races.Race.Prices.Price.HorsesWithPrice.HorseListWithPrice;
        }
    }
}
