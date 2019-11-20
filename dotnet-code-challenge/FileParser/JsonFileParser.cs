using BeteasyTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BeteasyTest
{
    /// <summary>
    /// JSON File Parser
    /// </summary>
    /// <seealso cref="dotnet_code_challenge.Strategy.FileParseStrategy" />
    public class JsonFileParser : FileParser
    {
        /// <summary>
        /// Gets the participant horses.
        /// </summary>
        /// <param name="path">The JSON file path.</param>
        /// <returns>List of participant horses</returns>
        public override List<ParticipantHorse> GetParticipantHorses(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new List<ParticipantHorse>();
            }
            using (StreamReader file = File.OpenText(path))
                {
                    var jsonSerializer = new JsonSerializer();
                    var raceResult = (WolferhamptonRaceModel)jsonSerializer.Deserialize(file, typeof(WolferhamptonRaceModel));

                    RaceName = GetRaceName(raceResult);

                    var horses = new List<ParticipantHorse>();
                    //Get all the participating horses.
                    foreach (var participants in GetHorseRaceParticipants(raceResult))
                    {
                        var horse = new ParticipantHorse
                        {
                            Id = participants.Id.ToString(),
                            Name = participants.Name
                        };

                        horses.Add(horse);
                    }
                    //Get price of the participating horse.
                    foreach (var market in MarketList(raceResult))
                    {
                        foreach (var selection in market.Selections)
                        {
                            var horse = horses.Find(h => h.Id == selection.Tags.participant);
                            horse.Price = selection.Price;
                            
                        }
                    }
                    return horses;
                }
            
            
        }

        /// <summary>
        /// Gets the name of the race.
        /// </summary>
        /// <param name="raceResult">WolferhamptonRaceModel model.</param>
        /// <returns>Name of the race</returns>
        private string GetRaceName(WolferhamptonRaceModel model)
        {
            return model.RawData.FixtureName;
        }

        /// <summary>
        /// Gets the race participants.
        /// </summary>
        /// <param name="raceResult">WolferhamptonRaceModel model.</param>
        /// <returns>list of participants</returns>
        private List<Participant> GetHorseRaceParticipants(WolferhamptonRaceModel model)
        {
            return model.RawData.Participants;
        }

        /// <summary>
        /// Gets the list of markets after null checks.
        /// </summary>
        /// <param name="raceResult">WolferhamptonRaceModel model.</param>
        /// <returns>list of market</returns>
        private List<Market> MarketList(WolferhamptonRaceModel model)
        {
            return model.RawData.Markets;
        }
    }
}
