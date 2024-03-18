using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialASPNET.SistemaInventario.AccesodeDatos.Data;

namespace SistemaInventario.AccesodeDatos.Repositorio
{
    public class UnidadTrabajo : iUnidadTrabajo
    {

        private readonly ApplicationDbContext _db;
        public iBodegaRepositorio Bodega {  get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);

        }
        public void Dispose() 
        {
            _db.Dispose();
        }

 

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
