using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewModels;
using SistemaInventario.Utilidades;

namespace TutorialASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly iUnidadTrabajo _unidadtrabajo;

        public ProductoController(iUnidadTrabajo unidadtrabajo)
        {
            _unidadtrabajo = unidadtrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Categoria"),
                MarcaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Marca")
            };

            if (id == null)
            {
                // crear nuevo prodcuto
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unidadtrabajo.Producto.obtener(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }


        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadtrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos }); // Aqui es con el nombre con el que se va a referenciar en el javascript
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadtrabajo.Producto.obtener(id);
            if (productoDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Producto" });
            }
            _unidadtrabajo.Producto.Remover(productoDb);
            await _unidadtrabajo.Guardar();
            return Json(new { success = true, message = "Producto borrada exitosamente" });
        }

        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarNombre(string serie, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadtrabajo.Producto.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.NumSerie.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion
    }
}