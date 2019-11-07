using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modulo_seguridad_webapi.Modelo
{
    public class UsuarioModelo
    {
        public int id_Usuario { get; set; }
        public string S_Nombre { get; set; }
        public string S_Apellido { get; set; }
        public string S_Contraseña { get; set; }
        public int S_Edad { get; set; }
        public string S_Tipo_Usuario { get; set; }

    }
}
