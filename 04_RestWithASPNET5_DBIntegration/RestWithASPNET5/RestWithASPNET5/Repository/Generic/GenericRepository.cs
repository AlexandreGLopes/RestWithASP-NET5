using Microsoft.EntityFrameworkCore;
using RestWithASPNET5.Model.Base;
using RestWithASPNET5.Model.Context;
using RestWithASPNET5.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;

        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            // Com isso vamos set dinamicamente o DbSet (sem ter que colocar o atributo no MySQLContext)
            // desta forma já vai pegar dinamicamente de qual tipo de Classe será esse repositório em tempo de execução
            dataset = _context.Set<T>();
        }
        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                return null;
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }
    }
}
