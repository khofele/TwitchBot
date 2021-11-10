using System.Collections.Generic;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class GeneralManager
    {
        private FileManager fileManager = new FileManager();

        public string SuggestCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string suggestMessage = chatMessage.Replace("!suggest", " suggested: ");
            fileManager.WriteToFile(User.GetUser(e) + suggestMessage, FileManager.SuggestPath);
            return User.GetUser(e) + " thank you for your suggestion! <3";
        }

        public string BreakCommand()
        {
            return "ALRIGHT GUYS IT'S BREAKTIME! STAND UP! BUMBUMS IN THE AIR! STRETCH! HYDRATE! DROP YOUR PENS! HYDRATE! WE DON'T WANT YOU TO BURN OUT! <3";
        }

        public Command UnoCommand(string user)
        {
            Command lastCommand;
            if(LastUsedCommand.lastUsedCommands.ContainsKey(user))
            {
                lastCommand = LastUsedCommand.lastUsedCommands[user];
                return lastCommand;
            }
            else
            { 
                return Command.NULL;
            }            
        }

        public string YoCommand(string user)
        {
            if(user == "akaTripzz")
            {
                return "YO MIKE!";
            }
            else
            {
                return "YO YO YO YO YO YO";
            }
        }

        public string LoveCommand(string user, OnChatCommandReceivedArgs e)
        {
            return "akatri2Lovings " + User.GetUser(e) + " reminds " + user + " that they are a beautiful human being and we love them very much <3 akatri2Lovings";
        }

        public string HugCommand(string user, OnChatCommandReceivedArgs e)
        {
            return "/me " + User.GetUser(e) + " hugs " + user + " akatri2Lovings akatri2Lovings akatri2Lovings";
        }

        public string TragerCommand(string user)
        {
            if(user == "BettIsSomewhere")
            {
                return "/me Bett receives a long warm hug from Trager! <3 JUSTICE FOR TRAGER!";
            }
            else
            {
                return "JUSTICE FOR TRAGER!";
            }
        }

        public string VincentCommand(string user)
        {
            if(user == "karomagkekse")
            {
                return "/me Karo finally found Vincent! Let's just leave her in the asylum. :p <3";
            }
            else if(user == "akaTripzz")
            {
                return "/me Mike sneaks out of the asylum. He can hear some giggles from far far away!";
            }
            else
            {
                return user + " you could not find Vincent.... yet";
            }
        }
    }
}
