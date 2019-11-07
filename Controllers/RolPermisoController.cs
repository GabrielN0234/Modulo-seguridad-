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
    public class RolPermisoController : ControllerBase
    {
        private readonly RolPermisoData _rolPermisodata;

        public RolPermisoController(RolPermisoData rolPermisoData)
        {
            _rolPermisodata = rolPermisoData ?? throw new ArgumentNullException(nameof(rolPermisoData));
        }
        // GET: api/RolPermiso
        [HttpGet]
        public async Task<List<RolPermisoModelo>> Get()
        {
            return await _rolPermisodata.GetAll();
        }

        // GET: api/RolPermiso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolPermisoModelo>> Get(int id)
        {
            var response = await _rolPermisodata.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST: api/RolPermiso
        [HttpPost]
        public async Task Post([FromBody] RolPermisoModelo rolPermisoModelo)
        {
            await _rolPermisodata.Insert(rolPermisoModelo);
        }

        // PUT: api/RolPermiso/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _rolPermisodata.DeleteById(id);
        }
    }
}