using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MarcadoresPersonal
{
    [Serializable()]
    public class Categoria 
    {
        public String nombre { get; set; }
        public String descripcion { get; set; }
    }
}
