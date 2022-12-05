using APIAspNetCore5.Model;
using APIAspNetCore5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAspNetCore5.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private readonly MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public Books Create(Books book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        // Método responsável por retornar uma pessoa
        public Books FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Método responsável por retornar todas as pessoas
        public List<Books> FindAll()
        {
            return _context.Books.ToList();
        }

        // Método responsável por atualizar uma pessoa
        public Books Update(Books book)
        {
            // Verificamos se a pessoa existe na base
            // Se não existir retornamos uma instancia vazia de pessoa
            if (!Exists(book.Id)) return null;

            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null) _context.Books.Remove(result);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(string id)
        {
            return _context.Books.Any(b => b.Id.Equals(id));
        }
    }
}
