﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public virtual ChatUser Sender { get; set; }
    }
}
