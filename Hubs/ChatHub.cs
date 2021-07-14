using Microsoft.AspNetCore.SignalR;
using PersonalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Hubs
{
    /// <summary>
    /// Class that represent SignalR Hub type in this application
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Method that send message from user to all users whos connected with the same hub
        /// </summary>
        /// <param name="message">Message type object that represent message to send
        /// </param>
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
