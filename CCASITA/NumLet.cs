using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaCasita
{
    public static class NumLet
    {

        public static string Get(decimal cantidad, string moneda = null)
        {
            var letras = string.Empty;

            var cantidadR = decimal.Round(cantidad, 2);

            var entero = Math.Truncate(cantidadR);

            var decimales = Convert.ToByte((cantidad - entero) * 100);

            string[] unidades = { "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE", "VEINTE", "VEINTIÚN", "VEINTIDÓS", "VEINTITRÉS", "VEINTICUATRO", "VEINTICINCO", "VEINTISÉIS", "VEINTISIETE", "VEINTIOCHO", "VEINTINUEVE" };

            string[] decenas = { "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };

            string[] centenas = { "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };

            string monedaLarga;
            /* switch (moneda)
             {
                 case "MXP":
                 case "MXN":
                     monedaLarga = (cantidadR >= 2 ? "PESOS" : "PESO");
                     break;
                 case "USD":
                     monedaLarga = (cantidadR >= 2 ? "DÓLARES" : "DÓLAR");
                     break;
                 case "EUR":
                     monedaLarga = (cantidadR >= 2 ? "EUROS" : "EURO");
                     break;
                 default:
                     monedaLarga = (cantidadR >= 2 ? "PESOS" : "PESO");
                     moneda = "M.N.";
                     break;
             }*/
            switch (moneda.ToUpper())
            {
                case "MXP":
                case "MXN":
                    monedaLarga = (cantidadR >= 2 ? "PESOS" : "PESO");
                    break;
                case "USD":
                    monedaLarga = (cantidadR >= 2 ? "DÓLARES" : "DÓLAR");
                    break;
                case "DOLARES":
                    monedaLarga = (cantidadR >= 2 ? "DÓLARES" : "DÓLAR");
                    break;
                case "EUR":
                    monedaLarga = (cantidadR >= 2 ? "EUROS" : "EURO");
                    break;
                default:
                    monedaLarga = (cantidadR >= 2 ? "PESOS" : "PESO");
                    moneda = "M.N.";
                    break;
            }

            byte lnNumeroBloques = 1;

            do
            {
                var primerDigito = 0;
                var segundoDigito = 0;
                var tercerDigito = 0;
                var bloque = string.Empty;
                var bloqueCero = 0;
                for (byte I = 1; I <= 3; I++)
                {
                    dynamic digito = Convert.ToByte(entero % 10);
                    if (digito != 0)
                    {
                        switch (I)
                        {
                            case 1:
                                bloque = string.Format(" {0}", unidades[digito - 1]);
                                primerDigito = digito;
                                break;
                            case 2:
                                bloque = digito <= 2
                                    ? string.Format(" {0}", unidades[(digito * 10) + primerDigito - 1])
                                    : string.Format(" {0}", decenas[digito - 1] + (primerDigito != 0 ? " Y" : null) + bloque);
                                segundoDigito = digito;
                                break;
                            case 3:
                                bloque = string.Format(" {0}", (digito == 1 & primerDigito == 0 & segundoDigito == 0 ? "CIEN" : centenas[digito - 1]) + bloque);
                                tercerDigito = digito;
                                break;
                        }
                    }
                    else
                    {
                        bloqueCero = bloqueCero + 1;
                    }
                    entero = Math.Truncate(entero / 10);
                    if (entero == 0) break;
                }
                switch (lnNumeroBloques)
                {
                    case 1:
                        letras = bloque;
                        break;
                    case 2:
                        letras = string.Format("{0} MIL{1}", bloque, letras);
                        break;
                    case 3:
                        letras = string.Format("{0} {1}", bloque, (primerDigito == 1 & segundoDigito == 0 & tercerDigito == 0 ? "MILLÓN" : "MILLONES") + letras);
                        break;
                }
                lnNumeroBloques = (byte)(lnNumeroBloques + 1);
            } while (entero != 0);
            return string.Format("({0} {1} {2}/100 {3})", letras.Trim(), monedaLarga, decimales.ToString("00"), moneda);
        }

    }
}
