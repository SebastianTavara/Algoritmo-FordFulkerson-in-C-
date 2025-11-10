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


            cmbFuente.SelectedIndexChanged += (s, e) => DibujarGrafo();
            cmbSumidero.SelectedIndexChanged += (s, e) => DibujarGrafo();

            // notificaicoon
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "FF";

            notifyIcon1.Icon = SystemIcons.Application;


        }
        private void Cmbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DibujarGrafo();
        }
        private void ModoAleatorio_Load(object sender, EventArgs e)
        {
            // limpia la tabla
            GenerarTabla(vertices);

            // generar combo box de fuente
            cmbFuente.Items.Clear();
            // generar combo box de sumidero
            cmbSumidero.Items.Clear();

            for (int i = 0; i < vertices; ++i)
            {
                cmbFuente.Items.Add(i.ToString());
                cmbSumidero.Items.Add(i.ToString());

            }

            //por default
            cmbFuente.SelectedIndex = 0;
            cmbSumidero.SelectedIndex = Math.Max(0, vertices - 1);

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


        private MaxFlowResult ultimoResultado = null;
        private int pasoActual = 0;


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
            int fuente = Convert.ToInt32(cmbFuente.SelectedItem.ToString());
            int sumidero = Convert.ToInt32(cmbSumidero.SelectedItem.ToString());

            if (fuente == sumidero)
            {
                NotificacionFuenteIncorrecta();
                return;
            }

            FordFulkerson Fd = new FordFulkerson(vertices);
            Funciones fn = new Funciones();

            Grafo grafo = fn.MatrizAGrafo(MatrizActual, vertices);

            // Guardar resultado completo (paso a paso)
            ultimoResultado = Fd.MaxFlowWithSteps(grafo, fuente, sumidero);

            txtbMFlujo.Text = ultimoResultado.MaxFlow.ToString();

            // Reiniciar a primer paso
            pasoActual = 0;

            // Dibujar primer snapshot
            DibujarGrafoConFlujo(ultimoResultado.Steps[pasoActual].FlowSnapshot);
        }

        private void DibujarGrafoConFlujo(int[,] flujo)
        {
            Graph graph = new Graph("flujo");
            graph.Attr.LayerDirection = LayerDirection.LR;

            for (int i = 0; i < vertices; i++)
            {
                graph.AddNode(i.ToString());

                for (int j = 0; j < vertices; j++)
                {
                    if (MatrizActual[i, j] > 0)
                    {
                        Edge edge = graph.AddEdge(i.ToString(), j.ToString());
                        int cap = MatrizActual[i, j];
                        int fl = flujo[i, j];

                        edge.LabelText = $"{fl}/{cap}";
                        edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                    }
                }
            }

            // colorear fuente y sumidero
            graph.FindNode(cmbFuente.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode(cmbSumidero.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;

            viewer.Graph = graph;
        }


        private void DibujarGrafo()
        {
            Graph graph = new Graph("grafo");
            graph.Attr.LayerDirection = LayerDirection.LR;

            // agregar nodos y aristas
            for (int i = 0; i < vertices; ++i)
            {
                var node = graph.AddNode(i.ToString());
                // color por defecto transparente
                node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;

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

            // obtener valores seguros de los combobox
            string fuenteText = (cmbFuente.SelectedItem ?? cmbFuente.Text ?? "").ToString().Trim();
            string sumideroText = (cmbSumidero.SelectedItem ?? cmbSumidero.Text ?? "").ToString().Trim();

            // colorear fuente si existe
            if (!string.IsNullOrEmpty(fuenteText))
            {
                var nodeF = graph.FindNode(fuenteText);
                if (nodeF != null)
                {
                    nodeF.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                }

            }

            // colorear sumidero si existe (y distinto)
            if (!string.IsNullOrEmpty(sumideroText) && sumideroText != fuenteText)
            {
                var nodeS = graph.FindNode(sumideroText);
                if (nodeS != null)
                {
                    nodeS.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                }
            }

            viewer.Graph = graph;
        }

        private void NotificacionFuenteIncorrecta()
        {
            notifyIcon1.BalloonTipTitle = "Acción no permitida";
            notifyIcon1.BalloonTipText = "Está usando el mismo vértice de origen y destino. Por favor, corríjalo para continuar.";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;

            notifyIcon1.ShowBalloonTip(5000);
        }

        private void btnSgte_Click(object sender, EventArgs e)
        {
            if (ultimoResultado == null) return;

            if (pasoActual < ultimoResultado.Steps.Count - 1)
                pasoActual++;

            DibujarGrafoConFlujo(ultimoResultado.Steps[pasoActual].FlowSnapshot);
        }

        private void btnAnt_Click(object sender, EventArgs e)
        {
            if (ultimoResultado == null) return;

            if (pasoActual > 0)
                pasoActual--;

            DibujarGrafoConFlujo(ultimoResultado.Steps[pasoActual].FlowSnapshot);
        }

        private void btnCortmin_Click(object sender, EventArgs e)
        {
            int fuente = Convert.ToInt32(cmbFuente.SelectedItem);
            int sumidero = Convert.ToInt32(cmbSumidero.SelectedItem);

            FordFulkerson ff = new FordFulkerson(vertices);
            Grafo grafo = new Grafo(vertices);

            // llenar grafo con matriz actual
            for (int i = 0; i < vertices; i++)
                for (int j = 0; j < vertices; j++)
                    if (MatrizActual[i, j] > 0)
                        grafo.agregarArista(i, j, MatrizActual[i, j]);

            // calcular flujo máximo con pasos
            MaxFlowResult resultado = ff.MaxFlowWithSteps(grafo, fuente, sumidero);

            // obtener corte mínimo
            bool[] visitado = new bool[vertices];
            DFS(grafo.Capacidad, fuente, visitado);

            // mostrar aristas del corte mínimo en txtbCorteMinimo
            txtbCorteMinimo.Clear();
            for (int u = 0; u < vertices; u++)
            {
                for (int v = 0; v < vertices; v++)
                {
                    if (visitado[u] && !visitado[v] && grafo.Capacidad[u, v] > 0)
                    {
                        txtbCorteMinimo.AppendText($"Arista: {u} -> {v} Capacidad: {grafo.Capacidad[u, v]}\r\n");
                    }
                }
            }
        }
        private void DFS(int[,] residual, int u, bool[] visitado)
        {
            visitado[u] = true;
            for (int v = 0; v < vertices; v++)
            {
                if (residual[u, v] > 0 && !visitado[v])
                    DFS(residual, v, visitado);
            }
        }
    }

}
