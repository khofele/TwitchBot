using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    class ChristmasCommands
    {
        private List<string> goodList = new List<string>();
        private List<string> naughtyList = new List<string>();
        private Random random = null;
        private List<string> commonPresents = new List<string> { 
            "A nuzzle from the bot. Wait, that's me! nuzzzzleessss",
            "A wine glass.",
            "A dice.",
            "A wet shoe.",
            "A dry shoe.",
            "A pair of pants. I hope they aren't Tripzz's.",
            "A lot of packaging, but it doesn't seem like a lot of much less.",
            "A crumpled piece of paper.",
            "A banana. ",
            "A pear.",
            "A wink. Look, writing these are hard, okay?",
            "An empty Christmas card. pfft.",
            "2 broken chocolate bars. Does that mean there's only one in total?",
            "A bag of meh oranges, one of which has a little mould on the side",
            "A hot piece of coal, AHHHHH",
            "a handful of gravel? Yeah, nice one Santa",
            "A wet sock. That's gross",
            "Half a bottle of water. I mean, thanks, I guess?",
            "A fart in a jar? Yup, that's at the top of everyone's Xmas list this year.",
            "A candle that smells like poo? uhh....",
            "A soggy teabag. Nice.",
            "A half-burned candle. That's cool, I guess.",
            "A handful of walnuts. Christmassy.",
            "A slap. Well, that's just how life is.",
            "A lemon. Guess it's time to make lemonade.",
            "A mini skirt? I think Tripzz running out of gift ideas here",
            "An iron ingot. Cool... maybe?",
            "A chocolate bar. NOM.",
            "A snowball to the ear. BRRRR.",
            "An orange. That's it.",
            "A fresh pair of socks. Perfect for after being thrown in the pond.",
            "Cute lil candy canes. Aww, how festive.",
            "A pair of gloves; perfect for those colder winter nights.",
            "A bag of delicious oranges",
            "10,000 Blopcoins! No way",
            "A packet of gummy bears. Bet you wish they were real, huh.",
            "Your favourite pastry. Mmm.",
            "A beautiful muffin. With chocolate chips and blueberries and all the extras.",
            "A plate of gingerbreads. Nom nom nom.",
            "A Harry Potter boxset! SICK!",
            "A pillow with Tripzz's face on it. Erm, okay then.",
            "A pillow with Karo's face on it. Aww CUTE!",
            "A Trager body pillow? Sweet dreams!",
            "A ducky tie. That's kinda cool.",
            "A life-size model of the Death Star. I don't think it works though?",
            "A trip in time? POG, where you gonna do?",
            "A redemption from the naughty list. Save someone or keep it? It's up to you :3",
            "A hug from a blop. I wish this was real.",
            "A hug. Awwww :3",
            "A Merry Christmas. You deserve it.",
            "100 tasks added to task target. Wow, you know how to make friends.",
            "50 tasks added to the task target. I mean, I guess it keeps us busy.",
            "10 task added to the task target. Heh.",
            "A ride in Santa's sleigh! Choo Choo!",
            "Five spins! You're going to do it too?",
            "Your own baby Blop <3 Cuteeeeeee",
            "A jar of pond water. Let's hope it's from the Lub Pond and not the smelly one.",
            "A Christmas Cracker joke. Sigh. This should be good...",
            "A really pretty candle. Aww, that's nice",
            "A babywave, n'awhhh :3",
            "A hydration redeem. Well, that's actually important. Drink up everyone :)",
            "A tickle. Don't make it weird.",
            "-100 BlopCoins. Wow. As if this even exists.",
            "Frankencese. Does that make you Jesus?",
            "A push in the pond. Sorry, Karo's already on her way...",
            "A headpat. pat pat pat.",
            "A boop from Winterber. AWWWW I WANT ONE TOO",
            "A boop on camera. BOOP.",
            "A keyring of a bumblebee. I kinda what that too.",
            "A super tickle. BRRRRRRRRR",
            "A kiwi. The hairy fruit, not the guy.",
            "Tripzz says hey in chat. Hey.",
            "Nothing. You're the gift. To the world. You are loved. Thank you for existing.",
            "A telescope. That's pretty POG. Planets and that.",
            "A basket of MOFOing KITTENS LETS GO! Meow.",
            "A box of the best tasting cookies around",
            "A weekend for two with Blep? I don't know if that's good or bad",
            "A little baby bunny. Aww :)",
            "A Christmas song played on break! Which one will you pick?"
        };

        private List<string> rarePresents = new List<string> {
            "A battle royale! CHARGGGGEEE",
            "A BOSS FIGHT?! Get ready troops",
            "A basketball game! Yay!",
            "A piece of folded paper with the PRICE OF THE FISH?! HOLY....",
            "A meditation break. Okay, let's get zen",
            "A video yoga break? Let's get stretchy!",
            "OMG! A scribbl break? Let's GOOOOOOOOOOO!",
            "Sick! No, not sick sick. Means sick, you got a Tetrio break! Game On!",
            "A drawing of your choice from Tripzz? Wait, is this the return of Doodle Time?",
            "1 Trillion Blopcoins. !gamble all. Lost. Sorry, you get nothing.",
            "SCOOTER CONCERT TICKETS?! POG",
            "A ONE WAY TICKET TO THE NAUGHTY LIST! HEHEHEHEh",
            "A jar of Trippz's bathwater. WTF is this list?!",
            "VIP for the rest of the day. Okay, that's kinda POG.",
            "20-second Slow Mode for the rest of the pomo. Wow, you had to, didn't you?",
            "5-second Slow mode for the next ten minutes. Hmmm.",
            "Emote only mode for one minute. Kinda pointless but okay Santa.",
            "A timeout for 3 minutes. Heh.",
            "5 minutes added to the next timer. WOW!",
            "10 minutes added to the next timer. :O",
            "20 minutes added to the next timer. HOLY",
            "Tripzz stares into webcam for ten seconds. Ew. Why.",
            "The ability to time someone out for 3 minutes. Bye bye!",
            "A chat timeout for 5 minutes. Well, that sucks for you.",
            "Holy Blop Jesus, 10,000,000 BlopCoins?! That's intense. Drinks on you?",
            "One minute of emote only mode? Let's go :P",
            "An appearance from Raffles? Let's hope she's not sleeping...",
            "A K3 song on break. Oh good Lord....",
            "Emote only chat for the next five minutes? See you in five!"
        };

        private List<string> legendaryPresents = new List<string> {
            "Control of the raid? Okay okay. Pick someone nice <3",
            "A Just Dance Break? Let's go!",
            "Tripzz Travel story time! Tuck yourself in!",
            "10 Push Ups? Tripzz will be happy. You gonna do them too?",
            "A bedtime story stream! Aww, this will be nice <3",
            "1v1 Chess game against Tripzz. Better bring your A game bish.",
            "A gifted sub to whoever you choose? Let's go!"
        };

        private int GetRandom(int min, int max)
        {
            random = new Random();
            return random.Next(min, max);
        }

        public string ListCommand(string user)
        {
            if (goodList.Count > 0)
            {
                for (int i = 0; i < goodList.Count; i++)
                {
                    if(goodList[i] == user)
                    {
                        return user + " is a good blop! akatri2Lovings";
                    }
                }
            }

            if(naughtyList.Count > 0)
            {
                for(int i = 0; i < naughtyList.Count; i++)
                {
                    if(naughtyList[i] == user)
                    {
                        return user + " is a naughty blop! akatri2Pew";
                    }
                }
            }

            return user + " is not on any list!";
        }

        public string GoodListCommand(string user)
        {
            if (naughtyList.Count > 0 && naughtyList.Contains(user))
            {
                naughtyList.Remove(user);
            }

            if(goodList.Contains(user) == false)
            {
                goodList.Add(user);
                return user + " has been added to the good list! " + user + " is a very good blop! akatri2Lovings";
            }
            else
            {
                return user + " is already on the good list! akatri2Lovings";
            }
        }

        public string NaughtyListCommand(string user)
        {
            if (goodList.Count > 0 && goodList.Contains(user))
            {
                goodList.Remove(user);
            }

            if (naughtyList.Contains(user) == false)
            {
                naughtyList.Add(user);
                return user + " has been added to the naughty list! " + user + " is a very naughty blop! akatri2Pew";
            }
            else
            {
                return user + " is already on the naughty list! akatri2Pew";
            }
        }

        public string MistleToeCommand(string user, OnChatCommandReceivedArgs e)
        {
            if(User.GetUser(e) != user)
            {
                return User.GetUser(e) + " kisses " + user + " under the mistletoe ;) <3";
            }
            else
            {
                int randomUser = GetRandom(0, Bot.UserList.Count);
                return User.GetUser(e) + " kisses " + Bot.UserList[randomUser] + " under the mistletoe ;) <3";
            }
        }

        public string PresentCheckCommand(string user)
        {
            int listProb = GetRandom(1, 101);

            if(listProb <= 85)
            {
                // common
                int randomNumber = GetRandom(0, commonPresents.Count -1);

                return "/me " + user + " You creep downstairs, tiptoeing to stay as quiet as you can. You hide your squeals as Santa has been! The Christmas Tree has so many presents! You sneak over, pick the biggest box you can find to receive.... " + commonPresents[randomNumber];
            }
            else if(listProb <= 98)
            {
                // rare
                int randomNumber = GetRandom(0, rarePresents.Count -1);

                return "/me " + user + " You creep downstairs, tiptoeing to stay as quiet as you can. You hide your squeals as Santa has been! The Christmas Tree has so many presents! You sneak over, pick the biggest box you can find to receive.... " + rarePresents[randomNumber];
            }
            else
            {
                // legendary
                int randomNumber = GetRandom(0, legendaryPresents.Count -1);

                return "/me " + user + " You creep downstairs, tiptoeing to stay as quiet as you can. You hide your squeals as Santa has been! The Christmas Tree has so many presents! You sneak over, pick the biggest box you can find to receive.... " + legendaryPresents[randomNumber];
            }
        }
    }
}
