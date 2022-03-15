using RestWithASPNET5.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNET5.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<Person> FindAll()
        {
            List<Person> people = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }
            return people;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Alexandre",
                LastName = "Lopes",
                Address = "Rua Adão Ordakowski, 120 - Curitiba (PR), Brasil",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Alexandre",
                LastName = "Lopes",
                Address = "Rua Adão Ordakowski, 120 - Curitiba (PR), Brasil",
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }

}
