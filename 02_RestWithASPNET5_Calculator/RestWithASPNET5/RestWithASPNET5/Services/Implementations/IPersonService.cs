using RestWithASPNET5.Model;
using System.Collections.Generic;

namespace RestWithASPNET5.Services.Implementations
{
    public interface IPersonService
    {

        Person Create(List<Person> people, Person person);
        Person FindById(List<Person> people, long id);
        List<Person> FindAll(List<Person> people);
        Person Update(List<Person> people, Person person);
        void Delete(List<Person> people, long id);
    }
}
