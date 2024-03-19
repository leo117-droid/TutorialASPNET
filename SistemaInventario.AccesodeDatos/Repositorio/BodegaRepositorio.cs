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
    public class BodegaRepositorio : Repositorio<Bodega>, iBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void actualizar(Bodega bodega)
        {
            var bodegaDB = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDB != null)
            {
                bodega.Nombre = bodegaDB.Nombre;
                bodega.Descripcion = bodegaDB.Descripcion;
                bodega.Estado = bodegaDB.Estado;
                bodega.Id = bodegaDB.Id;
                _db.SaveChanges();
            }
        }

        public Task<Bodega> Obtener(int v)
        {
            throw new NotImplementedException();
        }
    }
}
