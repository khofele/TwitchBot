using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    enum Quote
    {
        ONEMORE, TEA, BIO
    }

    enum Pomodoro
    {
        ADD, EDIT, DONE, REMOVE, FINISHEDTASKS, ALLFINISHEDTASKS, SETTARGET, MYTASK
    }

    enum RandomCounter
    {
        SPICECHECK, NAPCHECK, HYPECHECK, LOVECHECK, CHECKCHECK, BOOBACHECK, SPOOKCHECK, SUSCHECK, BOJOCHECK, BUMBUM
    }

    enum General
    {
        SUGGEST, BREAK
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
                    DisplayPomodoroCommand(Pomodoro.ADD, e);
                    break;

                case "edittask":
                    DisplayPomodoroCommand(Pomodoro.EDIT, e);
                    break;

                case "removetask":
                    DisplayPomodoroCommand(Pomodoro.REMOVE, e);
                    break;

                case "taskdone":
                    DisplayPomodoroCommand(Pomodoro.DONE, e);
                    break;

                case "finishedtasks":
                    DisplayPomodoroCommand(Pomodoro.FINISHEDTASKS, e);
                    break;

                case "allfinishedtasks":
                    DisplayPomodoroCommand(Pomodoro.ALLFINISHEDTASKS, e);
                    break;

                case "mytask":
                    DisplayPomodoroCommand(Pomodoro.MYTASK, e);
                    break;


                // QUOTES
                case "tea":
                    DisplayQuoteCommand(Quote.TEA, e);
                    break;

                case "onemore":
                    DisplayQuoteCommand(Quote.ONEMORE, e);
                    break;

                case "hello":
                    DisplayQuoteCommand(Quote.BIO, e);
                    break;

                // RANDOM
                case "spicecheck":
                    DisplayRandomCommand(RandomCounter.SPICECHECK, e);
                    break;

                case "napcheck":
                    DisplayRandomCommand(RandomCounter.NAPCHECK, e);
                    break;

                case "hypecheck":
                    DisplayRandomCommand(RandomCounter.HYPECHECK, e);
                    break;

                case "lovecheck":
                    DisplayRandomCommand(RandomCounter.LOVECHECK, e);
                    break;

                case "checkcheck":
                    DisplayRandomCommand(RandomCounter.CHECKCHECK, e);
                    break;

                case "boobacheck":
                    DisplayRandomCommand(RandomCounter.BOOBACHECK, e);
                    break;

                case "suscheck":
                    DisplayRandomCommand(RandomCounter.SUSCHECK, e);
                    break;

                case "bojo":
                    DisplayRandomCommand(RandomCounter.BOJOCHECK, e);
                    break;

                case "bumbum":
                    DisplayRandomCommand(RandomCounter.BUMBUM, e);
                    break;

                // SPOOKTOBER
                case "spookcheck":
                    DisplayRandomCommand(RandomCounter.SPOOKCHECK, e);
                    break;

                // GENERAL
                case "suggest":
                    DisplayGeneralCommand(General.SUGGEST, e);
                    break;

                case "break":
                    DisplayGeneralCommand(General.BREAK, e);
                    break;

                // MODS ONLY
                // POMO
                case "timeoutdelete":
                    break;

                case "settarget":
                    DisplayPomodoroCommand(Pomodoro.SETTARGET, e);
                    break;
            }
        }

        private void DisplayPomodoroCommand(Pomodoro pomo, OnChatCommandReceivedArgs e)
        {
            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponsePomodoro(pomo, e);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOffPomodoro(pomo) == true)
            {
                GetResponsePomodoro(pomo, e);
                Cooldown.globalCooldownsPomos[pomo] = DateTime.Now;
                Cooldown.globalCooldownsRunningPomos[pomo] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }


        private void DisplayQuoteCommand(Quote quote, OnChatCommandReceivedArgs e)
        {
            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponseQuote(quote, e);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOffQuote(quote) == true)
            {
                GetResponseQuote(quote, e);
                Cooldown.globalCooldownsQuotes[quote] = DateTime.Now;
                Cooldown.globalCooldownsRunningQuotes[quote] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }

        private void DisplayRandomCommand(RandomCounter random, OnChatCommandReceivedArgs e)
        {
            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponseRandomCommand(random, e);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOffRandom(random) == true)
            {
                GetResponseRandomCommand(random, e);
                Cooldown.globalCooldownsRandom[random] = DateTime.Now;
                Cooldown.globalCooldownsRunningRandom[random] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }

        private void DisplayGeneralCommand(General general, OnChatCommandReceivedArgs e)
        {
            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponseGeneralCommand(general, e);
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOffGeneral(general) == true)
            {
                GetResponseGeneralCommand(general, e);
                Cooldown.globalCooldownsGeneral[general] = DateTime.Now;
                Cooldown.globalCooldownsRunningGeneral[general] = true;
                if (response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
        }

        private string GetResponsePomodoro(Pomodoro pomo, OnChatCommandReceivedArgs e)
        {
            switch (pomo)
            {
                case Pomodoro.SETTARGET:
                    if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
                    {
                        response = taskManager.SetTargetCommand(e);
                    }
                    break;

                case Pomodoro.ADD:
                    response = taskManager.AddTaskCommand(e);
                    break;

                case Pomodoro.EDIT:
                    response = taskManager.EditTaskCommand(e);
                    break;

                case Pomodoro.DONE:
                    response = taskManager.TaskDoneCommand(e);
                    break;

                case Pomodoro.REMOVE:
                    response = taskManager.RemoveTaskCommand(e);
                    break;

                case Pomodoro.FINISHEDTASKS:
                    response = taskManager.FinishedTasksCommand(e);
                    break;

                case Pomodoro.ALLFINISHEDTASKS:
                    response = taskManager.GetAllFinishedTasks();
                    break;

                case Pomodoro.MYTASK:
                    response = taskManager.MyTaskCommand(e);
                    break;
            }
            return response;
        }

        private string GetResponseRandomCommand(RandomCounter random, OnChatCommandReceivedArgs e)
        {
            switch (random)
            {
                case RandomCounter.SPICECHECK:
                    response = randomManager.SpiceCheckCommand(e);
                    break;

                case RandomCounter.NAPCHECK:
                    response = randomManager.NapCheckCommand(e);
                    break;

                case RandomCounter.HYPECHECK:
                    response = randomManager.HypeCheckCommand(e);
                    break;

                case RandomCounter.LOVECHECK:
                    response = randomManager.LoveCheckCommand(e);
                    break;

                case RandomCounter.CHECKCHECK:
                    response = randomManager.CheckCheckCommand(e);
                    break;

                case RandomCounter.BOOBACHECK:
                    response = randomManager.BoobaCheckCommand(e);
                    break;

                case RandomCounter.SUSCHECK:
                    response = randomManager.SusCheckCommand(e);
                    break;

                case RandomCounter.SPOOKCHECK:
                    response = randomManager.SpookCheckCommand(e);
                    break;

                case RandomCounter.BOJOCHECK:
                    response = randomManager.BojoCheckCommand(e);
                    break;

                case RandomCounter.BUMBUM:
                    response = randomManager.BumBumCommand(e);
                    break;
            }
            return response;
        }

        private string GetResponseQuote(Quote quote, OnChatCommandReceivedArgs e)
        {
            switch (quote)
            {
                case Quote.ONEMORE:
                    response = "“i will just play one more game, one more, i promise!” - Mike 2019";
                    break;

                case Quote.TEA:
                    response = "Yes, yes i love mint tea.I have about 5 cups a day.Dont touch my tea. -Mike 22 - 6 - 2019";
                    break;

                case Quote.BIO:
                    if (e.Command.ChatMessage.DisplayName == "bioklappstuhl")
                    {
                        response = "hello bio! you are so loved! hihi <3";
                    }
                    else
                    {
                        response = "you're not bio :c";
                    }
                    break;
            }
            return response;
        }

        private string GetResponseGeneralCommand(General general, OnChatCommandReceivedArgs e)
        {
            switch (general)
            {
                case General.SUGGEST:
                    response = generalManager.SuggestCommand(e);
                    break;

                case General.BREAK:
                    response = generalManager.BreakCommand();
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
    }
}
