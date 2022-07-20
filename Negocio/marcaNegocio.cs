using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace Negocio
{
    public class marcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> listaMarcas = new List<Marca>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM Marcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["Id"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];

                    listaMarcas.Add(marca);
                }
                return listaMarcas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
