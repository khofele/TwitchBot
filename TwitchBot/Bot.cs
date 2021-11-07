using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    enum Command
    {
        // Quote
        ONEMORE, TEA, BIO,

        // Pomo
        ADD, EDIT, DONE, REMOVE, FINISHEDTASKS, ALLFINISHEDTASKS, SETTARGET, MYTASK,
        
        // Check-commands
        SPICECHECK, NAPCHECK, HYPECHECK, LOVECHECK, CHECKCHECK, BOOBACHECK, SPOOKCHECK, SUSCHECK, BOJOCHECK, BUMBUM,

        // General
        SUGGEST, BREAK, UNO,

        NULL
    }

    class Bot
    {
        private ConnectionCredentials creds = new ConnectionCredentials("blopsquadbot", "oauth:4h6ckv84qqeu1eexdnt57qgulfjwjo");
        private static TwitchClient client;
        private static string channel = "karomagkekse";
        private string response;
        private TaskCommandManager taskManager = new TaskCommandManager();
        private RandomCommandManager randomManager = new RandomCommandManager();
        private GeneralManager generalManager = new GeneralManager();

        public static TwitchClient Client
        {
            get => client;
        }

        public Bot()
        {
            client = new TwitchClient();
            client.Initialize(creds, channel);

            client.OnLog += Client_OnLog;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            switch (e.Command.CommandText.ToLower())
            {
                // EVERYONE
                // POMO
                case "addtask":
                    DisplayCommand(Command.ADD, e);
                    break;

                case "edittask":
                    DisplayCommand(Command.EDIT, e);
                    break;

                case "removetask":
                    DisplayCommand(Command.REMOVE, e);
                    break;

                case "taskdone":
                    DisplayCommand(Command.DONE, e);
                    break;

                case "finishedtasks":
                    DisplayCommand(Command.FINISHEDTASKS, e);
                    break;

                case "allfinishedtasks":
                    DisplayCommand(Command.ALLFINISHEDTASKS, e);
                    break;

                case "mytask":
                    DisplayCommand(Command.MYTASK, e);
                    break;


                // QUOTES
                case "tea":
                    DisplayCommand(Command.TEA, e);
                    break;

                case "onemore":
                    DisplayCommand(Command.ONEMORE, e);
                    break;

                case "hello":
                    DisplayCommand(Command.BIO, e);
                    break;

                // RANDOM
                case "spicecheck":
                    DisplayCommand(Command.SPICECHECK, e);
                    break;

                case "napcheck":
                    DisplayCommand(Command.NAPCHECK, e);
                    break;

                case "hypecheck":
                    DisplayCommand(Command.HYPECHECK, e);
                    break;

                case "lovecheck":
                    DisplayCommand(Command.LOVECHECK, e);
                    break;

                case "checkcheck":
                    DisplayCommand(Command.CHECKCHECK, e);
                    break;

                case "boobacheck":
                    DisplayCommand(Command.BOOBACHECK, e);
                    break;

                case "suscheck":
                    DisplayCommand(Command.SUSCHECK, e);
                    break;

                case "bojo":
                    DisplayCommand(Command.BOJOCHECK, e);
                    break;

                case "bumbum":
                    DisplayCommand(Command.BUMBUM, e);
                    break;

                // SPOOKTOBER
                //case "spookcheck":
                //    DisplayCommand(Command.SPOOKCHECK, e);
                //    break;

                // GENERAL
                case "suggest":
                    DisplayCommand(Command.SUGGEST, e);
                    break;

                case "break":
                    DisplayCommand(Command.BREAK, e);
                    break;

                case "uno":
                    DisplayCommand(Command.UNO, e);
                    break;

                // ------------------------------------------------------  MODS ONLY ------------------------------------------------------  
                // POMO
                case "timeoutdelete":
                    break;

                case "settarget":
                    DisplayCommand(Command.SETTARGET, e);
                    break;

                // Debug
                //case "banana":
                //    CheckAndGetTaggedUser(e);
                //    break;
            }
        }

        private void DisplayCommand(Command command, OnChatCommandReceivedArgs e)
        {
            LastUsedCommand.LastUsedCommandCheck(e);

            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponse(command, e);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOff(command) == true)
            {
                GetResponse(command, e);
                Cooldown.globalCooldowns[command] = DateTime.Now;
                Cooldown.globalCooldownsRunning[command] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }


        private string GetResponse(Command command, OnChatCommandReceivedArgs e)
        {
            string user = CheckAndGetTaggedUser(e);
            switch(command)
            {
                // Pomodoro
                case Command.SETTARGET:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetTargetCommand(e);
                    }
                    break;

                case Command.ADD:
                    response = taskManager.AddTaskCommand(e);
                    break;

                case Command.EDIT:
                    response = taskManager.EditTaskCommand(e);
                    break;

                case Command.DONE:
                    response = taskManager.TaskDoneCommand(e);
                    break;

                case Command.REMOVE:
                    response = taskManager.RemoveTaskCommand(e);
                    break;

                case Command.FINISHEDTASKS:
                    response = taskManager.FinishedTasksCommand(e);
                    break;

                case Command.ALLFINISHEDTASKS:
                    response = taskManager.GetAllFinishedTasks();
                    break;

                case Command.MYTASK:
                    response = taskManager.MyTaskCommand(e);
                    break;

                // Check-Commands
                case Command.SPICECHECK:
                    response = randomManager.SpiceCheckCommand(user);
                    break;

                case Command.NAPCHECK:
                    response = randomManager.NapCheckCommand(user);
                    break;

                case Command.HYPECHECK:
                    response = randomManager.HypeCheckCommand(user);
                    break;

                case Command.LOVECHECK:
                    response = randomManager.LoveCheckCommand(user);
                    break;

                case Command.CHECKCHECK:
                    response = randomManager.CheckCheckCommand(user);
                    break;

                case Command.BOOBACHECK:
                    response = randomManager.BoobaCheckCommand(user);
                    break;

                case Command.SUSCHECK:
                    response = randomManager.SusCheckCommand(user);
                    break;

                //case Command.SPOOKCHECK:
                //    response = randomManager.SpookCheckCommand(e);
                //    break;

                case Command.BOJOCHECK:
                    response = randomManager.BojoCheckCommand(user);
                    break;

                case Command.BUMBUM:
                    response = randomManager.BumBumCommand(user);
                    break;

                // Quotes
                case Command.ONEMORE:
                    response = "“i will just play one more game, one more, i promise!” - Mike 2019";
                    break;

                case Command.TEA:
                    response = "Yes, yes i love mint tea.I have about 5 cups a day.Dont touch my tea. -Mike 22 - 6 - 2019";
                    break;

                case Command.BIO:
                    if (e.Command.ChatMessage.DisplayName == "bioklappstuhl")
                    {
                        response = "hello bio! you are so loved! hihi <3";
                    }
                    else
                    {
                        response = "you're not bio :c";
                    }
                    break;

                // General
                case Command.SUGGEST:
                    response = generalManager.SuggestCommand(e);
                    break;

                case Command.BREAK:
                    response = generalManager.BreakCommand();
                    break;

                case Command.UNO:
                    if(generalManager.UnoCommand(e) != Command.NULL)
                    {
                        DisplayCommand(generalManager.UnoCommand(e), e);
                    }
                    break;
            }
            return response;
        }

        private bool CheckModerator(OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.IsModerator == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBroadcaster(OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.IsBroadcaster == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SendChatMessage(string response)
        {
            client.SendMessage(channel, response);
            Console.WriteLine($"[Bot]: {response}");
        }

        public string CheckAndGetTaggedUser(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message;
            string user = null;

            string text = chatMessage.Replace(("!" + e.Command.CommandText).ToString(), "");
            
            if(text != "" && text != " " && text != "  " && text != null)
            {
                user = text;
            }
            else
            {
                user = User.GetUser(e);
            }

            return user;
        }
    }
}
