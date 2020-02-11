using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Parent : Person
    {
        public int ChildCount { get; set; }

        public Parent() { }

        public Parent(int pId, string pName, string pAge, int pChildCount) : base(pId, pName, pAge)
        {
            ChildCount = pChildCount;
        }
    }
}