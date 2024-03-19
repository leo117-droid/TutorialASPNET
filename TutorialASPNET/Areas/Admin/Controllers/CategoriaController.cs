using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace TutorialASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly iUnidadTrabajo _unidadtrabajo;

        public CategoriaController(iUnidadTrabajo unidadtrabajo)
        {
            _unidadtrabajo = unidadtrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if (id == null)
            {
                //Crear una nueva bodega
                categoria.Estado = true;
                return View(categoria);
            }
            //Actualizamos bodega
            categoria = await _unidadtrabajo.Categoria.obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadtrabajo.Categoria.Agregar(categoria);
                    TempData[DS.exitosa] = "Categoria creada exitosamente";
                }
                else
                {
                    _unidadtrabajo.Categoria.actualizar(categoria);
                    TempData[DS.exitosa] = "Categoria actualizada exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.error] = "Error al grabar categoria";
            return View(categoria);
        }
        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadtrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos }); // Aqui es con el nombre con el que se va a referenciar en el javascript
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDb = await _unidadtrabajo.Categoria.obtener(id);
            if (categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Categoria" });
            }
            _unidadtrabajo.Categoria.Remover(categoriaDb);
            await _unidadtrabajo.Guardar();
            return Json(new { success = true, message = "Categoria borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadtrabajo.Categoria.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
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