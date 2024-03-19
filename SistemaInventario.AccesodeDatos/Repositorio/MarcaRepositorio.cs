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
            var marcaDB = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id);
            if (marcaDB != null)
            {
                marca.Nombre = marcaDB.Nombre;
                marca.Descripcion = marcaDB.Descripcion;
                marca.Estado = marcaDB.Estado;
                marca.Id = marcaDB.Id;
                _db.SaveChanges();
            }
        }
    }
}
