namespace LaCasita
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.btWEB = new System.Windows.Forms.Button();
            this.btConfig = new System.Windows.Forms.Button();
            this.btSATSAE = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.btFacturacion = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Image = global::LaCasita.Properties.Resources.barcode;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(227, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 80);
            this.button1.TabIndex = 6;
            this.button1.Text = "Etiquetas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btWEB
            // 
            this.btWEB.BackgroundImage = global::LaCasita.Properties.Resources.logo_login;
            this.btWEB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btWEB.Location = new System.Drawing.Point(227, 30);
            this.btWEB.Name = "btWEB";
            this.btWEB.Size = new System.Drawing.Size(175, 80);
            this.btWEB.TabIndex = 5;
            this.btWEB.Text = "Reporte WEB";
            this.btWEB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btWEB.UseVisualStyleBackColor = true;
            this.btWEB.Click += new System.EventHandler(this.btWEB_Click);
            // 
            // btConfig
            // 
            this.btConfig.BackgroundImage = global::LaCasita.Properties.Resources.cfg;
            this.btConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btConfig.Location = new System.Drawing.Point(29, 322);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(175, 80);
            this.btConfig.TabIndex = 4;
            this.btConfig.Text = "Configuración";
            this.btConfig.UseVisualStyleBackColor = true;
            this.btConfig.Click += new System.EventHandler(this.btConfig_Click);
            // 
            // btSATSAE
            // 
            this.btSATSAE.Image = global::LaCasita.Properties.Resources.SAT;
            this.btSATSAE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSATSAE.Location = new System.Drawing.Point(227, 225);
            this.btSATSAE.Name = "btSATSAE";
            this.btSATSAE.Size = new System.Drawing.Size(175, 80);
            this.btSATSAE.TabIndex = 3;
            this.btSATSAE.Text = "Reporte SAT vs SAE";
            this.btSATSAE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSATSAE.UseVisualStyleBackColor = true;
            this.btSATSAE.Click += new System.EventHandler(this.button2_Click);
            // 
            // btSalir
            // 
            this.btSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btSalir.Image = global::LaCasita.Properties.Resources.EXIT_48;
            this.btSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSalir.Location = new System.Drawing.Point(227, 322);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(175, 80);
            this.btSalir.TabIndex = 2;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = false;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btFacturacion
            // 
            this.btFacturacion.Image = global::LaCasita.Properties.Resources.factura_481;
            this.btFacturacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btFacturacion.Location = new System.Drawing.Point(29, 30);
            this.btFacturacion.Name = "btFacturacion";
            this.btFacturacion.Size = new System.Drawing.Size(175, 80);
            this.btFacturacion.TabIndex = 1;
            this.btFacturacion.Text = "Reporte de Facturación";
            this.btFacturacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFacturacion.UseVisualStyleBackColor = true;
            this.btFacturacion.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LaCasita.Properties.Resources.logo_casita;
            this.pictureBox1.Location = new System.Drawing.Point(29, 129);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(177, 144);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 431);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btWEB);
            this.Controls.Add(this.btConfig);
            this.Controls.Add(this.btSATSAE);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btFacturacion);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de Ventas";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btFacturacion;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btSATSAE;
        private System.Windows.Forms.Button btConfig;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btWEB;
        private System.Windows.Forms.Button button1;
    }
}

