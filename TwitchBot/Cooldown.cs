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

        public static Dictionary<Quote, int> cooldownLengthsQuotes = new Dictionary<Quote, int>()
        {
            { Quote.TEA, 30 },
            { Quote.ONEMORE, 30 },
            { Quote.BIO, 30}
        };

        // ---------- POMO-COMMANDS ----------
        public static Dictionary<Pomodoro, DateTime> globalCooldownsPomos = new Dictionary<Pomodoro, DateTime>()
        {
            { Pomodoro.ADD, DateTime.Now },
            { Pomodoro.EDIT, DateTime.Now },
            { Pomodoro.DONE, DateTime.Now },
            { Pomodoro.REMOVE, DateTime.Now}
        };

        public static Dictionary<Pomodoro, int> cooldownLengthsPomos = new Dictionary<Pomodoro, int>()
        {
            { Pomodoro.ADD, 5 },
            { Pomodoro.EDIT, 5 },
            { Pomodoro.DONE, 5 },
            { Pomodoro.REMOVE, 5}
        };
    }
}
