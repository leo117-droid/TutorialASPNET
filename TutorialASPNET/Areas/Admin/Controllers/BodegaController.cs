using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;

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
