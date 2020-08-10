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

                int score = 0;
                for (var i = 0;  i < session.Throws.Count; i++)
                {

                    switch (session.Throws[i])
                    {
                        case "X":
                            score += 10 - Convert.ToInt32(session.Throws[i + 1]) + Convert.ToInt32(session.Throws[i + 2]);
                            break;
                        case "/":
                            score += 10 - Convert.ToInt32(session.Throws[i - 1]) + Convert.ToInt32(session.Throws[i + 1]);
                            break;
                        case "-":
                           
                            break;
                        case "F":
                            
                            break;
                        default:
                            score += Convert.ToInt32(session.Throws[i]);                           
                            break;
                    }
                }

                Console.WriteLine(score);
            }
            Console.ReadLine();
        }
    }
}
