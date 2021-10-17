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
            { Pomodoro.FINISHEDTASKS, DateTime.Now },
            { Pomodoro.ALLFINISHEDTASKS, DateTime.Now },
            { Pomodoro.MYTASK, DateTime.Now }
        };

        public static Dictionary<Pomodoro, int> globalCooldownLengthsPomos = new Dictionary<Pomodoro, int>()
        {
            { Pomodoro.ADD, 0 },
            { Pomodoro.EDIT, 0 },
            { Pomodoro.DONE, 0 },
            { Pomodoro.REMOVE, 0 },
            { Pomodoro.FINISHEDTASKS, 0 },
            { Pomodoro.ALLFINISHEDTASKS, 0 },
            { Pomodoro.MYTASK, 0 }
        };

        public static Dictionary<Pomodoro, bool> globalCooldownsRunningPomos = new Dictionary<Pomodoro, bool>()
        {
            { Pomodoro.ADD, false },
            { Pomodoro.EDIT, false },
            { Pomodoro.DONE, false },
            { Pomodoro.REMOVE, false },
            { Pomodoro.FINISHEDTASKS, false },
            { Pomodoro.ALLFINISHEDTASKS, false },
            { Pomodoro.MYTASK, false}
        };

        // ---------- RANDOM-COUNTER-COMMANDS ----------
        public static Dictionary<RandomCounter, DateTime> globalCooldownsRandom = new Dictionary<RandomCounter, DateTime>()
        {
            { RandomCounter.SPICECHECK, DateTime.Now },
            { RandomCounter.NAPCHECK, DateTime.Now },
            { RandomCounter.HYPECHECK, DateTime.Now },
            { RandomCounter.LOVECHECK, DateTime.Now },
            { RandomCounter.BOOBACHECK, DateTime.Now },
            { RandomCounter.CHECKCHECK, DateTime.Now },
            { RandomCounter.SUSCHECK, DateTime.Now },
            { RandomCounter.SPOOKCHECK, DateTime.Now }
        };

        public static Dictionary<RandomCounter, int> globalCooldownLengthsRandom = new Dictionary<RandomCounter, int>()
        {
            { RandomCounter.SPICECHECK, 10 },
            { RandomCounter.NAPCHECK, 10},
            { RandomCounter.HYPECHECK, 10 },
            { RandomCounter.LOVECHECK, 10 },
            { RandomCounter.BOOBACHECK, 10 },
            { RandomCounter.CHECKCHECK, 10 },
            { RandomCounter.SUSCHECK, 10},
            { RandomCounter.SPOOKCHECK, 10 }
        };

        public static Dictionary<RandomCounter, bool> globalCooldownsRunningRandom = new Dictionary<RandomCounter, bool>()
        {
            { RandomCounter.SPICECHECK, false },
            { RandomCounter.NAPCHECK, false },
            { RandomCounter.HYPECHECK, false },
            { RandomCounter.LOVECHECK, false },
            { RandomCounter.BOOBACHECK, false },
            { RandomCounter.CHECKCHECK, false },
            { RandomCounter.SUSCHECK, false },
            { RandomCounter.SPOOKCHECK, false }
        };

        // ---------- GENERAL-COMMANDS ----------
        public static Dictionary<General, DateTime> globalCooldownsGeneral = new Dictionary<General, DateTime>()
        {
            { General.SUGGEST, DateTime.Now }
        };

        public static Dictionary<General, int> globalCooldownLengthsGeneral = new Dictionary<General, int>()
        {
            { General.SUGGEST, 10 }
        };

        public static Dictionary<General, bool> globalCooldownsRunningGeneral = new Dictionary<General, bool>()
        {
            { General.SUGGEST, false }
        };

        public static bool CheckCooldownOffQuote(Quote quote)
        {
            if(DateTime.Now >= globalCooldownsQuotes[quote].AddSeconds(globalCooldownLengthsQuotes[quote])) {
                globalCooldownsRunningQuotes[quote] = false;
                return true;
            } 
            else
            {
                return false;
            }
        }

        public static bool CheckCooldownOffPomodoro(Pomodoro pomo)
        {
            if (DateTime.Now >= globalCooldownsPomos[pomo].AddSeconds(globalCooldownLengthsPomos[pomo]))
            {
                globalCooldownsRunningPomos[pomo] = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCooldownOffRandom(RandomCounter random)
        {
            if (DateTime.Now >= globalCooldownsRandom[random].AddSeconds(globalCooldownLengthsRandom[random]))
            {
                globalCooldownsRunningRandom[random] = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCooldownOffGeneral(General general)
        {
            if (DateTime.Now >= globalCooldownsGeneral[general].AddSeconds(globalCooldownLengthsGeneral[general]))
            {
                globalCooldownsRunningGeneral[general] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
