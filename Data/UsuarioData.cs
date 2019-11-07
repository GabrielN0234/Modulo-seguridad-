using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modulo_seguridad_webapi.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Data
{
    public class UsuarioData
    {
        private readonly string _connectionString;

        public UsuarioData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task<List<UsuarioModelo>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Consulta_Todos_Usuario", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<UsuarioModelo>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToUsuario(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private UsuarioModelo MapToUsuario(SqlDataReader reader)
        {
            return new UsuarioModelo()
            {
                id_Usuario = (int)reader["id_Usuario"],
                S_Nombre = reader["S_Nombre"].ToString(),
                S_Apellido = reader["S_Apellido"].ToString(),
                S_Contraseña = reader["S_Contraseña"].ToString(),
                S_Edad = (int)reader["S_Edad"],
                S_Tipo_Usuario = reader["S_Tipo_Usuario"].ToString()
            };
        }

        public async Task<UsuarioModelo> IniciarSesion(String datos)
        {
            var dataset = datos.Split(",");
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_inicia_sesion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_Nombre", dataset[0]));
                    cmd.Parameters.Add(new SqlParameter("@S_Apellido", dataset[1]));
                    cmd.Parameters.Add(new SqlParameter("@S_Contraseña", dataset[2]));
                    UsuarioModelo response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToUsuario(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task Insert(UsuarioModelo usuarioModelo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_usuario", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_Nombre", usuarioModelo.S_Nombre));
                    cmd.Parameters.Add(new SqlParameter("@S_Apellido", usuarioModelo.S_Apellido));
                    cmd.Parameters.Add(new SqlParameter("@S_Contraseña", usuarioModelo.S_Contraseña));
                    cmd.Parameters.Add(new SqlParameter("@S_Edad", usuarioModelo.S_Edad));
                    cmd.Parameters.Add(new SqlParameter("@S_Tipo_Usuario", usuarioModelo.S_Tipo_Usuario));
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
                using (SqlCommand cmd = new SqlCommand("PA_borrar_usuario", sql))
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
