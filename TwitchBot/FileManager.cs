using System.IO;
using System.Text.RegularExpressions;

namespace TwitchBot
{
    class FileManager
    {
        private string path = @"E:\Desktop\Temp\text.txt";
        private string tempPath = @"E:\Desktop\Temp\text.txt";
        public void WriteToFile(string message)
        {
            using (StreamWriter writer = File.AppendText(tempPath))
            {
                writer.WriteLine(message + "\r");
            }
        }

        public void DeleteTaskInFile(string user)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(path))
            {
                using (StreamWriter writer = new StreamWriter(tempPath))
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
            string line = "";

            using (StreamReader reader = new StreamReader(path))
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

        public string FindAndEditTask(string user, string message)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(path))
            {
                using (StreamWriter writer = new StreamWriter(tempPath))
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
            return null;
        }

        private void DeleteEmptyLines()
        {
            string text = File.ReadAllText(path);
            string result = Regex.Replace(text, @"(^\p{Zs}*\r\n){2,}", "\r\n", RegexOptions.Multiline);
            File.WriteAllText(tempPath, result);
            RenameAndDeleteFile();
        }

        private void RenameAndDeleteFile()
        {
            File.Delete(path);
            File.Move(tempPath, path);
            File.Delete(tempPath);
        }
    }
}
