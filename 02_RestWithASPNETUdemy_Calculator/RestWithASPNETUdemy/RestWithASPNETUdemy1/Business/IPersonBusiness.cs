using APIAspNetCore5.Model;
using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;

namespace APIAspNetCore5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);
        bool Exists(long id);
    }
}