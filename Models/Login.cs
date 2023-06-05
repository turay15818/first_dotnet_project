using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }
        public string address { get; set; }
        public string universityName { get; set; }
        public string department { get; set; }
        public string course { get; set; }
        public int year { get; set; }
        public string nameHOD { get; set; }
        public string hodEmail { get; set; }
        public string hodPhone { get; set; }
        public string userRole {get; set; }
    }
}

