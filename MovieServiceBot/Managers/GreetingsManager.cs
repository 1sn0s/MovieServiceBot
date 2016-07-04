using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieServiceBot.Managers
{
    public class GreetingsManager
    {
        static string currentGreeting;
        public GreetingsManager(string currentGreeting)
        {
            GreetingsManager.currentGreeting = currentGreeting;
        }

        public string GetEchoGreeting()
        {
            return $"{currentGreeting}";
        }
    }
}