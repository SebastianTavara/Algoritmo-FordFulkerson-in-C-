namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Boton modo aleatorio
        private void button1_Click(object sender, EventArgs e)
        {
            ModoAleatorio modoAleatorio = new ModoAleatorio(numericUpDown1.Value.GetHashCode());
            modoAleatorio.ShowDialog();
        }
        // boton modo manual
        private void button2_Click(object sender, EventArgs e)
        {
            ModoManual modoManual = new ModoManual(numericUpDown1.Value.GetHashCode());
            modoManual.ShowDialog();
        }
    }
}
