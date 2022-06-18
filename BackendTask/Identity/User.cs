using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Identity
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsOnline { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EmailConfirmDate { get; set; }
        public int ConfirmCode { get; set; }
    }
}
