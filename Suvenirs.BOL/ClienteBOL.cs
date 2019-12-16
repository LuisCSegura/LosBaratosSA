using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Suvenirs.DAL;

namespace Suvenirs.BOL
{
    public class ClienteBOL
    {
        ClienteDAL cliDal = new ClienteDAL();
        public LinkedList<Cliente> CargarTodos()
        {
            try
            {
                return cliDal.CargarTodos();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Eliminar(Cliente seleccionado)
        {
            try
            {
                cliDal.Eliminar(seleccionado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Editar(Cliente c)
        {
            try
            {
                Validar(c);
                cliDal.Editar(c);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void InsertarNuevo(Cliente c)
        {
            try
            {
                Validar(c);
                cliDal.InsertarNuevo(c);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        private void Validar(Cliente c)
        {
            if (string.IsNullOrEmpty(c.Nombre))
            {
                throw new Exception("Debe digitar un nombre valido");
            }
            if (string.IsNullOrEmpty(c.Cedula))
            {
                throw new Exception("Debe digitar una cedula valida");
            }
            if (string.IsNullOrEmpty(c.Correo))
            {
                throw new Exception("Debe digitar un correo valido");
            }

        }
    }
}
