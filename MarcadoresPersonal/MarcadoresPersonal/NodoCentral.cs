using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcadoresPersonal
{
    class NodoCentral
    {

        public List<Categoria> Categorias { get; set; }
        public List<Pagina> Paginas { get; set; }

        public NodoCentral()
        {
            Categorias = new List<Categoria>();
            Paginas = new List<Pagina>();

        }

    }
}
