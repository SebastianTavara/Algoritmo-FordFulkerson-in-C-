namespace WinFormsApp1
{
    partial class ModoManual
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            dvg1 = new DataGridView();
            panel1 = new Panel();
            btnMFlujo = new Button();
            label3 = new Label();
            cmbFuente = new ComboBox();
            label2 = new Label();
            txtbMFlujo = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dvg1).BeginInit();
            SuspendLayout();
            // 
            // dvg1
            // 
            dvg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvg1.Location = new Point(664, 10);
            dvg1.Name = "dvg1";
            dvg1.Size = new Size(416, 297);
            dvg1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(211, 218, 217);
            panel1.Location = new Point(21, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(627, 297);
            panel1.TabIndex = 5;
            // 
            // btnMFlujo
            // 
            btnMFlujo.Location = new Point(814, 326);
            btnMFlujo.Name = "btnMFlujo";
            btnMFlujo.Size = new Size(138, 51);
            btnMFlujo.TabIndex = 6;
            btnMFlujo.Text = "Encontrar el maximo flujo";
            btnMFlujo.UseVisualStyleBackColor = true;
            btnMFlujo.Click += btnMFlujo_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(211, 218, 217);
            label3.Location = new Point(756, 434);
            label3.Name = "label3";
            label3.Size = new Size(258, 16);
            label3.TabIndex = 14;
            label3.Text = "El nodo Sumidero siempre sera el ultimo vertice";
            // 
            // cmbFuente
            // 
            cmbFuente.FormattingEnabled = true;
            cmbFuente.Location = new Point(875, 393);
            cmbFuente.Name = "cmbFuente";
            cmbFuente.Size = new Size(121, 23);
            cmbFuente.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(211, 218, 217);
            label2.Location = new Point(756, 398);
            label2.Name = "label2";
            label2.Size = new Size(91, 16);
            label2.TabIndex = 12;
            label2.Text = "Nodo Fuente:";
            // 
            // txtbMFlujo
            // 
            txtbMFlujo.Location = new Point(887, 465);
            txtbMFlujo.Name = "txtbMFlujo";
            txtbMFlujo.ReadOnly = true;
            txtbMFlujo.Size = new Size(100, 23);
            txtbMFlujo.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(211, 218, 217);
            label1.Location = new Point(756, 467);
            label1.Name = "label1";
            label1.Size = new Size(125, 16);
            label1.TabIndex = 10;
            label1.Text = "El maximo flujo es:";
            // 
            // ModoManual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(55, 53, 62);
            ClientSize = new Size(1105, 500);
            Controls.Add(label3);
            Controls.Add(cmbFuente);
            Controls.Add(label2);
            Controls.Add(txtbMFlujo);
            Controls.Add(label1);
            Controls.Add(btnMFlujo);
            Controls.Add(panel1);
            Controls.Add(dvg1);
            Name = "ModoManual";
            Text = "ModoManual";
            Load += ModoManual_Load;
            ((System.ComponentModel.ISupportInitialize)dvg1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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

            // lenar de 0s

            for (int i = 0; i < vertices; ++i) {
                for (int j = 0; j < vertices; ++j) {
                    dvg1.Rows[i].Cells[j].Value = 0;
                }
            }

        }

        private DataGridView dvg1;
        private Panel panel1;
        private Button btnMFlujo;
        private Label label3;
        private ComboBox cmbFuente;
        private Label label2;
        private TextBox txtbMFlujo;
        private Label label1;
    }
}