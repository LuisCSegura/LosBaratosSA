using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Suvenirs.DAL;

namespace Suvenirs.BOL
{
    public class FacturaBOL
    {
        FacturaDAL facDal = new FacturaDAL();
        public void InsertarNuevo(Factura factura)
        {
            try
            {
                Validar(factura);
                facDal.InsertarNuevo(factura);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        private void Validar(Factura f)
        {
            if (f.productos.Count < 1)
            {
                throw new Exception("Debe agregar algún producto a la factura");
            }
            if(f.Cliente==null || f.Cliente.Id < 1)
            {
                throw new Exception("Debe seleccionar un Cliente");
            }
        }

        public LinkedList<Factura> CargarDia(DateTime f)
        {
            try
            {
                return facDal.CargarDia(f);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public int ObtenerSigId()
        {
            try
            {
                return facDal.ObtenerSiguienteID()+1;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
