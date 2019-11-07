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
    public class PermisoController : ControllerBase
    {
        private readonly PermisoData _permisodata;

        public PermisoController(PermisoData permisoData)
        {
            _permisodata = permisoData ?? throw new ArgumentNullException(nameof(permisoData));
        }
        // GET: api/Permiso
        [HttpGet]
        public async Task<List<PermisoModelo>> Get()
        {
            return await _permisodata.GetAll();
        }

        // GET: api/Permiso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermisoModelo>> Get(int id)
        {
            var response = await _permisodata.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST: api/Permiso
        [HttpPost]
        public async Task Post([FromBody] PermisoModelo rolModelo)
        {
            await _permisodata.Insert(rolModelo);
        }

        // PUT: api/Permiso/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _permisodata.DeleteById(id);
        }
    }
}