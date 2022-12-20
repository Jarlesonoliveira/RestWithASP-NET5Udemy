using APIAspNetCore5.Data.Converters;
using APIAspNetCore5.Model;
using APIAspNetCore5.Repository;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace APIAspNetCore5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {            
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }
    }
}