using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Context;
using RestWithASPNET5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Repository.Implementations
{
    public class PersonRepositoryImplementation : GenericRepository<Person>, IPersonRepository
    {
        //O construtor dessa classe vai extender o construtor da classe generic repository
        //ELe vai receber como parâmetro o MySQLContext e vaio extender isso do base, ou seja, vai pegar do repositório genérico
        public PersonRepositoryImplementation(MySQLContext context) : base (context)
        {
        }

        public Person Disable(long id)
        {
            // Se não tiver uma pessoa com o id do parâmetro vai retornar null
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return user;
        }
    }
}
