using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvenirs.Entities
{
    public class ProductoFactura
    {
        public int Cantidad { get; set; }
        public Producto Producto{ get; set; }
        public ProductoFactura(Producto producto)
        {
            Cantidad = 1;
            Producto = producto;
        }
        public double ObtenerPrecio()
        {
            return Cantidad * Producto.Precio;
        }
        public string Detalle()
        {
            return Cantidad+"    " + Producto.Nombre + "     " + ObtenerPrecio();
        }
    }
    
}
