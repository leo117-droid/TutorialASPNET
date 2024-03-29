﻿using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
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
        public iCategoriaRepositorio Categoria { get; private set; }

        public iMarcaRepositorio Marca { get; private set; }
        public iProductoRepositorio Producto { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);

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
