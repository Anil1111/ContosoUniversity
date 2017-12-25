using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName {get; set;}
        public DateTime RegistrationDate { get; set; }

        public ICollection<Enrollment> Enollments { get; set; }
    }
}
