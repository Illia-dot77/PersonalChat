using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Models
{
    /// <summary>
    /// Class that represent default identity in this application (set as default in Startup.cs)
    /// </summary>
    public class ChatUser : IdentityUser
    {
        /// <summary>
        /// ChatUser default constructor that set Messages field
        /// </summary>
        public ChatUser()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
