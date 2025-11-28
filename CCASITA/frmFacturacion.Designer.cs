namespace LaCasita
{
    partial class frmFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturacion));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chStsE = new System.Windows.Forms.CheckBox();
            this.chStsO = new System.Windows.Forms.CheckBox();
            this.chStsC = new System.Windows.Forms.CheckBox();
            this.chD = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbVenta = new System.Windows.Forms.RadioButton();
            this.rbFactura = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chC = new System.Windows.Forms.CheckBox();
            this.chT = new System.Windows.Forms.CheckBox();
            this.chA = new System.Windows.Forms.CheckBox();
            this.chP = new System.Windows.Forms.CheckBox();
            this.chM = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbSalir = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslContador = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslEncontrados = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gvFacturas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturas)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cbCliente);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.chD);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvFacturas);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(994, 586);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 499);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cliente";
            // 
            // cbCliente
            // 
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(12, 515);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(141, 21);
            this.cbCliente.TabIndex = 8;
            this.cbCliente.SelectionChangeCommitted += new System.EventHandler(this.cbCliente_SelectionChangeCommitted);
            this.cbCliente.SelectedValueChanged += new System.EventHandler(this.cbCliente_SelectedValueChanged);
            this.cbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCliente_KeyDown);
            this.cbCliente.Leave += new System.EventHandler(this.cbCliente_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chStsE);
            this.groupBox4.Controls.Add(this.chStsO);
            this.groupBox4.Controls.Add(this.chStsC);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 389);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(70, 93);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status";
            // 
            // chStsE
            // 
            this.chStsE.AutoSize = true;
            this.chStsE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chStsE.Location = new System.Drawing.Point(14, 42);
            this.chStsE.Name = "chStsE";
            this.chStsE.Size = new System.Drawing.Size(33, 17);
            this.chStsE.TabIndex = 0;
            this.chStsE.Text = "E";
            this.chStsE.UseVisualStyleBackColor = true;
            this.chStsE.Click += new System.EventHandler(this.chTipoE_Click);
            // 
            // chStsO
            // 
            this.chStsO.AutoSize = true;
            this.chStsO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chStsO.Location = new System.Drawing.Point(14, 65);
            this.chStsO.Name = "chStsO";
            this.chStsO.Size = new System.Drawing.Size(34, 17);
            this.chStsO.TabIndex = 0;
            this.chStsO.Text = "O";
            this.chStsO.UseVisualStyleBackColor = true;
            this.chStsO.Click += new System.EventHandler(this.chTipoO_Click);
            // 
            // chStsC
            // 
            this.chStsC.AutoSize = true;
            this.chStsC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chStsC.Location = new System.Drawing.Point(14, 19);
            this.chStsC.Name = "chStsC";
            this.chStsC.Size = new System.Drawing.Size(33, 17);
            this.chStsC.TabIndex = 0;
            this.chStsC.Text = "C";
            this.chStsC.UseVisualStyleBackColor = true;
            this.chStsC.Click += new System.EventHandler(this.chTipoC_Click);
            // 
            // chD
            // 
            this.chD.AutoSize = true;
            this.chD.Location = new System.Drawing.Point(22, 353);
            this.chD.Name = "chD";
            this.chD.Size = new System.Drawing.Size(91, 17);
            this.chD.TabIndex = 1;
            this.chD.Text = "Devoluciones";
            this.chD.UseVisualStyleBackColor = true;
            this.chD.Click += new System.EventHandler(this.chD_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbVenta);
            this.groupBox3.Controls.Add(this.rbFactura);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 64);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criterio de Búsqueda";
            // 
            // rbVenta
            // 
            this.rbVenta.AutoSize = true;
            this.rbVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVenta.Location = new System.Drawing.Point(17, 42);
            this.rbVenta.Name = "rbVenta";
            this.rbVenta.Size = new System.Drawing.Size(101, 17);
            this.rbVenta.TabIndex = 0;
            this.rbVenta.TabStop = true;
            this.rbVenta.Text = "Fecha de Venta";
            this.rbVenta.UseVisualStyleBackColor = true;
            this.rbVenta.Click += new System.EventHandler(this.rbVenta_Click);
            // 
            // rbFactura
            // 
            this.rbFactura.AutoSize = true;
            this.rbFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFactura.Location = new System.Drawing.Point(17, 19);
            this.rbFactura.Name = "rbFactura";
            this.rbFactura.Size = new System.Drawing.Size(109, 17);
            this.rbFactura.TabIndex = 0;
            this.rbFactura.TabStop = true;
            this.rbFactura.Text = "Fecha de Factura";
            this.rbFactura.UseVisualStyleBackColor = true;
            this.rbFactura.Click += new System.EventHandler(this.rbFactura_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaInicial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaFinal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 77);
            this.groupBox1.TabIndex = 6;
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
            this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.dtpFechaInicial_ValueChanged);
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
            this.dtpFechaFinal.ValueChanged += new System.EventHandler(this.dtpFechaFinal_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chC);
            this.groupBox2.Controls.Add(this.chT);
            this.groupBox2.Controls.Add(this.chA);
            this.groupBox2.Controls.Add(this.chP);
            this.groupBox2.Controls.Add(this.chM);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 136);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tiendas ";
            // 
            // chC
            // 
            this.chC.AutoSize = true;
            this.chC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chC.Location = new System.Drawing.Point(14, 111);
            this.chC.Name = "chC";
            this.chC.Size = new System.Drawing.Size(80, 17);
            this.chC.TabIndex = 0;
            this.chC.Text = "Corporativo";
            this.chC.UseVisualStyleBackColor = true;
            this.chC.Click += new System.EventHandler(this.chC_Click);
            // 
            // chT
            // 
            this.chT.AutoSize = true;
            this.chT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chT.Location = new System.Drawing.Point(14, 88);
            this.chT.Name = "chT";
            this.chT.Size = new System.Drawing.Size(56, 17);
            this.chT.TabIndex = 0;
            this.chT.Text = "Torres";
            this.chT.UseVisualStyleBackColor = true;
            this.chT.Click += new System.EventHandler(this.chT_Click);
            // 
            // chA
            // 
            this.chA.AutoSize = true;
            this.chA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chA.Location = new System.Drawing.Point(14, 42);
            this.chA.Name = "chA";
            this.chA.Size = new System.Drawing.Size(79, 17);
            this.chA.TabIndex = 0;
            this.chA.Text = "Av. México";
            this.chA.UseVisualStyleBackColor = true;
            this.chA.Click += new System.EventHandler(this.chA_Click);
            // 
            // chP
            // 
            this.chP.AutoSize = true;
            this.chP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chP.Location = new System.Drawing.Point(14, 65);
            this.chP.Name = "chP";
            this.chP.Size = new System.Drawing.Size(69, 17);
            this.chP.TabIndex = 0;
            this.chP.Text = "Parroqua";
            this.chP.UseVisualStyleBackColor = true;
            this.chP.Click += new System.EventHandler(this.chP_Click);
            // 
            // chM
            // 
            this.chM.AutoSize = true;
            this.chM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chM.Location = new System.Drawing.Point(14, 19);
            this.chM.Name = "chM";
            this.chM.Size = new System.Drawing.Size(87, 17);
            this.chM.TabIndex = 0;
            this.chM.Text = "Miguel Angel";
            this.chM.UseVisualStyleBackColor = true;
            this.chM.Click += new System.EventHandler(this.chM_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExcel,
            this.tsbSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 55);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::LaCasita.Properties.Resources.Excel_icon;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(36, 52);
            this.tsbExcel.Text = "toolStripButton1";
            this.tsbExcel.ToolTipText = "Exporta el reporte a Excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // tsbSalir
            // 
            this.tsbSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSalir.Image = global::LaCasita.Properties.Resources.EXIT_48;
            this.tsbSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSalir.Name = "tsbSalir";
            this.tsbSalir.Size = new System.Drawing.Size(52, 52);
            this.tsbSalir.Text = "toolStripButton1";
            this.tsbSalir.ToolTipText = "Salir";
            this.tsbSalir.Click += new System.EventHandler(this.tsbSalir_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslContador,
            this.tsslEncontrados,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(813, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslContador
            // 
            this.tsslContador.Name = "tsslContador";
            this.tsslContador.Size = new System.Drawing.Size(87, 17);
            this.tsslContador.Text = "Total Registros:";
            // 
            // tsslEncontrados
            // 
            this.tsslEncontrados.Name = "tsslEncontrados";
            this.tsslEncontrados.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // gvFacturas
            // 
            this.gvFacturas.AllowUserToAddRows = false;
            this.gvFacturas.AllowUserToDeleteRows = false;
            this.gvFacturas.AllowUserToOrderColumns = true;
            this.gvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFacturas.Location = new System.Drawing.Point(0, 55);
            this.gvFacturas.Name = "gvFacturas";
            this.gvFacturas.ReadOnly = true;
            this.gvFacturas.Size = new System.Drawing.Size(813, 509);
            this.gvFacturas.TabIndex = 2;
            // 
            // frmFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 586);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFacturacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Facturas";
            this.Load += new System.EventHandler(this.frmFacturacion_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chT;
        private System.Windows.Forms.CheckBox chA;
        private System.Windows.Forms.CheckBox chP;
        private System.Windows.Forms.CheckBox chM;
        private System.Windows.Forms.CheckBox chC;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslContador;
        private System.Windows.Forms.ToolStripStatusLabel tsslEncontrados;
        private System.Windows.Forms.ToolStripButton tsbSalir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbVenta;
        private System.Windows.Forms.RadioButton rbFactura;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chD;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chStsE;
        private System.Windows.Forms.CheckBox chStsO;
        private System.Windows.Forms.CheckBox chStsC;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.DataGridView gvFacturas;
    }
}