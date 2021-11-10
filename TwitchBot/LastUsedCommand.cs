using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class LastUsedCommand
    {
        public static Dictionary<String, Command> lastUsedCommands = new Dictionary<string, Command>();

        public static void LastUsedCommandCheck(string commandText, string user)
        {
            Command command;
            switch (commandText.ToLower())
            {
                // POMO
                //case "addtask":
                //    command = Command.ADD;
                //    break;

                //case "edittask":
                //    command = Command.EDIT;
                //    break;

                //case "removetask":
                //    command = Command.REMOVE;
                //    break;

                //case "taskdone":
                //    command = Command.DONE;
                //    break;

                //case "finishedtasks":
                //    DisplayCommand(Command.FINISHEDTASKS, e);
                //    break;

                //case "allfinishedtasks":
                //    DisplayCommand(Command.ALLFINISHEDTASKS, e);
                //    break;

                //case "mytask":
                //    DisplayCommand(Command.MYTASK, e);
                //    break;


                // QUOTES
                case "tea":
                    command = Command.TEA;
                    break;

                case "onemore":
                    command = Command.ONEMORE;
                    break;

                case "hello":
                    command = Command.BIO;
                    break;

                // RANDOM
                case "spicecheck":
                    command = Command.SPICECHECK;
                    break;

                case "napcheck":
                    command = Command.NAPCHECK;
                    break;

                case "hypecheck":
                    command = Command.HYPECHECK;
                    break;

                case "lovecheck":
                    command = Command.LOVECHECK;
                    break;

                case "checkcheck":
                    command = Command.CHECKCHECK;
                    break;

                case "boobacheck":
                    command = Command.BOOBACHECK;
                    break;

                case "suscheck":
                    command = Command.SUSCHECK;
                    break;

                case "bojo":
                    command = Command.BOJOCHECK;
                    break;

                case "bumbum":
                    command = Command.BUMBUM;
                    break;

                case "chaircheck":
                    command = Command.CHAIRCHECK;
                    break;

                case "happyhippo":
                    command = Command.HAPPYHIPPO;
                    break;

                // SPOOKTOBER
                //case "spookcheck":
                //    command = Command.SPOOKCHECK;
                //    break;

                // GENERAL
                //case "suggest":
                //    command = Command.SUGGEST;
                //    break;

                case "break":
                    command = Command.BREAK;
                    break;

                case "yo":
                    command = Command.YO;
                    break;

                case "love":
                    command = Command.LOVE;
                    break;

                case "hug":
                    command = Command.HUG;
                    break;

                case "trager":
                    command = Command.TRAGER;
                    break;

                case "vincent":
                    command = Command.VINCENT;
                    break;

                //case "uno":
                //    DisplayCommand(Command.UNO, e);
                //    break;

                // POMO
                //case "timeoutdelete":
                //    break;

                //case "settarget":
                //    DisplayCommand(Command.SETTARGET, e);
                //    break;

                default:
                    command = Command.NULL;
                    break;
            }
            if(command != Command.NULL && lastUsedCommands.ContainsKey(user) == false)
            {
                lastUsedCommands.Add(user, command);
            }
            else if(command != Command.NULL)
            {
                lastUsedCommands[user] = command;
            }
        }
    }
}
