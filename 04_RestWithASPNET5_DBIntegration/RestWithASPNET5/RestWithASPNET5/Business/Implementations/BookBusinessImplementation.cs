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
    public class BookBusinessImplementation : IBookBusiness
    {
        //Não vai acessar diretamente o MySQLContext, quem vai fazer isso é Repository
        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}
