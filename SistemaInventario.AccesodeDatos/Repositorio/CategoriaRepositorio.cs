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
    public class CategoriaRepositorio : Repositorio<Categoria>, iCategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void actualizar(Categoria categoria)
        {
            var bodegaDB = _db.Bodegas.FirstOrDefault(b => b.Id == categoria.Id);
            if (bodegaDB != null)
            {
                categoria.Nombre = bodegaDB.Nombre;
                categoria.Descripcion = bodegaDB.Descripcion;
                categoria.Estado = bodegaDB.Estado;
                categoria.Id = bodegaDB.Id;
                _db.SaveChanges();
            }
        }
    }
}
