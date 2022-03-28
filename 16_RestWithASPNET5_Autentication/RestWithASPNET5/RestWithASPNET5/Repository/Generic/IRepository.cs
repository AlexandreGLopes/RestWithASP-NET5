using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Base;
using System.Collections.Generic;

namespace RestWithASPNET5.Repository.Implementations
{
    // A classe genérica vai extender da classe BaseEntity que só tem um Id como atributo
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
