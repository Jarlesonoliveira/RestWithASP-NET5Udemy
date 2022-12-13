using APIAspNetCore5.Data.Converters;
using APIAspNetCore5.Data.VO;
using APIAspNetCore5.Model;
using APIAspNetCore5.Repository;
using RestWithASPNETUdemy.Data.VO;
using System;
using System.Collections.Generic;

namespace APIAspNetCore5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;
        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var BookEntity = _converter.Parse(book);
            BookEntity = _repository.Create(BookEntity);
            return _converter.Parse(BookEntity);
        }

        BookVO IBookBusiness.FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        List<BookVO> IBookBusiness.FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO Update(BookVO book)
        {
            var BookEntity = _converter.Parse(book);
            BookEntity = _repository.Update(BookEntity);
            return _converter.Parse(BookEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}