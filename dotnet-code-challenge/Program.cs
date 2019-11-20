using System;
using System.IO;
using System.Collections.Generic;

namespace BeteasyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Display Horses By Price Ascending");

            var paths = new List<string>();
            paths.Add(@"FeedData\Caulfield_Race1.xml");
            paths.Add(@"FeedData\Wolferhampton_Race1.json");

            try
            {
                var readFiles = new ParserStyle();
                foreach (var path in paths)
                {
                    var fileParse = readFiles.GetFileParser(path);
                    if (fileParse != null)
                    {
                        var horses = fileParse.GetParticipantHorses(path);
                        fileParse.DisplayHorses(horses);
                    }
                    else
                    {
                        Console.WriteLine("Invalid File or Invalid Path");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
