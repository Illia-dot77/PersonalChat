using PersonalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Helpers
{
    public static class Helpers
    {
        public static bool CheckMessageValid(Message message)
        {
            if (message.Text.Length == 0 || message.Time.Date != DateTime.Today) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckUserEmptyMessages(ChatUser user)
        {
            if (user.Messages.Contains(new Message {Text = "" }))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static ChatUser FindUserHaveBiggestCountMessages(IEnumerable<ChatUser> users)
        {
            string userId = "";
            int userMessages = 0;
            foreach (var user in users)
            {
                if (user.Messages.Count > userMessages)
                {
                    userId = user.Id;
                }
            }

            return users.FirstOrDefault(u => u.Id == userId);
        }

        public static List<ChatUser> FindUsersWithEmptyMessages(IEnumerable<ChatUser> users)
        {
            List<ChatUser> usersWithEmptyMess = new List<ChatUser>();

            foreach (var user in users)
            {
                foreach (var message in user.Messages)
                {
                    if(message.Text == "")
                    {
                        usersWithEmptyMess.Add(user);
                    }
                }
            }

            return usersWithEmptyMess;
        }
    }
}
