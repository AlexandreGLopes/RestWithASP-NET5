using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Context;
using RestWithASPNET5.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNET5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        //Não vai acessar diretamente o MySQLContext, quem vai fazer isso é Repository
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}
