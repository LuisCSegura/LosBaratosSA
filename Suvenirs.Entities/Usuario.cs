using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvenirs.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Contrasenna { get; set; }
        public bool Administrador { get; set; }
        public bool Activo { get; set; }


        public Usuario()
        {

            Activo = true;
        }
    }
}
