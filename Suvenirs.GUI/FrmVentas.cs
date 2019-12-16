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
using System.Net.Mail;
using System.Net;

namespace Suvenirs.GUI
{
    public partial class FrmVentas : Form
    {
        ClienteBOL cliBol = new ClienteBOL();
        ProductoBOL proBol = new ProductoBOL();
        FacturaBOL facBol = new FacturaBOL();
        LinkedList<Cliente> clientes;
        LinkedList<Producto> productos;
        Factura factura;
        public FrmVentas()
        {
            InitializeComponent();
            clientes = new LinkedList<Cliente>();
            clientes = new LinkedList<Cliente>();
            factura = new Factura();
            CargarLstClientes();
            CargarLstProductos();
        }
        private void CargarLstClientes()
        {
            try
            {
                clientes = cliBol.CargarTodos();
                lstClientes.Items.Clear();
                foreach (Cliente cliente in clientes)
                {
                    lstClientes.Items.Add(cliente);
                }
            }
            catch (Exception e)
            {
                lblErrores.Text = e.Message;
            }

        }
        private void CargarLstProductos()
        {
            try
            {
                productos = proBol.CargarTodos();
                
                lstProductos.Items.Clear();
                foreach (Producto producto in productos)
                {
                    bool suficientes = true;
                    foreach (ProductoFactura prodFact in factura.productos)
                    {
                        if (prodFact.Producto.Codigo == producto.Codigo)
                        {
                            if ((producto.Cantidad - prodFact.Cantidad) <= 0)
                            {
                                suficientes = false;
                            }
                        }
                    }
                    if (producto.Cantidad > 0 && suficientes)
                    {
                        lstProductos.Items.Add(producto);
                    }

                }
            }
            catch (Exception e)
            {
                lblErrores.Text = e.Message;
            }

        }
        private void CargarClientesFiltro(string filtro)
        {
            lstClientes.Items.Clear();
            foreach (Cliente cliente in clientes)
            {
                if (cliente.Nombre.ToLower().Contains(filtro.ToLower()))
                {
                    lstClientes.Items.Add(cliente);
                }
            }
        }
        private void CargarProductosFiltro(string filtro)
        {
            lstProductos.Items.Clear();
            foreach (Producto producto in productos)
            {
                bool suficientes = true;
                foreach (ProductoFactura prodFact in factura.productos)
                {
                    if (prodFact.Producto.Codigo == producto.Codigo)
                    {
                        if ((producto.Cantidad - prodFact.Cantidad) <= 0)
                        {
                            suficientes = false;
                        }
                    }
                }
                if (producto.Cantidad > 0 && suficientes)
                {
                    if ((producto.Nombre.ToLower().Contains(filtro.ToLower())) || (producto.Codigo.ToLower().Contains(filtro.ToLower())))
                    {
                        lstProductos.Items.Add(producto);
                    }
                }
                
            }
            
        }
        private void CargarPnlFactura()
        {
            
            dgvFactura.RowCount = 0;
            foreach (ProductoFactura productoFactura in factura.productos)
            {
                int n = dgvFactura.Rows.Add();
                dgvFactura.Rows[n].Cells[0].Value = productoFactura.Cantidad;
                dgvFactura.Rows[n].Cells[1].Value = productoFactura.Producto.Nombre;
                dgvFactura.Rows[n].Cells[2].Value = productoFactura.ObtenerPrecio();
            }
            double totalSinImpuesto = factura.ObtenerTotal();
            factura.Total = totalSinImpuesto + (totalSinImpuesto * 0.13);
            lblTotalSinImpuesto.Text = "Total sin Impuesto:    " + totalSinImpuesto;
            lblTotal.Text = "T O T A L :    "+factura.Total;
        }
        private void EnviarMail()
        {
            MailMessage mmsg = new MailMessage();
            mmsg.To.Add(factura.Cliente.Correo);
            mmsg.Subject = "Comprobante de Factura Electronica Los Baratos S.A";
            mmsg.SubjectEncoding = Encoding.UTF8;
            mmsg.Body = factura.GetInfo();
            mmsg.BodyEncoding = Encoding.UTF8;
            mmsg.From = new MailAddress("losbaratossa@gmail.com");
            SmtpClient cliente = new SmtpClient();
            cliente.Credentials = new NetworkCredential("losbaratossa@gmail.com","baraticos123");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";
            try
            {
                cliente.Send(mmsg);
            }
            catch (Exception ex)
            {

               throw new Exception("Ha ocurrido un problema al Enviar el Correo\n Por favor verifique su conexión y la dirección proporcionada");
            }
        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtFiltroClientes_TextChanged(object sender, EventArgs e)
        {
            CargarClientesFiltro(txtFiltroClientes.Text);
        }

        private void TxtFiltroProductos_TextChanged(object sender, EventArgs e)
        {
            CargarProductosFiltro(txtFiltroProductos.Text);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrores.Text = "";
                if (lstProductos.SelectedIndex >= 0)
                {
                    Producto p = (Producto)(lstProductos.SelectedItem);
                    ProductoFactura pf = new ProductoFactura(p);
                    pf.Cantidad = (int)(nudCantidad.Value);
                    if (pf.Cantidad > pf.Producto.Cantidad)
                    {
                        throw new Exception("No hay suficientes unidades del producto");
                    }
                    bool agregado = false;
                    foreach (ProductoFactura producto in factura.productos)
                    {
                        if (producto.Producto.Codigo == p.Codigo)
                        {
                            producto.Cantidad += pf.Cantidad;
                            agregado = true;
                        }
                    }
                    if (!agregado)
                    {
                        factura.productos.AddLast(pf);
                    }
                    CargarPnlFactura();
                    CargarLstProductos();
                    nudCantidad.Value = 1;
                }
                else
                {
                    throw new Exception("Debe seleccionar un producto de la lista");
                }
            } 
            catch (Exception ex)
            {

                lblErrores.Text =ex.Message;
            }
            
        }

