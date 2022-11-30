using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _Repository;

        public PersonBusinessImplementation(IPersonRepository _Repository) 
        {
            this._Repository = _Repository;
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll() 
        {
            return _Repository.FindAll();
        }

        // Método responsável por retornar uma pessoa
        public Person FindByID(long id) 
        {
            return _Repository.FindByID(id);
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public Person Create(Person person) 
        {
            return _Repository.Create(person);
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person) 
        {
            return _Repository.Update(person);
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id) 
        {
            _Repository.Delete(id);
        }
    }
}