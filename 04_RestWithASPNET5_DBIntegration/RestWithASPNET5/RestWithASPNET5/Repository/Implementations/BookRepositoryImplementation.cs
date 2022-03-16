using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNET5.Business.Implementations;
using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNET5.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(b => b.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;
            var result = _context.Books.SingleOrDefault(b => b.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return book;
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(b => b.Id.Equals(id));
        }
    }
}
