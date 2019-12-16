using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvenirs.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            Activo = true;
        }
        override
        public string ToString()
        {
            return "  ► " + Nombre + " - " + Codigo + " - " + Categoria+"..."+Precio+ " ₡/u";
        }
        public string Detalle()
        {
            return "1    " + Nombre + "     " + Precio;
        }
    }
}
