using PersonalChat.Helpers;
using PersonalChat.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void MessageValidTest()
        {
            // ARANGE
            Message message = new Message {Text = "" };
            Message message2 = new Message { Text = "some text here" };

            // ACT
            bool result = Helpers.CheckMessageValid(message);
            bool result2 = Helpers.CheckMessageValid(message2);

            // ASSERT
            Assert.Equal(false, result);
            Assert.Equal(true, result2);

        }

        [Fact]
        public void ChatUserMessagesCountTest()
        {
            // ARANGE
            Message message = new Message { Text = "" };
            Message message2 = new Message { Text = "some text here" };
            ChatUser user = new ChatUser();
            user.Messages.Add(message);
            user.Messages.Add(message2);

            // ACT
            bool result = Helpers.CheckUserEmptyMessages(user);

            // ASSERT
            Assert.Equal(true, result);

        }

        [Fact]
        public void FindUserHaveBiggestCountMessagesTest()
        {
            // ARANGE
            Message message = new Message { Text = "" };
            Message message2 = new Message { Text = "some text here" };
            Message message3 = new Message { Text = "" };
            Message message4 = new Message { Text = "some text here" };
            Message message5 = new Message { Text = "" };
            Message message6 = new Message { Text = "some text here" };
            ChatUser user = new ChatUser();
            ChatUser user2 = new ChatUser();
            ChatUser user3 = new ChatUser();
            user.Messages.Add(message);
            user.Messages.Add(message2);
            user2.Messages.Add(message);
            user2.Messages.Add(message2);
            user2.Messages.Add(message3);
            user3.Messages.Add(message2);
            user3.Messages.Add(message6);
            user3.Messages.Add(message5);
            user3.Messages.Add(message3);

            // ACT
            ChatUser result = Helpers.FindUserHaveBiggestCountMessages(new List<ChatUser> {user, user2, user3 });

            // ASSERT
            Assert.Equal(user3, result);

        }

        [Fact]
        public void FindUsersWithEmptyMessagesTest()
        {
            // ARANGE
            Message message = new Message { Text = "" };
            Message message2 = new Message { Text = "some text here" };
            Message message3 = new Message { Text = "" };
            Message message4 = new Message { Text = "some text here" };
            Message message5 = new Message { Text = "" };
            Message message6 = new Message { Text = "some text here" };
            ChatUser user = new ChatUser();
            ChatUser user2 = new ChatUser();
            ChatUser user3 = new ChatUser();
            user.Messages.Add(message);
            user.Messages.Add(message2);
            user2.Messages.Add(message4);
            user2.Messages.Add(message2);
            user2.Messages.Add(message6);
            user3.Messages.Add(message2);
            user3.Messages.Add(message6);
            user3.Messages.Add(message4);

            // ACT
            List<ChatUser> result = Helpers.FindUsersWithEmptyMessages(new List<ChatUser> { user, user2, user3 });

            // ASSERT
            Assert.Contains(user, result);
        }
    }
}
