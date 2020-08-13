using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingTest.GameLogic
{ 
    public class Logic
    {
        //check current and consecutive throw to see if it's a Double
        public static bool IsDouble(string currThrow, string throw1)
        {
            if (currThrow == "X" && throw1 == "X")
            {
                return true;
            }

            return false;
        }

        //check current and consecutive 2 throws to see if it's a Turkey
        public static bool IsTurkey(string currThrow, string throw1, string throw2)
        {
            if (currThrow == "X" && throw1 == "X" && throw2 == "X")
            {
                return true;
            }

            return false;
        }

        //If current throw is strike then check second throw of next frame for spare
        public static bool isStrikeThenSpare(string throw2)
        {
            if (throw2 == "/")
            {
                return true;
            }

            return false;
        }

        public static bool IsOutOfIndexForStrike(int numOfThrows, int listLength)
        {
            //Minus length of list from num of throws to see how many indexes are left for strike
            if (listLength - numOfThrows <= 2)
            {
                return true;
            }

            return false;
        }

        public static bool IsOutOfIndexForSpare(int numOfThrows, int listLength)
        {
            //Minus length of list from num of throws to see how many indexes are left for psare
            if (listLength - numOfThrows <= 1)
            {
                return true;
            }

            return false;
        }
    }
}
