using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Model
{
    public class UserCredentialsModel
    {
        [Key]
        public int UserId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password {get; set; } = string.Empty;

    }
}
