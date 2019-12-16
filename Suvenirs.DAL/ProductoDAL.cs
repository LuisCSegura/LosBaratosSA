using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Npgsql;

namespace Suvenirs.DAL
{
    public class ProductoDAL
    {
        public LinkedList<Producto> CargarTodos()
        {
            LinkedList<Producto> productos = new LinkedList<Producto>();
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT id,nombre,codigo,categoria,cantidad,precio FROM productos" +
                " WHERE activo = true; ";
            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    productos.AddLast(CargarNuevoProducto(rs));
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
            return productos;
        }

        

        public Producto CargarNuevoProducto(NpgsqlDataReader rs)
        {
            Producto p = new Producto();
            p.Id = int.Parse(rs.GetValue(0).ToString());
            p.Nombre = rs.GetValue(1).ToString();
            p.Codigo = rs.GetValue(2).ToString();
            p.Categoria = rs.GetValue(3).ToString();
            p.Cantidad = int.Parse(rs.GetValue(4).ToString());
            p.Precio= double.Parse(rs.GetValue(5).ToString());
            return p;
        }
        public void InsertarNuevo(Producto p)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "INSERT INTO productos(nombre,codigo,categoria,cantidad,precio) " +
                "VALUES ('{0}','{1}','{2}',{3},{4});";
            sqlCode = string.Format(sqlCode, p.Nombre,p.Codigo,p.Categoria,p.Cantidad,p.Precio);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("El codigo del producto ya está en uso");
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
        public void Editar(Producto p)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "UPDATE productos SET nombre='{0}',codigo='{1}',categoria='{2}',cantidad={3},precio={4}" +
                " WHERE id = {5}; ";
            sqlCode = string.Format(sqlCode, p.Nombre, p.Codigo, p.Categoria, p.Cantidad,p.Precio,p.Id);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("El codigo del producto ya está en uso");
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
        public void Eliminar(Producto seleccionado)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "UPDATE productos SET activo=false" +
                " WHERE id = {0}; ";
            sqlCode = string.Format(sqlCode, seleccionado.Id);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("Error al eliminiar el registro");
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
    }
}
