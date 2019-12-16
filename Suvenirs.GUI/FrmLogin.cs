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
using System.Drawing.Drawing2D;

namespace Suvenirs.GUI
{
    public partial class FrmLogin : Form
    {
        UsuarioBOL usuBol = new UsuarioBOL();
        public FrmLogin()
        {
            InitializeComponent();
            
        }
        private void PibxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtUsuario_Enter(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void TxtUsuario_Leave(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void TxtContra_Enter(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            if (txtContra.Text == "CONTRASEÑA")
            {
                txtContra.Text = "";
                txtContra.ForeColor = Color.LightGray;
                txtContra.UseSystemPasswordChar = true;

            }
        }

        private void TxtContra_Leave(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            if (txtContra.Text == "")
            {
                txtContra.Text = "CONTRASEÑA";
                txtContra.ForeColor = Color.DimGray;
                txtContra.UseSystemPasswordChar = false;

            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                Usuario usuario = new Usuario();
                usuario.Username = txtUsuario.Text;
                usuario.Contrasenna = txtContra.Text;
                usuario.Cedula = "000000000";
                usuario.Nombre = "Solicitante";
                usuBol.Loguear(usuario);
                if (usuario.Id > 0)
                {
                    Form p = new Form();
                    if (usuario.Administrador)
                    {
                        p = new FrmPrincipal(this,usuario);
                    }
                    else
                    {
                        p = new FrmPrincipalClientes(usuario, this);
                    }
                    p.Visible = true;
                    txtUsuario.Text = "USUARIO";
                    txtContra.Text = "CONTRASEÑA";
                    txtContra.ForeColor = Color.DimGray;
                    txtUsuario.ForeColor = Color.DimGray;
                    txtContra.UseSystemPasswordChar = false;
                    this.Visible = false;
                }
                else
                {
                    throw new Exception("Los datos ingresados no coinciden con ningún usuario");
                }
            }
            catch (Exception ex)
            {

                lblErrores.Text = ex.Message;
            }
        }

        private void PbxCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
