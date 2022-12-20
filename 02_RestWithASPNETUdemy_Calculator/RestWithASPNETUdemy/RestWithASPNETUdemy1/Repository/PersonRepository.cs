﻿using APIAspNetCore5.Model;
using APIAspNetCore5.Model.Context;
using APIAspNetCore5.Repository.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace RestWithASPNETUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long id)
        {
            if(!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if(user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                } 
                catch(Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}