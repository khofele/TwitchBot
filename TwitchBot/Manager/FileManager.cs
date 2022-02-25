using System.IO;
using System.Text.RegularExpressions;

namespace TwitchBot
{
    class FileManager
    {
        private static string taskPath = @"E:\Desktop\Temp\text.txt";
        private string tempTaskPath = @"E:\Desktop\Temp\temptext.txt";
        private static string targetPath = @"E:\Desktop\Temp\target.txt";
        private static string targetPathVersionTwo = @"E:\Desktop\Temp\target2.txt";
        private static string suggestPath = @"E:\Desktop\Temp\suggestions.txt";
        private static string weeklyTargetPath = @"E:\Desktop\Temp\weeklytarget.txt";
        private static string pomoCounterPath = @"E:\Desktop\Temp\pomocounter.txt";
        private static string pomoCounterPathVersionTwo = @"E:\Desktop\Temp\pomocounter2.txt";


        public static string TaskPath
        {
            get => taskPath;
        }

        public static string TargetPath
        {
            get => targetPath;
        }

        public static string SuggestPath
        {
            get => suggestPath;
        }

        public static string WeeklyTargetPath
        {
            get => weeklyTargetPath;
        }

        public static string PomoCounterPath
        {
            get => pomoCounterPath;
        }

        public static string PomoCounterPathVersionTwo
        {
            get => pomoCounterPathVersionTwo;
        }

        public static string TargetPathVersionTwo
        {
            get => targetPathVersionTwo;
        }

        public void WriteToFile(string message, string path)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(message);
            }
        }

        public void DeleteTaskInFile(string user)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(taskPath))
            {
                using (StreamWriter writer = new StreamWriter(tempTaskPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("• " + user))
                        {
                            continue;
                        }
                        writer.WriteLine(line);
                    }
                }
            }
            RenameAndDeleteFile();
            DeleteEmptyLines();
        }

        public string FindTask(string user)
        {
            if (File.Exists(FileManager.TaskPath))
            {
                string line = "";

                using (StreamReader reader = new StreamReader(taskPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("• "+user))
                        {
                            string modifiedLine = line.Replace((user + " - "), "");
                            return modifiedLine;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public void FindAndEditTask(string user, string message)
        {
            if(File.Exists(FileManager.TaskPath))
            {
                string line = "";

                using (StreamReader reader = new StreamReader(taskPath))
                {
                    using (StreamWriter writer = new StreamWriter(tempTaskPath))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("• " + user))
                            {
                                string modifiedLine = line.Replace(line, "• " + user + message);
                                writer.WriteLine(modifiedLine);
                                continue;
                            }
                            writer.WriteLine(line);
                        }
                    }
                }
                RenameAndDeleteFile();
                DeleteEmptyLines();
            }
        }

        private void DeleteEmptyLines()
        {
            string text = File.ReadAllText(taskPath);
            string result = Regex.Replace(text, @"(^\p{Zs}*\r\n){2,}", "\r\n", RegexOptions.Multiline);
            File.WriteAllText(tempTaskPath, result);
            RenameAndDeleteFile();
        }

        private void RenameAndDeleteFile()
        {
            File.Delete(taskPath);
            File.Move(tempTaskPath, taskPath);
            File.Delete(tempTaskPath);
        }

        public void ResetTargetFile()
        {
            if(File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            if(File.Exists(targetPathVersionTwo))
            {
                File.Delete(targetPathVersionTwo);
            }
        }

        public void ResetWeeklyTargetFile()
        {
            if(File.Exists(weeklyTargetPath))
            {
                File.Delete(weeklyTargetPath);
            }
        }

        public void ResetPomoCounterFile()
        {
            if(File.Exists(pomoCounterPath))
            {
                File.Delete(pomoCounterPath);
            }

            if(File.Exists(pomoCounterPathVersionTwo))
            {
                File.Delete(pomoCounterPathVersionTwo);
            }
        }

        public static void ReadInExistingTasks()
        {
            if (File.Exists(taskPath))
            {
                string line = "";
                string user = "";
                string task = "";
                

                using (StreamReader reader = new StreamReader(taskPath))
                {
                    while((line = reader.ReadLine()) != null)
                    {
                        string modifiedLine = line.Replace("• ", "");
                        string[] split = modifiedLine.Split(new[] {" - "}, System.StringSplitOptions.None);

                        user = split[0];
                        for(int i = 1; i < split.Length; i++)
                        {
                            task += split[i];
                        }

                        TaskCommandManager.tasks.Add(new Task(user, task));
                        
                    }
                }
            }
        }

        public static void ReadInExistingTargetAndCurrent()
        {
            if (File.Exists(targetPath))
            {
                string line = "";
                string target = "";
                string current = "";


                using (StreamReader reader = new StreamReader(targetPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        string modifiedLine = line.Replace("Target: ", "");
                        string modifiedLineDone = modifiedLine.Replace("Current: ", "");

                        string[] split = modifiedLineDone.Split(' ');

                        target = split[0];
                        current = split[1];

                        TaskCommandManager.Target = target;
                        int.TryParse(current, out int value);
                        TaskCommandManager.Current = value;
                    }
                }
            }
        }

        public static void ReadInExistingWeeklyTargetAndCurrent()
        {
            if (File.Exists(weeklyTargetPath))
            {
                string line = "";
                string weeklyTarget = "";
                string weeklyCurrent = "";

                using (StreamReader reader = new StreamReader(weeklyTargetPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        string modifiedLine = line.Replace("Weekly Target: ", "");
                        string modifiedLineDone = modifiedLine.Replace("Current: ", "");

                        string[] split = modifiedLineDone.Split(' ');

                        weeklyTarget = split[0];
                        weeklyCurrent = split[1];

                        TaskCommandManager.WeeklyTarget = weeklyTarget;
                        int.TryParse(weeklyCurrent, out int value);
                        TaskCommandManager.WeeklyCurrent = value;
                    }
                }
            }
        }

        public bool ResetTaskList()
        {
            if (File.Exists(taskPath))
            {
                File.Delete(taskPath);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
