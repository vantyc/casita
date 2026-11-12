using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySqlConnector;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;

namespace LaCasita
{
    public partial class frmWEB : Form
    {
        string connectionString = @"server=" + Globales.URL + "; database=siilcp_siidb; uid=siilcp_usr; pwd=siilCp9002";
      
        public frmWEB()
        {
            InitializeComponent();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWEB_Load(object sender, EventArgs e)
        {
            chEfectivo.Checked = true;
            chTarjeta.Checked = true;
            chTransfer.Checked = true;
            chOtro.Checked = true;
            //rbPrevio.Checked = true;
            chTVGranel.Checked = true;
            chTVEtiqueta.Checked = true;
            chTVPedidos.Checked = true;
            ActualizaTransferencias();
            crystalReportViewer1.ShowLogo = false;
        }
        private void ConsultaCortes(bool lineas)
        {
            // VALIDACIONES PRELIMINARES
            // LA FECHA INICIAL NO PUEDE SER MAYOR A LA FECHA FINAL
            if (dtpFechaF.Value < dtpFechaI.Value)
            {
                MessageBox.Show("Error la fecha final no puede ser menor a la fecha inicial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaI.Focus();
                return;
            }

            // CORTE FISCAL
            if (chFiscal.Checked == true)
            {
                //chMA.Checked = true;

                // SE MARCA LA TARDE PORQUE TODA LA OPERACION DE LA MAÑANA DESAPARECE
                rbTarde.Checked = true;
            }

            if (Globales.URL == "")
            {
                MessageBox.Show("No se especificó la URL de la base de datos.\nVaya a la sección de configuración del menú principal y especifique la URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object resultado;
            string _query = "";
            string sucursalStr = "";
            string pago = "";
            string tipo = "";
            string _TiempoCorteTarde = "";
            string fechai = "", fechaf = "";
            string fechaI = "", fechaF = "";
            fechaI = dtpFechaI.Value.Year.ToString() + "/" + dtpFechaI.Value.Month.ToString() + "/" + dtpFechaI.Value.Day.ToString() + " 00:00:00";
            fechaF = dtpFechaF.Value.Year.ToString() + "/" + dtpFechaF.Value.Month.ToString() + "/" + dtpFechaF.Value.Day.ToString() + " 23:59:59";

            MySqlConnection con = new MySqlConnection(connectionString);
            DataSet cortesDS = new DataSet();
            MySqlCommand com = new MySqlCommand(_query, con);
            //MySqlDataReader dr;
            MySqlDataAdapter ad = new MySqlDataAdapter(_query, con);


            try
            {
                // SE ABRE LA CONEXION PARA TODO EL LOOP DE DIAS DEL CICLO FOREACH
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la base de datos.\n"+ex.Message,"Error (82)");
            }

            //decimal? totalDescuentos = null;
            decimal totalDescuentos = 0;
            foreach (DateTime day in EachDay(Convert.ToDateTime(fechaI), Convert.ToDateTime(fechaF)))
            {
                sucursalStr = "";
                pago = "";
                tipo = "";
             

                fechai = day.Year.ToString() + "/" + day.Month.ToString() + "/" + day.Day.ToString() + " 00:00:00";
                fechaf = day.Year.ToString() + "/" + day.Month.ToString() + "/" + day.Day.ToString() + " 23:59:59";

                // *** SUCURSAL
                sucursalStr = chMA.Checked == true ? "( nu_sucursal = 1 " : sucursalStr;
                sucursalStr = sucursalStr == "" ? (chAV.Checked == true ? " ( nu_sucursal = 2" : sucursalStr) : (chAV.Checked == true ? sucursalStr + " OR nu_sucursal = 2 " : sucursalStr);
                sucursalStr = sucursalStr == "" ? (chPA.Checked == true ? " ( nu_sucursal = 3" : sucursalStr) : (chPA.Checked == true ? sucursalStr + " OR nu_sucursal = 3 " : sucursalStr);
                sucursalStr = sucursalStr == "" ? (chTO.Checked == true ? " ( nu_sucursal = 4" : sucursalStr) : (chTO.Checked == true ? sucursalStr + " OR nu_sucursal = 4 " : sucursalStr);
                sucursalStr = sucursalStr == "" ? "" : sucursalStr + " ) "; 

                if (sucursalStr == "")
                {
                    MessageBox.Show("Debe escoger por lo menos 1 sucursal para consultar el corte.","Error (99)");
                    return;
                }

                // *** TIPO DE CORTE
                //tipo = rbPrevio.Checked == true ? " nu_corte = '1'" : tipo;
                tipo = rbManana.Checked == true ? (tipo == "" ? " nu_corte = '2'" : tipo + " OR nu_corte = '2'") : tipo;
                tipo = rbTarde.Checked == true ? (tipo == "" ? " nu_corte = '3'" : tipo + " OR nu_corte = '3'") : tipo;
                tipo = rbFinal.Checked == true ? (tipo == "" ? " nu_corte = '4'" : tipo + " OR nu_corte = '4'") : tipo;
                tipo = tipo == "" ? tipo : " AND (" + tipo + ")";
                if (tipo.Trim() == "")
                {
                    MessageBox.Show("Elija al menos un tipo de corte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //rbPrevio.Focus();
                    rbManana.Focus();
                    return;
                }

                // *** FORMA DE PAGO
                pago = chEfectivo.Checked == true ? "( tp_pago = 1 " : pago;
                pago = pago == "" ? (chTarjeta.Checked == true ? "( tp_pago = 2 " : pago) : (chTarjeta.Checked == true ? pago + " OR tp_pago = 2 " : pago);
                pago = pago == "" ? (chOtro.Checked == true ? "( tp_pago = 3 " : pago) : (chOtro.Checked == true ? pago + " OR tp_pago = 3 " : pago);
                pago = pago == "" ? (chTransfer.Checked == true ? "( tp_pago = 4 " : pago) : (chTransfer.Checked == true ? pago + " OR tp_pago = 4 " : pago);
                pago = pago == "" ? "" : pago + " ) ";

                if (pago.Trim() == "")
                {
                    MessageBox.Show("Elija al menos una forma de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chEfectivo.Focus();
                    return;
                }

                //** PRIMERO SER VERIFICA EL TIPO DE CORTE SOLICITADO: mañana tarde y final dependen de que se haya hecho el de la tarde
                //** por lo tanto se va a validar si existe corte de la tarde y en caso de existir se tomara la hora para dividir el query en MAÑANA, TARDE
                //** y realizar el final a menos que sean las 23:59:59 o menos.
                // EJEMPLO: select fh_corte from corte where nu_corte=2 and nu_sucursal=2 and st_corte='EJ' and fh_alta between '2016/11/01 00:00:00' and '2016/11/01 23:59:59'

                bool HayCorte = false;

                if (rbManana.Checked || rbTarde.Checked || rbFinal.Checked)
                {
                    // Mismo rango que la consulta de cortes de SII (CorteReporteFact.getRangoHistorico): la mañana
                    // termina un minuto después del corte de la mañana ejecutado y la tarde empieza en ese momento.
                    _query = "select DATE_FORMAT(DATE_ADD(fh_corte, INTERVAL 1 MINUTE), '%Y/%m/%d %H:%i:%s') from siilcp_siidb.corte where nu_corte = 2 and " + sucursalStr + " and st_corte = 'EJ' and fh_corte between '" + fechai + "' AND '" + fechaf + "' order by fh_corte limit 1";
                    com = new MySqlCommand(_query, con);
                    try
                    {
                        //resultado = com.ExecuteScalar();
                        // http://stackoverflow.com/questions/5440168/exception-there-is-already-an-open-datareader-associated-with-this-connection-w
                        using (MySqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                _TiempoCorteTarde = dr[0].ToString();
                                HayCorte = true;
                            }
                            else
                            {
                                MessageBox.Show("No se ha realizado el corte de la tarde en esta sucursal.\nNo se puede consultar esta informacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dr.Close();
                                dr.Dispose();
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo consultar la tabla de cortes.\n" + ex.Message, "Error 154", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // En SII la tarde termina en el corte de la tarde ejecutado; si todavía no existe, al final del día.
                string _TiempoCorteTardeFin = "";
                if (rbTarde.Checked && HayCorte && chFiscal.Checked == false)
                {
                    _query = "select DATE_FORMAT(fh_corte, '%Y/%m/%d %H:%i:%s') from siilcp_siidb.corte where nu_corte = 3 and " + sucursalStr + " and st_corte = 'EJ' and fh_corte between '" + fechai + "' AND '" + fechaf + "' order by fh_corte limit 1";
                    com = new MySqlCommand(_query, con);
                    try
                    {
                        resultado = com.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                            _TiempoCorteTardeFin = resultado.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo consultar la tabla de cortes.\n" + ex.Message, "Error 155", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (rbManana.Checked && HayCorte)
                {
                    fechaf = _TiempoCorteTarde;
                }
                if (rbTarde.Checked && HayCorte)
                { 
                    fechai = _TiempoCorteTarde;
                    if (_TiempoCorteTardeFin != "")
                        fechaf = _TiempoCorteTardeFin;
                }

                // Estatus que SII suma en el corte; las ventas canceladas (C) no entran.
                string estatusVenta = "('PG','PA','PL','N','PP')";
                string filtroMostrador = "v.fh_alta between '" + fechai + "' AND '" + fechaf + "' AND " + sucursalStr.Replace("nu_sucursal", "v.nu_sucursal") + " AND " + pago.Replace("tp_pago", "v.tp_pago") + " AND v.st_venta in " + estatusVenta + " AND IFNULL(v.nu_pedido, 0) = 0";
                string granel = "", etiquetas = "", pedidos = "";
                _query = "";
                // QUERY PARA PRODUCTOS A GRANEL
                // La tasa de IEPS se corrige con la misma regla del botón "Corrige IEPS" (frmConfiguracion),
                // para que el reporte no dependa de que alguien lo haya apretado.
                if (chTVGranel.Checked == true)
                {
                    granel = @"(select
                        v.nu_venta, 
                        vp.nu_producto as product, 
                        g.ds_grupo as grupo, 
                        v.tp_pago, 
                        vp.nu_cantidad, 
                        p.ds_producto as descrip, 
                        cast(vp.nu_subtotal*(1-1/(1+IF(vp.tp_impuesto <> 8 AND p.tp_impuesto > 0 AND p.nu_indica_etiquetado = 1, p.tp_impuesto, vp.tp_impuesto)/100)) as decimal(36,2)) as ieps,
                        cast(vp.nu_subtotal as decimal(36,2)) as subtotal
                        from siilcp_siidb.venta as v
                        inner join siilcp_siidb.venta_producto as vp on vp.nu_venta = v.nu_venta
                        left join siilcp_siidb.producto as p on p.nu_producto = vp.nu_producto
                        left join siilcp_siidb.grupo as g on g.nu_grupo = p.nu_grupo
                        where " + filtroMostrador + ")";
                    _query = granel;
                }
                // QUERY PARA PRODUCTOS CON ETIQUETA: SII los guarda en venta_etiqueta, no en venta_producto
                if (chTVEtiqueta.Checked == true)
                {
                    etiquetas = @"(select
                        v.nu_venta, 
                        e.nu_producto as product, 
                        g.ds_grupo as grupo, 
                        v.tp_pago, 
                        ve.nu_cantidad, 
                        p.ds_producto as descrip, 
                        cast(ve.nu_subtotal*(1-1/(1+IF(ve.tp_impuesto > 0, ve.tp_impuesto, IFNULL(p.tp_impuesto, 0))/100)) as decimal(36,2)) as ieps,
                        cast(ve.nu_subtotal as decimal(36,2)) as subtotal
                        from siilcp_siidb.venta as v
                        inner join siilcp_siidb.venta_etiqueta as ve on ve.nu_venta = v.nu_venta
                        left join siilcp_siidb.etiqueta as e on e.nu_etiqueta = ve.nu_etiqueta
                        left join siilcp_siidb.producto as p on p.nu_producto = e.nu_producto
                        left join siilcp_siidb.grupo as g on g.nu_grupo = p.nu_grupo
                        where " + filtroMostrador + ")";
                    _query = _query == "" ? etiquetas : _query + " UNION ALL " + etiquetas;
                }
                // QUERY PARA PEDIDOS
                if (chTVPedidos.Checked == true)
                {
                    pedidos = @"(select 
                               v2.nu_venta, 
                               '' as product, 
                               '{0}' as grupo, 
                               v2.tp_pago, 
                               '1' as nu_cantidad, 
                               concat('Pedido: ', (select p.nu_pedido_sucursal FROM siilcp_siidb.pedido as p where (p.nu_pedido = v2.nu_pedido) and p.st_pedido<>'C')) as descrip, 
                               IFNULL(v2.nu_impuesto1, 0) as ieps,
                               cast(v2.nu_total as decimal(36, 2)) as subtotal
                               from siilcp_siidb.venta as v2 where " + sucursalStr + "  AND v2.fh_alta between '" + fechai + "' AND '" + fechaf + "' AND IFNULL(v2.nu_pedido, 0) <> 0 AND v2.st_venta in " + estatusVenta + " AND " + pago + ")";
                    pedidos = string.Format(pedidos, Globales.CatPedidos == "" ? "Pedidos" : Globales.CatPedidos);
                    _query = _query == "" ? pedidos : _query + " UNION ALL " + pedidos;
                }
                if (_query == "")
                {
                    MessageBox.Show("Elija al menos un tipo de venta.", "Error");
                    chTVGranel.Focus();
                    return;
                }
                else
                {
                    _query = _query + " ORDER BY grupo ASC, descrip ASC";
                }
                // MessageBox.Show(_query, "QUERY (253)");
                // return;

                try
                {
                    ad = new MySqlDataAdapter(_query, con);
                    ad.Fill(cortesDS, "venta");
                    gvCortes.DataSource = cortesDS;

                    gvCortes.DataMember = "venta";

                    gvCortes.Columns["subtotal"].DefaultCellStyle.Format = "0.00##";
                    gvCortes.Columns["subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    gvCortes.Columns["ieps"].DefaultCellStyle.Format = "0.00##";
                    gvCortes.Columns["ieps"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    ActualizaContadorCortes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+" _query: "+_query,"Error al realizar la consulta. 275");
                    cortesDS.Clear();
                }

                //************** BLOQUE DE  CONSULTA DE DESCUENTOS INICIO
                // Mismo rango y estatus que los renglones del reporte, sumando cada día del ciclo.
                _query = "select cast(sum(nu_cantidad_descuento) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + fechai + "' AND '" + fechaf + "' AND st_venta in " + estatusVenta + " AND " + pago;
                com = new MySqlCommand(_query, con);
                try
                {
                    resultado = com.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                        totalDescuentos += (decimal)resultado;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al realizar la consulta. 288");
                    cortesDS.Clear();
                }
                fechai = dtpFechaI.Value.Year.ToString() + "/" + dtpFechaI.Value.Month.ToString() + "/" + dtpFechaI.Value.Day.ToString() + " 00:00:00";
                fechaf = dtpFechaF.Value.Year.ToString() + "/" + dtpFechaF.Value.Month.ToString() + "/" + dtpFechaF.Value.Day.ToString() + " 23:59:59";
                //************** BLOQUE DE  CONSULTA DE DESCUENTOS FINAL
            }

            // SE CERRA LA CONEXIÓN DE DB TERMINADO EL FOREACH
            con.Close();
/*
            //************** BLOQUE DE  CONSULTA DE DESCUENTOS INICIO
            // SE VAN A CONSULTAR LOS DESCUENTOS
            fechai = dtpFechaI.Value.Year.ToString() + "/" + dtpFechaI.Value.Month.ToString() + "/" + dtpFechaI.Value.Day.ToString() + " 00:00:00";
            fechaf = dtpFechaF.Value.Year.ToString() + "/" + dtpFechaF.Value.Month.ToString() + "/" + dtpFechaF.Value.Day.ToString() + " 23:59:59";
            decimal? totalDescuentos = null;
            // SI EL REPORTE SE PIDE DE LA MAÑANA EL DESCUENTO SERA LA SUMA DE LOS DESCUENTOS DE LA HORA CERO DEL DIA HASTA LA HORA DEL CORTE DE LA TARDE
            // SI EL REPORTE SE PIDE DE LA TARDE EL DESCUENTO TOTAL SERA LA SUMA DE LOS DESCUENTOS DE LA HORA DE CORTE A LA HORA ULTIMA DEL DIA
            // SI EL REPORTE SE PIDE FINAL EL DESCUENTO TOTAL SERA DEL LA HORA PRIMERA A LA HORA ULTIMA DEL DIA EN CUESTION

            if(rbManana.Checked)
                _query = "select cast(sum(nu_cantidad_descuento) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + fechai + "' AND '" + _TiempoCorteTarde + "' AND " + pago;
            else if(rbTarde.Checked)
                _query = "select cast(sum(nu_cantidad_descuento) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + _TiempoCorteTarde + "' AND '" + fechaf + "' AND " + pago;
            else
                _query = "select cast(sum(nu_cantidad_descuento) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + fechai + "' AND '" + fechaf + "' AND " + pago;
            com = new MySqlCommand(_query, con);
            try
            {
                con.Open();
                resultado = com.ExecuteScalar();
                if (resultado.GetType() != typeof(DBNull))
                {
                    totalDescuentos = (decimal?)resultado;
                }
                else
                    totalDescuentos = 0;
                con.Close();
                //MessageBox.Show("Consulta Finalizada", "Busqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la consulta. 241");
                cortesDS.Clear();
            }
            //************** BLOQUE DE  CONSULTA DE DESCUENTOS FINAL*/

            // EN EL CORTE FISCAL SE TIENE QUE:
            // 1.- EL TOTAL DEL DIA ES IGUAL A LA VENTA DE LA TARDE
            // 2.- EL TOTAL DEL DIA DE TARJETAS NO SE ALTERA = TARJETAS MAÑANA + TARJETAS TARDE
            // 3.- EL TOTAL DE EFECTIVO DEL DIA = (EFECTIVO DE LA TARDE) - (TARJETAS DE LA MAÑANA) SIEMPRE QUE ET > TM

            // SE VA A CONSULTAR EL EFECTIVO DE LA TARDE
            decimal? efectivoTarde = null;
            _query = "select cast(sum(nu_total) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + _TiempoCorteTarde + "' AND '" + fechaf + "' AND " + "tp_pago = 1 ";
            com = new MySqlCommand(_query, con);
            try
            {
                con.Open();
                resultado = com.ExecuteScalar();
                if (resultado.GetType() != typeof(DBNull))
                {
                    efectivoTarde = (decimal?)resultado;
                }
                else
                    efectivoTarde = 0;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la consulta. 309");
                cortesDS.Clear();
            }

            // SE VA A CONSULTAR LAS TARJETAS DE LA MAÑANA
            decimal? tarjetasManana = null;
            _query = "select cast(sum(nu_total) as decimal(36, 2)) from siilcp_siidb.venta where " + sucursalStr + "  AND fh_alta between '" + fechai + "' AND '" + _TiempoCorteTarde + "' AND " + "tp_pago = 2 ";
            com = new MySqlCommand(_query, con);
            try
            {
                con.Open();
                resultado = com.ExecuteScalar();
                if (resultado.GetType() != typeof(DBNull))
                {
                    tarjetasManana = (decimal?)resultado;
                }
                else
                    tarjetasManana = 0;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al realizar la consulta. 309");
                cortesDS.Clear();
            }

            sucursalStr = "";
            sucursalStr = chMA.Checked == true ? "Miguel Angel" : sucursalStr;
            sucursalStr = sucursalStr == "" ? (chAV.Checked == true ? "Avenida México" : sucursalStr) : (chAV.Checked == true ? sucursalStr + ", Avenida México" : sucursalStr);
            sucursalStr = sucursalStr == "" ? (chPA.Checked == true ? "Parroquia" : sucursalStr) : (chPA.Checked == true ? sucursalStr + ", Parroquia" : sucursalStr);
            sucursalStr = sucursalStr == "" ? (chTO.Checked == true ? "Torres" : sucursalStr) : (chTO.Checked == true ? sucursalStr + ", Torres" : sucursalStr);
            sucursalStr = sucursalStr == "" ? "" : sucursalStr;
            GeneraReporte(sucursalStr, lineas,efectivoTarde,tarjetasManana, totalDescuentos);
        }
       
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private void GeneraReporte(string sucursal, bool lineas,decimal? efectivoTarde, decimal? tarjetasManana, decimal? totalDescuentos)
        {
            DataSet1 ds = new DataSet1();
            string fecha = "";

            fecha = dtpFechaF.Text == "" ? dtpFechaI.Text:dtpFechaI.Text+" al "+dtpFechaF.Text ;
            int filas = gvCortes.Rows.Count;
            string tipoCorte = "";

            if (rbFinal.Checked == true)
                tipoCorte = "Final";
            if (rbManana.Checked == true)
                tipoCorte = "Mañana";
            if (rbTarde.Checked == true)
                tipoCorte = "Tarde";
            //if (rbPrevio.Checked == true)
            //    tipoCorte = "Previo";
            if (chFiscal.Checked == true)
                tipoCorte = "Fiscal";

            for (int i = 0; i <= filas - 1; i++)
            {
                ds.Tables[0].Rows.Add
                    (new object[] {
                     gvCortes[2,i].Value.ToString(),        // 1 categoria
                     Convert.ToDouble(gvCortes[4,i].Value), // 2 cantidad
                     gvCortes[5,i].Value.ToString(),        // 3 descripcion
                     Convert.ToDouble(gvCortes[6,i].Value), // 4 ieps
                     Convert.ToDouble(gvCortes[7,i].Value), // 5 subtotal
                     gvCortes[3,i].Value.ToString(),        // 6 tipo de pago tp_pago
                     tipoCorte,                             // 7 tipo de corte
                     totalDescuentos,                       // 8 descuentos
                     sucursal,                              // 9 sucursales
                     fecha,                                 // 10 fecha
                     efectivoTarde>tarjetasManana?efectivoTarde-tarjetasManana:0// 11 Efectivo Tarde
                    });
            }
            
            ReportDocument cRep = new ReportDocument();
            if(lineas)
            {
                cRep.Load("CRLineas.rpt");
            }
            else
            {
                if (chFiscal.Checked == true)
                {
                    cRep.Load("CRCorteFinalMA.rpt");
                }
                else
                {
                    try
                    {
                        cRep.Load("CRCorte.rpt");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Error al Carga del Reporte de Cortes (425)");
                    }
                }
            }
            cRep.SetDataSource(ds);
            crystalReportViewer1.ReportSource=cRep;
        }
        private void ActualizaContadorCortes()
        {
            tsCortesEncontrados.Text = gvCortes.RowCount.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btBuscarCortes_Click(object sender, EventArgs e)
        {
            ConsultaCortes(false);
        }
        private void tsbExcelCorte_Click(object sender, EventArgs e)
        {
            ExportaExcel(gvCortes);
        }
        private void ExportaExcel(DataGridView gv)
        {
            if (gv.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < gv.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = gv.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    for (int j = 0; j < gv.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = gv.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;


                // SE FORMATEA LA HOJA
                int columnas = 0;
                string columna_final, celda_final, filas = "";

                filas = (gv.Rows.Count + 1).ToString();
                columnas = gv.Columns.Count;
                columna_final = Convert.ToChar(gv.Columns.Count + 64).ToString();
                celda_final = columna_final + (gv.Rows.Count + 1).ToString();

                // SE FORMATEA EL ENCABEZADO
                XcelApp.Range["A1", columna_final + 1].EntireColumn.AutoFit();
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Size = 10;
                XcelApp.Range["A1", columna_final + 1].Cells.Font.Bold = true;
                XcelApp.Range["A1", columna_final + 1].Cells.Interior.Color = Color.LightSkyBlue;

                // SE FORMATEA EL DETALLE
                XcelApp.Range["A1", celda_final].EntireColumn.AutoFit();
                XcelApp.Range["D1", "D"+filas].NumberFormat = "#,##.00";
                XcelApp.Range["A1", celda_final].Cells.Font.Name = "Calibri";
                XcelApp.Range["A1", celda_final].Cells.Font.Size = 10;

                // SE AGREGAN FILTROS
                //sheet.Cells["A RANGE HERE"].AutoFilter = true;
            }
            else
            {
                MessageBox.Show("No hay registros que visualizar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void chFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if (chFiscal.Checked == true)
            {
                chMA.Checked = true;
                chAV.Checked = false;
                chPA.Checked = false;
                chTO.Checked = false;

                rbFinal.Checked = true;
                //dtpFechaF.Value = dtpFechaI.Value;
            }
        }
        private void btLineas_Click(object sender, EventArgs e)
        {
            ConsultaCortes(true);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ConsultaCortes(false);
        }
        private void tsbLineas_Click(object sender, EventArgs e)
        {
            ConsultaCortes(true);
        }
        private void chMA_Click(object sender, EventArgs e)
        {
            if (chMA.Checked == false)
            {
                chFiscal.Checked = false;
            }
        }
        private void chAV_Click(object sender, EventArgs e)
        {
            if (chAV.Checked == true)
            {
                chFiscal.Checked = false;
            }
        }
        private void chPA_Click(object sender, EventArgs e)
        {
            if (chPA.Checked == true)
            {
                chFiscal.Checked = false;
            }
        }
        private void chTO_Click(object sender, EventArgs e)
        {
            if (chTO.Checked == true)
            {
                chFiscal.Checked = false;
            }
        }
        private void rbManana_Click(object sender, EventArgs e)
        {
            if (rbManana.Checked == true)
            {
                chFiscal.Checked = false;
            }
        }
        private void rbTarde_Click(object sender, EventArgs e)
        {
            if (rbTarde.Checked == true)
            {
                chFiscal.Checked = false;
            }
        }
        private void dtpFechaI_ValueChanged(object sender, EventArgs e)
        {
            if(dtpFechaI.Value!=dtpFechaF.Value)
            {
                chFiscal.Checked = false;
            }
        }
        private void dtpFechaF_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaI.Value != dtpFechaF.Value)
            {
                chFiscal.Checked = false;
            }
        }
        private void ActualizaTransferencias()
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                con.Open();
                MySqlDataAdapter daC = new MySqlDataAdapter("SELECT DATE_FORMAT(fh_alta,'%Y-%m-%d') as FECHA, nu_total as IMPORTE, nu_impuesto1 as IEPS, nu_venta FROM venta WHERE tp_pago = 4 order by FECHA asc",con);
                DataSet dsC = new DataSet();
                daC.Fill(dsC, "edocta");
                dgvTransferencias.DataSource = dsC;
                dgvTransferencias.DataMember = "edocta";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error (618)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            };
        }
        private void tsbAgregar_Click(object sender, EventArgs e)
        {
            frmTransferencia trans = new frmTransferencia();
            trans.FormClosing += new FormClosingEventHandler(trans_FormClosing);
            trans.ShowDialog();
        }
        private void trans_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmTransferencia ftransfer = sender as frmTransferencia;
            if (ftransfer.DialogResult == DialogResult.OK)
            {
                ActualizaTransferencias();
            }
        }
        private void tsbSalirT_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            ActualizaTransferencias();
        }
        private void tsbCortes_Click(object sender, EventArgs e)
        {
            ConsultaCortes(false);
        }

        private void dgvTransferencias_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int nu_venta = Convert.ToInt32(dgvTransferencias.Rows[e.RowIndex].Cells["nu_venta"].Value);

            // Se crea una nueva instancia de Transferencia pasandole un argumento tipo int 
            // hay un constructor que al recibir un argumento el formulario
            // hará una consulta a la tabla venta para obtener ese registro y desplegarlos en el formulario                                                                                                                           
            
            //frmTransferencia frmEditar = new frmTransferencia(nu_venta);
            //frmEditar.MdiParent = this.ParentForm;
            //frmEditar.Show();
            
            frmTransferencia trans = new frmTransferencia(nu_venta);
            trans.FormClosing += new FormClosingEventHandler(trans_FormClosing);
            trans.ShowDialog();
        }
    }
}
