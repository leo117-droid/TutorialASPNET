using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialASPNET.SistemaInventario.AccesodeDatos.Data;

namespace SistemaInventario.AccesodeDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, iProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void actualizar(Producto producto)
        {
            var productoDB = _db.Productos.FirstOrDefault(b => b.Id == producto.Id);
            if (productoDB != null)
            {
                if (producto.ImgURL != null)
                {
                   productoDB.ImgURL = producto.ImgURL;
                }
                productoDB.NumSerie = producto.NumSerie;
                productoDB.Descripcion = producto.Descripcion;
                productoDB.Costo = producto.Costo;
                productoDB.Padre = producto.Padre;
                productoDB.Precio = producto.Precio;
                productoDB.CategoriaId = producto.CategoriaId;
                productoDB.MarcaId = producto.MarcaId;
                productoDB.Estado = producto.Estado;

                _db.SaveChanges();
            }         
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            if (obj == "Marca")
            {
                return _db.Marcas.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            if (obj == "Producto")
            {
                return _db.Productos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
    
}
