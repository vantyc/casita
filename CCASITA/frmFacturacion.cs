using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Excel = Microsoft.Office.Interop.Excel;


namespace LaCasita
{
    public partial class frmFacturacion : Form
    {
        public frmFacturacion()
        {
            InitializeComponent();
        }

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            rbFactura.Checked = true;
            CargaClientes();
            Consulta();
        }

        private void CargaClientes()
        {
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";

            FbConnection confb = new FbConnection(connectionString);

            try
            {

                string _query = @"SELECT CLAVE FROM CLIE01 ORDER BY CLAVE ASC";

                FbDataAdapter da = new FbDataAdapter(_query, confb);

                DataSet ds = new DataSet();
                confb.Open();
                da.Fill(ds);

                cbCliente.DataSource = ds.Tables[0];
                cbCliente.DisplayMember = "clave";
                cbCliente.ValueMember = "clave";
                cbCliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la carga de clientes.");
            }
            confb.Close();

        }

        private void Consulta()
        {
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";

            FbConnection confb = new FbConnection(connectionString);
            DataSet ctasDS = new DataSet();
            string botonRadio = "";
            string tabla = ""; //Establece si es una factura(tabla FACTF01) o una devolucion (tabla FACTD01)
            string fventa = "";

            tabla = chD.Checked == true ? "FACTD01" : "FACTF01";
            fventa = chD.Checked == true ? "F.FECHA_ENT" : "E.FECHA_RECEP";
            botonRadio = rbFactura.Checked ? "F.FECHA_DOC" : fventa;

            try
            {
                string tienda = "";
                string tipo   = "";

                // *** TIENDA
                tienda = chA.Checked == true ? tienda + " AND (F.CVE_DOC LIKE 'FA%'" : tienda;
                tienda = chC.Checked == true ? (tienda != "" ? tienda + " OR F.CVE_DOC LIKE 'FC%'" : " AND (F.CVE_DOC LIKE 'FC%'") : tienda;
                tienda = chM.Checked == true ? (tienda != "" ? tienda + " OR F.CVE_DOC LIKE 'FM%'" : " AND (F.CVE_DOC LIKE 'FM%'") : tienda;
                tienda = chP.Checked == true ? (tienda != "" ? tienda + " OR F.CVE_DOC LIKE 'FP%'" : " AND (F.CVE_DOC LIKE 'FP%'") : tienda;
                tienda = chT.Checked == true ? (tienda != "" ? tienda + " OR F.CVE_DOC LIKE 'FT%'" : " AND (F.CVE_DOC LIKE 'FT%'") : tienda;
                tienda = chD.Checked == true ? (tienda != "" ? tienda + " OR F.CVE_DOC LIKE 'D%'"  : " AND (F.CVE_DOC LIKE 'D%'") : tienda;
                tienda = tienda == "" ? tienda : tienda + ")";
                if (tienda == "")
                {
                    gvFacturas.DataSource = null;
                    tsslEncontrados.Text = "0";
                    return;
                }

                // *** TIPO

                tipo = chStsC.Checked == true ? tipo + " AND (F.STATUS = 'C'" : tipo;
                tipo = chStsE.Checked == true ? (tipo != "" ? tipo + " OR F.STATUS = 'E'" : " AND (F.STATUS = 'E'") : tipo;
                tipo = chStsO.Checked == true ? (tipo != "" ? tipo + " OR F.STATUS = 'O'" : " AND (F.STATUS = 'O'") : tipo;
                tipo = tipo == "" ? tipo : tipo + ")";

                if (tipo == "")
                {
                    gvFacturas.DataSource = null;
                    tsslEncontrados.Text = "0";
                    return;
                }

                string _query = @"SELECT 
                                F.CVE_DOC AS FACTURA, C.NOMBRE AS NOMBRE, F.STATUS AS STATUS, F.FECHA_DOC AS FECHA_FACTURA, F.IMPORTE AS CANTIDAD, 
                                F.IMP_TOT1 AS IEPS, " + fventa + " AS FECHA_VENTA from " + tabla +
                @" F LEFT JOIN INFENVIO01 E ON F.DAT_ENVIO = E.CVE_INFO 
                                LEFT JOIN CLIE01 C ON F.CVE_CLPV = C.CLAVE 
                                where "
                                + (cbCliente.Text.Trim() == ""?"":("C.CLAVE = '"+cbCliente.Text.Trim())+ "' AND ") + botonRadio + " BETWEEN '" + dtpFechaInicial.Value.Date.ToString("yyyy-MM-dd") + "' AND '"
                + dtpFechaFinal.Value.Date.ToString("yyyy-MM-dd") + "'" + tienda + tipo +
                " order by " + botonRadio;

                FbDataAdapter adfb = new FbDataAdapter(_query, confb);

                confb.Open();
                adfb.Fill(ctasDS, "FACTF01");
                gvFacturas.DataSource = ctasDS;
                gvFacturas.DataMember = "FACTF01";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la consulta datos.");
                ctasDS.Clear();
            }
            ActualizaContador();
        }

