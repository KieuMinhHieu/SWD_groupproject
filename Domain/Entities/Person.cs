using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {       
        public string? Name { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; } // Alive, Deceased, ...
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string BirthPlace { get; set; }
        public string? RestingPlace { get; set; }
        public string? Father { get; set; }
        public string? Mother { get; set; }
        public string? Spouse { get; set; }
        public string? Children { get; set; }

        public FamilyTree FamilyTree { get; set; }
    }
}
