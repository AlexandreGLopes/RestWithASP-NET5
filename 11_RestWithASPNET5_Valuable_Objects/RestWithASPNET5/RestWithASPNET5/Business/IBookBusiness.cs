using RestWithASPNET5.Data.VO;
using System.Collections.Generic;

namespace RestWithASPNET5.Business.Implementations
{
    public interface IBookBusiness
    {

        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
