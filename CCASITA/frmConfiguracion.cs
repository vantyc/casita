using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace LaCasita
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        
       
        private void GuardaConfiguracion()
        {
            Globales.ServidorAspel = txtServer.Text;
            Globales.RutaAspel = txtRutaAspel.Text;
            Globales.CatPedidos = txtCatPedidos.Text;
            Globales.URL = txtURL.Text;

            //FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAT_CFDi.FDB; Dialect=3");
/*
            if (txtURL.Text == "")
            {
                MessageBox.Show("Debe especificar el URL de la Base de Datos en la Configuracion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtURL.Focus();
                //return;
            }

            if (txtServer.Text == "")
            {
                MessageBox.Show("Debe especificar el nombre del servidor Aspel en la sección de Configuracion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtServer.Focus();
                //return;
            }

            if (txtRutaAspel.Text == "")
            {
                MessageBox.Show("Debe especificar la ruta del archivo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRutaAspel.Focus();
                //return;
            }*/

            // Lee los valores de la GIU y los guarda en las variables Globales
            Globales.URL = txtURL.Text;
            Globales.ServidorAspel = txtServer.Text;
            Globales.RutaAspel = txtRutaAspel.Text;
            Globales.CatPedidos = txtCatPedidos.Text;

            // AHORA SE GUARDA LA CONFIGURACION EN EL REGISTRO DE WINDOWS
            var key = Registry.CurrentUser.CreateSubKey(@"Software\Buen Software\LaCasita");
            if (key == null) return;
            try
            {
                key.SetValue("URL", Globales.URL);
                key.SetValue("ServidorAspel", Globales.ServidorAspel);
                key.SetValue("RutaAspel",Globales.RutaAspel);
                key.SetValue("CatPedidos",Globales.CatPedidos);
                key.Close();
                //con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " +ex.Message,"Error al guardar la configuración",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void btCreaDB_Click(object sender, EventArgs e)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAT_CFDi.FDB; Dialect=3");
            
            GuardaConfiguracion();
            // CREA LA BASE DE DATOS
            try
            {
                FbConnection.CreateDatabase(con.ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al crear la BD del SAT");
                return;
            }

            //CREA LA TABLA CFDI
            FbCommand Command = new FbCommand(
                @"create table CFDI(
                nofac varchar(40),
                uuid varchar(36),
                tipo varchar(10),                
                fecha TIMESTAMP,
                serie varchar(40),
                folio varchar(40),
                subtotal varchar(40),
                total varchar(40),
                emisor varchar(80),
                receptor varchar(80))", con);

            Command.CommandType = CommandType.Text;
            try
            {
                con.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear la tabla CFDI");
                return;
            }
            finally
            {
                con.Close();
            }

            //CREA LA TABLA DE CONCEPTOS (DETALLES DE FACTURA)
            Command = new FbCommand(
                @"create table CONCEPTOS(
                uuid varchar(36),
                unitario varchar(40),
                unidad varchar(15),
                importe varchar(15),
                descripcion varchar(250),
                cantidad varchar(15))", con);

            Command.CommandType = CommandType.Text;

            try
            {
                con.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear la tabla Conceptos");
                return;
            }
            finally
            {
                con.Close();
            }

            // CREA LA TABLA DE CONCILIACION DE EXCEL
            Command = new FbCommand(
    @"create table EXCEL(
                factura varchar(30),
                tienda varchar(5),
                comentario varchar(100))", con);

            Command.CommandType = CommandType.Text;

            try
            {
                con.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear la tabla Excel");
                return;
            }
            finally
            {
                con.Close();
            }

            MessageBox.Show("La base de datos del SAT ha sido creada exitosamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btPoblarDB_Click(object sender, EventArgs e)
        {
              FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
              folderBrowserDialog1.SelectedPath = Globales.RutaDes;
             
              if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
              {
                  ListaDirectorios(folderBrowserDialog1.SelectedPath);
                  MessageBox.Show("Se han importado los CFDis a la Base de Datos", "Procesamiento Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
        }
        private void ListaDirectorios(string DirectorioInicial)
        {
            object misValue = System.Reflection.Missing.Value;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;

            int comprobanteActual = 0;

            string[] archivos = Directory.GetFiles(DirectorioInicial, "*.xml");

            foreach (string archivo in archivos)
            {
                comprobanteActual++;
                lbProgreso.Visible = true;
                lbProgreso.Text = "Poblando la BD con los XML del directorio indicado. Procesando Comprobante: " + comprobanteActual.ToString() + " de " + archivos.Count() + ".";
                lbProgreso.Refresh();
                
                var comprobante = Sat.Cfdi.V32.Comprobante.Deserialize(System.IO.File.ReadAllText(archivo));
                
                // POR CADA COMPROBANTE SE CREA UN REGISTRO EN LA TABLA DE CFDI
                GrabaCFDi(comprobante);
                // GRABA LAS PARTIDAS EN LA TABLA DE CONCEPTOS
                GrabaPartidas(comprobante);
            }
            lbProgreso.Visible = false;
        }
        private void GrabaCFDi(Sat.Cfdi.V32.Comprobante comprobante)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAT_CFDi.FDB; Dialect=3");

            string query;
            query = @"INSERT INTO CFDI  (
nofac,
uuid,
tipo,
fecha,
serie,
folio,
subtotal,
total,
emisor,
receptor  
                                               ) VALUES(     
@nofac,
@uuid,
@tipo,
@fecha,
@serie,
@folio,
@subtotal,
@total,
@emisor,
@receptor

                                                )";
            FbCommand com = new FbCommand(query, con);
            com.Parameters.AddWithValue("@nofac", comprobante.Serie.Trim()+ comprobante.Folio.Trim());
            com.Parameters.AddWithValue("@uuid", comprobante.Complemento.TimbreFiscalDigital.Uuid);
            com.Parameters.AddWithValue("@tipo", comprobante.TipoDeComprobante);
            com.Parameters.AddWithValue("@fecha", comprobante.Fecha);
            com.Parameters.AddWithValue("@serie", comprobante.Serie);
            com.Parameters.AddWithValue("@folio", comprobante.Folio);
            com.Parameters.AddWithValue("@subtotal", comprobante.SubTotal);
            com.Parameters.AddWithValue("@total", comprobante.Total);
            com.Parameters.AddWithValue("@emisor", comprobante.Emisor.Nombre);
            com.Parameters.AddWithValue("@receptor", comprobante.Receptor.Nombre);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ERROR AL INSERTAR REGISTRO " + comprobante.Complemento.TimbreFiscalDigital.Uuid,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
       
    //private FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
    
    private void GrabaPartidas(Sat.Cfdi.V32.Comprobante comprobante)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");

            for (int c = 0; c < comprobante.Conceptos.Count; c++)
            {
                string query;
                query = @"INSERT INTO CONCEPTOS(
uuid,
unitario,
unidad,
importe,
descripcion,
cantidad  
                                               ) VALUES(     
@uuid,
@unitario,
@unidad,
@importe,
@descripcion,
@cantidad
                                                )";
                FbCommand com = new FbCommand(query, con);
                com.Parameters.AddWithValue("@uuid", comprobante.Complemento.TimbreFiscalDigital.Uuid);
                com.Parameters.AddWithValue("@unitario", comprobante.Conceptos[c].ValorUnitario);
                com.Parameters.AddWithValue("@unidad", comprobante.Conceptos[c].Unidad);
                com.Parameters.AddWithValue("@importe", comprobante.Conceptos[c].Importe);
                com.Parameters.AddWithValue("@descripcion", comprobante.Conceptos[c].Descripcion);
                com.Parameters.AddWithValue("@cantidad", comprobante.Conceptos[c].Cantidad);

                try
                {

                    con.Open();
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "ERROR AL INSERTAR REGISTRO ENC CONCEPTOS " + comprobante.Complemento.TimbreFiscalDigital.Uuid);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            // CUANDO LA FORMA CARGA HAY QUE COPIAR LOS VALORES ALMACENADOS EN LAS VARIABLES GLOBALES EN LA GUI
            pbProgreso.Visible = false;
            txtURL.Text = Globales.URL;
            txtServer.Text = Globales.ServidorAspel;
            txtRutaAspel.Text = Globales.RutaAspel;
            txtCatPedidos.Text = Globales.CatPedidos;
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            GuardaConfiguracion();
            this.Close();
        }

        private void btImportaExcel_Click(object sender, EventArgs e)
        {
            string query;
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");

            // PRIMERO SE VERIFICA SI EXISTE LA TABLA DE EXCEL EN LA DB DE FIREBIRD
            query = "select count(rdb$relation_name) from rdb$relations WHERE rdb$relation_name = 'EXCEL'";
            FbCommand cmd = new FbCommand(query, con);
            try
            {
                con.Open();
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count == 0)
                {
                    DialogResult respuesta = MessageBox.Show("La tabla de excel no existe en la base de datos. Desea crearla?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        FbCommand Command = new FbCommand();
                        // CREA LA TABLA DE CONCILIACION DE EXCEL
                        Command = new FbCommand(
                        @"create table EXCEL(
                        factura varchar(30),
                        tienda varchar(5),
                        comentario varchar(100))", con);

                        Command.CommandType = CommandType.Text;

                        try
                        {
                            //con.Open();
                            Command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error al crear la tabla Excel (346)");
                            return;
                        }
                        finally
                        {
                            con.Close();
                        }
                        MessageBox.Show("La tabla de conciliación de Excel ha sido creada exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        con.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error al verificar la existencia de la tabla de Excel.");
            }
            finally
            {
                con.Close();
            }
            // FIN DE VERIFICACION DE EXISTENCIA EN EXCEL

            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Seleccionar el Archivo de Excel a procesar",
                Filter = "Archivos de Excel|*.xls;*.xlsx"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string fileName = dialog.FileName;
            OleDbConnection connection = new OleDbConnection(string.Format("Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;", fileName));
            connection.Open();

            // RANGO 3200
            string cmdText = "Select * from [$A1:C4000];";
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            DataRow fila = dataSet.Tables[0].Rows[0];
            int contador = 0;

            for (fila = dataSet.Tables[0].Rows[contador]; contador<dataSet.Tables[0].Rows.Count; contador++)
            {
                lbProgreso.Visible = true;
                lbProgreso.Text = "Importando hoja de Excel. Procesando Fila " + (contador+1).ToString();
                lbProgreso.Refresh();

                fila = dataSet.Tables[0].Rows[contador];
                GuardaExcel(fila.ItemArray.GetValue(0).ToString().Trim(), fila.ItemArray.GetValue(1).ToString().Trim(), fila.ItemArray.GetValue(2).ToString().Trim());
                //contador++;
            }
            
            MessageBox.Show("Se importaron "+contador.ToString()+" registros.","Fin del procesamiento.",MessageBoxButtons.OK,MessageBoxIcon.Information);
            lbProgreso.Text = "";
            lbProgreso.Visible=false;
        }

        private void GuardaExcel(string folio, string tienda, string comentario)
        {
            string query;
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");

            query = @"INSERT INTO EXCEL  (
                                           factura,
                                           tienda,
                                           comentario
                                        ) VALUES(     
                                           @factura,
                                           @tienda,
                                           @comentario
                                        )";

            FbCommand com = new FbCommand(query, con);
            com.Parameters.AddWithValue("@factura", folio);
            com.Parameters.AddWithValue("@tienda", tienda);
            com.Parameters.AddWithValue("@comentario", comentario);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ERROR AL INSERTAR REGISTRO: " + folio+"" +tienda);
            }
            finally
            {
                con.Close();
            }
        }

        private void btBorraExcel_Click(object sender, EventArgs e)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            string query;

            GuardaConfiguracion();

            // PRIMERO SE VERIFICA SI EXISTE LA TABLA DE EXCEL EN LA DB DE FIREBIRD
            query = "select count(rdb$relation_name) from rdb$relations WHERE rdb$relation_name = 'EXCEL'";
            FbCommand cmd = new FbCommand(query, con);
            try
            {
                con.Open();
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count == 0)
                {
                    DialogResult respuesta = MessageBox.Show("La tabla de excel no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error al verificar la existencia de la tabla de Excel.");
            }
            finally
            {
                con.Close();
            }
            cmd = new FbCommand(
                          @"DROP TABLE EXCEL", con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al borrar la tabla Excel");
                return;
            }
            finally
            {
                con.Close();
            }
            MessageBox.Show("La tabla de conciliación de Excel fue borrada con éxito.","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btFechasVenta_Click(object sender, EventArgs e)
        {

            DialogResult respuesta = MessageBox.Show("Esta funcion permite importar un archivo de Excel\n"+
                              "con las fechas de venta.\n\n"+
                              "El archivo de excel consta de dos columnas:\n"+
                              "Columna A = Numero de Factura\n"+
                              "Columna B = Fecha de venta.\n\n"+
                              "No deberán existir filas vacias en el listado\n"+
                              "Asegurese que su archivo posee este formato\n"+
                              "DESEA CONTINUAR CON LA IMPORTACIÓN","Importacion de Fechas de Venta",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (respuesta == DialogResult.No)
            {
                return;
            }
            else
            {
                ImportaFechasDeVenta();
            }

        }

        private string fechaFb(string fechaExcel)
        {
            string fechaFb = "";
            fechaFb = fechaExcel.Substring(6, 4) + "/" + fechaExcel.Substring(3,2)+"/"+fechaExcel.Substring(0,2);
            return fechaFb;
        }

        private void ImportaFechasDeVenta()
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            Excel.Application xlApp = new Excel.Application(); ;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            string str;
            int fila = 0;


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Archivos de Excel (*.xls or *.xlsx)|*.xls;*.xlsx";

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            xlWorkBook = xlApp.Workbooks.Open(fileDialog.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            int exitos = 0, errores = 0, totalfilas = 0;

            fila = 1;  // Los datos deben comenzar en la fila numero 1 es decir no deben haber encabezados.
            str = Convert.ToString((range.Cells[fila, 1] as Excel.Range).Value).Trim();
            string[] filacontenido = new string[] { "1", "2", "3"};
            string[] resultado = new string[] {"",""};

            // Mientras exista información en la columna 2 (TAG NAME) se realizará el proceso de importación, de lo contrario la rutina se detiene. 
            // Por esta razón no deben de existir registros sin TAG NAME intercalados en el archivo ya que las filas que se encuentren
            // después del TAGNAME nulo no serán importadas.

            // Primero se cuentan las filas que se van a procesar

            while((range.Cells[fila, 1] as Excel.Range).Value!=null)
            {
                fila++;
            }

            totalfilas = fila - 1;
            if (totalfilas == 0)
            {
                MessageBox.Show("No existen filas a importar", "Aviso");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Se van a procesar " + Convert.ToString(totalfilas) + " registros, \n¿Desea proceder con la importación?", "Aviso", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Importación cancelada.", "Mensaje");
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    return;
                }
            }

            pbProgreso.Visible = true;
            lbProgreso.Visible = true;
            pbProgreso.Maximum = totalfilas;
            fila = 1;

            while((range.Cells[fila, 1] as Excel.Range).Value != null)
            {
                filacontenido[0] = Convert.ToString((range.Cells[fila, 1] as Excel.Range).Value).Trim().ToUpper(); // factura
                filacontenido[1] = fechaFb(Convert.ToString((range.Cells[fila, 2] as Excel.Range).Value).Trim().ToUpper()); // fecha de venta
                                                                                                                            //filacontenido[2] = Convert.ToString((range.Cells[fila, 3] as Excel.Range).Value).Trim().ToUpper(); // Comentario
                resultado = ImportaFila(filacontenido); // Ni ImportaFila y grabaFila deben abrir la conexión porque se encuentra abierta.

                // si importación de la fila es exitosa el resultado de la funcion ImportaFila es nulo y en la columna 16 se escribira la leyenda Importación Exitosa en color verde.
                int Mensaje = 3; //columna donde se colocarán los mensajes
                if (resultado[0] == "")
                {
                    range.Cells[fila, Mensaje] = resultado[1];
                    exitos = exitos + 1;
                }
                // si la importación trae errores estos se indican en la cadena que devuelve la funcion ImportarFila y el texto se colocará en color rojo.
                else
                {
                    range.Cells[fila, Mensaje] = range.Cells[fila, Mensaje] + ", " + resultado[0]+", "+ resultado[1];
                    range.Columns.AutoFit();
                    errores = errores + 1;
                }
                resultado[0] = "";
                pbProgreso.Value = fila;
                lbProgreso.Text = "Procesando fila " + Convert.ToString(fila - 3) + " de " + Convert.ToString(totalfilas)+ ". Factura: "+filacontenido[0];
                fila++;
            }
            // terminado el bucle while se cierra la conexión
            con.Close();

            pbProgreso.Visible = false;
            lbProgreso.Visible = false;
            string NuevoNombre;
            NuevoNombre = fileDialog.FileName + " - Resultado" + Path.GetExtension(fileDialog.FileName);

            if (File.Exists(NuevoNombre))
            {
                File.Delete(NuevoNombre);
            }
            xlWorkBook.SaveAs(NuevoNombre);
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.FinalReleaseComObject(xlApp);

            if (errores > 0)
            {
                DialogResult dr = MessageBox.Show(Convert.ToString(errores + exitos) + " filas del archivo de excel fueron procesadas. \n" + Convert.ToString(exitos) + " registros fueron importados con éxito. \n" + Convert.ToString(errores) + " registros no se consiguieron importar por errores. \n \n Desea abrir el archivo de salida para conocer la causa? \n(Requiere tener Excel instalado).", "Fin de Importación", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", Path.GetFullPath(fileDialog.FileName + " - Resultado" + Path.GetExtension(fileDialog.FileName)));
                }
            }
            else
            {
                MessageBox.Show(Convert.ToString(errores + exitos) + " filas del archivo de excel fueron procesadas. \n" + Convert.ToString(exitos) + " registros fueron importados con éxito.\n\n  No se encontraron errores.", "Fin de imporatación.");
            }

        }

        private string[] ImportaFila(string[] filacontenido)
        {
          
            string[] retorno = {"", ""};
            

            retorno = validaDatos(filacontenido);

            if(retorno[0] == "")
                grabaFila(filacontenido);
            return retorno;
        }

        private string[] validaDatos(string[] filacontenido)
        {

            string[] salida = {"",""};
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";
            FbConnection con = new FbConnection(connectionString);

            // 1.- Verificar que el numero de factura exista en la tabla de facturas (FACTF01)
            string query = @"SELECT COUNT(*) FROM FACTF01 WHERE CVE_DOC = '" + filacontenido[0]+"'";
            FbCommand com = new FbCommand(query, con);
            int resultado = 0;

            try
            {
                con.Open();
                resultado = Convert.ToInt32(com.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error [730]");
            }
            finally
            {
                con.Close();
            }


            if (resultado == 0) // El numero de factura existe en la tabla de facturas
            {
                salida[0] = "No existe el número de factura:";
                salida[1] = filacontenido[0];
                return salida;
                
            }

            string resultado2 = "";

            // 2.- Verificar que la factura tiene número de folio de envio
            query = @"SELECT DAT_ENVIO FROM FACTF01 WHERE CVE_DOC = '" + filacontenido[0] + "'";

            FbCommand com2 = new FbCommand(query, con);

            FbDataReader lector = null;

            try
            {
                con.Open();
                lector = com2.ExecuteReader();

                if (lector.Read())
                {
                    resultado2 = Convert.ToString(lector["DAT_ENVIO"]);
                }


                if (resultado2 == "0") // Si el folio de envio es cero significa que hay que crear un registro en la tabla de envios con el ultimo consecutivo incrementado en 1
                {
                    salida[1] = CreaEmbarque(filacontenido[1]);
                    
                    ActualizaFacturaConEmbarque(filacontenido[0], salida[1]);
                    salida[0] = "";
                    salida[1] = "Nuevo embarque: " + salida[1];                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error [755]");
            }
            finally
            {
                con.Close();
            }
            return salida;
        }

        public string CreaEmbarque(string FechaVenta)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            Int32 UltimoFolioEmbarque = UltimoEnvio()+1;
            FbCommand com = new FbCommand("INSERT INTO INFENVIO01(CVE_INFO, FECHA_RECEP) VALUES('"+(UltimoFolioEmbarque).ToString()+"','"+FechaVenta+"')", con);
            
            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " [746] ", "No se pudo crear el embarque");
            }
            finally
            {
                con.Close();
            }      
            return UltimoFolioEmbarque.ToString();
        }
        
        private Int32 UltimoEnvio()
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            Int32 ultimofolio = 0;
            FbCommand com3 = new FbCommand("SELECT MAX(CVE_INFO) FROM INFENVIO01", con);

            try
            {
                con.Open();
                ultimofolio = Convert.ToInt32(com3.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al consultar el ultimo embarque [766]");
                return 0;
            }
            finally
            {
                con.Close();
            }
            return ultimofolio;
        }

        public void ActualizaFacturaConEmbarque(string factura, string embarque)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            FbCommand com = new FbCommand("UPDATE FACTF01 SET DAT_ENVIO = '"+embarque+"' WHERE CVE_DOC = '"+factura+"'", con);

            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar la factura con el numero de embarque.");
            }
            finally
            {
                con.Close();
            }
        }

        public static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

        // Coloca la fecha de venta en el campo FECHA_RECEP de la tabla INFENVIO01
        private string grabaFila(string[] filacontenido)
        {
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";
            FbConnection con = new FbConnection(connectionString);
            string salida = "";

            // Se graba en la tabla de envios la fecha de la factura de la tabla FACTF01 en el campo FECHA_RECEP de la tabla INFENVIO01
            string query = @"UPDATE INFEENVIO01 SET FECHA_RECEP = @fechaventa WHERE CVE_INFO = '@idenvio'";
            Int32 newID;

            FbCommand com = new FbCommand(query, con);
            com.Parameters.AddWithValue("@fechaventa", filacontenido[1]);
            com.Parameters.AddWithValue("@idenvio", NoEmbarque(filacontenido[0]));
            

            try
            {
                con.Open();
                newID = (Int32)com.ExecuteScalar();
            }
            catch
            {
                return "Error al grabar fecha de venta.";
            }
            finally
            {
                con.Close();
            }
            
            return salida;
        }

        // Obtiene el numero de ID de un embarque dada una factura
        private string NoEmbarque(string factura)
        {
            string connectionString = @"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"; Dialect=3";
            string query = "SELECT DAT_ENVIO FROM FACTF01  WHERE CVE_DOC = '"+factura+"'";
            FbConnection con = new FbConnection(connectionString);
            try
            {
                con.Open();
                using (FbCommand com = new FbCommand("SELECT DAT_ENVIO  FROM FACTF01 WHERE CVE_DOC = '" + factura+"'", con))
                using (FbDataReader rd = com.ExecuteReader())
                    while (rd.Read())
                    {
                        return Convert.ToString(rd["DAT_ENVIO"]);
                    }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+" [778] ");
            }
            finally
            {
                con.Close();
            }
            return "0";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FbConnection con = new FbConnection(@"User=SYSDBA; Password=masterkey; Database=" + Globales.ServidorAspel + @"/3050:" + Globales.RutaAspel + @"\SAE50EMPRE01.FDB; Dialect=3");
            object resultado;
            FbCommand com = new FbCommand("INSERT INTO INFENVIO01 (FECHA_RECEP) VALUES('2016/06/10') RETURNING CVE_INFO", con);
            try
            {
                con.Open();
                resultado = com.ExecuteScalar();
                MessageBox.Show("Número de Folio Asignado: "+ resultado.ToString(),"Registro insertado exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            finally
            {
                con.Close();
            }

        }

        private void btCorrigeIEPS_Click(object sender, EventArgs e)
        {
            string connectionString = @"server=" + Globales.URL + "; database=siilcp_siidb; uid=siilcp_usr; pwd=siilCp9002";
            string _query = "";
            object resultado;
            /*
start transaction;
UPDATE venta_producto as vp 
INNER JOIN producto as p 
ON vp.nu_producto=p.nu_producto

SET vp.tp_impuesto = p.tp_impuesto 
WHERE vp.tp_impuesto<>8 and p.tp_impuesto>0 and vp.fh_alta between '2018/01/01 00:00:00' and '2018/12/30 23:59:59' and p.nu_indica_etiquetado=1;
commit;
             */

            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlCommand com = con.CreateCommand();
            //  SE PREPARA EL ALTA DE LA VENTA EN LA TABLA VENTA
            //_query = "insert into venta (nu_venta,nu_venta_sucursal,nu_sucursal, st_venta, nu_total, nu_impuesto1, nu_cantidad_descuento, nu_usralta, tp_pago, nu_descuento, nu_usrmod,fh_alta,fh_mod,ds_comentario) values((SELECT MAX(coco1.nu_venta)+1 FROM venta as coco1),(SELECT max(coco2.nu_venta_sucursal)+1 FROM venta as coco2 where coco2.nu_sucursal=1 order by coco2.nu_venta_sucursal desc limit 1),1,'PG', @Importe, @IEPS,0,1,4,0,1,@Fecha, @Fecha,'Transferencia')";
            _query = @"UPDATE venta_producto as vp inner JOIN producto as p 
ON vp.nu_producto=p.nu_producto
SET vp.tp_impuesto = p.tp_impuesto 
WHERE vp.tp_impuesto<>8 and p.tp_impuesto>0 and vp.fh_alta between DATE_FORMAT(NOW() ,'%Y-01-01 00:00:00') and  now() and p.nu_indica_etiquetado=1;";
            com.CommandText = _query;

            try
            {
                con.Open();
                resultado = com.ExecuteNonQuery();
                MessageBox.Show("Se realizó la corrección de IEPS en SII.", "MENSAJE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al corregir el IESP en la BD de SII. 952");
                // NO SE CIERRA LA CONEXION PARA OBTENER EL ID GENERADO
                //con.Close();
            }
            con.Close();
            com.Dispose();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}