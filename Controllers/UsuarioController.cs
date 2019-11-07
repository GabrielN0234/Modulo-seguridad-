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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioData _usuariodata;

        public UsuarioController(UsuarioData usuarioData)
        {
            _usuariodata = usuarioData ?? throw new ArgumentNullException(nameof(usuarioData));
        }
        // GET: api/Usuario
        [HttpGet]
        public async Task<List<UsuarioModelo>> Get()
        {
            return await _usuariodata.GetAll();
        }

        // GET: api/Usuario/5
        [HttpGet("{datos}")]
        public async Task<ActionResult<UsuarioModelo>> Get(string datos)
        {
            var response = await _usuariodata.IniciarSesion(datos);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task Post([FromBody] UsuarioModelo usuarioModelo)
        {
            await _usuariodata.Insert(usuarioModelo);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _usuariodata.DeleteById(id);
        }
    }
}
