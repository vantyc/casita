using MySqlConnector;
using System;
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

        public frmTransferencia(int nu_venta) : this()
        {
            _nu_venta = nu_venta;
        }

        string connectionString = @"server=" + Globales.URL +
                                  "; database=siilcp_siidb; uid=siilcp_usr; pwd=siilCp9002; SslMode=None";

        string _query = "";
        object resultado;

        // ---------------------------------------------------------------------
        // Helper para sustituir MySqlHelper.ExecuteScalar
        // ---------------------------------------------------------------------
        private object EjecutarScalar(MySqlConnection con, string query)
        {
            using (var cmd = new MySqlCommand(query, con))
            {
                return cmd.ExecuteScalar();
            }
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (_nu_venta.HasValue)
                ActualizaTransferencia();
            else
                CreaNuevaTransferencia();
        }

        private void CreaNuevaTransferencia()
        {
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

            using (var con = new MySqlConnection(connectionString))
            {
                MySqlCommand com = con.CreateCommand();

                string fecha = $"{dtpFecha.Value:yyyy/MM/dd} 23:59:59";

                decimal ieps = txtIEPS.Text == "" ? 0 : Convert.ToDecimal(txtIEPS.Text);
                decimal total = txtTotal.Text == "" ? 0 : Convert.ToDecimal(txtTotal.Text);
                decimal importe = total - ieps;
                decimal tasaIEPS = total == 0 ? 0 : (1 / (1 - ieps / total) - 1) * 100;

                _query = "insert into venta (nu_venta,nu_venta_sucursal,nu_sucursal, st_venta, nu_total, nu_subtotal, nu_impuesto1, nu_cantidad_descuento, nu_usralta, tp_pago, nu_descuento, nu_usrmod,fh_alta,fh_mod,ds_comentario)" +
                    " values((SELECT MAX(coco1.nu_venta)+1 FROM venta as coco1)," +
                    "(SELECT max(coco2.nu_venta_sucursal)+1 FROM venta as coco2 where coco2.nu_sucursal=1 order by coco2.nu_venta_sucursal desc limit 1)," +
                    "1,'PG', " + total + ", " + importe + ", " + ieps + ", 0, 1, 4, 0, 1, @Fecha, @Fecha,'Transferencia')";

                com.CommandText = _query;
                com.Parameters.AddWithValue("@Fecha", fecha);

                con.Open();
                com.ExecuteNonQuery();

                // Obtener último ID
                int ultimoID = 0;
                try
                {
                    object obj = EjecutarScalar(con, "select LAST_INSERT_ID()");
                    if (obj != null)
                    {
                        ultimoID = Convert.ToInt32(obj);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al obtener el último ID. 65");
                }

                // Insertar en venta_producto
                string producto = cbProducto.SelectedValue.ToString();
                MySqlCommand com2 = con.CreateCommand();

                _query = @"INSERT INTO venta_producto 
                        (nu_venta, nu_producto, nu_cantidad, nu_subtotal, nu_total, tp_impuesto, nu_impuesto1, nu_descuento, nu_cantidad_descuento, nu_usralta, fh_alta) 
                        VALUES ((select nu_venta from venta order by nu_venta desc limit 1), 
                        " + producto +
                        @", 1 , @Importe , @Total, @tasaIEPS, @IEPS, 0, 0, 1, @Fecha)";

                com2.CommandText = _query;
                com2.Parameters.AddWithValue("@Importe", importe);
                com2.Parameters.AddWithValue("@Total", total);
                com2.Parameters.AddWithValue("@tasaIEPS", tasaIEPS);
                com2.Parameters.AddWithValue("@IEPS", ieps);
                com2.Parameters.AddWithValue("@Fecha", fecha);

                com2.ExecuteNonQuery();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ActualizaTransferencia()
        {
            string fecha = $"{dtpFecha.Value:yyyy/MM/dd} 23:59:59";

            decimal ieps = txtIEPS.Text == "" ? 0 : Convert.ToDecimal(txtIEPS.Text);
            decimal total = txtTotal.Text == "" ? 0 : Convert.ToDecimal(txtTotal.Text);
            decimal importe = total - ieps;

            string query = @"UPDATE venta SET 
                             nu_total=@total,
                             fh_alta=@fh_alta,
                             nu_impuesto1=@nu_impuesto1,
                             nu_subtotal=@nu_subtotal
                             WHERE nu_venta=@nu_venta";

            using (var con = new MySqlConnection(connectionString))
            using (var com = new MySqlCommand(query, con))
            {
                com.Parameters.AddWithValue("@total", total);
                com.Parameters.AddWithValue("@fh_alta", fecha);
                com.Parameters.AddWithValue("@nu_impuesto1", ieps);
                com.Parameters.AddWithValue("@nu_subtotal", importe);
                com.Parameters.AddWithValue("@nu_venta", _nu_venta);

                con.Open();
                com.ExecuteNonQuery();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ActualizaProductos()
        {
            string query = "select nu_producto, concat(ds_producto,' - ', tp_impuesto) as descripcion, tp_impuesto from producto order by descripcion asc";

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, con))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "producto");

                    cbProducto.DataSource = ds.Tables[0].DefaultView;
                    cbProducto.ValueMember = "nu_producto";
                    cbProducto.DisplayMember = "descripcion";
                }
            }
        }

        private void frmTransferencia_Load(object sender, EventArgs e)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                if (_nu_venta.HasValue)
                {
                    cbProducto.Enabled = false;
                    try
                    {
                        con.Open();
                        using (MySqlCommand com = new MySqlCommand("SELECT * FROM venta WHERE nu_venta = " + _nu_venta, con))
                        using (MySqlDataReader rd = com.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                txtnu_venta.Text = rd["nu_venta"].ToString();
                                txtTotal.Text = rd["nu_total"].ToString();
                                txtIEPS.Text = rd["nu_impuesto1"].ToString();
                                dtpFecha.Text = rd["fh_alta"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    ActualizaProductos();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usted eligió el producto con clave: " + cbProducto.SelectedValue +
                            "\nCuya Descripción es: " + cbProducto.Text);
        }

        // ---------------------------------------------------------------------
        // EVENTOS QUE EL DISEÑADOR REQUIERE
        // ---------------------------------------------------------------------

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txtIEPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }
    }
}
