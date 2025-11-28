namespace LaCasita
{
    partial class frmWEB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWEB));
            this.tc1 = new System.Windows.Forms.TabControl();
            this.tpCortes = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chTVPedidos = new System.Windows.Forms.CheckBox();
            this.chTVEtiqueta = new System.Windows.Forms.CheckBox();
            this.chTVGranel = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaI = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbFinal = new System.Windows.Forms.RadioButton();
            this.rbTarde = new System.Windows.Forms.RadioButton();
            this.rbManana = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chTransfer = new System.Windows.Forms.CheckBox();
            this.chOtro = new System.Windows.Forms.CheckBox();
            this.chTarjeta = new System.Windows.Forms.CheckBox();
            this.chEfectivo = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chFiscal = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chPA = new System.Windows.Forms.CheckBox();
            this.chTO = new System.Windows.Forms.CheckBox();
            this.chAV = new System.Windows.Forms.CheckBox();
            this.chMA = new System.Windows.Forms.CheckBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.gvCortes = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbExcelCorte = new System.Windows.Forms.ToolStripButton();
            this.tsbCortes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLineas = new System.Windows.Forms.ToolStripButton();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsCortesEncontrados = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvTransferencias = new System.Windows.Forms.DataGridView();
            this.tsbSalirT = new System.Windows.Forms.ToolStrip();
            this.tsbAgregar = new System.Windows.Forms.ToolStripButton();
            this.tsbSalir = new System.Windows.Forms.ToolStripButton();
            this.tsbActualizar = new System.Windows.Forms.ToolStripButton();
            this.tc1.SuspendLayout();
            this.tpCortes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCortes)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).BeginInit();
            this.tsbSalirT.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc1
            // 
            this.tc1.Controls.Add(this.tpCortes);
            this.tc1.Controls.Add(this.tabPage1);
            this.tc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc1.Location = new System.Drawing.Point(0, 0);
            this.tc1.Name = "tc1";
            this.tc1.SelectedIndex = 0;
            this.tc1.Size = new System.Drawing.Size(921, 666);
            this.tc1.TabIndex = 0;
            // 
            // tpCortes
            // 
            this.tpCortes.Controls.Add(this.splitContainer2);
            this.tpCortes.Location = new System.Drawing.Point(4, 22);
            this.tpCortes.Name = "tpCortes";
            this.tpCortes.Padding = new System.Windows.Forms.Padding(3);
            this.tpCortes.Size = new System.Drawing.Size(913, 640);
            this.tpCortes.TabIndex = 2;
            this.tpCortes.Text = "Cortes";
            this.tpCortes.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox6);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox7);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox8);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.crystalReportViewer1);
            this.splitContainer2.Panel2.Controls.Add(this.gvCortes);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer2.Size = new System.Drawing.Size(907, 634);
            this.splitContainer2.SplitterDistance = 131;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chTVPedidos);
            this.groupBox2.Controls.Add(this.chTVEtiqueta);
            this.groupBox2.Controls.Add(this.chTVGranel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 491);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(106, 88);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Venta";
            // 
            // chTVPedidos
            // 
            this.chTVPedidos.AutoSize = true;
            this.chTVPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTVPedidos.Location = new System.Drawing.Point(18, 67);
            this.chTVPedidos.Name = "chTVPedidos";
            this.chTVPedidos.Size = new System.Drawing.Size(64, 17);
            this.chTVPedidos.TabIndex = 2;
            this.chTVPedidos.Text = "Pedidos";
            this.chTVPedidos.UseVisualStyleBackColor = true;
            // 
            // chTVEtiqueta
            // 
            this.chTVEtiqueta.AutoSize = true;
            this.chTVEtiqueta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTVEtiqueta.Location = new System.Drawing.Point(18, 43);
            this.chTVEtiqueta.Name = "chTVEtiqueta";
            this.chTVEtiqueta.Size = new System.Drawing.Size(65, 17);
            this.chTVEtiqueta.TabIndex = 1;
            this.chTVEtiqueta.Text = "Etiqueta";
            this.chTVEtiqueta.UseVisualStyleBackColor = true;
            // 
            // chTVGranel
            // 
            this.chTVGranel.AutoSize = true;
            this.chTVGranel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTVGranel.Location = new System.Drawing.Point(18, 19);
            this.chTVGranel.Name = "chTVGranel";
            this.chTVGranel.Size = new System.Drawing.Size(57, 17);
            this.chTVGranel.TabIndex = 0;
            this.chTVGranel.Text = "Granel";
            this.chTVGranel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaI);
            this.groupBox1.Controls.Add(this.dtpFechaF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 104);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fechas";
            // 
            // dtpFechaI
            // 
            this.dtpFechaI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaI.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaI.Location = new System.Drawing.Point(9, 32);
            this.dtpFechaI.Name = "dtpFechaI";
            this.dtpFechaI.Size = new System.Drawing.Size(81, 20);
            this.dtpFechaI.TabIndex = 1;
            this.dtpFechaI.ValueChanged += new System.EventHandler(this.dtpFechaI_ValueChanged);
            // 
            // dtpFechaF
            // 
            this.dtpFechaF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaF.Location = new System.Drawing.Point(9, 78);
            this.dtpFechaF.Name = "dtpFechaF";
            this.dtpFechaF.Size = new System.Drawing.Size(81, 20);
            this.dtpFechaF.TabIndex = 6;
            this.dtpFechaF.ValueChanged += new System.EventHandler(this.dtpFechaF_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Final";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbFinal);
            this.groupBox5.Controls.Add(this.rbTarde);
            this.groupBox5.Controls.Add(this.rbManana);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(15, 376);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(103, 109);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo de Corte";
            // 
            // rbFinal
            // 
            this.rbFinal.AutoSize = true;
            this.rbFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFinal.Location = new System.Drawing.Point(19, 72);
            this.rbFinal.Name = "rbFinal";
            this.rbFinal.Size = new System.Drawing.Size(47, 17);
            this.rbFinal.TabIndex = 4;
            this.rbFinal.TabStop = true;
            this.rbFinal.Text = "Final";
            this.rbFinal.UseVisualStyleBackColor = true;
            // 
            // rbTarde
            // 
            this.rbTarde.AutoSize = true;
            this.rbTarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTarde.Location = new System.Drawing.Point(19, 51);
            this.rbTarde.Name = "rbTarde";
            this.rbTarde.Size = new System.Drawing.Size(53, 17);
            this.rbTarde.TabIndex = 3;
            this.rbTarde.TabStop = true;
            this.rbTarde.Text = "Tarde";
            this.rbTarde.UseVisualStyleBackColor = true;
            this.rbTarde.Click += new System.EventHandler(this.rbTarde_Click);
            // 
            // rbManana
            // 
            this.rbManana.AutoSize = true;
            this.rbManana.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbManana.Location = new System.Drawing.Point(19, 28);
            this.rbManana.Name = "rbManana";
            this.rbManana.Size = new System.Drawing.Size(64, 17);
            this.rbManana.TabIndex = 2;
            this.rbManana.TabStop = true;
            this.rbManana.Text = "Mañana";
            this.rbManana.UseVisualStyleBackColor = true;
            this.rbManana.Click += new System.EventHandler(this.rbManana_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chTransfer);
            this.groupBox6.Controls.Add(this.chOtro);
            this.groupBox6.Controls.Add(this.chTarjeta);
            this.groupBox6.Controls.Add(this.chEfectivo);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(15, 237);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(103, 121);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Forma de Pago";
            // 
            // chTransfer
            // 
            this.chTransfer.AutoSize = true;
            this.chTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTransfer.Location = new System.Drawing.Point(18, 75);
            this.chTransfer.Name = "chTransfer";
            this.chTransfer.Size = new System.Drawing.Size(91, 17);
            this.chTransfer.TabIndex = 3;
            this.chTransfer.Text = "Transferencia";
            this.chTransfer.UseVisualStyleBackColor = true;
            // 
            // chOtro
            // 
            this.chOtro.AutoSize = true;
            this.chOtro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chOtro.Location = new System.Drawing.Point(18, 98);
            this.chOtro.Name = "chOtro";
            this.chOtro.Size = new System.Drawing.Size(46, 17);
            this.chOtro.TabIndex = 2;
            this.chOtro.Text = "Otro";
            this.chOtro.UseVisualStyleBackColor = true;
            // 
            // chTarjeta
            // 
            this.chTarjeta.AutoSize = true;
            this.chTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTarjeta.Location = new System.Drawing.Point(18, 53);
            this.chTarjeta.Name = "chTarjeta";
            this.chTarjeta.Size = new System.Drawing.Size(59, 17);
            this.chTarjeta.TabIndex = 1;
            this.chTarjeta.Text = "Tarjeta";
            this.chTarjeta.UseVisualStyleBackColor = true;
            // 
            // chEfectivo
            // 
            this.chEfectivo.AutoSize = true;
            this.chEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEfectivo.Location = new System.Drawing.Point(18, 30);
            this.chEfectivo.Name = "chEfectivo";
            this.chEfectivo.Size = new System.Drawing.Size(65, 17);
            this.chEfectivo.TabIndex = 0;
            this.chEfectivo.Text = "Efectivo";
            this.chEfectivo.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chFiscal);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(15, 585);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(103, 39);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Modalidad";
            // 
            // chFiscal
            // 
            this.chFiscal.AutoSize = true;
            this.chFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chFiscal.Location = new System.Drawing.Point(18, 18);
            this.chFiscal.Name = "chFiscal";
            this.chFiscal.Size = new System.Drawing.Size(53, 17);
            this.chFiscal.TabIndex = 2;
            this.chFiscal.Text = "Fiscal";
            this.chFiscal.UseVisualStyleBackColor = true;
            this.chFiscal.CheckedChanged += new System.EventHandler(this.chFiscal_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chPA);
            this.groupBox8.Controls.Add(this.chTO);
            this.groupBox8.Controls.Add(this.chAV);
            this.groupBox8.Controls.Add(this.chMA);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(15, 122);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(103, 109);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Sucursal";
            // 
            // chPA
            // 
            this.chPA.AutoSize = true;
            this.chPA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chPA.Location = new System.Drawing.Point(18, 65);
            this.chPA.Name = "chPA";
            this.chPA.Size = new System.Drawing.Size(71, 17);
            this.chPA.TabIndex = 3;
            this.chPA.Text = "Parroquia";
            this.chPA.UseVisualStyleBackColor = true;
            this.chPA.Click += new System.EventHandler(this.chPA_Click);
            // 
            // chTO
            // 
            this.chTO.AutoSize = true;
            this.chTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTO.Location = new System.Drawing.Point(18, 86);
            this.chTO.Name = "chTO";
            this.chTO.Size = new System.Drawing.Size(56, 17);
            this.chTO.TabIndex = 2;
            this.chTO.Text = "Torres";
            this.chTO.UseVisualStyleBackColor = true;
            this.chTO.Click += new System.EventHandler(this.chTO_Click);
            // 
            // chAV
            // 
            this.chAV.AutoSize = true;
            this.chAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chAV.Location = new System.Drawing.Point(18, 42);
            this.chAV.Name = "chAV";
            this.chAV.Size = new System.Drawing.Size(76, 17);
            this.chAV.TabIndex = 1;
            this.chAV.Text = "Av México";
            this.chAV.UseVisualStyleBackColor = true;
            this.chAV.Click += new System.EventHandler(this.chAV_Click);
            // 
            // chMA
            // 
            this.chMA.AutoSize = true;
            this.chMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chMA.Location = new System.Drawing.Point(18, 19);
            this.chMA.Name = "chMA";
            this.chMA.Size = new System.Drawing.Size(87, 17);
            this.chMA.TabIndex = 0;
            this.chMA.Text = "Miguel Angel";
            this.chMA.UseVisualStyleBackColor = true;
            this.chMA.Click += new System.EventHandler(this.chMA_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 55);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(772, 557);
            this.crystalReportViewer1.TabIndex = 3;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // gvCortes
            // 
            this.gvCortes.AllowUserToAddRows = false;
            this.gvCortes.AllowUserToDeleteRows = false;
            this.gvCortes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvCortes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gvCortes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvCortes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCortes.Location = new System.Drawing.Point(16, 335);
            this.gvCortes.Name = "gvCortes";
            this.gvCortes.ReadOnly = true;
            this.gvCortes.Size = new System.Drawing.Size(733, 245);
            this.gvCortes.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbExcelCorte,
            this.tsbCortes,
            this.toolStripSeparator1,
            this.tsbLineas});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(772, 55);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::LaCasita.Properties.Resources.exit_32;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Salir";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbExcelCorte
            // 
            this.tsbExcelCorte.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExcelCorte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcelCorte.Image = global::LaCasita.Properties.Resources.Excel_icon;
            this.tsbExcelCorte.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcelCorte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcelCorte.Name = "tsbExcelCorte";
            this.tsbExcelCorte.Size = new System.Drawing.Size(36, 52);
            this.tsbExcelCorte.Text = "toolStripButton1";
            this.tsbExcelCorte.ToolTipText = "Exporta Resultados a Excel";
            this.tsbExcelCorte.Click += new System.EventHandler(this.tsbExcelCorte_Click);
            // 
            // tsbCortes
            // 
            this.tsbCortes.Image = ((System.Drawing.Image)(resources.GetObject("tsbCortes.Image")));
            this.tsbCortes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCortes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCortes.Name = "tsbCortes";
            this.tsbCortes.Size = new System.Drawing.Size(100, 52);
            this.tsbCortes.Text = "CORTES";
            this.tsbCortes.ToolTipText = "Reporte de Corte de Caja";
            this.tsbCortes.Click += new System.EventHandler(this.tsbCortes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // tsbLineas
            // 
            this.tsbLineas.Image = ((System.Drawing.Image)(resources.GetObject("tsbLineas.Image")));
            this.tsbLineas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLineas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLineas.Name = "tsbLineas";
            this.tsbLineas.Size = new System.Drawing.Size(97, 52);
            this.tsbLineas.Text = "LINEAS";
            this.tsbLineas.ToolTipText = "Reporte por Línea de Producto";
            this.tsbLineas.Click += new System.EventHandler(this.tsbLineas_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.tsCortesEncontrados});
            this.statusStrip2.Location = new System.Drawing.Point(0, 612);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(772, 22);
            this.statusStrip2.TabIndex = 0;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel1.Text = "Registros Encontrados:";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // tsCortesEncontrados
            // 
            this.tsCortesEncontrados.Name = "tsCortesEncontrados";
            this.tsCortesEncontrados.Size = new System.Drawing.Size(0, 17);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTransferencias);
            this.tabPage1.Controls.Add(this.tsbSalirT);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(913, 640);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Transferencias";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvTransferencias
            // 
            this.dgvTransferencias.AllowUserToAddRows = false;
            this.dgvTransferencias.AllowUserToDeleteRows = false;
            this.dgvTransferencias.AllowUserToOrderColumns = true;
            this.dgvTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransferencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransferencias.Location = new System.Drawing.Point(3, 58);
            this.dgvTransferencias.Name = "dgvTransferencias";
            this.dgvTransferencias.ReadOnly = true;
            this.dgvTransferencias.Size = new System.Drawing.Size(907, 579);
            this.dgvTransferencias.TabIndex = 1;
            this.dgvTransferencias.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransferencias_CellContentDoubleClick);
            // 
            // tsbSalirT
            // 
            this.tsbSalirT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAgregar,
            this.tsbSalir,
            this.tsbActualizar});
            this.tsbSalirT.Location = new System.Drawing.Point(3, 3);
            this.tsbSalirT.Name = "tsbSalirT";
            this.tsbSalirT.Size = new System.Drawing.Size(907, 55);
            this.tsbSalirT.TabIndex = 0;
            this.tsbSalirT.Text = "toolStrip1";
            // 
            // tsbAgregar
            // 
            this.tsbAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAgregar.Image = global::LaCasita.Properties.Resources.new_48;
            this.tsbAgregar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAgregar.Name = "tsbAgregar";
            this.tsbAgregar.Size = new System.Drawing.Size(52, 52);
            this.tsbAgregar.Text = "Agregar Transferencia";
            this.tsbAgregar.Click += new System.EventHandler(this.tsbAgregar_Click);
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
            this.tsbSalir.Text = "Salir de Transferencias";
            this.tsbSalir.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.tsbSalir.Click += new System.EventHandler(this.tsbSalirT_Click);
            // 
            // tsbActualizar
            // 
            this.tsbActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbActualizar.Image = global::LaCasita.Properties.Resources.available_updates_48;
            this.tsbActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActualizar.Name = "tsbActualizar";
            this.tsbActualizar.Size = new System.Drawing.Size(52, 52);
            this.tsbActualizar.Text = "Actualizar vista";
            this.tsbActualizar.Click += new System.EventHandler(this.tsbActualizar_Click);
            // 
            // frmWEB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 666);
            this.Controls.Add(this.tc1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWEB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rerportes Casita en WEB";
            this.Load += new System.EventHandler(this.frmWEB_Load);
            this.tc1.ResumeLayout(false);
            this.tpCortes.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCortes)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).EndInit();
            this.tsbSalirT.ResumeLayout(false);
            this.tsbSalirT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc1;
        private System.Windows.Forms.TabPage tpCortes;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chOtro;
        private System.Windows.Forms.CheckBox chTarjeta;
        private System.Windows.Forms.CheckBox chEfectivo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaI;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chPA;
        private System.Windows.Forms.CheckBox chTO;
        private System.Windows.Forms.CheckBox chAV;
        private System.Windows.Forms.CheckBox chMA;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbExcelCorte;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridView gvCortes;
        private System.Windows.Forms.ToolStripStatusLabel tsCortesEncontrados;
        private System.Windows.Forms.RadioButton rbFinal;
        private System.Windows.Forms.RadioButton rbTarde;
        private System.Windows.Forms.RadioButton rbManana;
        private System.Windows.Forms.DateTimePicker dtpFechaF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chFiscal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chTVPedidos;
        private System.Windows.Forms.CheckBox chTVEtiqueta;
        private System.Windows.Forms.CheckBox chTVGranel;
        private System.Windows.Forms.ToolStripButton tsbCortes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLineas;
        private System.Windows.Forms.CheckBox chTransfer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvTransferencias;
        private System.Windows.Forms.ToolStrip tsbSalirT;
        private System.Windows.Forms.ToolStripButton tsbAgregar;
        private System.Windows.Forms.ToolStripButton tsbSalir;
        private System.Windows.Forms.ToolStripButton tsbActualizar;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
    }
}