using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Model
{
    public class Person
    {

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public Person()
        {
        }

        public Person(long Id, string FirstName, string LastName, string Address, string Gender)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.Gender = Gender;
        }
    }
}
