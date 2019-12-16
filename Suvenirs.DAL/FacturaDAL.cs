using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Suvenirs.Entities;

namespace Suvenirs.DAL
{
    public class FacturaDAL
    {
        public void InsertarNuevo(Factura f)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "INSERT INTO facturas(fecha,id_cliente,total) " +
                "VALUES ('{0}/{1}/{2}',{3},{4});";
            sqlCode = string.Format(sqlCode, f.Fecha.Year,f.Fecha.Month,f.Fecha.Day,f.Cliente.Id,f.Total);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("Ha ocurrido un problema al insertar el registro");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public LinkedList<Factura> CargarDia(DateTime f)
        {
            LinkedList<Factura> facturas= new LinkedList<Factura>();
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT id,total FROM facturas" +
                " WHERE activo = true AND fecha ='{0}/{1}/{2}'; ";
            sqlCode = string.Format(sqlCode, f.Year, f.Month, f.Day);
            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    facturas.AddLast(CargarNuevaFactura(rs,f));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return facturas;
        }

        public int ObtenerSiguienteID()
        {
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT MAX(id) FROM facturas; ";
            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                if (rs.Read())
                {
                    return (int)(rs.GetValue(0));
                }
                else;
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        private Factura CargarNuevaFactura(NpgsqlDataReader rs,DateTime fecha)
        {
            Factura f = new Factura();
            f.Id= int.Parse(rs.GetValue(0).ToString());
            f.Fecha = fecha;
            f.Total= double.Parse(rs.GetValue(1).ToString());
            return f;
        }
    }
}
