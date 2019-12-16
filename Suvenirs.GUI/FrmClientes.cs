using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Suvenirs.BOL;
using Suvenirs.Entities;

namespace Suvenirs.GUI
{
    public partial class FrmClientes : Form
    {
        ClienteBOL cliBol = new ClienteBOL();
        LinkedList<Cliente> clientes;
        Cliente seleccionado;
        public FrmClientes()
        {
            InitializeComponent();
            clientes = new LinkedList<Cliente>();
            seleccionado = new Cliente();
            CargarTablaClientes();
        }
        private void CargarTablaClientes()
        {
            try
            {
                clientes = cliBol.CargarTodos();
                dgvClientes.RowCount = 0;
                ; foreach (Cliente cliente in clientes)
                {
                    int n = dgvClientes.Rows.Add();
                    dgvClientes.Rows[n].Cells[0].Value = cliente.Nombre;
                    dgvClientes.Rows[n].Cells[1].Value = cliente.Cedula;
                    dgvClientes.Rows[n].Cells[2].Value = cliente.Telefono;
                    dgvClientes.Rows[n].Cells[3].Value = cliente.Correo;

                }
            }
            catch (Exception e)
            {
                lblErrores.Text = e.Message;
            }

        }
        private void LimpiarDatos()
        {
            seleccionado = new Cliente();
            txtCedula.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                lblNuevo.Text = "N U E V O";
                LimpiarDatos();
                seleccionado = new Cliente();
            }
            catch (Exception ex)
            {

                lblErrores.Text = ex.Message;
            }
        }

        private void DgvClientes_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblErrores.Text = "";
            if (e.RowIndex != -1)
            {
                lblNuevo.Text = "E D I T A R";
                Cliente c = clientes.ElementAt(e.RowIndex);
                seleccionado.Id = c.Id;
                seleccionado.Nombre = c.Nombre;
                seleccionado.Cedula = c.Cedula;
                seleccionado.Telefono = c.Telefono;
                seleccionado.Correo = c.Correo;
                txtCedula.Text = seleccionado.Cedula;
                txtNombre.Text = seleccionado.Nombre;
                txtCorreo.Text = seleccionado.Correo;
                txtTelefono.Text = seleccionado.Telefono;
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                if (seleccionado.Id > 0)
                {
                    if (MessageBox.Show("Desea Eliminar el cliente: " + seleccionado.Nombre+"?",
                        "Eliminar Registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cliBol.Eliminar(seleccionado);
                        CargarTablaClientes();
                        seleccionado = new Cliente();
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
                seleccionado.Cedula = txtCedula.Text;
                seleccionado.Telefono = txtTelefono.Text;
                seleccionado.Correo = txtCorreo.Text;



                if (seleccionado.Id > 0)
                {
                    cliBol.Editar(seleccionado);
                }
                else
                {
                    cliBol.InsertarNuevo(seleccionado);
                }
                CargarTablaClientes();
                seleccionado = new Cliente();
                lblNuevo.Text = "N U E V O";
                LimpiarDatos();


            }
            catch (Exception ex)
            {
                lblErrores.Text = ex.Message;
            }
        }
    }
}
