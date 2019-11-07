using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modulo_seguridad_webapi.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Data
{
    public class RolPermisoData
    {
        private readonly string _connectionString;

        public RolPermisoData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task<List<RolPermisoModelo>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Obtener_todos_rolPermiso", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<RolPermisoModelo>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToRolPermiso(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private RolPermisoModelo MapToRolPermiso(SqlDataReader reader)
        {
            return new RolPermisoModelo()
            {
                S_id_rol_permiso = (int)reader["S_id_rol_permiso "],
                S_id_rol = (int)reader["S_id_rol"],
                S_id_permiso = (int)reader["S_id_permiso"]
            };
        }

        public async Task<RolPermisoModelo> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_obtener_rolPermiso_por_id", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    RolPermisoModelo response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToRolPermiso(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task Insert(RolPermisoModelo rolpermisoModelo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_RolPermiso", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_id_rol", rolpermisoModelo.S_id_rol));
                    cmd.Parameters.Add(new SqlParameter("@S_id_permiso", rolpermisoModelo.S_id_permiso));
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
                using (SqlCommand cmd = new SqlCommand("PA_Eliminar_RolPermiso", sql))
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
