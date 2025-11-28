using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LaCasita
{
    public partial class frmTransferencia : Form
    {
        private int? _nu_venta;

        public frmTransferencia()
        {
            InitializeComponent();
        }

        public frmTransferencia(int nu_venta)
            : this()
        {
            _nu_venta = nu_venta;
        }

        //string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";
        string connectionString = @"server=" + Globales.URL + "; database=siilcp_siidb; uid=siilcp_usr; pwd=siilCp9002";
        string _query = "";
        object resultado;
        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (_nu_venta.HasValue)
                ActualizaTransferencia();
            else
                CreaNuevaTransferencia();
        }

        private void CreaNuevaTransferencia() {

            if (txtTotal.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar un monto");
                txtTotal.Focus();
                return;
            }
            else if (cbProducto.Text == "")
            {
                MessageBox.Show("Debe asociar un producto a esta transferencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbProducto.Focus();
                return;
            }
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlCommand com = con.CreateCommand();
            //string fecha = dtpFecha.Value.Year.ToString() + "/" + dtpFecha.Value.Month.ToString() + "/" + dtpFecha.Value.Day.ToString() + " " + DateTime.Now.ToString("HH:mm:ss");
            string fecha = dtpFecha.Value.Year.ToString() + "/" + dtpFecha.Value.Month.ToString() + "/" + dtpFecha.Value.Day.ToString() + " 23:59:59";
            decimal ieps = txtIEPS.Text == "" ? 0 : Convert.ToDecimal(txtIEPS.Text);
            //decimal importe = txtImporte.Text == "" ? 0 : Convert.ToDecimal(txtImporte.Text);
            //decimal total = importe + ieps;
            decimal total = txtTotal.Text == "" ? 0 : Convert.ToDecimal(txtTotal.Text);
            decimal importe = total - ieps;
            decimal tasaIEPS = total == 0 ? 0 : (1 / (1 - ieps / total) - 1) * 100;

            //  SE PREPARA EL ALTA DE LA VENTA EN LA TABLA VENTA
            //_query = "insert into venta (nu_venta,nu_venta_sucursal,nu_sucursal, st_venta, nu_total, nu_impuesto1, nu_cantidad_descuento, nu_usralta, tp_pago, nu_descuento, nu_usrmod,fh_alta,fh_mod,ds_comentario) values((SELECT MAX(coco1.nu_venta)+1 FROM venta as coco1),(SELECT max(coco2.nu_venta_sucursal)+1 FROM venta as coco2 where coco2.nu_sucursal=1 order by coco2.nu_venta_sucursal desc limit 1),1,'PG', @Importe, @IEPS,0,1,4,0,1,@Fecha, @Fecha,'Transferencia')";
            _query = "insert into venta (nu_venta,nu_venta_sucursal,nu_sucursal, st_venta, nu_total, nu_subtotal, nu_impuesto1, nu_cantidad_descuento, nu_usralta, tp_pago, nu_descuento, nu_usrmod,fh_alta,fh_mod,ds_comentario)" +
                " values((SELECT MAX(coco1.nu_venta)+1 " +
                "FROM venta as coco1),(SELECT max(coco2.nu_venta_sucursal)+1 FROM venta as coco2 where coco2.nu_sucursal=1 order by coco2.nu_venta_sucursal desc limit 1),1,'PG', " + total + ", " + importe + ", " + ieps + ", 0, 1, 4, 0, 1, @Fecha, @Fecha,'Transferencia')";
            com.CommandText = _query;
            /*com.Parameters.AddWithValue("@Importe", txtImporte.Text);
            com.Parameters.AddWithValue("@IEPS", txtIEPS.Text == "" ? "0" : txtIEPS.Text);*/
            com.Parameters.AddWithValue("@Fecha", fecha);

            try
            {
                con.Open();
                resultado = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar la transferencia. 56");
                // NO SE CIERRA LA CONEXION PARA OBTENER EL ID GENERADO
                //con.Close();
            }
            finally
            {
                // NO SE CIERRA LA CONEXION PARA OBTENER EL ID GENERADO// NO SE CIERRA LA CONEXION PARA OBTENER EL ID GENERADO
                // con.Close();
            }
            // NO SE CIERRA LA CONEXION PARA OBTENER EL ID GENERADO
            // con.Close();

            //  SE VA A CONSULTAR EL ID GENERADO
            int ultimoID = 0;
            try
            {
                object obj = MySqlHelper.ExecuteScalar(con, "select LAST_INSERT_ID()");
                if (obj != null)
                {
                    ulong temp = (ulong)obj;
                    ultimoID = (int)temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al obtener el ultimo id. 65");
                con.Close();
            }

            com.Dispose();

            MySqlCommand com2 = con.CreateCommand();
            //  SE PREPARA EL ALTA DE LA VENTA EN  LA TABLA VENTA_PRODUCTOS
            string producto = cbProducto.SelectedValue.ToString();
            _query = @"INSERT INTO venta_producto (nu_venta, nu_producto, nu_cantidad, nu_subtotal, nu_total, tp_impuesto, nu_impuesto1, nu_descuento, nu_cantidad_descuento, nu_usralta, fh_alta) values((select nu_venta from venta order by nu_venta desc limit 1), " + producto + ",1 ,@Importe , @Total, @tasaIEPS, @IEPS, 0, 0, 1, @Fecha)";
            //com.Parameters.Clear();

            com2.CommandText = _query;
            com2.Parameters.AddWithValue("@Importe", txtTotal.Text);
            com2.Parameters.AddWithValue("@Total", total.ToString());
            com2.Parameters.AddWithValue("@tasaIEPS", tasaIEPS.ToString());
            com2.Parameters.AddWithValue("@IEPS", ieps.ToString());
            com2.Parameters.AddWithValue("@Fecha", fecha);

            try
            {
                con.Open();
                resultado = com2.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar la Transferencia en VENTA_PRODUCTO. 105");
            }


            con.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ActualizaTransferencia()
        {

            // SE DEBEN ACTUALIZAR DOS TABLAS: 1. VENTA(nu_venta) Y 2. VENTA_PRODUCTO(nu_venta, nu_producto)
            
            // ACTUALIZANDO VENTA
            string fecha = dtpFecha.Value.Year.ToString() + "/" + dtpFecha.Value.Month.ToString() + "/" + dtpFecha.Value.Day.ToString() + " 23:59:59";
            decimal ieps = txtIEPS.Text == "" ? 0 : Convert.ToDecimal(txtIEPS.Text);
            decimal total = txtTotal.Text == "" ? 0 : Convert.ToDecimal(txtTotal.Text);
            decimal importe = total - ieps;
            decimal tasaIEPS = total == 0 ? 0 : (1 / (1 - ieps / total) - 1) * 100;

            string query = @"UPDATE venta  set 
                                    nu_total                = @total                  ,
                                    fh_alta                 = @fh_alta                ,
                                    nu_impuesto1            = @nu_impuesto1           ,
                                    nu_subtotal             = @nu_subtotal            
	  	                            WHERE 
                                            nu_venta =  @nu_venta";

            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlCommand com = new MySqlCommand(query, con);

            com.Parameters.AddWithValue("@total", total);
            com.Parameters.AddWithValue("@fh_alta", fecha);
            com.Parameters.AddWithValue("@nu_impuesto1", ieps);
            com.Parameters.AddWithValue("@nu_subtotal", importe);
            com.Parameters.AddWithValue("@nu_venta", _nu_venta);

            try
            {
                con.Open();
                com.ExecuteNonQuery();
                //System.Windows.Forms.MessageBox.Show("EL REGISTO SE ACTUALIZO CORRECTAMENTE EN LA BASE DE DATOS", "MENSAJE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ERROR AL CONECTAR CON BASE DE DATOS - ACTUALIZACION");
                con.Close();
            }
            finally
            {
                con.Close();
            }

            con.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }
        private void txtIEPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }
        private void ActualizaProductos()
        {
            string query = "select nu_producto, concat(ds_producto,' - ', tp_impuesto) as descripcion, tp_impuesto from producto order by descripcion asc"; ;
            MySqlConnection con = new MySqlConnection(connectionString);

            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds,"producto");

            cbProducto.DataSource = ds.Tables[0].DefaultView;
            cbProducto.ValueMember = "nu_producto";
            cbProducto.DisplayMember = "descripcion";

            da.Dispose();
            ds.Dispose();
            con.Close();
        }

        private void frmTransferencia_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            if (_nu_venta.HasValue)
            {
                cbProducto.Enabled = false;
                // LLENANDO DE venta
                try
                {
                    con.Open();
                    using (MySqlCommand com = new MySqlCommand("SELECT * FROM venta WHERE nu_venta = " + _nu_venta, con))
                    using (MySqlDataReader rd = com.ExecuteReader())
                        while (rd.Read())
                        {
                            txtnu_venta.Text = Convert.ToString(rd["nu_venta"]);
                            txtTotal.Text = Convert.ToString(rd["nu_total"]);
                            txtIEPS.Text = Convert.ToString(rd["nu_impuesto1"]);
                            dtpFecha.Text = Convert.ToString(rd["fh_alta"]);
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            else 
            { 
                ActualizaProductos();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Usted eligió el producto con clave: "+cbProducto.SelectedValue+"\nCuya Descripcion es:"+cbProducto.Text);
        }
    }
}
