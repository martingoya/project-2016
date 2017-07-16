using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Oh_lala_Web.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Nombre")]
        public string FromName { get; set; }
        [Required, Display(Name = "Mail"), EmailAddress]
        public string FromEmail { get; set; }
        [Display(Name = "Telefono")]
        public string Phone { get; set; }
        [Required, Display(Name = "Mensaje")]
        public string Message { get; set; }
    }
}