using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;


namespace Negocio
{
    public class articuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("select Codigo, Nombre, A.Descripcion Descripcion, C.Descripcion Categoría, M.Descripcion Marca, ImagenUrl, Precio From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoría"];

                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    

                    listaArticulos.Add(aux);
                }
                return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public void agregar(Articulo nuevoArticulo)
        {
            Acceso acceso = new Acceso();
            try
            {
                acceso.setearConsulta("Insert Into ARTICULOS");
                acceso.ejecutarConsulta();
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                acceso.cerrarConexion();
            }
        }
    }
}
