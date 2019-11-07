using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modulo_seguridad_webapi.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Data
{
    public class PermisoData
    {
        private readonly string _connectionString;

        public PermisoData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task<List<PermisoModelo>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_consulta_todos_permiso", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<PermisoModelo>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToPermiso(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private PermisoModelo MapToPermiso(SqlDataReader reader)
        {
            return new PermisoModelo()
            {
                id_Permiso = (int)reader["id_Permiso"],
                S_Nombre_permiso = reader["S_Nombre_permiso"].ToString(),
                S_descripcion_permiso = reader["S_descripcion_permiso"].ToString()
            };
        }

        public async Task<PermisoModelo> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_obtener_permiso_id", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    PermisoModelo response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToPermiso(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task Insert(PermisoModelo permisoModelo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_permiso", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_Nombre_permiso", permisoModelo.S_Nombre_permiso));
                    cmd.Parameters.Add(new SqlParameter("@S_descripcion_permiso", permisoModelo.S_descripcion_permiso));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DeleteById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Borrar_permiso", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
