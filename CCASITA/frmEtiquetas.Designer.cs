namespace LaCasita
{
    partial class frmEtiquetas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEtiquetas));
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.txtFormato = new System.Windows.Forms.TextBox();
            this.rbTexto = new System.Windows.Forms.RadioButton();
            this.rbFormato = new System.Windows.Forms.RadioButton();
            this.btTexto = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(101, 34);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(120, 20);
            this.txtTexto.TabIndex = 3;
            // 
            // txtFormato
            // 
            this.txtFormato.Location = new System.Drawing.Point(101, 85);
            this.txtFormato.Multiline = true;
            this.txtFormato.Name = "txtFormato";
            this.txtFormato.Size = new System.Drawing.Size(120, 66);
            this.txtFormato.TabIndex = 4;
            // 
            // rbTexto
            // 
            this.rbTexto.AutoSize = true;
            this.rbTexto.Location = new System.Drawing.Point(9, 37);
            this.rbTexto.Name = "rbTexto";
            this.rbTexto.Size = new System.Drawing.Size(89, 17);
            this.rbTexto.TabIndex = 5;
            this.rbTexto.TabStop = true;
            this.rbTexto.Text = "Texto Simple:";
            this.rbTexto.UseVisualStyleBackColor = true;
            this.rbTexto.CheckedChanged += new System.EventHandler(this.rbTexto_CheckedChanged);
            // 
            // rbFormato
            // 
            this.rbFormato.AutoSize = true;
            this.rbFormato.Location = new System.Drawing.Point(9, 85);
            this.rbFormato.Name = "rbFormato";
            this.rbFormato.Size = new System.Drawing.Size(66, 17);
            this.rbFormato.TabIndex = 6;
            this.rbFormato.TabStop = true;
            this.rbFormato.Text = "Formato:";
            this.rbFormato.UseVisualStyleBackColor = true;
            this.rbFormato.CheckedChanged += new System.EventHandler(this.rbFormato_CheckedChanged);
            // 
            // btTexto
            // 
            this.btTexto.Location = new System.Drawing.Point(103, 255);
            this.btTexto.Name = "btTexto";
            this.btTexto.Size = new System.Drawing.Size(103, 36);
            this.btTexto.TabIndex = 7;
            this.btTexto.Text = "Imprimir";
            this.btTexto.UseVisualStyleBackColor = true;
            this.btTexto.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(328, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTexto);
            this.groupBox1.Controls.Add(this.rbTexto);
            this.groupBox1.Controls.Add(this.rbFormato);
            this.groupBox1.Controls.Add(this.txtFormato);
            this.groupBox1.Location = new System.Drawing.Point(19, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 177);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // frmEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 318);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btTexto);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmEtiquetas";
            this.Text = "Impresión de Etiquetas";
            this.Load += new System.EventHandler(this.frmEtiquetas_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.TextBox txtFormato;
        private System.Windows.Forms.RadioButton rbTexto;
        private System.Windows.Forms.RadioButton rbFormato;
        private System.Windows.Forms.Button btTexto;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}