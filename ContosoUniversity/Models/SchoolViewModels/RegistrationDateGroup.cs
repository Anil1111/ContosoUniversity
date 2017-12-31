using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class RegistrationDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }

        public int StudentCount { get; set; }
    }
}
