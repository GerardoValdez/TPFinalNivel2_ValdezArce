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
using AccesoDatos;
using Negocio;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        List<Articulo> listaArticulos;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está seguro de salir del programa?", "SALIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarFormulario();
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Precio");

        }

        private void cargarFormulario()
        {
            articuloNegocio articuloNegocio = new articuloNegocio();
            try
            {
                listaArticulos = articuloNegocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
                cargarImagen(listaArticulos[0].ImagenUrl);
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {    
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;         
            cargarImagen(seleccionado.ImagenUrl);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                
                pbxArticulos.Load(imagen);
            }
            catch (Exception )
            {
                pbxArticulos.Load("http://www.carsaludable.com.ar/wp-content/uploads/2014/03/default-placeholder.png"); 
            }
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta= new frmAltaArticulo();          
            alta.ShowDialog();
            cargarFormulario();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            
            if(dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                cargarFormulario();
            }
         
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            Articulo seleccionado;

            try
            {
                if(MessageBox.Show("El registro se eliminará del programa y de la Base de Datos. ¿Desea continuar?","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargarFormulario();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            
            if(opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");

            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccionar el campo para filtrar");
                return true;
            }

            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccionar el criterio para filtrar");
                return true;
            }

            if(cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltro.Text))
                {
                    MessageBox.Show("Debe ingresar números para realizar la busqueda");
                    return true;
                }
                if (!(soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Sólo se permiten números enteros");
                    return true;
                }
            }
     
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach ( char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();

            try
            {
                if (validarFiltro())
                    return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvArticulos.DataSource = negocio.filtar(campo,criterio,filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

       
    }
}
