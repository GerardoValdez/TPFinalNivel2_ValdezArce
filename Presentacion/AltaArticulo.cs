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
using System.IO;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {
        Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();      
        }

        public frmAltaArticulo(Articulo seleccionado)
        {
            InitializeComponent();
            Text = "Modificar Artículo";
            articulo = seleccionado;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                articuloNegocio negocio = new articuloNegocio();

                try
                {
                    if (articulo == null)
                        articulo = new Articulo();
                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                    articulo.Marca = (Marca)cboMarca.SelectedItem;
                    articulo.ImagenUrl = txtImagen.Text;
                    articulo.Precio = decimal.Parse(txtPrecio.Text);



                    if (articulo.Id != 0)
                    {
                        negocio.modificar(articulo);
                        MessageBox.Show("Se modificó el artículo correctamente");
                    }
                    else
                    {
                        negocio.agregar(articulo);
                        MessageBox.Show("Se agregó el artículo correctamente");
                    }

                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            } 
            
        }

        private bool validar()
        {
            bool validado = true;


            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecio.Text) || cboCategoria.SelectedIndex < 0 || cboMarca.SelectedIndex < 0)
            {
                validado = false;
                MessageBox.Show("Debe ingresar un código, un nombre, un precio, seleccionar categoría y marca para agregar un artículo nuevo","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
             
            }

            if (!(string.IsNullOrEmpty(txtPrecio.Text)))
                try
                {
                    decimal.Parse(txtPrecio.Text);
                }
                catch (Exception)
                {
                    validado = false;
                    MessageBox.Show("Debe ingresar números en el precio del artículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                }

            return validado;

        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            categoriaNegocio negocioC = new categoriaNegocio();
            marcaNegocio negocioM = new marcaNegocio();

            try 
            {
                cboCategoria.DataSource = negocioC.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.DataSource = negocioM.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.SelectedIndex = -1;
                cboMarca.SelectedIndex = -1;



                if (articulo!= null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;

                    txtImagen.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    txtPrecio.Text = articulo.Precio.ToString();

                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;


                }

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
