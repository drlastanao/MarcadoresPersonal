using System;
using System.Collections.Generic;

namespace MarcadoresPersonal
{
    [Serializable()]

    public class NodoCentral 
    {

        public List<Categoria> Categorias { get; set; }
        public List<Nodo> Paginas { get; set; }
        public List<Nodo> Carpetas { get; set; }

        public NodoCentral()
        {
            Categorias = new List<Categoria>();
            Paginas = new List<Nodo>();

        }

    }
}
