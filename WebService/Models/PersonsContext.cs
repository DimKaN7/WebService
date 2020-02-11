using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class PersonsContext: DbContext
    {
        public PersonsContext(): base("DefaultConnection") {}
        public DbSet<Person> Persons { get; set; }
        public DbSet<MarriedPerson> MarriedPersons { get; set; }
        public DbSet<Parent> Parents { get; set; }
    }
}