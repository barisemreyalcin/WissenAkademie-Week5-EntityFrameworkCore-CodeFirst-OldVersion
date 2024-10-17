using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Gender
    {
        [Key]
        public int GenderID { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenderName{ get; set; }

        public bool State {  get; set; }

        // bire sonsuz ilişki tanımlayacak ICollection
        public ICollection<Student> Students { get; set; }

    }
}
