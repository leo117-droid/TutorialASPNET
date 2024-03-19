using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace TutorialASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        private readonly iUnidadTrabajo _unidadtrabajo;

        public BodegaController(iUnidadTrabajo unidadtrabajo)
        {
            _unidadtrabajo = unidadtrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            Bodega bodega = new Bodega();
            if(id == null) 
            {
                //Crear una nueva bodega
                bodega.Estado = true;
                return View(bodega);
            }
            //Actualizamos bodega
            bodega = await _unidadtrabajo.Bodega.obtener(id.GetValueOrDefault());
            if(bodega == null) 
            {
                return NotFound();
            }
            return View(bodega);
        }
        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    await _unidadtrabajo.Bodega.Agregar(bodega);
                    TempData[DS.exitosa] = "Bodega creada exitosamente";
                }
                else
                {
                    _unidadtrabajo.Bodega.actualizar(bodega);
                    TempData[DS.exitosa] = "Bodega actualizada exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.error] = "Error al grabar bodega";
            return View(bodega);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() 
        {
            var todos = await _unidadtrabajo.Bodega.ObtenerTodos();
            return Json(new {data = todos});
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            var BodegaDB = await _unidadtrabajo.Bodega.obtener(id);
            if(BodegaDB == null) 
            {
                return Json(new { success = false, message = "Error al eliminar la bodega" });
            }
            _unidadtrabajo.Bodega.Remover(BodegaDB);
            await _unidadtrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borrada exitosamente" });

        }
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadtrabajo.Bodega.ObtenerTodos();
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
#endregion