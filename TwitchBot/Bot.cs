using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    enum Command
    {
        // Quote
        ONEMORE, TEA,

        // Pomo
        ADD, EDIT, DONE, REMOVE, FINISHEDTASKS, ALLFINISHEDTASKS, SETTARGET, MYTASK, SETCURRENT, RESET, SETWEEKLYTARGET, SETPOMO, SETPOMOGOAL, DELETETASK,
        
        // Check-commands
        SPICECHECK, NAPCHECK, HYPECHECK, LOVECHECK, CHECKCHECK, BOOBACHECK, SUSCHECK, BOJOCHECK, BUMBUM, CHAIRCHECK, HAPPYHIPPO, VINCENT, VIBECHECK,

        // General
        BIO, SUGGEST, BREAK, UNO, YO, LOVE, HUG, TRAGER, 

        // Christmas
        GOODLIST, NAUGHTYLIST, LIST, PRESENTCHECK, MISTLETOE,

        // Spooktober
        SPOOKCHECK,

        // NULL / DEBUG
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
        private ChristmasCommands christmasManager = new ChristmasCommands();
        private static List<string> userList = new List<string>();

        public static TwitchClient Client
        {
            get => client;
        }

        public static List<string> UserList
        {
            get => userList;
        }

        public Bot()
        {
            client = new TwitchClient();
            client.Initialize(creds, channel);

            client.OnLog += Client_OnLog;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.OnUserJoined += Client_OnUserJoined;

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            userList.Add(e.Username);
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

                case "chaircheck":
                    DisplayCommand(Command.CHAIRCHECK, e);
                    break;

                case "happyhippo":
                    DisplayCommand(Command.HAPPYHIPPO, e);
                    break;

                case "vincent":
                    DisplayCommand(Command.VINCENT, e);
                    break;

                case "vibecheck":
                    DisplayCommand(Command.VIBECHECK, e);
                    break;

                // SPOOKTOBER
                //case "spookcheck":
                //    DisplayCommand(Command.SPOOKCHECK, e);
                //    break;

                // CHRISTMAS
                case "goodlist":
                    DisplayCommand(Command.GOODLIST, e);
                    break;

                case "naughtylist":
                    DisplayCommand(Command.NAUGHTYLIST, e);
                    break;

                case "list":
                    DisplayCommand(Command.LIST, e);
                    break;

                case "presentcheck":
                    DisplayCommand(Command.PRESENTCHECK, e);
                    break;

                case "mistletoe":
                    DisplayCommand(Command.MISTLETOE, e);
                    break;

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

                case "yo":
                    DisplayCommand(Command.YO, e);
                    break;

                case "love":
                    DisplayCommand(Command.LOVE, e);
                    break;

                case "hug":
                    DisplayCommand(Command.HUG, e);
                    break;

                case "trager":
                    DisplayCommand(Command.TRAGER, e);
                    break;

                // ------------------------------------------------------  MODS ONLY ------------------------------------------------------  
                // POMO
                //case "timeoutdelete":
                //    break;

                case "settarget":
                    DisplayCommand(Command.SETTARGET, e);
                    break;

                case "setcurrent":
                    DisplayCommand(Command.SETCURRENT, e);
                    break;

                case "reset":
                    DisplayCommand(Command.RESET, e);
                    break;

                case "setweeklytarget":
                    DisplayCommand(Command.SETWEEKLYTARGET, e);
                    break;

                case "setpomo":
                    DisplayCommand(Command.SETPOMO, e);
                    break;

                case "setpomogoal":
                    DisplayCommand(Command.SETPOMOGOAL, e);
                    break;

                case "deletetask":
                    DisplayCommand(Command.DELETETASK, e);
                    break;

                // Debug
                //case "banana":
                //    break;
            }
        }

        private void DisplayCommand(Command command, OnChatCommandReceivedArgs e)
        {
            LastUsedCommand.LastUsedCommandCheck(e.Command.CommandText, User.GetUser(e));
            string taggedUser = CheckAndGetTaggedUser(e);

            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponse(command, e, taggedUser);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOff(command) == true)
            {
                GetResponse(command, e, taggedUser);
                Cooldown.globalCooldowns[command] = DateTime.Now;
                Cooldown.globalCooldownsRunning[command] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }


        private string GetResponse(Command command, OnChatCommandReceivedArgs e, string taggedUser)
        {
            switch(command)
            {
                // Pomodoro
                case Command.SETTARGET:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetTargetCommand(e);
                    }
                    break;

                case Command.SETCURRENT:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetCurrentCommand(e);
                    }
                    break;

                case Command.RESET:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.ResetListCommand();
                    }
                    break;

                case Command.SETWEEKLYTARGET:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetWeeklyTargetCommand(e);
                    }
                    break;

                case Command.SETPOMO:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetPomoCommand(e);
                    }
                    break;

                case Command.SETPOMOGOAL:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetPomoGoalCommand(e);
                    }
                    break;

                case Command.DELETETASK:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.DeleteTaskCommand(taggedUser, e);
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
                    response = randomManager.SpiceCheckCommand(taggedUser);
                    break;

                case Command.NAPCHECK:
                    response = randomManager.NapCheckCommand(taggedUser);
                    break;

                case Command.HYPECHECK:
                    response = randomManager.HypeCheckCommand(taggedUser);
                    break;

                case Command.LOVECHECK:
                    response = randomManager.LoveCheckCommand(taggedUser);
                    break;

                case Command.CHECKCHECK:
                    response = randomManager.CheckCheckCommand(taggedUser);
                    break;

                case Command.BOOBACHECK:
                    response = randomManager.BoobaCheckCommand(taggedUser);
                    break;

                case Command.SUSCHECK:
                    response = randomManager.SusCheckCommand(taggedUser);
                    break;

                //case Command.SPOOKCHECK:
                //    response = randomManager.SpookCheckCommand(e);
                //    break;

                case Command.BOJOCHECK:
                    response = randomManager.BojoCheckCommand(taggedUser);
                    break;

                case Command.BUMBUM:
                    response = randomManager.BumBumCommand(taggedUser);
                    break;

                case Command.CHAIRCHECK:
                    response = randomManager.ChairCheckCommand(taggedUser);
                    break;

                case Command.HAPPYHIPPO:
                    response = randomManager.HappyHippoCommand(taggedUser);
                    break;

                case Command.VINCENT:
                    response = randomManager.VincentCommand(taggedUser);
                    break;

                case Command.VIBECHECK:
                    response = randomManager.VibeCheckCommand(taggedUser);
                    break;

                // CHRISTMAS SPECIAL
                case Command.GOODLIST:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = christmasManager.GoodListCommand(taggedUser);
                    }
                    break;

                case Command.NAUGHTYLIST:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = christmasManager.NaughtyListCommand(taggedUser);
                    }
                    break;

                case Command.LIST:
                    response = christmasManager.ListCommand(taggedUser);
                    break;

                case Command.PRESENTCHECK:
                    response = christmasManager.PresentCheckCommand(taggedUser);
                    break;

                case Command.MISTLETOE:
                    response = christmasManager.MistleToeCommand(taggedUser, e);
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
                    Command lastCommand = generalManager.UnoCommand(taggedUser);
                    response = GetResponse(lastCommand, e, taggedUser);
                    break;

                case Command.YO:
                    response = generalManager.YoCommand(taggedUser);
                    break;

                case Command.LOVE:
                    response = generalManager.LoveCommand(taggedUser, e);
                    break;

                case Command.HUG:
                    response = generalManager.HugCommand(taggedUser, e);
                    break;

                case Command.TRAGER:
                    response = generalManager.TragerCommand(taggedUser);
                    break;

                default:
                    response = null;
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
            string text = null;

            if(chatMessage.ToLower().StartsWith("!deletetask @"))
            {
                text = chatMessage.Replace(("!" + e.Command.CommandText).ToString() + " @", "");
            }
            else if (chatMessage.ToLower().StartsWith("!uno") || chatMessage.ToLower().StartsWith("!deletetask"))
            {
                text = chatMessage.Replace(("!" + e.Command.CommandText).ToString() + " ", "");
            }
            else
            {
                text = chatMessage.Replace(("!" + e.Command.CommandText).ToString(), "");
            }

            if (text != "" && text != " " && text != "  " && text != null)
            {
                user = text;

            }
            else
            {
                user = User.GetUser(e);
            }

            return user;
        }

            
        private string Test(OnExistingUsersDetectedArgs e)
        {
            Random random = new Random();
            int randomNum = random.Next(1, e.Users.Count);
            return e.Users[randomNum].ToString();
        }


    }
}
