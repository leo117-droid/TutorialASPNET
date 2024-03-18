using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesodeDatos.Repositorio.iRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TutorialASPNET.SistemaInventario.AccesodeDatos.Data;

namespace SistemaInventario.AccesodeDatos.Repositorio
{
    public class Repositorio<T> : iRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); //insert into Table
        }

        public async Task<T> obtener(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isTracking = true,
            string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); //select * from where
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //Ejemplo categoría, marca, etc
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isTracking = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if(filtro != null) 
            {
                query = query.Where(filtro); //select * from where
            }
            if(incluirPropiedades != null) 
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //Ejemplo categoría, marca, etc
                }
            }
            if (orderBy != null) 
            {
                query = orderBy(query);
            }
            if (!isTracking) 
            {
                query = query.AsNoTracking(); 
            }

            return await query.ToListAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
