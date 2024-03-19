using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesodeDatos.Repositorio.iRepositorio
{
    public interface iProductoRepositorio : iRepositorio<Producto>
    {
        void actualizar(Producto producto);

        IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
    }
}
