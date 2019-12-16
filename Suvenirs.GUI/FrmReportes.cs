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
    public partial class FrmReportes : Form
    {
        FacturaBOL facBol = new FacturaBOL();
        LinkedList<Factura> facturas;
        public FrmReportes()
        {
            InitializeComponent();
            facturas = new LinkedList<Factura>();
            CargarReportes();
        }
        private void CargarReportes()
        {
            try
            {
                facturas = facBol.CargarDia(dtpFecha.Value);
                double totalIngresos = 0;
                foreach (Factura factura in facturas)
                {
                    totalIngresos += factura.Total;
                }
                int cantVentas = facturas.Count;
                double avgCompras = totalIngresos / cantVentas;
                double totalNeto = totalIngresos - (totalIngresos * 0.13);
                lblTotalIngresos.Text = totalIngresos.ToString();
                lblCantVentas.Text = cantVentas.ToString();
                lblAVGCompras.Text = avgCompras.ToString();
                lblTotalNeto.Text = totalNeto.ToString();
            }
            catch (Exception ex)
            {

                lblErrores.Text=ex.Message;
            }
            
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarReportes();
        }
    }
    
}
