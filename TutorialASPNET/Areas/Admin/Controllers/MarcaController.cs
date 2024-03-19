using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace TutorialASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly iUnidadTrabajo _unidadtrabajo;

        public MarcaController(iUnidadTrabajo unidadtrabajo)
        {
            _unidadtrabajo = unidadtrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();
            if (id == null)
            {
                //Crear una nueva bodega
                marca.Estado = true;
                return View(marca);
            }
            //Actualizamos bodega
            marca = await _unidadtrabajo.Marca.obtener(id.GetValueOrDefault());
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    await _unidadtrabajo.Marca.Agregar(marca);
                    TempData[DS.exitosa] = "Marca creada exitosamente";
                }
                else
                {
                    _unidadtrabajo.Marca.actualizar(marca);
                    TempData[DS.exitosa] = "Marca actualizada exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.error] = "Error al grabar marca";
            return View(marca);
        }
        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadtrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos }); // Aqui es con el nombre con el que se va a referenciar en el javascript
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marcaDb = await _unidadtrabajo.Marca.obtener(id);
            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Marca" });
            }
            _unidadtrabajo.Marca.Remover(marcaDb);
            await _unidadtrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadtrabajo.Marca.ObtenerTodos();
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