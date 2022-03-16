using RestWithASPNET5.Data.Converter.Implementations;
using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Model;
using RestWithASPNET5.Repository.Implementations;
using System.Collections.Generic;

namespace RestWithASPNET5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        //Não vai acessar diretamente o MySQLContext, quem vai fazer isso é Repository
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            // Não vamos retornar um objeto igual ao que está no banco de dados. O que vamos fazer é
            // buscar o objeto ou a lista de objetos e convertê-los para um VO que será retornado em resposta
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            // Não vamos retornar um objeto igual ao que está no banco de dados. O que vamos fazer é
            // buscar o objeto ou a lista de objetos e convertê-los para um VO que será retornado em resposta
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            // quando o objeto chega ele é um VO e não dá pra persistir ele diretamente na base de dados
            // Então precisamos parsear ele para entidade
            var bookEntity = _converter.Parse(book);
            // como entidade vamos poder persistir ele no banco e o resultado dessa persistência será colocado dentro de personEntity
            // Ou seja, gerou um id auto incremental, etc.
            bookEntity = _repository.Create(bookEntity);
            // Depois convertemos a entidade para VO novamente e devolvemos a resposta
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public BookVO Update(BookVO book)
        {
            // quando o objeto chega ele é um VO e não dá pra persistir ele diretamente na base de dados
            // Então precisamos parsear ele para entidade
            var bookEntity = _converter.Parse(book);
            // como entidade vamos poder persistir ele no banco e o resultado dessa persistência será colocado dentro de personEntity
            // Ou seja, gerou um id auto incremental, etc.
            bookEntity = _repository.Update(bookEntity);
            // Depois convertemos a entidade para VO novamente e devolvemos a resposta
            return _converter.Parse(bookEntity);
        }
    }
}
