using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyvaultWindowsFormsApp.Models
{
    public class KeyViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
