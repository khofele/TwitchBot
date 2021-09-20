using System;
using System.Collections.Generic;
using System.Threading;
using TwitchLib.Api.Core.Common;

namespace TwitchBot
{
    class Cooldown
    {
        // ---------- QUOTES ----------
        public static Dictionary<Quote, DateTime> globalCooldownsQuotes = new Dictionary<Quote, DateTime>()
        {
            { Quote.TEA, DateTime.Now },
            { Quote.ONEMORE, DateTime.Now },
            { Quote.BIO, DateTime.Now}
        };

        public static Dictionary<Quote, int> globalCooldownLengthsQuotes = new Dictionary<Quote, int>()
        {
            { Quote.TEA, 30 },
            { Quote.ONEMORE, 30 },
            { Quote.BIO, 30}
        };

        public static Dictionary<Quote, bool> globalCooldownsRunningQuotes = new Dictionary<Quote, bool>()
        {
            { Quote.TEA, false },
            { Quote.ONEMORE, false },
            { Quote.BIO, false}
        };

        // ---------- POMO-COMMANDS ----------
        public static Dictionary<Pomodoro, DateTime> globalCooldownsPomos = new Dictionary<Pomodoro, DateTime>()
        {
            { Pomodoro.ADD, DateTime.Now },
            { Pomodoro.EDIT, DateTime.Now },
            { Pomodoro.DONE, DateTime.Now },
            { Pomodoro.REMOVE, DateTime.Now},
            { Pomodoro.FINISHEDTASKS, DateTime.Now }
        };

        public static Dictionary<Pomodoro, int> globalCooldownLengthsPomos = new Dictionary<Pomodoro, int>()
        {
            { Pomodoro.ADD, 5 },
            { Pomodoro.EDIT, 5 },
            { Pomodoro.DONE, 5 },
            { Pomodoro.REMOVE, 5 },
            { Pomodoro.FINISHEDTASKS, 5 }
        };

        public static Dictionary<Pomodoro, bool> globalCooldownsRunningPomos = new Dictionary<Pomodoro, bool>()
        {
            { Pomodoro.ADD, false },
            { Pomodoro.EDIT, false },
            { Pomodoro.DONE, false },
            { Pomodoro.REMOVE, false },
            { Pomodoro.FINISHEDTASKS, false }
        };

        //public static bool CheckCommandAvailableQuote(Quote quote)
        //{
        //    if (globalCooldownsRunningQuotes[quote] == true)
        //    {
        //        return false;
        //    } 
        //    else
        //    {
        //        return true;
        //    }
        //}

        public static bool CheckCooldownOffQuote(Quote quote)
        {
            if(DateTime.Now >= Cooldown.globalCooldownsQuotes[quote].AddSeconds(Cooldown.globalCooldownLengthsQuotes[quote])) {
                globalCooldownsRunningQuotes[quote] = false;
                return true;
            } 
            else
            {
                return false;
            }
        }

        //public static bool CheckCommandAvailablePomodoro(Pomodoro pomo)
        //{
        //    if (globalCooldownsRunningPomos[pomo] == true)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public static bool CheckCooldownOffPomodoro(Pomodoro pomo)
        {
            if (DateTime.Now >= Cooldown.globalCooldownsPomos[pomo].AddSeconds(Cooldown.globalCooldownLengthsPomos[pomo]))
            {
                globalCooldownsRunningPomos[pomo] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
