namespace LaCasita
{
    partial class frmSAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSAT));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chFC = new System.Windows.Forms.CheckBox();
            this.btConsulta = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chIngreso = new System.Windows.Forms.CheckBox();
            this.chEgresos = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbSATNoExcel = new System.Windows.Forms.RadioButton();
            this.rbExcelNoSAT = new System.Windows.Forms.RadioButton();
            this.rbExelYSAT = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.gvSAT = new System.Windows.Forms.DataGridView();
            this.tsb1 = new System.Windows.Forms.StatusStrip();
            this.tsslContador = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSAT)).BeginInit();
            this.tsb1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chFC);
            this.splitContainer1.Panel1.Controls.Add(this.btConsulta);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvSAT);
            this.splitContainer1.Panel2.Controls.Add(this.tsb1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(994, 558);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 0;
            // 
            // chFC
            // 
            this.chFC.AutoSize = true;
            this.chFC.Location = new System.Drawing.Point(32, 406);
            this.chFC.Name = "chFC";
            this.chFC.Size = new System.Drawing.Size(109, 17);
            this.chFC.TabIndex = 11;
            this.chFC.Text = "Excluir la serie FC";
            this.chFC.UseVisualStyleBackColor = true;
            // 
            // btConsulta
            // 
            this.btConsulta.Location = new System.Drawing.Point(32, 460);
            this.btConsulta.Name = "btConsulta";
            this.btConsulta.Size = new System.Drawing.Size(75, 23);
            this.btConsulta.TabIndex = 10;
            this.btConsulta.Text = "Consultar";
            this.btConsulta.UseVisualStyleBackColor = true;
            this.btConsulta.Click += new System.EventHandler(this.btConsulta_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chIngreso);
            this.groupBox2.Controls.Add(this.chEgresos);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(18, 314);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 66);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Comprobante";
            // 
            // chIngreso
            // 
            this.chIngreso.AutoSize = true;
            this.chIngreso.Location = new System.Drawing.Point(23, 19);
            this.chIngreso.Name = "chIngreso";
            this.chIngreso.Size = new System.Drawing.Size(66, 17);
            this.chIngreso.TabIndex = 9;
            this.chIngreso.Text = "Ingresos";
            this.chIngreso.UseVisualStyleBackColor = true;
            // 
            // chEgresos
            // 
            this.chEgresos.AutoSize = true;
            this.chEgresos.Location = new System.Drawing.Point(22, 42);
            this.chEgresos.Name = "chEgresos";
            this.chEgresos.Size = new System.Drawing.Size(64, 17);
            this.chEgresos.TabIndex = 9;
            this.chEgresos.Text = "Egresos";
            this.chEgresos.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSATNoExcel);
            this.groupBox3.Controls.Add(this.rbExcelNoSAT);
            this.groupBox3.Controls.Add(this.rbExelYSAT);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(18, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 97);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criterios";
            // 
            // rbSATNoExcel
            // 
            this.rbSATNoExcel.AutoSize = true;
            this.rbSATNoExcel.Location = new System.Drawing.Point(6, 65);
            this.rbSATNoExcel.Name = "rbSATNoExcel";
            this.rbSATNoExcel.Size = new System.Drawing.Size(129, 17);
            this.rbSATNoExcel.TabIndex = 9;
            this.rbSATNoExcel.TabStop = true;
            this.rbSATNoExcel.Text = "En SAT y no en Excel";
            this.rbSATNoExcel.UseVisualStyleBackColor = true;
            // 
            // rbExcelNoSAT
            // 
            this.rbExcelNoSAT.AutoSize = true;
            this.rbExcelNoSAT.Location = new System.Drawing.Point(6, 42);
            this.rbExcelNoSAT.Name = "rbExcelNoSAT";
            this.rbExcelNoSAT.Size = new System.Drawing.Size(129, 17);
            this.rbExcelNoSAT.TabIndex = 9;
            this.rbExcelNoSAT.TabStop = true;
            this.rbExcelNoSAT.Text = "En Excel y no en SAT";
            this.rbExcelNoSAT.UseVisualStyleBackColor = true;
            // 
            // rbExelYSAT
            // 
            this.rbExelYSAT.AutoSize = true;
            this.rbExelYSAT.Location = new System.Drawing.Point(6, 19);
            this.rbExelYSAT.Name = "rbExelYSAT";
            this.rbExelYSAT.Size = new System.Drawing.Size(125, 17);
            this.rbExelYSAT.TabIndex = 9;
            this.rbExelYSAT.TabStop = true;
            this.rbExelYSAT.Text = "En Excel y en el SAT";
            this.rbExelYSAT.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaInicial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaFinal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 77);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Intervalo de Fechas";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(50, 19);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicial";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(50, 48);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // gvSAT
            // 
            this.gvSAT.AllowUserToAddRows = false;
            this.gvSAT.AllowUserToDeleteRows = false;
            this.gvSAT.AllowUserToResizeRows = false;
            this.gvSAT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSAT.Location = new System.Drawing.Point(0, 55);
            this.gvSAT.Name = "gvSAT";
            this.gvSAT.Size = new System.Drawing.Size(808, 481);
            this.gvSAT.TabIndex = 2;
            // 
            // tsb1
            // 
            this.tsb1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslContador});
            this.tsb1.Location = new System.Drawing.Point(0, 536);
            this.tsb1.Name = "tsb1";
            this.tsb1.Size = new System.Drawing.Size(808, 22);
            this.tsb1.TabIndex = 1;
            this.tsb1.Text = "statusStrip1";
            // 
            // tsslContador
            // 
            this.tsslContador.Name = "tsslContador";
            this.tsslContador.Size = new System.Drawing.Size(87, 17);
            this.tsslContador.Text = "Total Registros:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::LaCasita.Properties.Resources.EXIT_48;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Salir";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::LaCasita.Properties.Resources.Excel_icon;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(36, 52);
            this.tsbExcel.Text = "toolStripButton2";
            this.tsbExcel.ToolTipText = "Exportar a Excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // frmSAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 558);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturas Resgistradas SAT";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSAT)).EndInit();
            this.tsb1.ResumeLayout(false);
            this.tsb1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip tsb1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DataGridView gvSAT;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel tsslContador;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbSATNoExcel;
        private System.Windows.Forms.RadioButton rbExcelNoSAT;
        private System.Windows.Forms.RadioButton rbExelYSAT;
        private System.Windows.Forms.CheckBox chEgresos;
        private System.Windows.Forms.CheckBox chIngreso;
        private System.Windows.Forms.Button btConsulta;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chFC;
        private System.Windows.Forms.ToolStripButton tsbExcel;
    }
}