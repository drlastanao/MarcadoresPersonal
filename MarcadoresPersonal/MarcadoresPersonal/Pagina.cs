using System;
using System.Collections.Generic;

namespace MarcadoresPersonal
{ 
    [Serializable()]
    public class Pagina 
    {


        public String Url { get; set; }
        public String Descripcion { get; set; }
        public List<Categoria> categorias { get; set; }

        public Pagina()
        {
            categorias = new List<Categoria>();

        }
    }
}
