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
        private static string weeklyTarget = "0";
        private static int weeklyCurrent = 0;
        private static string currentPomo = "0";
        private static string pomoGoal = "0";

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

        public static string WeeklyTarget
        {
            get => weeklyTarget;
            set
            {
                weeklyTarget = value;
            }
        }

        public static int WeeklyCurrent
        {
            get => weeklyCurrent;
            set
            {
                weeklyCurrent = value;
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
                return User.GetUser(e) + " canceled the task: " + canceledTask.Replace("• ", "") + "! akatri2Pew ";
            }
            else
            {
                return User.GetUser(e) + " you have to add a task to remove a task! akatri2Pew";
            }
        }

        public string DeleteTaskCommand(string user, OnChatCommandReceivedArgs e)
        {
            if(User.GetUser(e) != user)
            {
                if(fileManager.FindTask(user) != null)
                {
                    RemoveTask(user);
                    fileManager.DeleteTaskInFile(user);

                    return User.GetUser(e)+ " " + user + "'s task got deleted!";
                }
                return User.GetUser(e) + " user's task not found!";
            }
            else
            {
                return User.GetUser(e) + " please tag someone!";
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
                SetWeeklyTargetToFile(weeklyTarget);
                CheckTargetAchieved();
                return "CONGRATS " + User.GetUser(e) + "! akatri2Hype YOU COMPLETED YOUR TASK! " + finishedTask.Replace("• ", "") + " IS DONE! " + response + " akatri2Hype akatri2Lovings";
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
                return "THE BLOPSQUAD FINISHED 1 TASK TODAY! YOU'RE DOING AMAZING GUYS! akatri2Work akatri2Hype";
            } 
            else if(current == 0)
            {
                return "THE BLOPSQUAD HASN'T FINISHED ANY TASKS TODAY! COME ON GUYS YOU CAN DO IT! akatri2Lovings";
            }
            else
            {
                return "THE BLOPSQUAD FINISHED " + current + " TASKS TODAY! YOU'RE DOING AMAZING GUYS! akatri2Work akatri2Hype";
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

        public string SetWeeklyTargetCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString().ToLower(); ;
            weeklyTarget = chatMessage.Replace("!setweeklytarget ", "");
            SetWeeklyTargetToFile(weeklyTarget);
            string response = "Weekly target set to: " + weeklyTarget + "!";
            return response;
        }

        public string SetPomoCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString().ToLower();
            currentPomo = chatMessage.Replace("!setpomo ", "");
            SetPomoToFile(currentPomo); 
            string response = "Current pomo: " + currentPomo + "!";
            return response;
        }

        public string SetPomoGoalCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString().ToLower();
            pomoGoal = chatMessage.Replace("!setpomogoal ", "");
            SetPomoGoalToFile(pomoGoal);
            string response = "Pomo goal: " + pomoGoal + "!";
            return response;
        }

        public string MyTaskCommand(OnChatCommandReceivedArgs e)
        {
            string user = User.GetUser(e);
            if (fileManager.FindTask(User.GetUser(e)) != null)
            {
                string task = fileManager.FindTask(user);
                string response = user + " you are working on: " + task.Replace("• ", "") + "! Good luck! <3";
                return response;
            } else
            {
                return user + " you have to add a task first! You got this! <3";
            }
        }

        public string ResetListCommand()
        {
            int counter = 0;
            if (fileManager.ResetTaskList())
            {
                tasks.Clear();
                finishedTasks.Clear();
                counter += 1;
            }
            if(File.Exists(FileManager.TargetPath))
            {
                fileManager.ResetTargetFile();
                current = 0;
                target = "0";
                counter += 1;
            }
            if(File.Exists(FileManager.WeeklyTargetPath))
            {
                weeklyCurrent = 0;
                weeklyTarget = "0";
                fileManager.ResetWeeklyTargetFile();
                counter += 1;
            }
            if(File.Exists(FileManager.PomoCounterPath))
            {
                pomoGoal = "0";
                currentPomo = "0";
                fileManager.ResetPomoCounterFile();
                counter += 1;
            }

            if (counter >= 1)
            {
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

        private void SetWeeklyTargetToFile(string weeklyTarget)
        {
            fileManager.ResetWeeklyTargetFile();
            fileManager.WriteToFile("Weekly Target: " + weeklyTarget + " " + "Current: " + weeklyCurrent, FileManager.WeeklyTargetPath);
        }

        private void SetPomoToFile(string currentPomo)
        {
            fileManager.ResetPomoCounterFile();
            fileManager.WriteToFile(currentPomo + "/" + pomoGoal, FileManager.PomoCounterPath);
        }

        private void SetPomoGoalToFile(string pomoGoal)
        {
            fileManager.ResetPomoCounterFile();
            fileManager.WriteToFile(currentPomo + "/" + pomoGoal, FileManager.PomoCounterPath);
        }

        private void CheckTargetAchieved()
        {
            if(current >= int.Parse(target) && int.Parse(target) > 0)
            {
                Bot.SendChatMessage("TARGET ACHIEVED! TARGET ACHIEVED! I AM SO PROUD OF Y'ALL! akatri2Lovings");
            }

            if(weeklyCurrent >= int.Parse(weeklyTarget) && int.Parse(weeklyTarget) > 0)
            {
                Bot.SendChatMessage("WEEKLY TARGET ACHIEVED! WEEKLY TARGET ACHIEVED! I AM SO PROUD OF Y'ALL! akatri2Lovings");
            }
        }


        private string CheckAndAddTask(string taskMessage, string user, OnChatCommandReceivedArgs e)
        {
            task = new Task(user, taskMessage);
            CheckTaskUser(task.User);

            if (NotAdded == true)
            {
                fileManager.WriteToFile("• " + User.GetUser(e) + task.UserTask, FileManager.TaskPath);
                AddTask(task);
                return User.GetUser(e) + " added a task: " + taskMessage.Replace("- ", "") + "! akatri2Work";
            }
            else
            {
                return User.GetUser(e) + " you already added a task! Remove or finish your task first! akatri2Pew";
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
                return User.GetUser(e) + " finished one task today! akatri2Hype";
            }
        }

        private string CheckFinishedTask(OnChatCommandReceivedArgs e)
        {
            if (finishedTasks.ContainsKey(User.GetUser(e)))
            {
                int finished = finishedTasks[User.GetUser(e)];

                if (finished == 1)
                {
                    return User.GetUser(e) + " finished " + finished + " task today! akatri2Hype";
                }
                else
                {
                    return User.GetUser(e) + " finished " + finished + " tasks today! akatri2Hype";
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
            weeklyCurrent += 1;
            return current;
        }
    }
}
