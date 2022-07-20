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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está por salir de esta ventana?","CANCELAR OPERACIÓN",MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)== DialogResult.OK)
            {
                Close();
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {

                 ptbAltaArticulo.Load(imagen);
            }
            catch (Exception)
            {
                ptbAltaArticulo.Load("http://www.carsaludable.com.ar/wp-content/uploads/2014/03/default-placeholder.png");
            }
        }
    }
}
