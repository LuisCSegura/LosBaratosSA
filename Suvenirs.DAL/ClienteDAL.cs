using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Npgsql;

namespace Suvenirs.DAL
{
    public class ClienteDAL
    {
        public LinkedList<Cliente> CargarTodos()
        {
            LinkedList<Cliente> clientes = new LinkedList<Cliente>();
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT id,nombre,telefono,correo,cedula FROM clientes" +
                " WHERE activo = true; ";
            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    clientes.AddLast(CargarNuevoCliente(rs));
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
            return clientes;

        }

        

        public Cliente CargarNuevoCliente(NpgsqlDataReader rs)
        {
            Cliente c = new Cliente();
            c.Id = int.Parse(rs.GetValue(0).ToString());
            c.Nombre = rs.GetValue(1).ToString();
            c.Telefono = rs.GetValue(2).ToString();
            c.Correo = rs.GetValue(3).ToString();
            c.Cedula = rs.GetValue(4).ToString();
            return c;
        }
        public void InsertarNuevo(Cliente c)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "INSERT INTO clientes(nombre,telefono,correo,cedula) " +
                "VALUES ('{0}','{1}','{2}','{3}');";
            sqlCode = string.Format(sqlCode, c.Nombre,c.Telefono,c.Correo,c.Cedula);

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
        public void Editar(Cliente c)
        {Conexion conexion = new Conexion();

            String sqlCode = "UPDATE clientes SET nombre='{0}',telefono='{1}',correo='{2}',cedula='{3}'" +
                " WHERE id = {4}; ";
            sqlCode = string.Format(sqlCode, c.Nombre, c.Telefono, c.Correo, c.Cedula,c.Id);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception(sqle.Message);
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
        public void Eliminar(Cliente seleccionado)
        {

            Conexion conexion = new Conexion();

            String sqlCode = "UPDATE clientes SET activo=false" +
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
