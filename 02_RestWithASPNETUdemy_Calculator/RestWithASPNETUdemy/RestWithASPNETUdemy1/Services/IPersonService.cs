using RestWithASPNETUdemy1.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy1.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
