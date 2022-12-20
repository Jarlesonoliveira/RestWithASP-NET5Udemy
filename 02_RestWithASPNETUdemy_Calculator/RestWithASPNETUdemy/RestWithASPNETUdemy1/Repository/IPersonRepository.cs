using APIAspNetCore5.Model;
using APIAspNetCore5.Repository;

namespace RestWithASPNETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
