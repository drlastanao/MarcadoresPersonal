using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarcadoresPersonal
{
    public partial class GestionNodo : Form
    {
        public Boolean existente = false;
        public Nodo anterior;
        public NodoCentral raiz;
        public string tipo;





        public GestionNodo()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void GestionNodo_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            List<String> cat = new List<String>();
            foreach (var item in raiz.Categorias)
            {
                cat.Add(item.nombre);
            }

            checkedListBox1.Items.AddRange(cat.ToArray());



            if (anterior!=null)
            {
                textBox1.Text = anterior.Direccion;
                textBox2.Text = anterior.Descripcion;
                int g = 0;
                List<int> seleccionar = new List<int>();
                foreach (var item in anterior.categorias)
                {
                    string texto = item.nombre;

                    g = 0;
                    foreach (var item2 in checkedListBox1.Items)
                    {
                        if (texto == item2.ToString())
                            seleccionar.Add(g);
                        g++;
                    }
                }

                foreach (var item3 in seleccionar)
                    checkedListBox1.SetItemChecked(item3, true);



                foreach (var item in checkedListBox1.Items)
                {
                    if (anterior.categorias.Contains(item))
                        checkedListBox1.SelectedItems.Add(g);

                    g++;

                }

                foreach (var item in anterior.subnodos)
                    listBox1.Items.Add(item);

            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            verificarTodoCorrecto();

        }


        public void verificarTodoCorrecto()
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && checkedListBox1.CheckedIndices.Count > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Introduca nodo de tipo " + tipo, "Introducir Subnodo", "");

            if (resultado.Trim() != "")
            {
                listBox1.Items.Add(resultado);
                verificarTodoCorrecto();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            verificarTodoCorrecto();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nodo aux;

            if (existente == false)
                aux = new Nodo();
            else
                aux = anterior;




            aux.Direccion = textBox1.Text;
            aux.Descripcion = textBox2.Text;

            aux.categorias.Clear();
            aux.subnodos.Clear();



            foreach (var item in checkedListBox1.CheckedIndices)
                aux.categorias.Add(raiz.Categorias[Convert.ToInt16(item)]);

            foreach (var item in listBox1.Items)
                aux.subnodos.Add(item.ToString());

            if (anterior == null)
            {
                if (tipo == "Carpeta")
                    raiz.Carpetas.Add(aux);
                else
                    raiz.Paginas.Add(aux);
            }

            Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            verificarTodoCorrecto();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            verificarTodoCorrecto();
        }
    }

    }
