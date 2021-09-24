using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    class User
    {
        public static string GetUser(OnChatCommandReceivedArgs e)
        {
            return e.Command.ChatMessage.DisplayName.ToString();
        }
    }
}
