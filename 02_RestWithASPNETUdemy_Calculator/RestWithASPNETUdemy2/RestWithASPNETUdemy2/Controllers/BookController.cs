using APIAspNetCore5.Business;
using APIAspNetCore5.Data.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Hypermedia.Filters;
using System.Collections.Generic;

namespace APIAspNetCore5.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : Controller
    {
        //Declaração do serviço usado
        private IBookBusiness _bookBusiness;

        /* Injeção de uma instancia de IBookBusiness ao criar
        uma instancia de BookController */
        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/books/v1/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_bookBusiness.Create(book));
        }

        //Mapeia as requisições PUT para http://localhost:{porta}/api/books/v1/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            var updatedBook = _bookBusiness.Update(book);
            if (updatedBook == null) return BadRequest();
            return new ObjectResult(updatedBook);
        }


        //Mapeia as requisições DELETE para http://localhost:{porta}/api/books/v1/{id}
        //recebendo um ID como no Path da requisição
        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}