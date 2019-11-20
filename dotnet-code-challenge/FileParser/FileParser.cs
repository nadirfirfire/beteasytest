using System;
using System.Collections.Generic;
using System.Linq;


namespace BeteasyTest
{
    public abstract class FileParser
    {
        public string RaceName { get; set; }

        public abstract List<ParticipantHorse> GetParticipantHorses(string path);

        /// <summary>
        /// Displays the horses.
        /// </summary>
        /// <param name="horses">The horses.</param>
        public void DisplayHorses(List<ParticipantHorse> horses)
        {
            var horsesOrderByPrice = horses.OrderBy(horse => horse.Price);
            
            Console.WriteLine("{0} Race ", RaceName);
            Console.WriteLine("***************************");

            foreach (var horse in horsesOrderByPrice)
            {
                Console.WriteLine("Horse: {0}; Price: {1}", horse.Name, horse.Price);
            }
            Console.WriteLine("***************************");
        }
    }
}
