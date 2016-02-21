using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace MarcadoresPersonal
{
    public partial class Principal : Form
    {
        public   NodoCentral nodo = new NodoCentral();

        public Principal()
        {
            InitializeComponent();



            //borrar luego
            //Categoria c1 = new Categoria();
            //c1.nombre = "buscadores";
            //c1.descripcion = "buscadores de páginas";

            //Categoria c2 = new Categoria();
            //c2.nombre = "programación";
            //c2.descripcion = "páginas de programación";


            //Pagina p1 = new Pagina();
            //p1.Url = "www.google.es";
            //p1.categorias.Add(c1);
            //p1.Descripcion = "el mejor buscador";


            //Pagina p2 = new Pagina();
            //p2.Url = "www.udacity.com";
            //p2.categorias.Add(c2);
            //p2.Descripcion = "buenos cursos";





            //nodo.Categorias.Add(c1);
            //nodo.Categorias.Add(c2);

            //nodo.Paginas.Add(p1);
            //nodo.Paginas.Add(p2);


            XmlTextReader xmlReader=new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "Nodos.xml"); ;

            try
            {
                XmlSerializer objreader = new XmlSerializer(nodo.GetType());
                nodo = (NodoCentral)objreader.Deserialize(xmlReader);
     

             }
            catch (Exception e)
            {

            }
            finally
            {
                xmlReader.Close();
            }




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


            foreach (var item in nodo.Paginas)
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
            ActualizarCheckBoxList();

        }

        private void ActualizarCheckBoxList()
        {
            checkedListBox1.Items.Clear();
            List<String> cat = new List<String>();
            foreach (var item in nodo.Categorias)
            {
                cat.Add(item.nombre);
            }

            checkedListBox1.Items.AddRange(cat.ToArray());
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {

            XmlSerializer objWriter = new XmlSerializer(nodo.GetType());
   StreamWriter objfile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "Nodos.xml");
            objWriter.Serialize(objfile, nodo);
   objfile.Close();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Introduca url | descripción", "Introducir Pagina", "");

            if (resultado.Split('|').Length > 1)
            {
                Pagina c = new Pagina();
                c.Url = resultado.Split('|')[0];
                c.Descripcion = resultado.Split('|')[1];


                foreach (var item in checkedListBox1.CheckedIndices)
                    c.categorias.Add(nodo.Categorias[Convert.ToInt16(item)]);
                

                nodo.Paginas.Add(c);


            }
            else
                MessageBox.Show("Formato no válido!");



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string resultado= Interaction.InputBox("Introduca categoria | descripción", "Introducir Categoria", "");

            if (resultado.Split('|').Length > 1)
            {
                Categoria c = new Categoria();
                c.nombre = resultado.Split('|')[0];
                c.descripcion = resultado.Split('|')[1];

                nodo.Categorias.Add(c);


                ActualizarCheckBoxList();
            }
            else
                MessageBox.Show("Formato no válido!");

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                Process.Start(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            }
        }
    }
}
