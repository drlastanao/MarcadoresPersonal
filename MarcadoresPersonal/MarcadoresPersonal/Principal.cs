using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace MarcadoresPersonal
{
    public partial class Principal : Form
    {
        private NodoCentral n = new NodoCentral();

        public Principal()
        {
            InitializeComponent();


            //borrar luego
            Categoria c1 = new Categoria();
            c1.nombre = "buscadores";
            c1.descripcion = "buscadores de páginas";

            Categoria c2 = new Categoria();
            c2.nombre = "programación";
            c2.descripcion = "páginas de programación";


            Pagina p1 = new Pagina();
            p1.Url = "www.google.es";
            p1.categorias.Add(c1);
            p1.Descripcion = "el mejor buscador";


            Pagina p2 = new Pagina();
            p2.Url = "www.udacity.com";
            p2.categorias.Add(c2);
            p2.Descripcion = "buenos cursos";





            n.Categorias.Add(c1);
            n.Categorias.Add(c2);

            n.Paginas.Add(p1);
            n.Paginas.Add(p2);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Pagina> aux = new List<Pagina>();


            List<String> categoriasSeleccionadas = new List<String>();

            int g = 0;
            foreach (var item in checkedListBox1.CheckedIndices)
            {

                categoriasSeleccionadas.Add(checkedListBox1.Items[Convert.ToInt16(item)].ToString());
            }


            foreach (var item in n.Paginas)
            {
                Boolean seleccionar = true;
                String texto=textBox1.Text.Trim() ;

                if (texto!="")
                {
                    if (item.Url.Contains(texto) == false && item.Descripcion.Contains(texto) == false)
                        seleccionar = false;
                 }


                if (categoriasSeleccionadas.Count>0)
                {
                    Boolean alguna = false;

                    foreach (var item2 in item.categorias)
                    {
                        if (categoriasSeleccionadas.Contains(item2.nombre))
                            alguna = true;
                    }

                    if (alguna == false) seleccionar = false;


                }


                if (seleccionar)

                    aux.Add(item);                
            }




            dataGridView1.DataSource = aux;
            
                
                    }

        private void Principal_Load(object sender, EventArgs e)
        {
            List<String> cat = new List<String>();
            foreach (var item in n.Categorias)
            {
                cat.Add(item.nombre);
            }

            checkedListBox1.Items.AddRange(cat.ToArray());


        }
    }
}
