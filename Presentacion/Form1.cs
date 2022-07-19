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
            cargarForm();
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
        }

        private void cargarForm()
        {
            articuloNegocio articuloNegocio = new articuloNegocio();

            try
            {
                listaArticulos = articuloNegocio.listar();
                dgvArticulos.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta= new frmAltaArticulo();          
            alta.ShowDialog();
        }
    }
}
