﻿using System;
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
                datos.setearConsulta("select  A.Id, Codigo, Nombre, A.Descripcion Descripcion, C.Descripcion Categoría, A.idCategoria, M.Descripcion Marca, A.idMarca, ImagenUrl, Precio From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoría"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
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

        public void modificar(Articulo modificado)
        {
            Acceso acceso = new Acceso();

            try
            {
                acceso.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdCategoria = @IdCategoria, IdMarca = @IdMarca, imagenUrl = @imagenUrl, Precio = @Precio WHERE Id = @Id ");

                acceso.setearParametro("@Codigo", modificado.Codigo);
                acceso.setearParametro("@Nombre", modificado.Nombre);
                acceso.setearParametro("@Descripcion", modificado.Descripcion);
                acceso.setearParametro("@IdCategoria", modificado.Categoria.Id);
                acceso.setearParametro("@IdMarca", modificado.Marca.Id);
                acceso.setearParametro("@imagenUrl", modificado.ImagenUrl);
                acceso.setearParametro("@Precio", modificado.Precio);
                acceso.setearParametro("@Id", modificado.Id);

                acceso.ejecutarNoQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                acceso.cerrarConexion();
            }
        }

        public void agregar(Articulo nuevoArticulo)
        {
            Acceso acceso = new Acceso();
            try
            {
                acceso.setearConsulta("Insert Into ARTICULOS(Codigo, Nombre, Descripcion, IdCategoria, IdMarca, imagenUrl, Precio )" +
                                                   "Values(@Codigo, @Nombre, @Descripcion, @IdCategoria, @IdMarca, @imagenUrl, @Precio)");
                
                acceso.setearParametro("@Codigo",nuevoArticulo.Codigo);
                acceso.setearParametro("@Nombre", nuevoArticulo.Nombre);
                acceso.setearParametro("@Descripcion", nuevoArticulo.Descripcion);
                acceso.setearParametro("@IdCategoria", nuevoArticulo.Categoria.Id);
                acceso.setearParametro("@IdMarca", nuevoArticulo.Marca.Id);
                acceso.setearParametro("@imagenUrl", nuevoArticulo.ImagenUrl);
                acceso.setearParametro("@Precio", nuevoArticulo.Precio);
                acceso.ejecutarNoQuery();

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
