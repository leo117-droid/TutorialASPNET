﻿using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
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
            var categoriaDB = _db.Categorias.FirstOrDefault(b => b.Id == categoria.Id);
            if (categoriaDB != null)
            {
                categoria.Nombre = categoriaDB.Nombre;
                categoria.Descripcion = categoriaDB.Descripcion;
                categoria.Estado = categoriaDB.Estado;
                categoria.Id = categoriaDB.Id;
                _db.SaveChanges();
            }
        }
    }
}
