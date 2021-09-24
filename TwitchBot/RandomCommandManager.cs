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

        public int GetRandom()
        {
            random = new Random();
            return random.Next(1, 100);
        }

        public string SpiceCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom();
            if(randomCounter > 70)
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
