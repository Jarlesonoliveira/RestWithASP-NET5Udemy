using APIAspNetCore5.Model;
using System.Collections.Generic;

namespace APIAspNetCore5.Business
{
    public interface IBookBusiness
    {
        Books Create(Books book);
        Books FindById(long id);
        List<Books> FindAll();
        Books Update(Books book);
        void Delete(long id);
        bool Exists(string id);
    }
}