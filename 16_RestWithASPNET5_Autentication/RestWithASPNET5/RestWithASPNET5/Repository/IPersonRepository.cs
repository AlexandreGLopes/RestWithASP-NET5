using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Model;
using RestWithASPNET5.Repository.Implementations;

namespace RestWithASPNET5.Repository
{
    // Fizemos métodos genéricos mas agora o Person ganhou uma característica especial de enabled que não há nos outros
    // por isso vamos readicionar o PersonRepository
    // Ele vai extender nossa classe genérica só que de tipo person. Assim ele terá apenas um método.
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
