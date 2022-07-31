using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ActiveStatus { get; set; }
        public string LastLoginTime { get; set; }
        public string RegistrationTime { get; set; }

    }
}
