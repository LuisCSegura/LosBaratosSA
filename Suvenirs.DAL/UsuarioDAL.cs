using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Npgsql;

namespace Suvenirs.DAL
{
    public class UsuarioDAL
    {
        public LinkedList<Usuario> CargarTodos()
        {
            LinkedList<Usuario> usuarios = new LinkedList<Usuario>();
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT id,username,nombre,contrasenna,telefono,correo,cedula,administrador FROM usuarios" +
                " WHERE activo = true; ";
            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    usuarios.AddLast(CargarNuevoUsuario(rs));
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
            return usuarios;

        }
        public Usuario CargarNuevoUsuario(NpgsqlDataReader rs)
        {
            Usuario u = new Usuario();
            u.Id = int.Parse(rs.GetValue(0).ToString());
            u.Username = rs.GetValue(1).ToString();
            u.Nombre = rs.GetValue(2).ToString();
            u.Contrasenna = rs.GetValue(3).ToString();
            u.Telefono = rs.GetValue(4).ToString();
            u.Correo = rs.GetValue(5).ToString();
            u.Cedula = rs.GetValue(6).ToString();
            u.Administrador = bool.Parse(rs.GetValue(7).ToString());
            return u;
        }
        public void InsertarNuevo(Usuario u)
        {

            Conexion conexion = new Conexion();

            String sqlCode = "INSERT INTO usuarios(username,nombre,contrasenna,telefono,correo,cedula,administrador) " +
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6});";
            sqlCode = string.Format(sqlCode, u.Username,u.Nombre,u.Contrasenna,u.Telefono,u.Correo,u.Cedula,u.Administrador);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("El nombre de usuario no está disponible");
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

        

        public void Editar(Usuario u)
        {

            Conexion conexion = new Conexion();

            String sqlCode = "UPDATE usuarios SET username='{0}',nombre='{1}',contrasenna='{2}',telefono='{3}',correo='{4}',cedula='{5}'" +
                " WHERE id = {6}; ";
            sqlCode = string.Format(sqlCode, u.Username, u.Nombre, u.Contrasenna, u.Telefono, u.Correo, u.Cedula,u.Id);

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
        public void Eliminar(Usuario u)
        {

            Conexion conexion = new Conexion();

            String sqlCode = "UPDATE usuarios SET activo=false" +
                " WHERE id = {0}; ";
            sqlCode = string.Format(sqlCode, u.Id);

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
        public void Loguear(Usuario c)
        {
            Conexion conexion = new Conexion();

            String sqlCode = "SELECT id,username,nombre,contrasenna,telefono,correo,cedula,administrador FROM usuarios" +
                    " WHERE activo = true AND username = '{0}' AND contrasenna = '{1}'; ";
            sqlCode = string.Format(sqlCode, c.Username, c.Contrasenna);

            try
            {
                conexion.Conectar();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, conexion.con);
                NpgsqlDataReader rs = command.ExecuteReader();

                if (rs.Read())
                {
                    Usuario nuevo = CargarNuevoUsuario(rs);
                    c.Id = nuevo.Id;
                    c.Telefono = nuevo.Telefono;
                    c.Correo = nuevo.Correo;
                    c.Cedula = nuevo.Cedula;
                    c.Nombre = nuevo.Nombre;
                    c.Administrador = nuevo.Administrador;
                }
                else
                {
                    c.Id = 0;
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
    }
}
