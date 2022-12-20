using APIAspNetCore5.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using System.Collections.Generic;

namespace APIAspNetCore5.Controllers
{

    /* Mapeia as requisições de http://localhost:{porta}/api/persons/v1/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Person]Controller
    e expõe como endpoint REST
    */
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : Controller
    {
        //Declaração do serviço usado
        private IPersonBusiness _personBusiness;

        /* Injeção de uma instancia de IPersonBusiness ao criar
        uma instancia de PersonController */
        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/persons/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/persons/v1/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/persons/v1/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        //Mapeia as requisições PUT para http://localhost:{porta}/api/persons/v1/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null) return BadRequest();
            return new ObjectResult(updatedPerson);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = _personBusiness.Disable(id);
            return Ok(person);
        }
        //Mapeia as requisições DELETE para http://localhost:{porta}/api/persons/v1/{id}
        //recebendo um ID como no Path da requisição
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}