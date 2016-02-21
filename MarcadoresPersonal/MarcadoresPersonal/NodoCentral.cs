using System;
using System.Collections.Generic;

namespace MarcadoresPersonal
{
    [Serializable()]

    public class NodoCentral 
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
