using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    // Gender tabloma db yaratılırken data eklensin istiyorum, : initialize türlerimden birini ekliyorum aşağı
    public class SchoolDBInitializer : DropCreateDatabaseAlways<SchoolDbContext>
    {
        protected override void Seed(SchoolDbContext context)
        {
            // data eklemek için
            IList<Gender> genderList = new List<Gender>()
            {
                new Gender() {GenderName = "Male", State = true},
                new Gender() {GenderName = "Female", State = true},
                new Gender() {GenderName = "Default", State = false},
                // db'm bu datalarla ayağa kalkacak
            };
            context.Genders.AddRange(genderList);
            base.Seed(context); 
        }
    }
}
