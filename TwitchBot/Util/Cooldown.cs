using System;
using System.Collections.Generic;
using System.Threading;
using TwitchLib.Api.Core.Common;

namespace TwitchBot
{
    class Cooldown
    {
        public static Dictionary<Command, DateTime> globalCooldowns = new Dictionary<Command, DateTime>()
        {
            { Command.TEA, DateTime.Now },
            { Command.ONEMORE, DateTime.Now },


            { Command.ADD, DateTime.Now },
            { Command.EDIT, DateTime.Now },
            { Command.DONE, DateTime.Now },
            { Command.REMOVE, DateTime.Now},
            { Command.FINISHEDTASKS, DateTime.Now },
            { Command.ALLFINISHEDTASKS, DateTime.Now },
            { Command.MYTASK, DateTime.Now },


            { Command.SPICECHECK, DateTime.Now },
            { Command.NAPCHECK, DateTime.Now },
            { Command.HYPECHECK, DateTime.Now },
            { Command.LOVECHECK, DateTime.Now },
            { Command.BOOBACHECK, DateTime.Now },
            { Command.CHECKCHECK, DateTime.Now },
            { Command.SUSCHECK, DateTime.Now },
            { Command.BOJOCHECK, DateTime.Now },
            { Command.BUMBUM, DateTime.Now },
            { Command.HAPPYHIPPO, DateTime.Now },
            { Command.VINCENT, DateTime.Now },
            { Command.VIBECHECK, DateTime.Now },
            { Command.CHAIRCHECK, DateTime.Now },


            { Command.BIO, DateTime.Now },
            { Command.SUGGEST, DateTime.Now },
            { Command.BREAK, DateTime.Now },
            { Command.UNO, DateTime.Now },
            { Command.YO, DateTime.Now },
            { Command.LOVE, DateTime.Now },
            { Command.HUG, DateTime.Now },
            { Command.TRAGER, DateTime.Now },

            { Command.PRESENTCHECK, DateTime.Now },
            { Command.LIST, DateTime.Now },
            { Command.MISTLETOE, DateTime.Now },
            { Command.ALLNAUGHTYLIST, DateTime.Now },
            { Command.ALLGOODLIST, DateTime.Now },
            // ------------------------------------------
            { Command.NAUGHTYLIST, DateTime.Now },
            { Command.GOODLIST, DateTime.Now },

            { Command.SPOOKCHECK, DateTime.Now },

            { Command.DEBUG, DateTime.Now },

            //------- MODS ONLY -------
            { Command.SETTARGET, DateTime.Now },
            { Command.SETCURRENT, DateTime.Now },
            { Command.RESET, DateTime.Now },
            { Command.SETWEEKLYTARGET, DateTime.Now },
            { Command.SETPOMO, DateTime.Now },
            { Command.SETPOMOGOAL, DateTime.Now },
            { Command.DELETETASK, DateTime.Now }
        };

        public static Dictionary<Command, int> globalCooldownLengths = new Dictionary<Command, int>()
        {
            { Command.TEA, 30 },
            { Command.ONEMORE, 30 },


            { Command.ADD, 0 },
            { Command.EDIT, 0 },
            { Command.DONE, 0 },
            { Command.REMOVE, 0},
            { Command.FINISHEDTASKS, 0 },
            { Command.ALLFINISHEDTASKS, 0 },
            { Command.MYTASK, 0 },


            { Command.SPICECHECK, 2 },
            { Command.NAPCHECK, 2 },
            { Command.HYPECHECK, 2 },
            { Command.LOVECHECK, 2 },
            { Command.BOOBACHECK, 2 },
            { Command.CHECKCHECK, 2 },
            { Command.SUSCHECK, 2 },
            { Command.BOJOCHECK, 2 },
            { Command.BUMBUM, 2 },
            { Command.HAPPYHIPPO, 2},
            { Command.VINCENT, 2 },
            { Command.VIBECHECK, 2 },
            { Command.CHAIRCHECK, 2 },


            { Command.BIO, 30 },
            { Command.SUGGEST, 10 },
            { Command.BREAK, 5 },
            { Command.UNO, 5 },
            { Command.YO, 2 },
            { Command.LOVE, 0 },
            { Command.HUG, 0 },
            { Command.TRAGER, 2 },

            { Command.PRESENTCHECK, 2 },
            { Command.LIST, 2 },
            { Command.MISTLETOE, 2 },
            { Command.ALLNAUGHTYLIST, 5 },
            { Command.ALLGOODLIST, 5 },
            // ------------------------------------------
            { Command.NAUGHTYLIST, 0 },
            { Command.GOODLIST, 0 },

            { Command.SPOOKCHECK, 2 },

            { Command.DEBUG, 0},

            //------- MODS ONLY -------
            { Command.SETTARGET, 0 },
            { Command.SETCURRENT, 0 },
            { Command.RESET, 0 },
            { Command.SETWEEKLYTARGET, 0 },
            { Command.SETPOMO, 0 },
            { Command.SETPOMOGOAL, 0 },
            { Command.DELETETASK, 0 }
        };

        public static Dictionary<Command, bool> globalCooldownsRunning = new Dictionary<Command, bool>()
        {
            { Command.TEA, false },
            { Command.ONEMORE, false },


            { Command.ADD, false },
            { Command.EDIT, false },
            { Command.DONE, false },
            { Command.REMOVE, false},
            { Command.FINISHEDTASKS, false },
            { Command.ALLFINISHEDTASKS, false },
            { Command.MYTASK, false },


            { Command.SPICECHECK, false },
            { Command.NAPCHECK, false },
            { Command.HYPECHECK, false },
            { Command.LOVECHECK, false },
            { Command.BOOBACHECK, false },
            { Command.CHECKCHECK, false },
            { Command.SUSCHECK, false },
            { Command.BOJOCHECK, false },
            { Command.BUMBUM, false },
            { Command.HAPPYHIPPO, false },
            { Command.VINCENT, false },
            { Command.VIBECHECK, false },
            { Command.CHAIRCHECK, false },


            { Command.BIO, false },
            { Command.SUGGEST, false },
            { Command.BREAK, false },
            { Command.UNO, false },
            { Command.YO, false },
            { Command.LOVE, false },
            { Command.HUG, false },
            { Command.TRAGER, false },

            { Command.PRESENTCHECK, false },
            { Command.LIST, false },
            { Command.MISTLETOE, false },
            { Command.ALLGOODLIST, false },
            { Command.ALLNAUGHTYLIST, false },
            // ------------------------------------------
            { Command.NAUGHTYLIST, false },
            { Command.GOODLIST, false },

            { Command.SPOOKCHECK, false },

            { Command.DEBUG, false },

            //------- MODS ONLY -------
            { Command.SETTARGET, false },
            { Command.SETCURRENT, false },
            { Command.RESET, false },
            { Command.SETWEEKLYTARGET, false },
            { Command.SETPOMO, false },
            { Command.SETPOMOGOAL, false },
            { Command.DELETETASK, false }
        };


        public static bool CheckCooldownOff(Command command)
        {
            if (DateTime.Now >= globalCooldowns[command].AddSeconds(globalCooldownLengths[command]))
            {
                globalCooldownsRunning[command] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
