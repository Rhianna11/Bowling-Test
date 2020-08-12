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

                jsonString = jsonString.Replace("-", "0");
                jsonString = jsonString.Replace("F", "0");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(jsonString);

            foreach (var session in sessions)
            {
                Console.WriteLine(session.GameId);

                int score = 0, throwCount = 1, frameCount = 1; 
                bool isTurkey = false, isDouble = false;

                for (var i = 0;  i < session.Throws.Count; i++)
                {
                    if (throwCount == 2)
                    {
                        throwCount = 1;
                        frameCount += 1;
                    }

                    switch (session.Throws[i])
                    {
                        case "X":
                            score += 10;

                                isTurkey = GameLogic.Logic.IsTurkey(
                                    session.Throws[i],
                                    session.Throws[i + 1],
                                    session.Throws[i + 2]);

                                if (!isTurkey)
                                {
                                    isDouble = GameLogic.Logic.IsDouble(
                                    session.Throws[i],
                                    session.Throws[i + 1]);
                                }

                                if (isDouble)
                                {
                                    score += 10 + Convert.ToInt32(session.Throws[i + 2]);
                                }

                                else if (isTurkey)
                                {
                                    score += 20;
                                }

                                else if (session.Throws[i + 2] == "/")
                                {
                                    score += 10;
                                }
                                else
                                {
                                    score += Convert.ToInt32(session.Throws[i + 1]) + Convert.ToInt32(session.Throws[i + 2]);
                                }

                            break;

                        case "/":
                            score += 10 - Convert.ToInt32(session.Throws[i - 1]) + Convert.ToInt32(session.Throws[i + 1]);
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
