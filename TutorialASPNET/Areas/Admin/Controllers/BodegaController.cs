using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using SistemaInventario.Modelos;

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
            bodega = await _unidadtrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if(bodega == null) 
            {
                return NotFound();
            }
            return View(bodega);
        }   
        #region API

       

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() 
        {
            var todos = await _unidadtrabajo.Bodega.ObtenerTodos();
            return Json(new {data = todos});
        }

        #endregion

    }
}
