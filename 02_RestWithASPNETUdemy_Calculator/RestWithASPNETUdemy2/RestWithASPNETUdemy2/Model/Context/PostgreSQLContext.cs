﻿using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model;

namespace APIAspNetCore5.Model.Context
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext()
        {

        }

        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
