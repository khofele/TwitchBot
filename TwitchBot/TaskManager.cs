using System.Collections.Generic;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class TaskManager
    {
        public List<Task> tasks = new List<Task>();
        public Dictionary<string, int> finishedTasks = new Dictionary<string, int>();
        private int counter = 0;
        private bool notAdded = true;
        private Task task;

        private FileManager fileManager = new FileManager();

        public bool NotAdded
        {
            get => notAdded;
            set
            {
                notAdded = value;
            }
        }

        private void RemoveTask(string user)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].User == user)
                {
                    tasks.Remove(tasks[i]);
                }
            }
        }

        private void AddTask(Task task)
        {
            tasks.Add(task);
        }

        private void CheckTaskUser(string user)
        {
            ResetCounter();
            ResetNotAdded();
            if(tasks.Count != 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].User == user)
                    {
                        counter++;
                    }
                }

                if(counter >= 1)
                {
                    notAdded = false;
                    return;
                } 
                else if(counter <= 0)
                {
                    notAdded = true;
                    return;
                }
            }
        }

        private void ResetCounter()
        {
            counter = 0;
        }

        private void ResetNotAdded()
        {
            notAdded = true;
        }

        public string AddTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string taskMessage = chatMessage.Replace("!addtask", " -");
            string response = CheckAndAddTask(taskMessage, GetUser(e), e);
            if (finishedTasks.ContainsKey(GetUser(e)) == false)
            {
                finishedTasks.Add(GetUser(e), 0);
            }
            return response;
        }

        public string EditTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string editMessage = chatMessage.Replace("!edittask", " -");
            CheckAndEditTask(GetUser(e), editMessage, e);
            if (fileManager.FindTask(GetUser(e)) != null)
            {
                return GetUser(e) + " edited the task: " + editMessage.Replace(" -", "") + "! akatri2Work";
            }
            else
            {
                return GetUser(e) + " you have to add a task to edit a task! akatri2Pew";
            }
        }

        public string RemoveTaskCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(GetUser(e)) != null)
            {
                string canceledTask = fileManager.FindTask(GetUser(e));
                RemoveTask(GetUser(e));
                fileManager.DeleteTaskInFile(GetUser(e));
                return GetUser(e) + " canceled the task: " + canceledTask + "! akatri2Pew ";
            }
            else
            {
                return GetUser(e) + " you have to add a task to remove a task! akatri2Pew";
            }
        }

        public string TaskDoneCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(GetUser(e)) != null)
            {
                string response = CheckAndAddFinishedTask(e);
                string finishedTask = fileManager.FindTask(GetUser(e)).ToUpper();
                RemoveTask(GetUser(e));
                fileManager.DeleteTaskInFile(GetUser(e));
                return "CONGRATS " + GetUser(e) + "! akatri2Hype YOU COMPLETED YOUR TASK! " + finishedTask + " IS DONE! " + response + " akatri2Party akatri2Lovings";
            }
            else
            {
                return GetUser(e) + " you have to add a task to finish a task! akatri2Pew";
            }
        }

        public string FinishedTasksCommand(OnChatCommandReceivedArgs e)
        {
            return CheckFinishedTask(e);
        }

        private string CheckAndAddTask(string taskMessage, string user, OnChatCommandReceivedArgs e)
        {
            task = new Task(user, taskMessage);
            CheckTaskUser(task.User);

            if (NotAdded == true)
            {
                fileManager.WriteToFile(GetUser(e) + task.UserTask);
                AddTask(task);
                return GetUser(e) + " added a task: " + taskMessage.Replace("- ", "") + "! akatri2Work ";
            }
            else
            {
                return GetUser(e) + " you already added a task! Remove or finish your task first! akatri2Pew ";
            }
        }

        private void CheckAndEditTask(string user, string editMessage, OnChatCommandReceivedArgs e)
        {
            fileManager.FindAndEditTask(user, editMessage);
        }

        private string CheckAndAddFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (finishedTasks.ContainsKey(GetUser(e)))
            {
                int finished = finishedTasks[GetUser(e)];
                finished++;
                finishedTasks[GetUser(e)] = finished;

                if (finished == 1)
                {
                    return GetUser(e) + " finished " + finished + " task today!";
                }
                else
                {
                    return GetUser(e) + " finished " + finished + " tasks today!";
                }
            }
            else
            {
                finishedTasks.Add(GetUser(e), 1);
                return GetUser(e) + " finished one task today! akatri2Party";
            }
        }

        private string CheckFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (finishedTasks.ContainsKey(GetUser(e)))
            {
                int finished = finishedTasks[GetUser(e)];

                if (finished == 1)
                {
                    return GetUser(e) + " finished " + finished + " task today! akatri2Party";
                }
                else
                {
                    return GetUser(e) + " finished " + finished + " tasks today! akatri2Party";
                }
            }
            else
            {
                return GetUser(e) + " you finished 0 tasks today! COME ON YOU CAN DO IT! akatri2Lovings";
            }
        }

        private string GetUser(OnChatCommandReceivedArgs e)
        {
            return e.Command.ChatMessage.DisplayName.ToString();
        }
    }
}
