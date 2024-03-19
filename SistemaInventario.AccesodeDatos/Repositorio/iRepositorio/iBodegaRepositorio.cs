using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesodeDatos.Repositorio.iRepositorio
{
    public interface iBodegaRepositorio : iRepositorio<Bodega>
    {
        void actualizar(Bodega bodega);
        Task<Bodega> Obtener(int v);
    }
}
