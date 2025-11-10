namespace WinFormsApp1
{
    partial class ModoAleatorio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private List<string> pasosFF = new List<string>();

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModoAleatorio));
            dvg1 = new DataGridView();
            btnMFlujo = new Button();
            btnAleatorio = new Button();
            label1 = new Label();
            panel1 = new Panel();
            txtbMFlujo = new TextBox();
            label2 = new Label();
            cmbFuente = new ComboBox();
            label3 = new Label();
            notifyIcon1 = new NotifyIcon(components);
            label4 = new Label();
            cmbSumidero = new ComboBox();
            btnSgte = new Button();
            btnAnt = new Button();
            btnCortmin = new Button();
            txtbCorteMinimo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dvg1).BeginInit();
            SuspendLayout();
            // 
            // dvg1
            // 
            dvg1.AllowUserToAddRows = false;
            dvg1.AllowUserToDeleteRows = false;
            dvg1.AllowUserToResizeColumns = false;
            dvg1.AllowUserToResizeRows = false;
            dvg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvg1.Location = new Point(664, 10);
            dvg1.Margin = new Padding(3, 2, 3, 2);
            dvg1.Name = "dvg1";
            dvg1.ReadOnly = true;
            dvg1.RowHeadersWidth = 51;
            dvg1.Size = new Size(416, 297);
            dvg1.TabIndex = 0;
            // 
            // btnMFlujo
            // 
            btnMFlujo.Location = new Point(719, 321);
            btnMFlujo.Name = "btnMFlujo";
            btnMFlujo.Size = new Size(138, 51);
            btnMFlujo.TabIndex = 1;
            btnMFlujo.Text = "Encontrar el maximo flujo";
            btnMFlujo.UseVisualStyleBackColor = true;
            btnMFlujo.Click += btnMFlujo_Click;
            // 
            // btnAleatorio
            // 
            btnAleatorio.Location = new Point(893, 321);
            btnAleatorio.Name = "btnAleatorio";
            btnAleatorio.Size = new Size(138, 51);
            btnAleatorio.TabIndex = 2;
            btnAleatorio.Text = "Aleatorizar nuevamente";
            btnAleatorio.UseVisualStyleBackColor = true;
            btnAleatorio.Click += btnAleatorio_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(211, 218, 217);
            label1.Location = new Point(737, 458);
            label1.Name = "label1";
            label1.Size = new Size(125, 16);
            label1.TabIndex = 3;
            label1.Text = "El maximo flujo es:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(211, 218, 217);
            panel1.Location = new Point(21, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(627, 297);
            panel1.TabIndex = 4;
            // 
            // txtbMFlujo
            // 
            txtbMFlujo.Location = new Point(868, 456);
            txtbMFlujo.Name = "txtbMFlujo";
            txtbMFlujo.ReadOnly = true;
            txtbMFlujo.Size = new Size(100, 23);
            txtbMFlujo.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(211, 218, 217);
            label2.Location = new Point(624, 389);
            label2.Name = "label2";
            label2.Size = new Size(91, 16);
            label2.TabIndex = 6;
            label2.Text = "Nodo Fuente:";
            // 
            // cmbFuente
            // 
            cmbFuente.FormattingEnabled = true;
            cmbFuente.Location = new Point(719, 384);
            cmbFuente.Name = "cmbFuente";
            cmbFuente.Size = new Size(121, 23);
            cmbFuente.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(211, 218, 217);
            label3.Location = new Point(737, 425);
            label3.Name = "label3";
            label3.Size = new Size(258, 16);
            label3.TabIndex = 9;
            label3.Text = "El nodo Sumidero siempre sera el ultimo vertice";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(211, 218, 217);
            label4.Location = new Point(849, 388);
            label4.Name = "label4";
            label4.Size = new Size(108, 16);
            label4.TabIndex = 10;
            label4.Text = "Nodo Sumidero:";
            // 
            // cmbSumidero
            // 
            cmbSumidero.FormattingEnabled = true;
            cmbSumidero.Location = new Point(966, 384);
            cmbSumidero.Name = "cmbSumidero";
            cmbSumidero.Size = new Size(121, 23);
            cmbSumidero.TabIndex = 8;
            // 
            // btnSgte
            // 
            btnSgte.Location = new Point(414, 375);
            btnSgte.Name = "btnSgte";
            btnSgte.Size = new Size(75, 23);
            btnSgte.TabIndex = 11;
            btnSgte.Text = "Siguiente";
            btnSgte.UseVisualStyleBackColor = true;
            btnSgte.Click += btnSgte_Click;
            // 
            // btnAnt
            // 
            btnAnt.Location = new Point(278, 375);
            btnAnt.Name = "btnAnt";
            btnAnt.Size = new Size(75, 23);
            btnAnt.TabIndex = 12;
            btnAnt.Text = "Anterior";
            btnAnt.UseVisualStyleBackColor = true;
            btnAnt.Click += btnAnt_Click;
            // 
            // btnCortmin
            // 
            btnCortmin.Location = new Point(101, 375);
            btnCortmin.Name = "btnCortmin";
            btnCortmin.Size = new Size(100, 32);
            btnCortmin.TabIndex = 13;
            btnCortmin.Text = "Corte minimo";
            btnCortmin.UseVisualStyleBackColor = true;
            btnCortmin.Click += btnCortmin_Click;
            // 
            // txtbCorteMinimo
            // 
            txtbCorteMinimo.Location = new Point(76, 425);
            txtbCorteMinimo.Multiline = true;
            txtbCorteMinimo.Name = "txtbCorteMinimo";
            txtbCorteMinimo.Size = new Size(146, 36);
            txtbCorteMinimo.TabIndex = 14;
            // 
            // ModoAleatorio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(55, 53, 62);
            ClientSize = new Size(1105, 500);
            Controls.Add(txtbCorteMinimo);
            Controls.Add(btnCortmin);
            Controls.Add(btnAnt);
            Controls.Add(btnSgte);
            Controls.Add(cmbSumidero);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbFuente);
            Controls.Add(label2);
            Controls.Add(txtbMFlujo);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(btnAleatorio);
            Controls.Add(btnMFlujo);
            Controls.Add(dvg1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "ModoAleatorio";
            Text = "ModoAleatorio";
            Load += ModoAleatorio_Load;
            ((System.ComponentModel.ISupportInitialize)dvg1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dvg1;
        // genera la tabla con n columnas y filas
        // n: vertices
        private void GenerarTabla(int n)
        {
            dvg1.Columns.Clear();
            dvg1.Rows.Clear();
            dvg1.AllowUserToAddRows = false;
            dvg1.AllowUserToResizeColumns = false;
            dvg1.AllowUserToResizeRows = false;
            dvg1.RowHeadersWidth = 60;

            // crear columnas
            for (int j = 0; j < n; j++)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = j.ToString();
                col.Width = 50;
                dvg1.Columns.Add(col);
            }

            // crear filas
            for (int i = 0; i < n; i++)
            {
                dvg1.Rows.Add();
                dvg1.Rows[i].HeaderCell.Value = i.ToString();
            }
        }
        private Random x = new Random();
        private int[,] GenerarGrafoAleatorio(int nVertices, int probabilidad = 50, int capacidadMax = 20)
        {
            int[,] capacidad = new int[nVertices, nVertices];

            for (int i = 0; i < nVertices; ++i) {
                for (int j = 0; j < nVertices; ++j) {
                    if (i == j) continue; // no hay lazos 1->1 x
                    if (i == nVertices - 1) { 
                        capacidad[i, j] = 0;
                        break;
                    }
                    if (x.Next(100) < probabilidad)
                    {
                        capacidad[i, j] = x.Next(1, capacidadMax + 1);
                    }
                    else {
                        capacidad[i, j] = 0; // sin lazo
                    }
                }
            }
            return capacidad;
        }

        private Button btnMFlujo;
        private Button btnAleatorio;
        private Label label1;
        private Panel panel1;
        private TextBox txtbMFlujo;
        private Label label2;
        private ComboBox cmbFuente;
        private Label label3;
        private NotifyIcon notifyIcon1;
        private Label label4;
        private ComboBox cmbSumidero;
        private Button btnSgte;
        private Button btnAnt;
        private Button btnCortmin;
        private TextBox txtbCorteMinimo;
    }
}