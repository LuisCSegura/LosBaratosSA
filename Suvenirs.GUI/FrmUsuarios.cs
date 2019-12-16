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
    public partial class FrmUsuarios : Form
    {
        UsuarioBOL usuBol = new UsuarioBOL();
        LinkedList<Usuario> usuarios;
        Usuario seleccionado;
        public FrmUsuarios()
        {
            InitializeComponent();
            usuarios = new LinkedList<Usuario>();
            seleccionado = new Usuario();
            CargarTablaUsuarios();
        }
        private void CargarTablaUsuarios()
        {
            try
            {
                usuarios = usuBol.CargarTodos();
                dgvUsuarios.RowCount = 0;
                ; foreach (Usuario usuario in usuarios)
                {
                    int n = dgvUsuarios.Rows.Add();
                    dgvUsuarios.Rows[n].Cells[0].Value = usuario.Username;
                    dgvUsuarios.Rows[n].Cells[1].Value = usuario.Nombre;
                    dgvUsuarios.Rows[n].Cells[2].Value = usuario.Cedula;
                    dgvUsuarios.Rows[n].Cells[3].Value = usuario.Telefono;
                    dgvUsuarios.Rows[n].Cells[4].Value = usuario.Correo;

                }
            }
            catch (Exception e)
            {
                lblErrores.Text = e.Message;
            }

        }
        private void LimpiarDatos()
        {
            seleccionado = new Usuario();
            txtUsername.Text = "";
            txtCedula.Text = "";
            txtContrasenna.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
        }
        public static string DesEncriptar(string contrasenna)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(contrasenna);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }


        private void DgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                lblNuevo.Text = "N U E V O";
                LimpiarDatos();
                seleccionado = new Usuario();
            }
            catch (Exception ex)
            {

                lblErrores.Text = ex.Message;
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            
        }

        private void DgvUsuarios_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblErrores.Text = "";
            if (e.RowIndex != -1)
            {
                lblNuevo.Text = "E D I T A R";
                Usuario u = usuarios.ElementAt(e.RowIndex);
                seleccionado.Id = u.Id;
                seleccionado.Username = u.Username;
                seleccionado.Nombre = u.Nombre;
                seleccionado.Cedula = u.Cedula;
                seleccionado.Contrasenna = u.Contrasenna;
                seleccionado.Telefono = u.Telefono;
                seleccionado.Correo = u.Correo;
                txtUsername.Text = seleccionado.Username;
                txtCedula.Text = seleccionado.Cedula;
                txtContrasenna.Text = DesEncriptar(seleccionado.Contrasenna);
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
                    if (MessageBox.Show("Desea Eliminar el usuario: " + seleccionado.Username+"?",
                        "Eliminar Registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        usuBol.Eliminar(seleccionado);
                        CargarTablaUsuarios();
                        seleccionado = new Usuario();
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


                
                seleccionado.Username = txtUsername.Text;
                seleccionado.Nombre= txtNombre.Text;
                seleccionado.Cedula = txtCedula.Text;
                seleccionado.Contrasenna = txtContrasenna.Text;
                seleccionado.Telefono = txtTelefono.Text;
                seleccionado.Correo = txtCorreo.Text;
               
 

                    if (seleccionado.Id > 0)
                    {
                        usuBol.Editar(seleccionado);
                    }
                    else
                    {
                        seleccionado.Administrador = true;
                        usuBol.InsertarNuevo(seleccionado);
                    }
                    CargarTablaUsuarios();
                    seleccionado = new Usuario();
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
