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


            XmlTextReader xmlReader=new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\OneDrive\\" + "Nodos.xml"); ;

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
            List<Nodo> aux = new List<Nodo>();


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
                    if (Contains(item.Direccion,texto,StringComparison.CurrentCultureIgnoreCase) == false && Contains(item.Descripcion,texto,StringComparison.InvariantCultureIgnoreCase) == false)
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


                if (texto!="")
                foreach (var item3 in item.subnodos)
                {
                    if (Contains(item3, texto, StringComparison.CurrentCultureIgnoreCase) == true)
                        seleccionar = true;


                }

                if (seleccionar)
                    aux.Add(item);
     

            }

            foreach (var item in nodo.Carpetas)
            {
                Boolean seleccionar = true;
                String texto = textBox1.Text.Trim();

                if (texto != "")
                {
                    if (Contains(item.Direccion, texto, StringComparison.CurrentCultureIgnoreCase) == false && Contains(item.Descripcion, texto, StringComparison.InvariantCultureIgnoreCase) == false)
                        seleccionar = false;
                }


                if (categoriasSeleccionadas.Count > 0)
                {
                    Boolean alguna = false;

                    foreach (var item2 in item.categorias)
                    {
                        if (categoriasSeleccionadas.Contains(item2.nombre))
                            alguna = true;
                    }

                    if (alguna == false) seleccionar = false;


                }


                if (texto!="")
                foreach (var item3 in item.subnodos)
                {
                    if (Contains(item3, texto, StringComparison.CurrentCultureIgnoreCase) == true)
                        seleccionar = true;


                }

                if (seleccionar)
                    aux.Add(item);
            }



            dataGridView1.DataSource = aux;
            
                
                    }


        public  bool Contains( string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
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
            StreamWriter objfile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\OneDrive\\" + "Nodos.xml");
            objWriter.Serialize(objfile, nodo);
   objfile.Close();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string resultado = Interaction.InputBox("Introduca url | descripción", "Introducir Pagina", "");

            //if (resultado.Split('|').Length > 1)
            //{
            //    Nodo c = new Nodo();
            //    c.Direccion = resultado.Split('|')[0];
            //    c.Descripcion = resultado.Split('|')[1];


            //    foreach (var item in checkedListBox1.CheckedIndices)
            //        c.categorias.Add(nodo.Categorias[Convert.ToInt16(item)]);


            //    nodo.Paginas.Add(c);


            //}
            //else
            //    MessageBox.Show("Formato no válido!");

            GestionNodo n = new GestionNodo();
            n.Text = "Alta de Enlace";
            n.tipo = "Enlace";
            n.raiz = nodo;

            n.Show();


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
            if (e.RowIndex > -1)
            {
                Process.Start(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            }
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Process.Start(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string resultado = Interaction.InputBox("Introduca carpeta | descripción", "Introducir Carpeta", "");

            //if (resultado.Split('|').Length > 1)
            //{
            //    Nodo c = new Nodo();
            //    c.Direccion = resultado.Split('|')[0];
            //    c.Descripcion = resultado.Split('|')[1];


            //    foreach (var item in checkedListBox1.CheckedIndices)
            //        c.categorias.Add(nodo.Categorias[Convert.ToInt16(item)]);


            //    nodo.Carpetas.Add(c);


            //}
            //else
            //    MessageBox.Show("Formato no válido!");

            GestionNodo n = new GestionNodo();
            n.Text = "Alta de Carpeta";
            n.tipo="Carpeta";
            n.raiz = nodo;


            n.Show();



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex > -1 && e.Button==MouseButtons.Right)
            {


                //abrir ventana

                GestionNodo g = new GestionNodo();
                g.Text = "Gestión de Nodo";
                g.existente = true;
                g.raiz = nodo;


                string buscar = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                foreach (var item in nodo.Carpetas)
                {
                    if (item.Direccion == buscar)
                        g.anterior = (Nodo)item;
                }

                if (g.anterior == null)
                {
                    foreach (var item in nodo.Paginas)
                    {
                        if (item.Direccion == buscar)
                            g.anterior = (Nodo)item;
                    }


                }



                g.Show();



            }

        }
    }
}
