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
        ADD, EDIT, DONE, REMOVE, FINISHEDTASKS, ALLFINISHEDTASKS
    }

    enum RandomCounter
    {
        SPICECHECK
    }

    class Bot
    {
        private ConnectionCredentials creds = new ConnectionCredentials("blopsquadbot", "oauth:prmbz98t28zxk3qqs22xhy6x6tkp1y");
        private TwitchClient client;
        private string channel = "karomagkekse";
        private string response;
        private TaskCommandManager taskManager = new TaskCommandManager();
        private RandomCommandManager randomManager = new RandomCommandManager();

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


                // MODS ONLY
                // POMO
                case "timeoutdelete":
                    break;
            }
        }

        private void DisplayPomodoroCommand(Pomodoro pomo, OnChatCommandReceivedArgs e)
        {
            switch (pomo)
            {
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
            }

            SendChatMessage(response);
        }


        private void DisplayQuoteCommand(Quote quote, OnChatCommandReceivedArgs e)
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

            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                SendChatMessage(response);
                return;
            }
            else if (Cooldown.CheckCooldownOffQuote(quote) == true)
            {
                Cooldown.globalCooldownsQuotes[quote] = DateTime.Now;
                Cooldown.globalCooldownsRunningQuotes[quote] = true;
                SendChatMessage(response);
                return;
            }
        }

        private void DisplayRandomCommand(RandomCounter random, OnChatCommandReceivedArgs e)
        {
            switch (random)
            {
                case RandomCounter.SPICECHECK:
                    response = randomManager.SpiceCheckCommand(e);
                    break;
            }

            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                SendChatMessage(response);
                return;
            }
            else if (Cooldown.CheckCooldownOffRandom(random) == true)
            {
                Cooldown.globalCooldownsRandom[random] = DateTime.Now;
                Cooldown.globalCooldownsRunningRandom[random] = true;
                SendChatMessage(response);
                return;
            }
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

        private void SendChatMessage(string response)
        {
            client.SendMessage(channel, response);
            Console.WriteLine($"[Bot]: {response}");
        }
    }
}
