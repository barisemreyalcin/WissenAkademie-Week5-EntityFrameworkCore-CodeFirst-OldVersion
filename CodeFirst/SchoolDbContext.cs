using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class SchoolDbContext : DbContext
    {
        /* new ... Initialization Parameters
         * 1 - CreateDatabaseIfNotExists
         * 2 - DropCreateDatabaseIfModelChanges
         * 3 - DropCreateDatabaseAlways
         * 4 - Custom DB Init
         */
        public SchoolDbContext() : base("name=connectionString")
        {
            //Database.SetInitializer<SchoolDbContext>(new CreateDatabaseIfNotExists<SchoolDbContext>());
            Database.SetInitializer<SchoolDbContext>(new SchoolDBInitializer()); // bunu ekledim sonra
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Genders{ get; set; }

    }
}
