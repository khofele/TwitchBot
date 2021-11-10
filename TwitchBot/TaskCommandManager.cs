using System.Collections.Generic;
using System.IO;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class TaskCommandManager
    {
        public static List<Task> tasks = new List<Task>();
        public Dictionary<string, int> finishedTasks = new Dictionary<string, int>();
        private int counter = 0;
        private bool notAdded = true;
        private Task task;
        private static string target = "0";
        private static int current = 0;

        //private static int allFinishedTasks = 0;

        private FileManager fileManager = new FileManager();

        public bool NotAdded
        {
            get => notAdded;
            set
            {
                notAdded = value;
            }
        }

        public static string Target
        {
            get => target;
            set
            {
                target = value;
            }
        }

        public static int Current
        {
            get => current;
            set
            {
                current = value;
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
            string lowerMessage = chatMessage.Replace("!ADDTASK", "!addtask");
            string taskMessage = "";

            if (lowerMessage != null)
            {
                 taskMessage = lowerMessage.Replace("!addtask", " -");
            }
            else
            {
                taskMessage = chatMessage.Replace("!addtask", " -");
            }

            string response = CheckAndAddTask(taskMessage, User.GetUser(e), e);

            if (finishedTasks.ContainsKey(User.GetUser(e)) == false)
            {
                finishedTasks.Add(User.GetUser(e), 0);
            }
            return response;
        }

        public string EditTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string lowerMessage = chatMessage.Replace("!EDITTASK", "!edittask");
            string editMessage = "";

            if(lowerMessage != null)
            {
                editMessage = lowerMessage.Replace("!edittask", " -");
            }
            else
            {
                editMessage = chatMessage.Replace("!edittask", " -");
            }

            CheckAndEditTask(User.GetUser(e), editMessage, e);

            if (fileManager.FindTask((User.GetUser(e))) != null)
            {
                return User.GetUser(e) + " edited the task: " + editMessage.Replace(" -", "") + "! akatri2Work";
            }
            else
            {
                return User.GetUser(e) + " you have to add a task to edit a task! akatri2Pew";
            }
        }

        public string RemoveTaskCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(User.GetUser(e)) != null)
            {
                string canceledTask = fileManager.FindTask(User.GetUser(e));
                RemoveTask(User.GetUser(e));
                fileManager.DeleteTaskInFile(User.GetUser(e));
                return User.GetUser(e) + " canceled the task: " + canceledTask.Replace("•", "") + "! akatri2Pew ";
            }
            else
            {
                return User.GetUser(e) + " you have to add a task to remove a task! akatri2Pew";
            }
        }

        public string TaskDoneCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(User.GetUser(e)) != null)
            {
                string response = CheckAndAddFinishedTask(e);
                string finishedTask = fileManager.FindTask(User.GetUser(e)).ToUpper();
                RemoveTask(User.GetUser(e));
                fileManager.DeleteTaskInFile(User.GetUser(e));
                AddFinishedTask();
                SetTargetToFile(target);
                CheckTargetAchieved();
                return "CONGRATS " + User.GetUser(e) + "! akatri2Hype YOU COMPLETED YOUR TASK! " + finishedTask.Replace("•", "") + " IS DONE! " + response + " akatri2Party akatri2Lovings";
            }
            else
            {
                return User.GetUser(e) + " you have to add a task to finish a task! akatri2Pew";
            }
        }

        public string FinishedTasksCommand(OnChatCommandReceivedArgs e)
        {
            return CheckFinishedTask(e);
        }

        public string GetAllFinishedTasks()
        {
            if(current == 1)
            {
                return "THE BLOPSQUAD FINISHED 1 TASK TODAY! YOU'RE DOING AMAZING GUYS!  akatri2Party akatri2Hype";
            } 
            else if(current == 0)
            {
                return "THE BLOPSQUAD HASN'T FINISHED ANY TASKS TODAY! COME ON GUYS YOU CAN DO IT! akatri2Lovings";
            }
            else
            {
                return "THE BLOPSQUAD FINISHED " + current + " TASKS TODAY! YOU'RE DOING AMAZING GUYS!  akatri2Party akatri2Hype";
            }
        }

        public string SetTargetCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString().ToLower();
            target = chatMessage.Replace("!settarget ", "");
            SetTargetToFile(target);
            string response = "Target set to: " + target + "!";
            return response;
        }

        public string SetCurrentCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString().ToLower(); ;
            int.TryParse(chatMessage.Replace("!setcurrent ", ""), out int current);
            SetCurrentToFile(current);
            string response = "Current tasks set to: " + current + "!";
            return response;
        }

        public string MyTaskCommand(OnChatCommandReceivedArgs e)
        {
            string user = User.GetUser(e);
            if (fileManager.FindTask(User.GetUser(e)) != null)
            {
                string task = fileManager.FindTask(user);
                string response = user + " you are working on: " + task.Replace("•", "") + "! Good luck! <3";
                return response;
            } else
            {
                return user + " you have to add a task first! You got this! <3";
            }
        }

        public string ResetTaskListCommand()
        {
            if(fileManager.ResetTaskList())
            {
                tasks.Clear();
                finishedTasks.Clear();
                current = 0;
                target = "0";
                return "Reset successful!";
            }
            else
            {
                return "File not found!";
            }
        }

        private void SetTargetToFile(string target)
        {
            fileManager.ResetTargetFile();
            fileManager.WriteToFile("Target: " + target + " " + "Current: " + current, FileManager.TargetPath);
        }

        private void SetCurrentToFile(int currentTasks)
        {
            fileManager.ResetTargetFile();
            current = currentTasks;
            fileManager.WriteToFile("Target: " + target + " " + "Current: " + currentTasks, FileManager.TargetPath);
        }

        private void CheckTargetAchieved()
        {
            if(current >= int.Parse(target) && int.Parse(target) > 0)
            {
                Bot.SendChatMessage("TARGET ACHIEVED! TARGET ACHIEVED! I AM SO PROUD OF Y'ALL! <3");
            }
        }


        private string CheckAndAddTask(string taskMessage, string user, OnChatCommandReceivedArgs e)
        {
            task = new Task(user, taskMessage);
            CheckTaskUser(task.User);

            if (NotAdded == true)
            {
                fileManager.WriteToFile("•" + User.GetUser(e) + task.UserTask, FileManager.TaskPath);
                AddTask(task);
                return User.GetUser(e) + " added a task: " + taskMessage.Replace("- ", "") + "! akatri2Work ";
            }
            else
            {
                return User.GetUser(e) + " you already added a task! Remove or finish your task first! akatri2Pew ";
            }
        }

        private void CheckAndEditTask(string user, string editMessage, OnChatCommandReceivedArgs e)
        {
            fileManager.FindAndEditTask(user, editMessage);
        }

        private string CheckAndAddFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (finishedTasks.ContainsKey(User.GetUser(e)))
            {
                int finished = finishedTasks[User.GetUser(e)];
                finished++;
                finishedTasks[User.GetUser(e)] = finished;

                if (finished == 1)
                {
                    return User.GetUser(e) + " finished " + finished + " task today!";
                }
                else
                {
                    return User.GetUser(e) + " finished " + finished + " tasks today!";
                }
            }
            else
            {
                finishedTasks.Add(User.GetUser(e), 1);
                return User.GetUser(e) + " finished one task today! akatri2Party";
            }
        }

        private string CheckFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (finishedTasks.ContainsKey(User.GetUser(e)))
            {
                int finished = finishedTasks[User.GetUser(e)];

                if (finished == 1)
                {
                    return User.GetUser(e) + " finished " + finished + " task today! akatri2Party";
                }
                else
                {
                    return User.GetUser(e) + " finished " + finished + " tasks today! akatri2Party";
                }
            }
            else
            {
                return User.GetUser(e) + " you finished 0 tasks today! COME ON YOU CAN DO IT! akatri2Lovings";
            }
        }

        private int AddFinishedTask()
        {
            current += 1;
            return current;
        }
    }
}