        private void chM_Click(object sender, EventArgs e)
        {
            if (chM.Checked == true |
                chA.Checked == true |
                chP.Checked == true |
                chT.Checked == true |
                chC.Checked == true)
                chD.Checked = false;

            Consulta();
        }

        private void chA_Click(object sender, EventArgs e)
        {
            if (chM.Checked == true |
                chA.Checked == true |
                chP.Checked == true |
                chT.Checked == true |
                chC.Checked == true)
                chD.Checked = false;
            Consulta();
        }

        private void chP_Click(object sender, EventArgs e)
        {
            if (chM.Checked == true |
                chA.Checked == true |
                chP.Checked == true |
                chT.Checked == true |
                chC.Checked == true)
                chD.Checked = false;
            Consulta();
        }

        private void chT_Click(object sender, EventArgs e)
        {
            if (chM.Checked == true |
                chA.Checked == true |
                chP.Checked == true |
                chT.Checked == true |
                chC.Checked == true)
                chD.Checked = false;
            Consulta();
        }

        private void chC_Click(object sender, EventArgs e)
        {
            if (chM.Checked == true |
                chA.Checked == true |
                chP.Checked == true |
                chT.Checked == true |
                chC.Checked == true)
                chD.Checked = false;
            Consulta();
        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
            Consulta();
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            Consulta();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enviarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvFacturas.Rows.Count > 0)
            {

                Excel.Application XcelApp = null;
                XcelApp = new Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < gvFacturas.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = gvFacturas.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < gvFacturas.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < gvFacturas.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = gvFacturas.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void enviarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El envío automático del reporte se liberará en breve.","En construcción");
        }

        

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Disponible Próximamente");
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            if (gvFacturas.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < gvFacturas.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = gvFacturas.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < gvFacturas.Rows.Count; i++)
                {
                    for (int j = 0; j < gvFacturas.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = gvFacturas.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;

                // SE FORMATEA LA HOJA
                int columnas = 0;
                string columna_final, celda_final, filas = "";
                
                filas = (gvFacturas.Rows.Count + 1).ToString();
                columnas = gvFacturas.Columns.Count;
                columna_final = Convert.ToChar(gvFacturas.Columns.Count + 64).ToString();
                celda_final = columna_final + (gvFacturas.Rows.Count + 1).ToString();

                // SE FORMATEA EL ENCABEZADO
                XcelApp.Range["A1", columna_final + 1].EntireColumn.AutoFit();
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Size = 10;
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Bold = true;
                XcelApp.Range["A1", columna_final + 1].Cells.Interior.Color = Color.LightSkyBlue;

                // SE FORMATEA EL DETALLE
                XcelApp.Range["A1", celda_final].EntireColumn.AutoFit();
                XcelApp.Range["E1", "F" + filas].NumberFormat = "#,##.00";
                XcelApp.Range["A1", celda_final].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", celda_final].Cells.Font.Size = 10;
            }
            else
            {
                MessageBox.Show("No hay registros que visualizar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ActualizaContador()
        {
            tsslEncontrados.Text = gvFacturas.RowCount.ToString();
        }

        private void rbVenta_Click(object sender, EventArgs e)
        {
            Consulta();
        }
        private void rbFactura_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void chD_Click(object sender, EventArgs e)
        {
            if(chD.Checked==true)
            {
                chM.Checked = false;
                chA.Checked = false;
                chP.Checked = false;
                chT.Checked = false;
                chC.Checked = false;
            }
            Consulta();
        }

        private void chTipoC_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void chTipoE_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void chTipoO_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void cbCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
           // Consulta();
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            Consulta();
        }

        private void cbCliente_Leave(object sender, EventArgs e)
        {
            Consulta();
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Consulta();
            }
        }
    }
}
