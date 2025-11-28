using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LaCasita
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmFacturacion factura = new frmFacturacion();
            factura.ShowDialog();
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            string titulo = "Control de Ventas";
            if (ApplicationDeployment.IsNetworkDeployed)
                titulo = titulo + string.Format(" - v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            this.Text = titulo;

            CargaConfiguracion();
        }

        private void CargaConfiguracion()
        {
            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Buen Software\LaCasita"))
            {
                if (key == null)
                {
                    key.Close();
                    return;
                }

                // URL
                if (key.GetValue("URL") == null)
                {
                    key.SetValue("URL", "");
                    Globales.URL = "";
                }
                else
                {
                    Globales.URL = key.GetValue("URL").ToString();
                }

                // ServidorAspel
                if (key.GetValue("ServidorAspel") == null)
                {
                    key.SetValue("ServidorAspel", "");
                    Globales.ServidorAspel = "";
                }
                else
                {
                    Globales.ServidorAspel = key.GetValue("ServidorAspel").ToString();
                }
                // RutaAspel
                if (key.GetValue("RutaAspel") == null)
                {
                    key.SetValue("RutaAspel", "");
                    Globales.RutaAspel = "";
                }
                else
                {
                    Globales.RutaAspel = key.GetValue("RutaAspel").ToString();
                }
                // Categoria para Pedidos
                if (key.GetValue("CatPedidos") == null)
                {
                    key.SetValue("CatPedidos", "");
                    Globales.CatPedidos = "";
                }
                else
                {
                    Globales.CatPedidos = key.GetValue("CatPedidos").ToString();
                }
                key.Close();
            }
        }

        private void btConfig_Click(object sender, EventArgs e)
        {
                frmConfiguracion cfg = new frmConfiguracion();
                cfg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSAT sat = new frmSAT();
            sat.Show();
        }

        private void btWEB_Click(object sender, EventArgs e)
        {
            frmWEB web = new frmWEB();
            web.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmEtiquetas eti = new frmEtiquetas();
            eti.Show();
        }
    }
}
