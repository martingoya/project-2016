﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Oh_lala_Web.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}