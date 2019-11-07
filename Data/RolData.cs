using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modulo_seguridad_webapi.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Data
{
    public class RolData
    {
        private readonly string _connectionString;

        public RolData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task<List<RolModelo>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Obtener_todos_rol", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<RolModelo>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToRol(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private RolModelo MapToRol(SqlDataReader reader)
        {
            return new RolModelo()
            {
                id_Rol = (int)reader["id_Rol"],
                S_Nombre_rol = reader["S_Nombre_rol"].ToString(),
                S_descripcion_rol = reader["S_descripcion_rol"].ToString()
            };
        }

        public async Task<RolModelo> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_obtener_rol_por_id", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    RolModelo response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToRol(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task Insert(RolModelo RolModelo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_Rol", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_Nombre_rol", RolModelo.S_Nombre_rol));
                    cmd.Parameters.Add(new SqlParameter("@S_descripcion_rol", RolModelo.S_descripcion_rol));
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
                using (SqlCommand cmd = new SqlCommand("PA_Eliminar_Rol", sql))
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
