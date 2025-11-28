using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace LaCasita
{
    public partial class frmSAT : Form
    {
        public frmSAT()
        {
            InitializeComponent();
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void Consulta()
        {
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:"+ Globales.RutaAspel + @"\SAT_CFDI.FDB; Dialect=3";
            string query = "";

            FbConnection confb = new FbConnection(connectionString);
            DataSet ctasDS = new DataSet();

            if (rbExelYSAT.Checked) // FACTURAS QUE APARECEN TANTO EN LA RELACION DE EXCEL COMO EN EL SAT
            {
                query = "SELECT c.nofac, c.fecha, c.total, c.emisor, c.receptor, c.uuid from excel AS e INNER JOIN cfdi AS c ON c.nofac = e.factura" + " WHERE c.fecha BETWEEN '"
                    + dtpFechaInicial.Value.Date.ToString("yyyy-MM-dd") + "' AND '"
                    + dtpFechaFinal.Value.Date.ToString("yyyy-MM-dd") + "'"; ;
            }
            else if (rbSATNoExcel.Checked) // FACTURAS QUE ESTAN EN EL SAT Y QUE NO APARECEN EN EL EXCEL
            {
                query = "SELECT c.nofac, c.fecha, c.total, c.emisor, c.receptor, c.uuid FROM excel AS e RIGHT JOIN cfdi AS c ON e.factura = c.nofac WHERE e.factura is null" + " AND c.fecha BETWEEN '"
                    + dtpFechaInicial.Value.Date.ToString("yyyy-MM-dd") + "' AND '"
                    + dtpFechaFinal.Value.Date.ToString("yyyy-MM-dd") + "'";
            }
            else if (rbExcelNoSAT.Checked) // FACTURAS QUE APARECEN EN EXCEL Y NO APARECEN EN EL SAT
            {
                query = "SELECT e.factura, e.tienda, e.comentario FROM excel AS e LEFT JOIN cfdi AS c ON e.factura = c.nofac WHERE c.nofac IS NULL";
            }
            else
            {
                return;
            }
            if (chIngreso.Checked && chEgresos.Checked)
            {
                query = query + " AND (tipo = 'ingreso' OR tipo = 'egreso')";
            }
            else if (chEgresos.Checked)
            {
                query = query + " AND tipo = 'egreso'";

            }
            else if (chIngreso.Checked)
            {
                query = query + " AND tipo = 'ingreso'";
            }
            else
            {
                gvSAT.DataSource = null;
                return;
            }
            if (chFC.Checked)
            {
                query = query + " AND c.nofac NOT LIKE 'FC%'";
            }
            try
            {

                FbDataAdapter adfb = new FbDataAdapter(query, confb);

                confb.Open();
                adfb.Fill(ctasDS, "CFDI");
                gvSAT.DataSource = ctasDS;
                gvSAT.DataMember = "CFDI";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la consulta datos. [79]");
                ctasDS.Clear();
            }

            ActualizaContador();
        }
        private void ActualizaContador()
        {
            tsslContador.Text = "Registros Encontrados: "+gvSAT.RowCount.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            if (gvSAT.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < gvSAT.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = gvSAT.Columns[i - 1].HeaderText;
                }
                for (int fila = 0; fila < gvSAT.Rows.Count; fila++)
                {
                    for (int columna = 0; columna < gvSAT.Columns.Count; columna++)
                    {
                        XcelApp.Cells[fila + 2, columna + 1] = gvSAT.Rows[fila].Cells[columna].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;

                // SE FORMATEA LA HOJA
                int columnas = 0;
                string columna_final, celda_final, filas = "";
                
                filas = (gvSAT.Rows.Count + 1).ToString();
                columnas = gvSAT.Columns.Count;
                columna_final = Convert.ToChar(gvSAT.Columns.Count + 64).ToString();
                celda_final = columna_final + (gvSAT.Rows.Count + 1).ToString();

                // SE FORMATEA EL ENCABEZADO
                XcelApp.Range["A1", columna_final + 1].EntireColumn.AutoFit();
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Size = 10;
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Bold = true;
                XcelApp.Range["A1", columna_final + 1].Cells.Interior.Color = Color.LightSkyBlue;

                // SE FORMATEA EL DETALLE
                XcelApp.Range["A1", celda_final].EntireColumn.AutoFit();
                XcelApp.Range["C1", "C" + filas].NumberFormat = "#,##.00";
                XcelApp.Range["A1", celda_final].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", celda_final].Cells.Font.Size = 10;
            }
            else
            {
                MessageBox.Show("No hay registros que visualizar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
