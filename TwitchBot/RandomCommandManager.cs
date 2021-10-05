using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class RandomCommandManager
    {
        private Random random = null;

        public int GetRandom(int min, int max)
        {
            random = new Random();
            return random.Next(min, max);
        }

        public string SpiceCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 101);
            if(randomCounter == 69)
            {
                return "( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) " + User.GetUser(e) + " is " + randomCounter + "% spicy today! ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°)";
            }
            else if(randomCounter == 101)
            {
                return User.GetUser(e) + " is " + randomCounter + "% spicy today! THE SPICE IS MASSIVE! WE BOW DOWN! YOU ARE A SPICEGOD!";
            }
            else if(randomCounter > 70)
            {
                return User.GetUser(e) + " is " + randomCounter + "% spicy today! THE SPICE IS STRONG!";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% spicy today!";
            }
        }
    }
}
