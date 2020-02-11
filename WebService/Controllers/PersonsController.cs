using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;


namespace WebService.Controllers
{
    public class PersonsController : ApiController
    {
        PersonsContext db = new PersonsContext();

        public IEnumerable<string> Get()
        {
            List<string> jsons = new List<string>();
            for (int i = 0; i < db.Persons.Count(); i++)
            {
                jsons.Add(JsonConvert.SerializeObject(db.Persons.ToList().ElementAt(i)));
            }
            return jsons;
        }

        public Person Get(int id)
        {
            return db.Persons.Find(id);
        }
        
        public IHttpActionResult Post(HttpRequestMessage request)
        {
            System.Diagnostics.Debug.WriteLine("adsasd");
            var jsonString = request.Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            if (jsonObj["type"].Equals("0"))
            {
                Person person = new Person
                {
                    Name = jsonObj["pName"],
                    Age = jsonObj["pAge"]
                };
                db.Persons.Add(person);
                db.SaveChanges();
                return Ok(person);
            }
            else if (jsonObj["type"].Equals("1"))
            {
                MarriedPerson mPerson = new MarriedPerson()
                {
                    Name = jsonObj["pName"],
                    Age = jsonObj["pAge"],
                    PartnerName = jsonObj["partnerName"],
                    PartnerAge = jsonObj["partnerAge"]
                };
                db.MarriedPersons.Add(mPerson);
                db.SaveChanges();
                return Ok(mPerson);
            }
            else if (jsonObj["type"].Equals("2"))
            {
                Parent pPerson = new Parent()
                {
                    Name = jsonObj["pName"],
                    Age = jsonObj["pAge"],
                    ChildCount = int.Parse(jsonObj["childCount"])
                };
                db.Parents.Add(pPerson);
                db.SaveChanges();
                return Ok(pPerson);
            }
            else if (jsonObj["type"].Equals("r_1"))
            {
                var req = from p in db.Parents
                              select p.ChildCount;
                int result = req.Sum();
                return Ok(result.ToString());
            }
            else 
            {
                var req = from p in db.MarriedPersons.ToList()
                         .Where(p => Math.Abs(int.Parse(p.Age.Split('.')[2]) - int.Parse(p.PartnerAge.Split('.')[2])) <= 2)
                          select p;
                int result = req.Count();
                return Ok(result.ToString());
            }
        }

        public IHttpActionResult Put(HttpRequestMessage request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jsonString = request.Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            if (jsonObj["type"].Equals("0"))
            {
                Person person = db.Persons.Find(int.Parse(jsonObj["id"]));
                person.Name = jsonObj["pName"];
                person.Age = jsonObj["pAge"];
                db.SaveChanges();
                return Ok(person);
            }
            else if (jsonObj["type"].Equals("1"))
            {
                MarriedPerson mPerson = new MarriedPerson()
                {
                    Id = int.Parse(jsonObj["id"]),
                    Name = jsonObj["pName"],
                    Age = jsonObj["pAge"],
                    PartnerName = jsonObj["partnerName"],
                    PartnerAge = jsonObj["partnerAge"]
                };
                db.Entry(mPerson).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(mPerson);
            }
            else
            {
                Parent pPerson = new Parent()
                {
                    Id = int.Parse(jsonObj["id"]),
                    Name = jsonObj["pName"],
                    Age = jsonObj["pAge"],
                    ChildCount = int.Parse(jsonObj["childCount"])
                };
                db.Entry(pPerson).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(pPerson);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            Person person = db.Persons.Find(id);
            if (person != null)
            {
                db.Persons.Remove(person);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
