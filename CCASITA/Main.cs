using System;
using System.Reflection;
using System.Windows.Forms;

namespace LaCasita
{
    static partial class Program
    {
        //private static string _empresaRfc;

        //private static string _password;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //  SplashScreenInicializa();
            //  _splashScreen.Show();

            // EmbeddedAssembly.Load("FalconXML.dll.System.Data.SQLite.dll", "System.Data.SQLite.dll");
            EmbeddedAssembly.Load("FalconXML.dll.itextsharp.dll", "itextsharp.dll");

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;

            //Testing.Validar();

            // FormEntradaInicializa();

            //  FormPrincipalInicializa();

            // _splashScreen.Close();

            //   if (_formEntrada.ShowDialog() != DialogResult.OK) return;

            //    _formPrincipal.ShowDialog();

            Application.Run(new frmPrincipal());

        }

        static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

    }
}

