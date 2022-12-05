using APIAspNetCore5.Model;
using APIAspNetCore5.Repository;
using System.Collections.Generic;

namespace APIAspNetCore5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public Books Create(Books book)
        {
            return _repository.Create(book);
        }

        public Books FindById(long id)
        {
            return _repository.FindById(id);
        }

        public List<Books> FindAll()
        {
            return _repository.FindAll();
        }

        public Books Update(Books book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(string id)
        {
            return _repository.Exists(id);
        }
    }
}