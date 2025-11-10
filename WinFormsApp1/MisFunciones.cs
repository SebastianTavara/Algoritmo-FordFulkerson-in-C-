using System.Drawing.Drawing2D;
using WinFormsApp1;
public class Arista { 
    public int Origen { get; set; }
    public int Destino{ get; set; }
    public int Capacidad {  get; set; }
    public int Flujo {  get; set; }
    public Arista(int origen, int destino, int capacidad)
    {
        this.Origen = origen;
        this.Destino = destino;
        this.Capacidad = capacidad;
        this.Flujo = 0;
    }
}

public class Grafo {
    // num nodos
    public int N { get; private set; }
    //matriz de capacidadades
    public int[,] Capacidad { get; private set; }
    public Grafo(int numNodos) { 
        this.N = numNodos;
        this.Capacidad = new int[N, N];
    }
    public void agregarArista(int u, int v, int capacidad) {
        this.Capacidad[u, v] = capacidad;
    }
    public List<Arista> obtenerAristas() {
    var aristas = new List<Arista>();
        for (int i = 0; i < N; ++i) {
            for (int j = 0; j < N; ++j) {
                if (Capacidad[i, j] > 0) {
                    aristas.Add(new Arista(i, j, Capacidad[i, j]));                }
            }
        }
        return aristas;
    }
}

// ford fulkerson algoritmo usando BFS

public class FordFulkerson {
    private int N;
    public FordFulkerson(int n)
    {
        N = n;
    }

    // s: fuente
    // t: sumidero o vertedero 
    private bool BFS(int[,] Grafo, int s, int t, int[] parent) {
        bool[] visited = new bool[N];
        Queue<int> q = new Queue<int>();
        q.Enqueue(s);
        visited[s] = true;
        parent[s] = -1;

        while (q.Count > 0) {
        int u = q.Dequeue();
            for (int v = 0; v < N; v++) {
                if (!visited[v] && Grafo[u, v] > 0) {
                    q.Enqueue(v);
                    parent[v] = u;
                    visited[v] = true;
                }
            }
        }
        return visited[t];
    }
    public int MaxFlow(Grafo grafo, int s, int t, out int[,] flujoAsignado)
    {
        int[,] rGrafo = (int[,])grafo.Capacidad.Clone();
        int[] parent = new int[N];
        flujoAsignado = new int[N, N];

        int maxFlow = 0;

        while (BFS(rGrafo, s, t, parent)) {
            int pathFlow = int.MaxValue;
            // encontrar el minimo residual en el camino encontrado
            for (int v = t; v != s; v = parent[v]) {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, rGrafo[u, v]);
            }
            // actualizar aristas y flujoo
            for (int v = t; v != s; v = parent[v]) {
                int u = parent[v];
                rGrafo[u, v] -= pathFlow;
                rGrafo[v, u] += pathFlow;
                flujoAsignado[u, v] += pathFlow;
            }

            maxFlow += pathFlow;

        }
        return maxFlow;
    }

    public List<(int u, int v)> MinCut(Grafo grafo, int s)
    {
        // rGrafo: residual actual después de MaxFlowWithSteps
        int[,] rGrafo = (int[,])grafo.Capacidad.Clone();
        int[] parent = new int[N];

        // calcular flujo max
        int[,] flujoAsignado;
        MaxFlow(grafo, s, N - 1, out flujoAsignado); // actualiza residual internamente

        // BFS final para encontrar nodos alcanzables desde s
        bool[] visited = new bool[N];
        Queue<int> q = new Queue<int>();
        q.Enqueue(s);
        visited[s] = true;

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            for (int v = 0; v < N; v++)
            {
                if (!visited[v] && rGrafo[u, v] - flujoAsignado[u, v] > 0)
                {
                    visited[v] = true;
                    q.Enqueue(v);
                }
            }
        }

        // aristas del corte mínimo: de alcanzable a no alcanzable
        List<(int u, int v)> minCut = new List<(int, int)>();
        for (int u = 0; u < N; u++)
        {
            if (!visited[u]) continue; // solo nodos alcanzables
            for (int v = 0; v < N; v++)
            {
                if (!visited[v] && grafo.Capacidad[u, v] > 0)
                {
                    minCut.Add((u, v));
                }
            }
        }

        return minCut;
    }


    public MaxFlowResult MaxFlowWithSteps(Grafo grafo, int s, int t)
    {
        int[,] rGrafo = (int[,])grafo.Capacidad.Clone();
        int[] parent = new int[N];
        int[,] flujoAsignado = new int[N, N];

        var result = new MaxFlowResult { FlujoAsignado = flujoAsignado };
        int maxFlow = 0;

        while (BFS(rGrafo, s, t, parent))
        {
            int pathFlow = int.MaxValue;
            // encuentra minimo residual en el camino
            for (int v = t; v != s; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, rGrafo[u, v]);
            }

            var path = new List<int>();
            for (int v = t; v != s; v = parent[v])
            {
                path.Insert(0, v);
            }
            path.Insert(0, s);

            // aplicar flujo
            for (int v = t; v != s; v = parent[v])
            {
                int u = parent[v];
                rGrafo[u, v] -= pathFlow;
                rGrafo[v, u] += pathFlow;
                flujoAsignado[u, v] += pathFlow;
            }

            maxFlow += pathFlow;

            var step = new StepInfo
            {
                Path = new List<int>(path),
                PathFlow = pathFlow,
                ResidualSnapshot = (int[,])rGrafo.Clone(),
                FlowSnapshot = (int[,])flujoAsignado.Clone()
            };
            result.Steps.Add(step);
        }

        result.MaxFlow = maxFlow;
        return result;
    }

  

}

public class Funciones {

    // NVertices = Numero de nodos
    public Grafo MatrizAGrafo(int[,] Matriz, int NVertices) {

        Grafo grafo = new Grafo(NVertices);

        for (int i = 0; i < NVertices; ++i) {
            for (int j = 0; j < NVertices; ++j) {
                if (Matriz[i, j] != 0) { 
                grafo.agregarArista(i, j, Matriz[i, j]);
                }
            }
        }
        return grafo;
    }

}

public class StepInfo
{
    public List<int> Path { get; set; } = new List<int>();
    public int PathFlow { get; set; }
    public int[,] ResidualSnapshot { get; set; }
    public int[,] FlowSnapshot { get; set; }
}

public class MaxFlowResult
{
    public int MaxFlow { get; set; }
    public int[,] FlujoAsignado { get; set; }
    public List<StepInfo> Steps { get; set; } = new List<StepInfo>();
}

