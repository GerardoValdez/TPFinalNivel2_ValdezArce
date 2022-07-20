using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class Acceso
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public Acceso()
        {
            conexion = new SqlConnection("Data Source = VALDEZ; Initial Catalog = CATALOGO_DB; Integrated Security = True");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }
       
        public void ejecutarLectura()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }           
        }

        public void ejecutarNoQuery()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if(lector != null)
            {
                lector.Close();
                conexion.Close();
            }
        }

         
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

    }
}
