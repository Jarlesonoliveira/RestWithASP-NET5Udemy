﻿using APIAspNetCore5.Model.Context;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIAspNetCore5.Repository.Generic
{
    // A implementação do repositório genérico
    // Recebe qualquer Tipo T que implemente IRepository de mesmo tipo
    // Desde que T extenda BaseEntity
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly MySQLContext _context;

        // Declaração de um dataset genérico
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }


        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            } catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            } catch (Exception)
            {
                throw;
            }
        }
        public List<T> FindAll()
        {
            return dataset.ToList();
        }
        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }
        public bool Exists(long? id)
        {
            return dataset.Any(b => b.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection()) 
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }

        public T Update(T item)
        {
            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = dataset.SingleOrDefault(b => b.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                } 
                catch (Exception)
                {
                    throw;
                }
            } else
            {
                return null;
            }
        }
    }
}