using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Age { get; set; }

        public Person() { }

        public Person(int pId, string pName, string pAge)
        {
            Id = pId;
            Name = pName;
            Age = pAge;
        }
    }
}