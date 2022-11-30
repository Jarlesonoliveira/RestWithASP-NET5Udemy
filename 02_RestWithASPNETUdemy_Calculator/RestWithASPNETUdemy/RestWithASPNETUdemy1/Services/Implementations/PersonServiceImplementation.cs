﻿using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context) 
        {
            _context = context;
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll() 
        {
            return _context.Persons.ToList();
        }

        // Método responsável por retornar uma pessoa
        public Person FindByID(long id) 
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public Person Create(Person person) 
        {
            try 
            {
                _context.Add(person);
                _context.SaveChanges();
            } 
            catch (Exception) 
            {

                throw;
            }
            return person;
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person) 
        {
            // Verificamos se a pessoa existe na base
            // Se não existir retornamos uma instancia vazia de pessoa
            if (!Exists(person.Id)) return new Person();

            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null) 
            {

                try 
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                } 
                catch (Exception) 
                {

                    throw;
                }
            }
            return person;
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id) 
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null) 
            {

                try 
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                } 
                catch (Exception) 
                {

                    throw;
                }
            }
        }
        private bool Exists(long id) 
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}