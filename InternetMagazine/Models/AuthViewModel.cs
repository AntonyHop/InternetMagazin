using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetMagazine.Models
{
    public class AuthViewModel
    {
        public string NickName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}