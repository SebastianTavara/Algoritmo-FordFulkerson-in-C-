using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WinFormsApp1
{
    public partial class ModoAleatorio : Form
    {
        // n: vertices del grafo
        public ModoAleatorio(int vertices)
        {
            InitializeComponent();
            this.vertices = vertices;
            this.MatrizActual = new int[vertices, vertices];
            this.viewer = new GViewer();
            viewer.Dock = DockStyle.Fill;
            panel1.Controls.Add(viewer);

            // notificaicoon
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "FF";

            notifyIcon1.Icon = SystemIcons.Application;
        }

        private void ModoAleatorio_Load(object sender, EventArgs e)
        {
            // limpia la tabla
            GenerarTabla(vertices);

            // generar combo box de fuente
            cmbFuente.Items.Clear();
            for (int i = 0; i < vertices; ++i)
            {
                cmbFuente.Items.Add(i.ToString());
            }

            //por default
            cmbFuente.SelectedIndex = 0;

            // grafo aleatorio
            MatrizActual = GenerarGrafoAleatorio(vertices, 40);

            // llena tabla de datos
            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    dvg1.Rows[i].Cells[j].Value = MatrizActual[i, j];
                }
            }
            DibujarGrafo();
        }
        // vertices del grafo
        private int vertices = 0;
        private int[,] MatrizActual;
        // para el grafo dibujador // no importante
        private GViewer viewer;

        // boton para aleatorizar nuevamente
        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            // grafo aleatorio
            MatrizActual = GenerarGrafoAleatorio(vertices, 40);
            // llena tabla de datos
            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    dvg1.Rows[i].Cells[j].Value = MatrizActual[i, j];
                }
            }
            DibujarGrafo();
        }
        // boton del maximo flujo
        private void btnMFlujo_Click(object sender, EventArgs e)
        {
            //verifica si la fuente elegida no es la misma el que el vertedero
            if (Convert.ToInt32(cmbFuente.SelectedItem.ToString()) == vertices - 1)
            {
                // noti
                NotificacionFuenteIncorrecta();
                return;
            }

            // FF
            FordFulkerson Fd = new FordFulkerson(vertices);
            Funciones fn = new Funciones();

            int verticeFuente = Convert.ToInt32(cmbFuente.SelectedItem.ToString());
            Grafo grafo = fn.MatrizAGrafo(MatrizActual, vertices);

            int maxFlow = Fd.MaxFlow(grafo, verticeFuente, vertices - 1, out int[,] flujoAsignado);

            txtbMFlujo.Text = maxFlow.ToString();

        }


        private void DibujarGrafo()
        {

            Graph graph = new Graph("grafo");
            graph.Attr.LayerDirection = LayerDirection.LR;

            // agregar nodos y aristas
            for (int i = 0; i < vertices; ++i)
            {
                graph.AddNode(i.ToString());
                for (int j = 0; j < vertices; ++j)
                {
                    if (MatrizActual[i, j] > 0)
                    {
                        Edge edge = graph.AddEdge(i.ToString(), j.ToString());
                        edge.LabelText = MatrizActual[i, j].ToString();
                        edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                    }
                }
            }
            // color nodo inicio
            graph.FindNode(cmbFuente.Text.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            // ultimo nodo
            graph.FindNode((vertices - 1).ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;

            viewer.Graph = graph;

        }
        private void NotificacionFuenteIncorrecta()
        {
            notifyIcon1.BalloonTipTitle = "Acción no permitida";
            notifyIcon1.BalloonTipText = "Está usando el mismo vértice de origen y destino. Por favor, corríjalo para continuar.";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;

            notifyIcon1.ShowBalloonTip(5000);
        }

    }

}
