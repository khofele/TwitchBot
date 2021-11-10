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

        public string SpiceCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            if (user == "its_lunalia")
            {
                return "@its_lunalia BOW DOWN! SPICE QUEEN LUNA IS IN DA HOUSE! ALL HAIL OUR BELOVED SPICE QUEEN! WE LOVE YOU! <3";
            }
            else if (randomCounter == 69)
            {
                return "( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) " + user + " is " + randomCounter + "% spicy today! ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°)";
            }
            else if (randomCounter == 101)
            {
                return user + " is " + randomCounter + "% spicy today! THE SPICE IS MASSIVE! WE BOW DOWN! YOU ARE A SPICEGOD!";
            }
            else if (randomCounter > 70)
            {
                return user + " is " + randomCounter + "% spicy today! THE SPICE IS STRONG!";
            }
            else
            {
                return user + " is " + randomCounter + "% spicy today!";
            }
        }

        public string HypeCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            if (user == "karomagkekse")
            {
                return "@karomagkekse NUMBER ONE HYPE GIRL IS IN THE HOUSE! AYAYAYAYY KARO IS 1069% HYPED AND ABOUT TO EXPLODE AYYYYYYYYYYYYY! akatri2Party";
            }
            else if (randomCounter == 101)
            {
                return "akatri2Party " + randomCounter + "% HYPE! " + user + " IS HYPING IN THE STRATOSPHERE! AYAYAYAYAYAYAYYYYYYYYY akatri2Party";
            }
            else if (randomCounter == 69)
            {
                return user + " YOU ARE " + randomCounter + "% HYPED! S P I C E HYPE! akatri2Party";
            }
            else if (randomCounter > 50)
            {
                if (randomCounter > 90)
                {
                    return "akatri2Party " + randomCounter + "% HYPE! SHEEEEESH THE HYPE IS SO STRONG! " + user + " YOU'RE ABOUT TO EXPLODE! akatri2Party";
                }
                else
                {
                    return "akatri2Party  " + user + " is " + randomCounter + "% hyped today! THE HYPE IS STRONG! AYAYAYAYAYAY akatri2Party";
                }
            }
            else
            {
                return user + " is " + randomCounter + "% hyped today!";
            }
        }

        public string NapCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 101);
            if (randomCounter > 60)
            {
                return user + " is " + randomCounter + "% tired today! You better take a nap <3";
            }
            else if (randomCounter <= 10)
            {
                return user + " is " + randomCounter + "% tired today!.... WAIT YOU'RE NOT TIRED AT ALL BACK TO WORK :p <3";
            }
            else
            {
                return user + " is " + randomCounter + "% tired today!";
            }
        }

        public string LoveCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 101);
            if (user == "akaTripzz")
            {
                if (randomCounter == 69)
                {
                    return "Jódete el amor Mike aka FUCK YOUR LOVE MIKE! jk jk we love you <3";
                } 
                else if(randomCounter > 60)
                {
                    return "@akaTripzz Dear Mike! This is a friendly reminder, that you're an absolutely lovely person and that we all love you very much! You make us smile every fricking day! Thank you for everything! Lots of love to you! You're the best! <3";
                }
                else
                {
                    return "SOOOO " + user + " YOU WANNA CHECK ON BEING LOVED? ALRIGHT LEMME TELL YOU SOMETHING: YOU ARE SUCH A WONDERFUL HUMAN BEING AND I LOVE YOU SO SO SO SO MUCH! YOU ARE 2352325325252141% LOVED! TAKE ALL MY LUB BISH YOU ARE AMAZING <3 P.S.: Karo will push you in a pond if you don't believe me xoxo";
                }
            }
            else
            {
                return "SOOOO " + user + " YOU WANNA CHECK ON BEING LOVED? ALRIGHT LEMME TELL YOU SOMETHING: YOU ARE SUCH A WONDERFUL HUMAN BEING AND I LOVE YOU SO SO SO SO MUCH! YOU ARE 2352325325252141% LOVED! TAKE ALL MY LUB BISH YOU ARE AMAZING <3 P.S.: Karo will push you in a pond if you don't believe me xoxo";
            }
        }

        public string CheckCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            return user + " " + randomCounter + "% checked themselves before they wrecked themselves";
        }

        public string BoobaCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            if (user == "sollunatic")
            {
                return "@sollunatic MAIN BOOBA GIRL IS 1069% BOOBALICIOUS!";
            }
            else if(randomCounter == 69)
            {
                return user + " is " + randomCounter + "% boobalicious and hit that sweet spot ( ͡° ͜ʖ ͡°)";
            }
            else if(randomCounter == 101)
            {
                return user + " is " +randomCounter+ "% boobalicious! ALL HAIL BOOBA!";
            }
            else if(randomCounter < 10)
            {
                return user + " stop thinking about booba go back to work! jk pray to the booba-gods! may they never sag （• ㅅ •)";
            }
            else
            {
                return user + " is " + randomCounter + "% boobalicious!";
            }
        }

        public string SusCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            if (user == "kiwi_Sr")
            {
                return "@kiwi_Sr SUS-GOD! WE BOW DOWN! KIWI SUS!";
            }
            else if (randomCounter == 101)
            {
                return user + " IS SUS TO THE MAXIMUM SUS! WE CAN'T TRUST YOU! (jk jk you cheeky homeslice) akatri2Pew";
            }
            else if(randomCounter == 1)
            {
                return user + " you're the most innocent blop! <3";
            }
            else if(randomCounter == 69)
            {
                return "YOU ARE SUS AND SPICY SHEEEESH! ( ͡° ͜ʖ ͡°) " + user + " IS ON FIRE TODAY! akatri2Pew ";
            }
            else
            {
                return user + " is " + randomCounter + " % sus! akatri2Pew ";
            }
        }

        //public string SpookCheckCommand(OnChatCommandReceivedArgs e, string user)
        //{
        //    int randomCounter = GetRandom(1, 101);
        //    if(randomCounter == 100)
        //    {
        //        return user + " YOU ARE 100% SPOOKY! YOU ARE A SPOOKY SCARY SKELETON! BOOOOOO 👻";
        //    }
        //    else if(randomCounter == 69)
        //    {
        //        return user + " of course you can't just be spooky... SOMEONE HAS TO BE SPICY AND SPOOKY AND THAT'S YOUUUUUUU BOOOOOOOOO! 👻";
        //    }
        //    else if(randomCounter > 70)
        //    {
        //        return user + " is " + randomCounter + "% spooky! YOU ARE SCARY AS HELL! 👻";
        //    }
        //    else
        //    {
        //        return user + " is " + randomCounter + "% spooky! 👻";
        //    }
        //}

        public string BojoCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 102);
            if(randomCounter == 101)
            {
                return user + " OMG NO 1 UKGOV FAN! YOU ARE THE BIGGEST BOJO SHRIMP DAYUM! ( ͡° ͜ʖ ͡°) <3";
            }
            else if(randomCounter == 69)
            {
                return user + " OMG STOP THINKING ABOUT UKGOV! BOJO IS WATCHING YOU! YOU BETTER GET BACK TO WORK! akatri2Pew";
            }
            else if(randomCounter > 75)
            {
                return user + " YOU'RE A TRUE MEMBER OF THE BOJO-FANCLUB! CONGRATS! LIFEGOAL ACHIEVED!";
            }
            else
            {
                return user + " is " + randomCounter + "% in love with ukgov! OMG! <3";
            }
        }

        public string BumBumCommand(string user)
        {
            int randomCounter = GetRandom(1, 101);
            if(randomCounter == 101)
            {
                return user + "akatri2Party BUMBUM-GOD! WE BOW DOWN! BUMBUM IN THE AIR AND DANCE FOR US! akatri2Party";
            }
            else if(randomCounter == 69)
            {
                return user + "/me twerks through chat! akatri2Party";
            }
            else if(randomCounter < 40)
            {
                return user + " YOU ARE NOT HYPED ENOUGH BUMBUM IN THE AIR AND DANCE A LIL BIT! akatri2Party";
            }
            else
            {
                return user + " LOVES THE BUMBUM! <3";
            }
        }

        public string ChairCheckCommand(string user)
        {
            int randomCounter = GetRandom(1, 101);
            if(user == "akaTripzz") 
            {
                return "@akaTripzz YOU LOVE CHAIRIEL THE MOST BUT WE'RE STILL WAITING FOR THE BABY! :p <3";
            }
            else if(randomCounter == 69)
            {
                return "OOOH " + user + "! YOU HIT CHAIRIEL'S SWEET SPOT! ( ͡° ͜ʖ ͡°)";
            }
            else if(randomCounter >= 75)
            {
                return user + " loves Chairiel " + randomCounter + "%! You love Chairiel very much! ";
            }
            else if(randomCounter < 10)
            {
                return user + "loves Chairiel " + randomCounter + "%..... WOW YOU DON'T LOVE CHAIR-SENAPI VERY MUCH D:";
            }
            else
            {
                return user + " loves Chairiel " + randomCounter + " %!";
            }
        }

        public string HappyHippoCommand(string user)
        {
            int randomCounter = GetRandom(1, 101);
            if(user == "TriggerKR")
            {
                return "TriggerKR IS OUR FAVORITE HAPPY HIPPO! WE LOVE YOU SO MUCH! <3 (please stop gambling tho you need the money for your happy hippo cake :c)";
            }
            else if(randomCounter == 69)
            {
                return user + " ate all the happy hippos D:";
            }
            else if(randomCounter > 50 || user == "karomagkekse")
            {
                return user + " is the happiest hippo <3";
            }
            else
            {
                return user + " really really really wants a happy hippo rn D:";
            }
        }
    }
}
