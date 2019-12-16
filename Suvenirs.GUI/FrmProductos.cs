using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Suvenirs.Entities;
using Suvenirs.BOL;

namespace Suvenirs.GUI
{
    public partial class FrmProductos : Form
    {
        ProductoBOL proBol = new ProductoBOL();
        LinkedList<Producto> productos;
        Producto seleccionado;
        public FrmProductos()
        {
            InitializeComponent();
            productos = new LinkedList<Producto>();
            seleccionado = new Producto();
            CargarTablaProductos();
        }
        private void CargarTablaProductos()
        {
            try
            {
                productos = proBol.CargarTodos();
                dgvProductos.RowCount = 0;
                ; foreach (Producto producto in productos)
                {
                    int n = dgvProductos.Rows.Add();
                    dgvProductos.Rows[n].Cells[0].Value = producto.Nombre;
                    dgvProductos.Rows[n].Cells[1].Value = producto.Codigo;
                    dgvProductos.Rows[n].Cells[2].Value = producto.Categoria;
                    dgvProductos.Rows[n].Cells[3].Value = producto.Cantidad;
                    dgvProductos.Rows[n].Cells[4].Value = producto.Precio;

                }
            }
            catch (Exception e)
            {
                lblErrores.Text = e.Message;
            }

        }
        private void LimpiarDatos()
        {
            seleccionado = new Producto();
            nudCantidad.Value=0;
            nudPrecio.Value=0;
            txtNombre.Text = "";
            txtCodigo.Text = "";
            cbxCategoria.SelectedIndex=-1;
        }
        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                lblNuevo.Text = "N U E V O";
                LimpiarDatos();
                seleccionado = new Producto();
            }
            catch (Exception ex)
            {

                lblErrores.Text = ex.Message;
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                if (seleccionado.Id > 0)
                {
                    if (MessageBox.Show("Desea Eliminar el producto: " + seleccionado.Nombre + "?",
                        "Eliminar Registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        proBol.Eliminar(seleccionado);
                        CargarTablaProductos();
                        seleccionado = new Producto();
                        LimpiarDatos();
                    }

                }
                else
                {
                    throw new Exception("Debe seleccionar un Usuario");
                }
            }
            catch (Exception ex)
            {
                lblErrores.Text = ex.Message;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                seleccionado.Nombre = txtNombre.Text;
                seleccionado.Codigo = txtCodigo.Text;
                seleccionado.Categoria = cbxCategoria.Text;
                seleccionado.Cantidad = (int)(nudCantidad.Value);
                seleccionado.Precio = (double)(nudPrecio.Value);

                if (seleccionado.Id > 0)
                {
                    proBol.Editar(seleccionado);
                }
                else
                {
                    proBol.InsertarNuevo(seleccionado);
                }
                CargarTablaProductos();
                seleccionado = new Producto();
                lblNuevo.Text = "N U E V O";
                LimpiarDatos();


            }
            catch (Exception ex)
            {
                lblErrores.Text = ex.Message;
            }
        }

        private void DgvProductos_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblErrores.Text = "";
            if (e.RowIndex != -1)
            {
                lblNuevo.Text = "E D I T A R";
                Producto p = productos.ElementAt(e.RowIndex);
                seleccionado.Id = p.Id;
                seleccionado.Nombre = p.Nombre;
                seleccionado.Codigo = p.Codigo;
                seleccionado.Categoria = p.Categoria;
                seleccionado.Cantidad = p.Cantidad;
                seleccionado.Precio = p.Precio;
                txtCodigo.Text = seleccionado.Codigo;
                txtNombre.Text = seleccionado.Nombre;
                cbxCategoria.Text = seleccionado.Categoria;
                if(seleccionado.Cantidad<1|| seleccionado.Cantidad > 100000000)
                {
                    seleccionado.Cantidad = 1;
                }
                nudCantidad.Value = seleccionado.Cantidad;
                nudPrecio.Value = (decimal)seleccionado.Precio;
            }
        }
    }
}
