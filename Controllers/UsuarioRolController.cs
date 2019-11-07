using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modulo_seguridad_webapi.Data;
using Modulo_seguridad_webapi.Modelo;

namespace Modulo_seguridad_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly UsuarioRolData _usuarioRoldata;

        public UsuarioRolController(UsuarioRolData usuarioRolData)
        {
            _usuarioRoldata = usuarioRolData ?? throw new ArgumentNullException(nameof(usuarioRolData));
        }
        // GET: api/UsuarioRol
        [HttpGet]
        public async Task<List<UsuarioRolModelo>> Get()
        {
            return await _usuarioRoldata.GetAll();
        }

        // GET: api/UsuarioRol/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRolModelo>> Get(int id)
        {
            var response = await _usuarioRoldata.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST: api/UsuarioRol
        [HttpPost]
        public async Task Post([FromBody] UsuarioRolModelo usuarioRolModelo)
        {
            await _usuarioRoldata.Insert(usuarioRolModelo);
        }

        // PUT: api/UsuarioRol/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _usuarioRoldata.DeleteById(id);
        }
    }
}