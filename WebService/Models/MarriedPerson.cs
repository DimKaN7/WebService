using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class MarriedPerson : Person
    {
        public string PartnerName { get; set; }
        public string PartnerAge { get; set; }

        public MarriedPerson() { }

        public MarriedPerson(int pId, string pName, string pAge, string ppName, string ppAge) : base(pId, pName, pAge)
        {
            PartnerName = ppName;
            PartnerAge = ppAge;
        }
    }
}