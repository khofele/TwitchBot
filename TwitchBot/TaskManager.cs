using System.Collections.Generic;

namespace TwitchBot
{
    class TaskManager
    {
        public List<Task> tasks = new List<Task>();
        public Dictionary<string, int> finishedTasks = new Dictionary<string, int>();
        private int counter = 0;
        private bool notAdded = true;

        public bool NotAdded
        {
            get => notAdded;
            set
            {
                notAdded = value;
            }
        }

        public void RemoveTask(string user)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].User == user)
                {
                    tasks.Remove(tasks[i]);
                }
            }
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void CheckTaskUser(string user)
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

        public void ResetCounter()
        {
            counter = 0;
        }

        public void ResetNotAdded()
        {
            notAdded = true;
        }
    }
}
