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
            if (randomCounter == 69)
            {
                return "( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) " + User.GetUser(e) + " is " + randomCounter + "% spicy today! ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°)";
            }
            else if (randomCounter == 101)
            {
                return User.GetUser(e) + " is " + randomCounter + "% spicy today! THE SPICE IS MASSIVE! WE BOW DOWN! YOU ARE A SPICEGOD!";
            }
            else if (randomCounter > 70)
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
            if (e.Command.ChatMessage.DisplayName == "karomagkekse")
            {
                return "@karomagkekse NUMBER ONE HYPE GIRL IS IN THE HOUSE! AYAYAYAYY KARO IS 1069% HYPED AND ABOUT TO EXPLODE AYYYYYYYYYYYYY! akatri2Party";
            }
            else if (randomCounter == 101)
            {
                return "akatri2Party " + randomCounter + "% HYPE! " + User.GetUser(e) + " IS HYPING IN THE STRATOSPHERE! AYAYAYAYAYAYAYYYYYYYYY akatri2Party";
            }
            else if (randomCounter == 69)
            {
                return User.GetUser(e) + " YOU ARE " + randomCounter + "% HYPED! S P I C E HYPE! akatri2Party";
            }
            else if (randomCounter > 50)
            {
                if (randomCounter > 90)
                {
                    return "akatri2Party " + randomCounter + "% HYPE! SHEEEEESH THE HYPE IS SO STRONG! " + User.GetUser(e) + " YOU'RE ABOUT TO EXPLODE! akatri2Party";
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
            if (randomCounter > 60)
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today! You better take a nap <3";
            }
            else if (randomCounter <= 10)
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today!.... WAIT YOU'RE NOT TIRED AT ALL BACK TO WORK :p <3";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% tired today!";
            }
        }

        public string LoveCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 101);
            if (e.Command.ChatMessage.DisplayName == "akaTripzz")
            {
                if (randomCounter == 69)
                {
                    return "Jódete el amor Mike aka FUCK YOUR LOVE MIKE! jk jk we love you <3";
                } 
                else
                {
                    return "SOOOO " + User.GetUser(e) + " YOU WANNA CHECK ON BEING LOVED? ALRIGHT LEMME TELL YOU SOMETHING: YOU ARE SUCH A WONDERFUL HUMAN BEING AND I LOVE YOU SO SO SO SO MUCH! YOU ARE 2352325325252141% LOVED! TAKE ALL MY LUB BISH YOU ARE AMAZING <3 P.S.: Karo will push you in a pond if you don't believe me xoxo";
                }
            }
            else
            {
                return "SOOOO " + User.GetUser(e) + " YOU WANNA CHECK ON BEING LOVED? ALRIGHT LEMME TELL YOU SOMETHING: YOU ARE SUCH A WONDERFUL HUMAN BEING AND I LOVE YOU SO SO SO SO MUCH! YOU ARE 2352325325252141% LOVED! TAKE ALL MY LUB BISH YOU ARE AMAZING <3 P.S.: Karo will push you in a pond if you don't believe me xoxo";
            }
        }

        public string CheckCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 102);
            return User.GetUser(e) + " " + randomCounter + "% checked themselves before they wrecked themselves";
        }

        public string BoobaCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 102);
            if (e.Command.ChatMessage.DisplayName == "sollunatic")
            {
                return "@sollunatic MAIN BOOBA GIRL IS 1069% BOOBALICIOUS!";
            }
            else if(randomCounter == 69)
            {
                return User.GetUser(e) + " is " + randomCounter + "% boobalicious and hit that sweet spot ( ͡° ͜ʖ ͡°)";
            }
            else if(randomCounter == 101)
            {
                return User.GetUser(e) + " is " +randomCounter+ "% boobalicious! ALL HAIL BOOBA!";
            }
            else if(randomCounter < 10)
            {
                return User.GetUser(e) + " stop thinking about booba go back to work! jk pray to the booba-gods! may they never sag （• ㅅ •)";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% boobalicious!";
            }
        }

        public string SusCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 102);
            if (e.Command.ChatMessage.DisplayName == "kiwi_Sr")
            {
                return "@kiwi_Sr SUS-GOD! WE BOW DOWN! KIWI SUS!";
            }
            else if (randomCounter == 101)
            {
                return User.GetUser(e) + " IS SUS TO THE MAXIMUM SUS! WE CAN'T TRUST YOU! (jk jk you cheeky homeslice) akatri2Pew";
            }
            else if(randomCounter == 1)
            {
                return User.GetUser(e) + " you're the most innocent blop! <3";
            }
            else if(randomCounter == 69)
            {
                return "YOU ARE SUS AND SPICY SHEEEESH! ( ͡° ͜ʖ ͡°) " + User.GetUser(e) + " IS ON FIRE TODAY! akatri2Pew ";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + " % sus! akatri2Pew ";
            }
        }

        public string SpookCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 101);
            if(randomCounter == 100)
            {
                return User.GetUser(e) + " YOU ARE 100% SPOOKY! YOU ARE A SPOOKY SCARY SKELETON! BOOOOOO 👻";
            }
            else if(randomCounter == 69)
            {
                return User.GetUser(e) + " of course you can't just be spooky... SOMEONE HAS TO BE SPICY AND SPOOKY AND THAT'S YOUUUUUUU BOOOOOOOOO! 👻";
            }
            else if(randomCounter > 70)
            {
                return User.GetUser(e) + " is " + randomCounter + "% spooky! YOU ARE SCARY AS HELL! 👻";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% spooky! 👻";
            }
        }

        public string BojoCheckCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 102);
            if(randomCounter == 101)
            {
                return User.GetUser(e) + " OMG NO 1 UKGOV FAN! YOU ARE THE BIGGEST BOJO SHRIMP DAYUM! ( ͡° ͜ʖ ͡°) <3";
            }
            else if(randomCounter == 69)
            {
                return User.GetUser(e) + " OMG STOP THINKING ABOUT UKGOV! BOJO IS WATCHING YOU! YOU BETTER GET BACK TO WORK! akatri2Pew";
            }
            else if(randomCounter > 75)
            {
                return User.GetUser(e) + " YOU'RE A TRUE MEMBER OF THE BOJO-FANCLUB! CONGRATS! LIFEGOAL ACHIEVED!";
            }
            else
            {
                return User.GetUser(e) + " is " + randomCounter + "% in love with ukgov! OMG! <3";
            }
        }

        public string BumBumCommand(OnChatCommandReceivedArgs e)
        {
            int randomCounter = GetRandom(1, 101);
            if(randomCounter == 101)
            {
                return User.GetUser(e) + "akatri2Party BUMBUM-GOD! WE BOW DOWN! BUMBUM IN THE AIR AND DANCE FOR US! akatri2Party";
            }
            else if(randomCounter == 69)
            {
                return User.GetUser(e) + "/me is twerking through chat! akatri2Party";
            }
            else if(randomCounter < 40)
            {
                return User.GetUser(e) + " YOU ARE NOT HYPED ENOUGH BUMBUM IN THE AIR AND DANCE A LIL BIT! akatri2Party";
            }
            else
            {
                return User.GetUser(e) + " LOVES THE BUMBUM! <3";
            }
        }
    }
}
