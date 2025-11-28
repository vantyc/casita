using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaCasita
{
    public partial class frmEtiquetas : Form
    {
        public frmEtiquetas()
        {
            InitializeComponent();
        }

        private void btImprimir_Click(object sender, EventArgs e)
        {
            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    // Print the file to the printer.
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "Hello"; // device-dependent string, need a FormFeed?

            // Allow the user to select a printer.
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                // Send a printer-specific to the printer.
                RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    // Print the file to the printer.
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                }
            }
        }

        private void frmEtiquetas_Load(object sender, EventArgs e)
        {
            rbTexto.Enabled = true;
        }

        private void rbTexto_CheckedChanged(object sender, EventArgs e)
        {
            txtFormato.Enabled = false;
            txtTexto.Enabled = true;
        }

        private void rbFormato_CheckedChanged(object sender, EventArgs e)
        {
            txtTexto.Enabled = false;
            txtFormato.Enabled = true;
        }
    }
}
