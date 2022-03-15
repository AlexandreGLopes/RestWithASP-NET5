using RestWithASPNET5.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNET5.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(List<Person> people, Person person)
        {
            int novoId = 1;
            for (int i = 0; i < people.Count; i++)
            {
                novoId++;
            }
            person.Id = novoId;
            return person;
        }

        public void Delete(List<Person> people, long id)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Id == id)
                {
                    people.RemoveAt(i);
                }
            }
        }

        public List<Person> FindAll(List<Person> people)
        {
            return people;
        }

        public Person FindById(List<Person> people, long id)
        {
            for (int i=0; i<people.Count; i++)
            {
                if (people[i].Id == id)
                {
                    return people[i];
                }
            }
            return null;
        }

        public Person Update(List<Person> people, Person person)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Id == person.Id)
                {
                    if (person.FirstName != null) { people[i].FirstName = person.FirstName; }
                    if (person.LastName != null) { people[i].LastName = person.LastName; }
                    if (person.Address != null) { people[i].Address = person.Address; }
                    if (person.Gender != null) { people[i].Gender = person.Gender; }

                    return people[i];
                }
                
            }
            return null;
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
