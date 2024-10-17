using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Student
    {
        [Key]
        [Column(Order = 0)]
        public int StudentID { get; set; }

        [Column("StudentName", Order = 1)]
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Column("Surname", Order = 2)]
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Column("DoB", TypeName = "datetime2", Order = 5)]
        public DateTime? BirthDate { get; set; } // ?: uygulama seviyesinde null olabilir

        [Column(Order = 3)]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column(Order = 4)]
        [MaxLength(15)] 
        public string Phone { get; set; }

        public Gender Gender { get; set; } // bire sonsuz ilişki olacak (bu Gender bir class bu arada)

        [NotMapped]
        public int Weight { get; set; }

        [NotMapped]
        public int Height { get; set; }


    }
}
