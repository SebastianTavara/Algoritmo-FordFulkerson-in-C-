using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
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
            this.MatrizActual = new int[vertices, vertices];
            this.viewer = new GViewer();
            viewer.Dock = DockStyle.Fill;
            panel1.Controls.Add(viewer);
        }

        public int vertices;
        private int[,] MatrizActual;
        private GViewer viewer;
        private MaxFlowResult ultimoResultado = null;
        private int pasoActual = 0;
        private void Cmbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DibujarGrafo();
        }

        private void ModoManual_Load(object sender, EventArgs e)
        {
            GenerarTabla(vertices);

            cmbFuente.Items.Clear();
            cmbSumidero.Items.Clear();

            for (int i = 0; i < vertices; ++i)
            {
                cmbFuente.Items.Add(i.ToString());
                cmbSumidero.Items.Add(i.ToString());
            }
            cmbFuente.SelectedIndex = 0;
            cmbSumidero.SelectedIndex = Math.Max(0, vertices - 1);

            cmbFuente.SelectedIndexChanged += (s, e) => DibujarGrafo();
            cmbSumidero.SelectedIndexChanged += (s, e) => DibujarGrafo();

            // por default la fuente en 0
            cmbFuente.SelectedIndex = 0;

        }

        private void btnMFlujo_Click(object sender, EventArgs e)
        {
            // verifica si fuente != sumidero
            int verticeFuente = Convert.ToInt32(cmbFuente.SelectedItem);
            int verticeSumidero = Convert.ToInt32(cmbSumidero.SelectedItem);

            if (verticeFuente == verticeSumidero)
            {
                NotificacionFuenteIncorrecta();
                return;
            }

            FordFulkerson Fd = new FordFulkerson(vertices);
            Grafo grafo = new Grafo(vertices);

            // leer DataGridView y llenar grafo y MatrizActual
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    int val = Convert.ToInt32(dvg1.Rows[i].Cells[j].Value);
                    if (val != 0)
                    {
                        grafo.agregarArista(i, j, val);
                        MatrizActual[i, j] = val;
                    }
                }
            }

            // aquí se genera el resultado con pasos
            ultimoResultado = Fd.MaxFlowWithSteps(grafo, verticeFuente, verticeSumidero);

            txtbMFlujo.Text = ultimoResultado.MaxFlow.ToString();

            pasoActual = 0;

            // dibujar primer paso
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
                else
                {
                    // opcional: log / debug
                    // Console.WriteLine($"Nodo fuente '{fuenteText}' no encontrado en el grafo.");
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
                else
                {
                    // Console.WriteLine($"Nodo sumidero '{sumideroText}' no encontrado en el grafo.");
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

        private void btnAnte_Click(object sender, EventArgs e)
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

            // matriz residual final = capacidades originales - flujo asignado
            int[,] residual = new int[vertices, vertices];
            for (int i = 0; i < vertices; i++)
                for (int j = 0; j < vertices; j++)
                    residual[i, j] = grafo.Capacidad[i, j] - resultado.FlujoAsignado[i, j];

            // DFS desde la fuente para identificar nodos alcanzables
            bool[] visitado = new bool[vertices];
            DFS(residual, fuente, visitado);

            txtbCorteMinimo.Clear();
            int sumaCapacidades = 0;

            // recorrer todas las aristas y mostrar solo las del corte mínimo
            for (int u = 0; u < vertices; u++)
            {
                for (int v = 0; v < vertices; v++)
                {
                    if (visitado[u] && !visitado[v] && grafo.Capacidad[u, v] > 0)
                    {
                        txtbCorteMinimo.AppendText($"Arista: {u} -> {v} Capacidad: {grafo.Capacidad[u, v]}\r\n");
                        sumaCapacidades += grafo.Capacidad[u, v];
                    }
                }
            }

            txtbCorteMinimo.AppendText($"\r\nCapacidad total {sumaCapacidades}");
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
