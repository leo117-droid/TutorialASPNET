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
    public class MarcaRepositorio : Repositorio<Marca>, iMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void actualizar(Marca marca)
        {
            var categoriaDB = _db.Categorias.FirstOrDefault(b => b.Id == marca.Id);
            if (categoriaDB != null)
            {
                marca.Nombre = categoriaDB.Nombre;
                marca.Descripcion = categoriaDB.Descripcion;
                marca.Estado = categoriaDB.Estado;
                marca.Id = categoriaDB.Id;
                _db.SaveChanges();
            }
        }
    }
}
