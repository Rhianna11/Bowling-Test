using BowlingTest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BowlingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Documents\BowlingTest\Bin\Games.json";

            string jsonString = File.ReadAllText(path);

            List<Session> games = JsonConvert.DeserializeObject<List<Session>>(jsonString);


        }
    }
}
