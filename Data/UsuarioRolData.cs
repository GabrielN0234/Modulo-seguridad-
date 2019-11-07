using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modulo_seguridad_webapi.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Data
{
    public class UsuarioRolData
    {
        private readonly string _connectionString;

        public UsuarioRolData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task<List<UsuarioRolModelo>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Obtener_todos_UsuarioRol", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<UsuarioRolModelo>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToUsuarioRol(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private UsuarioRolModelo MapToUsuarioRol(SqlDataReader reader)
        {
            return new UsuarioRolModelo()
            {
                S_id_rol_usuario = (int)reader["S_id_rol_usuario"],
                S_id_Usuario = (int)reader["S_id_Usuario"],
                S_id_rol = (int)reader["S_id_rol"]
            };
        }

        public async Task<UsuarioRolModelo> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_obtener_UsuarioRol_por_id", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    UsuarioRolModelo response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToUsuarioRol(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task Insert(UsuarioRolModelo usuarioRolModelo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_UsuarioRol", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_id_Usuario", usuarioRolModelo.S_id_Usuario));
                    cmd.Parameters.Add(new SqlParameter("@S_id_rol", usuarioRolModelo.S_id_rol));
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
                using (SqlCommand cmd = new SqlCommand("PA_Eliminar_UsuarioRol", sql))
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
