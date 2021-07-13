﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Models
{
    public class ChatUser : IdentityUser
    {
        public ChatUser()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}