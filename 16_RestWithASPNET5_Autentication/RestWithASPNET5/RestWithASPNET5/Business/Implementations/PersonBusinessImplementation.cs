using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNET5.Data.Converter.Implementations;
using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Hypermedia.Utils;
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

        //Nós vamos começar a paginar. Antes funcionava como um getAll mas sem paginação
        //Entretanto a tabela vai crescer e vai ficar pesado para o banco trazer todos os dados de uma vez só
        //vamos salvar recursos paginando
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string SortDirection, int pagesize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(SortDirection)) && !SortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pagesize < 1) ? 10 : pagesize;
            var offset = page > 0 ? (page - 1) * size : 0;

            //este where 1 = 1 é para que quando as querys não gerarem nenhuma condição where não quebrar nosso sql
            string query = @"select * from person p where 1 = 1 ";
            // geração dinâmica da query
            if (!string.IsNullOrWhiteSpace(name)) query = query + $"and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            // A query sem geração dinâmica era assim:
            //"select * from
            //    person p
            //where 1 = 1
            //    and p.first_name like '%LEO%'
            //order by
            //    p.first_name asc limit 10 offset 1";

            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $"and p.first_name like '%{name}%' ";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindById(long id)
        {
            // Não vamos retornar um objeto igual ao que está no banco de dados. O que vamos fazer é
            // buscar o objeto ou a lista de objetos e convertê-los para um VO que será retornado em resposta
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string secondName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondName));
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
