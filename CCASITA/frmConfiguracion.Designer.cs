namespace LaCasita
{
    partial class frmConfiguracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguracion));
            this.btCreaDB = new System.Windows.Forms.Button();
            this.btPoblarDB = new System.Windows.Forms.Button();
            this.lbProgreso = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btBorraExcel = new System.Windows.Forms.Button();
            this.btImportaExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btGuardar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRutaAspel = new System.Windows.Forms.TextBox();
            this.btFechasVenta = new System.Windows.Forms.Button();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCatPedidos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btCorrigeIEPS = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCreaDB
            // 
            this.btCreaDB.Location = new System.Drawing.Point(28, 33);
            this.btCreaDB.Name = "btCreaDB";
            this.btCreaDB.Size = new System.Drawing.Size(136, 23);
            this.btCreaDB.TabIndex = 0;
            this.btCreaDB.Text = "Crear DB SAT";
            this.btCreaDB.UseVisualStyleBackColor = true;
            this.btCreaDB.Click += new System.EventHandler(this.btCreaDB_Click);
            // 
            // btPoblarDB
            // 
            this.btPoblarDB.Location = new System.Drawing.Point(28, 62);
            this.btPoblarDB.Name = "btPoblarDB";
            this.btPoblarDB.Size = new System.Drawing.Size(136, 23);
            this.btPoblarDB.TabIndex = 1;
            this.btPoblarDB.Text = "Poblar la DB del SAT";
            this.btPoblarDB.UseVisualStyleBackColor = true;
            this.btPoblarDB.Click += new System.EventHandler(this.btPoblarDB_Click);
            // 
            // lbProgreso
            // 
            this.lbProgreso.AutoSize = true;
            this.lbProgreso.Location = new System.Drawing.Point(68, 280);
            this.lbProgreso.Name = "lbProgreso";
            this.lbProgreso.Size = new System.Drawing.Size(10, 13);
            this.lbProgreso.TabIndex = 2;
            this.lbProgreso.Text = ".";
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(272, 339);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(108, 28);
            this.btCancelar.TabIndex = 3;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btBorraExcel);
            this.groupBox1.Controls.Add(this.btImportaExcel);
            this.groupBox1.Controls.Add(this.btCreaDB);
            this.groupBox1.Controls.Add(this.btPoblarDB);
            this.groupBox1.Location = new System.Drawing.Point(33, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base de Datos SAT";
            // 
            // btBorraExcel
            // 
            this.btBorraExcel.Location = new System.Drawing.Point(224, 62);
            this.btBorraExcel.Name = "btBorraExcel";
            this.btBorraExcel.Size = new System.Drawing.Size(136, 23);
            this.btBorraExcel.TabIndex = 3;
            this.btBorraExcel.Text = "Borra la Tabla de Excel";
            this.btBorraExcel.UseVisualStyleBackColor = true;
            this.btBorraExcel.Click += new System.EventHandler(this.btBorraExcel_Click);
            // 
            // btImportaExcel
            // 
            this.btImportaExcel.Location = new System.Drawing.Point(224, 33);
            this.btImportaExcel.Name = "btImportaExcel";
            this.btImportaExcel.Size = new System.Drawing.Size(136, 23);
            this.btImportaExcel.TabIndex = 2;
            this.btImportaExcel.Text = "Importar Archivo Excel";
            this.btImportaExcel.UseVisualStyleBackColor = true;
            this.btImportaExcel.Click += new System.EventHandler(this.btImportaExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Servidor Aspel:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(157, 50);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(312, 20);
            this.txtServer.TabIndex = 6;
            // 
            // btGuardar
            // 
            this.btGuardar.Location = new System.Drawing.Point(98, 339);
            this.btGuardar.Name = "btGuardar";
            this.btGuardar.Size = new System.Drawing.Size(108, 28);
            this.btGuardar.TabIndex = 7;
            this.btGuardar.Text = "OK";
            this.btGuardar.UseVisualStyleBackColor = true;
            this.btGuardar.Click += new System.EventHandler(this.btGuardar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ruta DB:";
            // 
            // txtRutaAspel
            // 
            this.txtRutaAspel.Location = new System.Drawing.Point(157, 87);
            this.txtRutaAspel.Name = "txtRutaAspel";
            this.txtRutaAspel.Size = new System.Drawing.Size(312, 20);
            this.txtRutaAspel.TabIndex = 9;
            // 
            // btFechasVenta
            // 
            this.btFechasVenta.Location = new System.Drawing.Point(61, 261);
            this.btFechasVenta.Name = "btFechasVenta";
            this.btFechasVenta.Size = new System.Drawing.Size(169, 32);
            this.btFechasVenta.TabIndex = 10;
            this.btFechasVenta.Text = "Importar Fechas de Venta";
            this.btFechasVenta.UseVisualStyleBackColor = true;
            this.btFechasVenta.Click += new System.EventHandler(this.btFechasVenta_Click);
            // 
            // pbProgreso
            // 
            this.pbProgreso.Location = new System.Drawing.Point(61, 310);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(363, 23);
            this.pbProgreso.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 17;
            // 
            // txtCatPedidos
            // 
            this.txtCatPedidos.Location = new System.Drawing.Point(157, 117);
            this.txtCatPedidos.Name = "txtCatPedidos";
            this.txtCatPedidos.Size = new System.Drawing.Size(312, 20);
            this.txtCatPedidos.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "URL Base de Datos:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(157, 12);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(312, 20);
            this.txtURL.TabIndex = 15;
            // 
            // btCorrigeIEPS
            // 
            this.btCorrigeIEPS.Location = new System.Drawing.Point(257, 269);
            this.btCorrigeIEPS.Name = "btCorrigeIEPS";
            this.btCorrigeIEPS.Size = new System.Drawing.Size(136, 23);
            this.btCorrigeIEPS.TabIndex = 16;
            this.btCorrigeIEPS.Text = "Corrige IEPS";
            this.btCorrigeIEPS.UseVisualStyleBackColor = true;
            this.btCorrigeIEPS.Click += new System.EventHandler(this.btCorrigeIEPS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Catálogo de pedidos:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 378);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btCorrigeIEPS);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCatPedidos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.btFechasVenta);
            this.Controls.Add(this.txtRutaAspel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btGuardar);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.lbProgreso);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCreaDB;
        private System.Windows.Forms.Button btPoblarDB;
        private System.Windows.Forms.Label lbProgreso;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btGuardar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRutaAspel;
        private System.Windows.Forms.Button btImportaExcel;
        private System.Windows.Forms.Button btBorraExcel;
        private System.Windows.Forms.Button btFechasVenta;
        private System.Windows.Forms.ProgressBar pbProgreso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCatPedidos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btCorrigeIEPS;
        private System.Windows.Forms.Label label5;
    }
}