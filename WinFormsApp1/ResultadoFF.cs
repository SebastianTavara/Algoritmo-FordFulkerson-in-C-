using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class ResultadoFF
    {
        public int MaxFlow { get; set; }
        public List<PasoFF> Steps { get; set; }

        public ResultadoFF()
        {
            Steps = new List<PasoFF>();
        }
    }


    public class PasoFF
    {
        public int[,] FlowSnapshot { get; set; }

        public PasoFF(int[,] snapshot)
        {
            FlowSnapshot = snapshot;
        }

    }
}
