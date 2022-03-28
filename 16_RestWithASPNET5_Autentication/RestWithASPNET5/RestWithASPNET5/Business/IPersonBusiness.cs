using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNET5.Business.Implementations
{
    public interface IPersonBusiness
    {

        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string secondName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string SortDirection, int pagesize, int page);
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);
    }
}
