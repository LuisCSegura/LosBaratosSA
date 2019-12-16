using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvenirs.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public Cliente()
        {
            Activo = true;
        }

        override
        public string ToString()
        {
            return "  ► " + Nombre + " - " + Correo + " - " + Cedula;
        }
    }
}