        private void BtnFacturar_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                if (lstClientes.SelectedIndex >= 0)
                {
                    Cliente c = (Cliente)(lstClientes.SelectedItem);
                    factura.Cliente = c;
                    factura.Id = facBol.ObtenerSigId();
                    DialogResult confirmDialog = (MessageBox.Show("Desea Enviar la Factura a el correo: " 
                        + c.Correo + "?", "Confirmar", MessageBoxButtons.YesNoCancel));
                    if (confirmDialog == DialogResult.Yes)
                    {
                        EnviarMail();
                        facBol.InsertarNuevo(factura);
                        foreach (ProductoFactura prodFact in factura.productos)
                        {
                            prodFact.Producto.Cantidad -= prodFact.Cantidad;
                            proBol.Editar(prodFact.Producto);
                        }
                        factura = new Factura();
                        CargarPnlFactura();
                        CargarLstProductos();
                        CargarLstClientes();
                    }
                    else if (confirmDialog == DialogResult.No)
                    {
                        facBol.InsertarNuevo(factura);
                        foreach (ProductoFactura prodFact in factura.productos)
                        {
                            prodFact.Producto.Cantidad -= prodFact.Cantidad;
                            proBol.Editar(prodFact.Producto);
                        }
                        factura = new Factura();
                        CargarPnlFactura();
                        CargarLstProductos();
                        CargarLstClientes();
                    }
                }
                else
                {
                    throw new Exception("Debe seleccionar un cliente");
                }
            }
            catch (Exception ex)
            {

                lblErrores.Text=ex.Message;
            }
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            lblErrores.Text = "";
            try
            {
                int row = dgvFactura.CurrentRow.Index;
                if (row >= 0)
                {
                    ProductoFactura pf = factura.productos.ElementAt<ProductoFactura>(row);
                    LinkedList<ProductoFactura> aEliminar = new LinkedList<ProductoFactura>();
                    foreach (ProductoFactura producto in factura.productos)
                    {
                        if (pf.Producto.Codigo == producto.Producto.Codigo)
                        {
                            aEliminar.AddLast(producto);
                        }
                    }
                    foreach (ProductoFactura item in aEliminar)
                    {
                        factura.productos.Remove(item);
                    }
                    CargarPnlFactura();
                    CargarLstProductos();
                }
                else
                {
                    throw new Exception("Debe seleccionar un producto de la tabla");
                }
            }
            catch (Exception ex)
            {

                lblErrores.Text =ex.Message;
            }
            
        }
    }
}
