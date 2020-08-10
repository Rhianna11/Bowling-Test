using BowlingTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BowlingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = "";
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"Games.json");

                jsonString = File.ReadAllText(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(jsonString);

            foreach (var session in sessions)
            {
                Console.WriteLine(session.GameId);

                for (var i = 0;  i < session.Throws.Count; i++)
                {

                }
            }
            Console.ReadLine();
        }
    }
}
