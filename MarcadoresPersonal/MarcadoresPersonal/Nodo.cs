using System;
using System.Collections.Generic;

namespace MarcadoresPersonal
{ 
    [Serializable()]
    public class Nodo 
    {


        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        
        public List<Categoria> categorias { get; set; }
        public List<string> subnodos { get; set; }

        public Nodo()
        {
            categorias = new List<Categoria>();
            subnodos = new List<string>();

        }
    }
}
