using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suvenirs.Entities;
using Suvenirs.DAL;

namespace Suvenirs.BOL
{
    public class ProductoBOL
    {
        ProductoDAL proDal = new ProductoDAL();
        public LinkedList<Producto> CargarTodos()
        {
            try
            {
                return proDal.CargarTodos();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Eliminar(Producto seleccionado)
        {
            try
            {
               proDal.Eliminar(seleccionado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Editar(Producto p)
        {
            try
            {
                Validar(p);
                proDal.Editar(p);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void InsertarNuevo(Producto p)
        {
            try
            {
                Validar(p);
                proDal.InsertarNuevo(p);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        private void Validar(Producto p)
        {
            if (string.IsNullOrEmpty(p.Nombre)||p.Nombre.Length>100)
            {
                throw new Exception("Debe digitar un nombre valido");
            }
            if (string.IsNullOrEmpty(p.Codigo) || p.Codigo.Length > 100)
            {
                throw new Exception("Debe digitar un codigo valido");
            }
            if (string.IsNullOrEmpty(p.Categoria) || p.Categoria.Length > 100)
            {
                throw new Exception("Debe seleccionar una categoria");
            }

        }
    }
}
