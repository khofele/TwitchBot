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
            int randomCounter = GetRandom(1, 102);
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

        public string HypeCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 102);
            if(randomCounter == 101)
            {
                return "akatri2Party " + randomCounter + "% HYPE! " + User.GetUser(e) + " IS HYPING IN THE STRATOSPHERE! AYAYAYAYAYAYAYYYYYYYYY akatri2Party";
            }
            else if(randomCounter == 69)
            {
                return User.GetUser(e) + " YOU ARE " + randomCounter + "% HYPED! S P I C E HYPE! akatri2Party";
            }
            else if (randomCounter > 50)
            {
                if(randomCounter > 90)
                {
                    return "akatri2Party " + randomCounter+ "% HYPE! SHEEEEESH THE HYPE IS SO STRONG! " + User.GetUser(e) + " YOU'RE ABOUT TO EXPLODE! akatri2Party";
                }
                else
                {
                    return "akatri2Party  " + User.GetUser(e) + " is " + randomCounter + "% hyped today! THE HYPE IS STRONG! AYAYAYAYAYAY akatri2Party";
                }
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% hyped today!";
            }
        }

        public string NapCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 101);
            randomCounter = 1;
            if (randomCounter > 60)
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today! You better take a nap <3";
            }
            else if(randomCounter <= 10)
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today!.... WAIT YOU'RE NOT TIRED AT ALL BACK TO WORK :p <3";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today!";
            }
        }
    }
}
