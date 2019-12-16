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

namespace Suvenirs.GUI
{
    public partial class FrmPrincipalClientes : Form
    {
        Usuario usuario;
        Form parent;
        public FrmPrincipalClientes(Usuario u, Form p)
        {
            InitializeComponent();
            usuario = u;
            parent = p;
        }
    }
}
