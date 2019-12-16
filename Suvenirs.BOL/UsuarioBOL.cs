using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.DAL;
using Suvenirs.Entities;

namespace Suvenirs.BOL
{
    public class UsuarioBOL
    {
        UsuarioDAL usuDal = new DAL.UsuarioDAL();
        public LinkedList<Usuario> CargarTodos()
        {
            try
            {
                return usuDal.CargarTodos();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void InsertarNuevo(Usuario u)
        {
            try
            {
                Validar(u);
                u.Contrasenna = Encriptar(u.Contrasenna);
                usuDal.InsertarNuevo(u);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        private void Validar(Usuario u)
        {
            if (string.IsNullOrEmpty(u.Username) || u.Username == "USUARIO")
            {
                throw new Exception("Debe digitar un nombre de usuario valido");
            }
            if (u.Cedula == null )
            {
                throw new Exception("Debe digitar una cedula valida");
            }
            if (u.Contrasenna == null || (!(u.Contrasenna.Length >= 8)) || u.Contrasenna == "CONTRASEÑA")
            {
                throw new Exception("Debe digitar una contraseña valida");
            }
            if (string.IsNullOrEmpty(u.Nombre))
            {
                throw new Exception("Debe digitar el nombre");
            }
        }
       
        private string Encriptar(string contrasenna)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenna);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public void Editar(Usuario u)
        {
            try
            {
                Validar(u);
                u.Contrasenna = Encriptar(u.Contrasenna);
                usuDal.Editar(u);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Loguear(Usuario usuario)
        {
            try
            {
                Validar(usuario);
                usuario.Contrasenna = Encriptar(usuario.Contrasenna);
                usuDal.Loguear(usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Eliminar(Usuario u)
        {
            try
            {
                usuDal.Eliminar(u);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
