using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    class Bot
    {
        ConnectionCredentials creds = new ConnectionCredentials("blopsquadbot", "oauth:prmbz98t28zxk3qqs22xhy6x6tkp1y");
        TwitchClient client;
        private string channel = "akaTripzz";
        private string response;
        private Task task;

        private FileManager fileManager = new FileManager();
        private TaskManager taskManager = new TaskManager();

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
                case "addtask":
                    AddTaskCommand(e);
                    break;

                case "edittask":
                    EditTaskCommand(e);
                    break;

                case "removetask":
                    RemoveTaskCommand(e);
                    break;

                case "taskdone":
                    TaskDoneCommand(e);
                    break;

                case "finishedtasks":
                    FinishedTasksCommand(e);
                    break;

                case "hello":
                    BioEasterEgg(e);
                    break;
            }
        }

        private void AddTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string taskMessage = chatMessage.Replace("!addtask", " -");
            CheckAndAddTask(taskMessage, GetUser(e), e);
            if (taskManager.finishedTasks.ContainsKey(GetUser(e)) == false)
            {
                taskManager.finishedTasks.Add(GetUser(e), 0);
            }
        }

        private void EditTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string editMessage = chatMessage.Replace("!edittask", " -");
            CheckAndEditTask(GetUser(e), editMessage, e);
            if (fileManager.FindTask(GetUser(e)) != null)
            {
                response = GetUser(e) + " edited the task: " + fileManager.FindTask(GetUser(e)) + "! akatri2Work";
                client.SendMessage(channel, response);
            } 
            else
            {
                response = GetUser(e) + " you have to add a task to edit a task! akatri2Pew";
                client.SendMessage(channel, response);
            }
        }

        private void RemoveTaskCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(GetUser(e)) != null)
            {
                response = GetUser(e) + " canceled the task: " + fileManager.FindTask(GetUser(e)) + "! akatri2Pew ";
                client.SendMessage(channel, response);
                Console.WriteLine($"[Bot]: {response}");
                taskManager.RemoveTask(GetUser(e));
                fileManager.DeleteTaskInFile(GetUser(e));
            } 
            else
            {
                response = GetUser(e) + " you have to add a task to remove a task! akatri2Pew";
                client.SendMessage(channel, response);
            }

        }



        private void TaskDoneCommand(OnChatCommandReceivedArgs e)
        {
            if(fileManager.FindTask(GetUser(e)) != null)
            {
                response = " CONGRATS " + GetUser(e) + "! akatri2Hype YOU COMPLETED YOUR TASK! " + fileManager.FindTask(GetUser(e)).ToUpper() + " IS DONE! akatri2Party akatri2Lovings";
                client.SendMessage(channel, response);
                taskManager.RemoveTask(GetUser(e));
                fileManager.DeleteTaskInFile(GetUser(e));
                CheckAndAddFinishedTask(e);
            }
            else
            {
                response = GetUser(e) + " you have to add a task to finish a task! akatri2Pew";
                client.SendMessage(channel, response);
            }

        }

        private void FinishedTasksCommand(OnChatCommandReceivedArgs e)
        {
            CheckAndAddFinishedTask(e);
        }

        private void BioEasterEgg(OnChatCommandReceivedArgs e)
        {
            if (GetUser(e) == "bioklappstuhl")
            {
                response = "hello bio! you are so loved! hihi <3";
                client.SendMessage(channel, response);
                Console.WriteLine($"[Bot]: {response}");
            }
            else
            {
                response = "you're not bio :c";
                client.SendMessage(channel, response);
                Console.WriteLine($"[Bot]: {response}");
            }
        }

        private void CheckAndAddTask(string taskMessage, string user, OnChatCommandReceivedArgs e)
        {
            task = new Task(user, taskMessage);
            taskManager.CheckTaskUser(task.User);

            if (taskManager.NotAdded == true)
            {
                response = GetUser(e) + " added a task: " + taskMessage.Replace("- ", "") + "! akatri2Work ";
                client.SendMessage(channel, response);
                Console.WriteLine($"[Bot]: {response}");
                fileManager.WriteToFile(GetUser(e) + task.UserTask);
                taskManager.AddTask(task);
            }
            else
            {
                response = GetUser(e) + " you already added a task! Remove or finish your task first! akatri2Pew ";
                client.SendMessage(channel, response);
                Console.WriteLine($"[Bot]: {response}");
            }
        }

        private void CheckAndEditTask(string user, string editMessage, OnChatCommandReceivedArgs e)
        {
            fileManager.FindAndEditTask(user, editMessage);
        }

        private void CheckAndAddFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (taskManager.finishedTasks.ContainsKey(GetUser(e)))
            {
                int finishedTasks = taskManager.finishedTasks[GetUser(e)];
                finishedTasks++;
                taskManager.finishedTasks[GetUser(e)] = finishedTasks;

                if(finishedTasks == 1)
                {
                    response = GetUser(e) + " finished " + finishedTasks + " task today! akatri2Party";
                }
                else
                {
                    response = GetUser(e) + " finished " + finishedTasks + " tasks today! akatri2Party";
                }
                client.SendMessage(channel, response);
            }
            else
            {
                taskManager.finishedTasks.Add(GetUser(e), 1);
                response = GetUser(e) + " finished one task today! akatri2Party";
                client.SendMessage(channel, response);
            }
        }

        private string GetUser(OnChatCommandReceivedArgs e)
        {
            return e.Command.ChatMessage.DisplayName.ToString();
        }
    }
}
