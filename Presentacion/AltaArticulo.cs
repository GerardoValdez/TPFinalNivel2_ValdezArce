using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
using AccesoDatos;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo nuevoArticulo = new Articulo();
            articuloNegocio negocio = new articuloNegocio();

            try
            {
                nuevoArticulo.Codigo = txtCodigo.Text;
                nuevoArticulo.Nombre = txtNombre.Text;
                nuevoArticulo.Descripcion = txtDescripcion.Text;
                nuevoArticulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                nuevoArticulo.Marca = (Marca)cboMarca.SelectedItem;
                nuevoArticulo.ImagenUrl = txtImagen.Text;
                nuevoArticulo.Precio = decimal.Parse(txtPrecio.Text);

                negocio.agregar(nuevoArticulo);

                MessageBox.Show("Se agregó el artículo correctamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            categoriaNegocio negocioC = new categoriaNegocio();
            marcaNegocio negocioM = new marcaNegocio();

            try
            {
                cboCategoria.DataSource = negocioC.listar();
                cboMarca.DataSource = negocioM.listar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}
