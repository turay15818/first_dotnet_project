using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string department { get; set; }
        public string form { get; set; }
        public int roleNo { get; set; }
        public string formMaster { get; set; }
        public string houseColor { get; set; }
    }
}