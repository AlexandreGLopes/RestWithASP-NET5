using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNET5.Data.Converter.Implementations;
using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Context;
using RestWithASPNET5.Repository;
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
        // Estávamos usando a IRepository que é o repositório genérico
        // Mas após a implementação de IPersonRepository vamos passar a usá-lo pois ele extende de IRepository
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            // Não vamos retornar um objeto igual ao que está no banco de dados. O que vamos fazer é
            // buscar o objeto ou a lista de objetos e convertê-los para um VO que será retornado em resposta
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            // Não vamos retornar um objeto igual ao que está no banco de dados. O que vamos fazer é
            // buscar o objeto ou a lista de objetos e convertê-los para um VO que será retornado em resposta
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            // quando o objeto chega ele é um VO e não dá pra persistir ele diretamente na base de dados
            // Então precisamos parsear ele para entidade
            var personEntity = _converter.Parse(person);
            // como entidade vamos poder persistir ele no banco e o resultado dessa persistência será colocado dentro de personEntity
            // Ou seja, gerou um id auto incremental, etc.
            personEntity = _repository.Create(personEntity);
            // Depois convertemos a entidade para VO novamente e devolvemos a resposta
            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity); 
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PersonVO Update(PersonVO person)
        {
            // quando o objeto chega ele é um VO e não dá pra persistir ele diretamente na base de dados
            // Então precisamos parsear ele para entidade
            var personEntity = _converter.Parse(person);
            // como entidade vamos poder persistir ele no banco e o resultado dessa persistência será colocado dentro de personEntity
            // Ou seja, gerou um id auto incremental, etc.
            personEntity = _repository.Update(personEntity);
            // Depois convertemos a entidade para VO novamente e devolvemos a resposta
            return _converter.Parse(personEntity);
        }
    }
}
