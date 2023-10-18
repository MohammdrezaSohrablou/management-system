using System;
using System.IO;

namespace AO
{
    public class KeyboardLock
    {
        public static string GetYesNoAnswer()
        {
            string answer = "";

            while (answer.ToUpper() != "Y" && answer.ToUpper() != "N")
            {
                Console.WriteLine("enter Y/N !");
                answer = Console.ReadLine();
            }

            return answer;
        }
    }
}
