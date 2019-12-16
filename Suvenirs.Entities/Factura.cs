using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvenirs.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public LinkedList<ProductoFactura> productos { get; set; }
        public double Total { get; set; }
        public bool Activo { get; set; }
        public Factura()
        {
            Fecha = DateTime.Now;
            Cliente = new Cliente();
            productos = new LinkedList<ProductoFactura>();
            Activo = true;
        }
        public double ObtenerTotal()
        {
            double total = 0;
            foreach (ProductoFactura item in productos)
            {
                total += item.ObtenerPrecio();
            }
            return total;
        }
        public string GetInfo()
        {
            string separador = "---------------------------------------------" + "\n";
            string txt = "  LOS BARATOS S.A\n  FACTURA NO " + Id + "\n";
            txt += "  Fecha: " + Fecha.Year + "/" + Fecha.Month + "/" + Fecha.Day + "\n";
            txt += separador;
            txt += "  CANTIDAD     DESCRIPCIÓN     PRECIO" + "\n";
            txt += separador;
            foreach (ProductoFactura detalle in productos)
            {
                txt += detalle.Detalle() + "\n";
            }
            txt += separador;
            txt += "Total sin Impuesto: "+ObtenerTotal() + "\n";
            txt += "  TOTAL: " + Total + "\n";

            return txt;
        }

    }
}
