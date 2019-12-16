using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Suvenirs.Entities;


namespace Suvenirs.GUI
{
    public partial class FrmPrincipal : Form
    {
        Usuario usuario;
        Form parent;
        public FrmPrincipal(Form p,Usuario u)
        {
            InitializeComponent();
            AgregarFormEnPanel(new FrmVentas());
            parent = p;
            usuario = u;
            lblUserName.Text = usuario.Username;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd,int wmsg,int wparam,int lparam);
        private void AgregarFormEnPanel(Form formHijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
            {
                this.pnlContenedor.Controls.RemoveAt(0);
            }
            Form frm = formHijo;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(frm);
            this.pnlContenedor.Tag = frm;
            frm.Show();
        }

        private void CambiarPanelIndice(Button btn)
        {
            pnlMainMenuIndex.Location = new Point(pnlMainMenuIndex.Location.X, btn.Location.Y);
            pnlMainMenuIndex.Height = btn.Size.Height;
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            AgregarFormEnPanel(new FrmVentas());
        }

        private void BtnVentas_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            CambiarPanelIndice(btn); ;
        }

        private void Button1_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            CambiarPanelIndice(btn);
        }

        private void Button2_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            CambiarPanelIndice(btn);
        }

        private void Button3_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            CambiarPanelIndice(btn);
        }

        private void Button4_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            CambiarPanelIndice(btn);
        }

        private void Button5_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn= sender as Button;
            CambiarPanelIndice(btn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PibxCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
        }

        private void PibxExpandir_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void PibxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PibxCerrar_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(30, 139, 163);
        }

        private void PibxCerrar_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(0, 109, 133);
        }

        private void PibxExpandir_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(30, 139, 163);
        }

        private void PibxMinimizar_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(30, 139, 163);
        }

        private void PibxExpandir_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(0, 109, 133);
        }

        private void PibxMinimizar_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.FromArgb(0, 109, 133);
        }

        private void PnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void PnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            AgregarFormEnPanel(new FrmUsuarios());
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            AgregarFormEnPanel(new FrmClientes());
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            AgregarFormEnPanel(new FrmProductos());
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            AgregarFormEnPanel(new FrmReportes());
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir de su cuenta?", "Salir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Dispose();
                parent.Visible = true;
            }
        }
    }
}
