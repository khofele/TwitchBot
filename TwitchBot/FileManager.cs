using System.IO;
using System.Text.RegularExpressions;

namespace TwitchBot
{
    class FileManager
    {
        private static string taskPath = @"E:\Desktop\Temp\text.txt";
        private string tempTaskPath = @"E:\Desktop\Temp\temptext.txt";
        private static string targetPath = @"E:\Desktop\Temp\target.txt";

        public static string TaskPath
        {
            get => taskPath;
        }

        public static string TargetPath
        {
            get => targetPath;
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
                        if (line.StartsWith(user))
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
                        if (line.StartsWith(user))
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
                            if (line.StartsWith(user))
                            {
                                string modifiedLine = line.Replace(line, user + message);
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
            File.Delete(targetPath);
        }
    }
}
