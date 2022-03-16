using RestWithASPNET5.Model;
using System.Collections.Generic;

namespace RestWithASPNET5.Repository.Implementations
{
    public interface IBookRepository
    {

        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
        bool Exists(long id);
    }
}
