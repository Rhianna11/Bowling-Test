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
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Games.json";

                jsonString = File.ReadAllText(path);

                //Replace miss and foul chars with a zero score
                jsonString = jsonString.Replace("-", "0");
                jsonString = jsonString.Replace("F", "0");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            //Deserialize JSON
            List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(jsonString);
        
            foreach (var session in sessions)
            {
                Console.WriteLine(session.GameId);

                int score = 0, throwCount = 0, frameCount = 1;
                bool isTurkey = false, isDouble = false, isStrikeThenSpare = false, isOutOfIndexForStrike = false, isOutOfIndexForSpare = false, isTenthFrame = false;

                //For loop for counting each throw
                for (var i = 0;  i < session.Throws.Count; i++)
                {
                    throwCount++;

                    //Set bool to true when framecount is ten
                    if (frameCount == 10)
                    {
                        isTenthFrame = true;
                    }

                    // Switch case for session throws
                    switch (session.Throws[i])
                    { 
                        case "X":                                                   
                            //Check if next two indexes for current Strike can be accessed
                            isOutOfIndexForStrike = GameLogic.Logic.IsOutOfIndexForStrike(i, session.Throws.Count);

                            //If index is not out of range
                            if (!isOutOfIndexForStrike)
                            {                               
                                    score += 10;

                                    //Pass current throw and next two throws to method
                                    isTurkey = GameLogic.Logic.IsTurkey(
                                        session.Throws[i],
                                        session.Throws[i + 1],
                                        session.Throws[i + 2]);

                                    //Pass second throw to method
                                    isStrikeThenSpare = GameLogic.Logic.isStrikeThenSpare
                                        (session.Throws[i + 2]);

                                    //Check to see if Turkey (three consecutive Strikes)
                                    if (!isTurkey)
                                    {
                                        isDouble = GameLogic.Logic.IsDouble(
                                        session.Throws[i],
                                        session.Throws[i + 1]);
                                    }
                                    //Check if current throw and next throw are Strikes
                                    if (isDouble)
                                    {
                                        score += 10 + Convert.ToInt32(session.Throws[i + 2]);
                                    }
                                    //If Turkey is true then add 20 to score
                                    else if (isTurkey)
                                    {
                                        score += 20;
                                    }
                                    //If there is a Spare after the current strike
                                    else if (isStrikeThenSpare)
                                    {
                                        score += 10;
                                    }
                                    //If non of the bools are satisfied then add the next two throws as normal
                                    else
                                    {
                                        score += Convert.ToInt32(session.Throws[i + 1]) + Convert.ToInt32(session.Throws[i + 2]);
                                    }
                            }

                            break;

                            
                        case "/":
                            //Check if next two indexes for current Spare can be accessed
                            isOutOfIndexForSpare = GameLogic.Logic.IsOutOfIndexForSpare(i, session.Throws.Count);

                            //If index is not out of range
                            if (!isOutOfIndexForSpare)
                            {
                                score += 10 - Convert.ToInt32(session.Throws[i - 1]) + Convert.ToInt32(session.Throws[i + 1]);
                            }
                            break;

                            //If normal throw (digits)
                        default:
                            if (i > 2 && isTenthFrame)
                            {
                                //Check to see if last frame was not a Strike to avoid overcounting score
                                if (session.Throws[i - 2] != "X")
                                {
                                    score += Convert.ToInt32(session.Throws[i]);
                                }
                            }
                            else
                            {
                                score += Convert.ToInt32(session.Throws[i]);
                            }
                            break;

                    }
                    //If current throw is Strike, increment framecount
                    if (session.Throws[i] == "X")
                    {
                        throwCount = 0;
                        frameCount++;
                    }
                    //If throwcount is 2 then set to zero and incrememnt framecount
                    if (throwCount == 2)
                    {
                        throwCount = 0;
                        frameCount += 1;
                    }                   
                }

                Console.WriteLine(score);
            }
            Console.ReadLine();

           
        }   
    }
}
