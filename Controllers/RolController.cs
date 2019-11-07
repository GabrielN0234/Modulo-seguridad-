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
    public class RolController : ControllerBase
    {
        private readonly RolData _roldata;

        public RolController(RolData rolData)
        {
            _roldata = rolData ?? throw new ArgumentNullException(nameof(rolData));
        }
        // GET: api/Rol
        [HttpGet]
        public async Task<List<RolModelo>> Get()
        {
            return await _roldata.GetAll();
        }

        // GET: api/Rol/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<RolModelo>> Get(int id)
        {
            var response = await _roldata.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST: api/Rol
        [HttpPost]
        public async Task Post([FromBody] RolModelo rolModelo)
        {
            await _roldata.Insert(rolModelo);
        }

        // PUT: api/Rol/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _roldata.DeleteById(id);
        }



    }
}