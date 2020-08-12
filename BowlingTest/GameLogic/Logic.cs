using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingTest.GameLogic
{ 
    public class Logic
    {
        public static bool IsDouble(string currThrow, string throw1)
        {
            if (currThrow == "X" && throw1 == "X")
            {
                return true;
            }

            return false;
        }

        public static bool IsTurkey(string currThrow, string throw1, string throw2)
        {
            if (currThrow == "X" && throw1 == "X" && throw2 == "X")
            {
                return true;
            }

            return false;
        }
    }
}
