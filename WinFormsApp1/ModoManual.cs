using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ModoManual : Form
    {
        public ModoManual(int vertices)
        {
            InitializeComponent();
            this.vertices = vertices;
        }

        public int vertices;
        private int[,] MatrizActual;

        private void ModoManual_Load(object sender, EventArgs e)
        {
            GenerarTabla(vertices);

            cmbFuente.Items.Clear();

            for (int i = 0; i < vertices; ++i)
            {
                cmbFuente.Items.Add(i.ToString());
            }
            // por default la fuente en 0
            cmbFuente.SelectedIndex = 0;
        }

        private void btnMFlujo_Click(object sender, EventArgs e)
        {
            FordFulkerson Fd = new FordFulkerson(vertices);
            Funciones fn = new Funciones();

            int verticeFuente = Convert.ToInt32(cmbFuente.SelectedItem.ToString());
            Grafo grafo = new Grafo(vertices);

            // lee el dataGriedView
            for (int i = 0; i < vertices; ++i) {
                for (int j = 0; j < vertices; ++j) {
                    int valor = Convert.ToInt32(dvg1.Rows[i].Cells[j].Value);
                    if (valor != 0) {
                        grafo.agregarArista(i, j, valor);
                    }
                }
            }

            int maxflow = Fd.MaxFlow(grafo, verticeFuente, vertices - 1, out int[,] FlujoAsignado);

            txtbMFlujo.Text = maxflow.ToString();

        }
    }
}
