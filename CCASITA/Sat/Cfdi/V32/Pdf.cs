using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System.Collections.Generic;

using Imploc10 = LaCasita.Sat.Cfdi.Complementos.ImpuestosLocales.V10.ImpuestosLocales;
using Nomina11 = LaCasita.Sat.Cfdi.Complementos.Nomina.V11.Nomina;
using Tifidi10 = LaCasita.Sat.Cfdi.Complementos.TimbreFiscalDigital.V10.TimbreFiscalDigital;

using Comprobante32 = LaCasita.Sat.Cfdi.V32.Comprobante;
using Pdf32 = LaCasita.Sat.Cfdi.V32.Pdf;
using System.Windows.Forms;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Pendiente
    /// </summary>
    public class Pdf
    {
        private static Comprobante _comprobante;

        private static Font _fontConceptosTitulos;
        private static Font _fontConceptosValues;

        private static Font _fontComprobanteTitulos;
        private static Font _fontComprobanteValues;

        private static Font _fontEmisorTitulos;
        private static Font _fontEmisorValues;
        private static Font _fontEmisorValuesB;

        private static Font _fontReceptorTitulos;
        private static Font _fontReceptorValues;
        private static Font _fontReceptorValuesB;

        private static Font _fontTimbreFiscalDigitalTitulos;
        private static Font _fontTimbreFiscalDigitalValues;
        private static Font _fontTimbreFiscalDigitalValuesB;

        private static Font _fontNominaTitulos;
        private static Font _fontNominaValues;

        private static Font _fontImpuestosLocalesTitulos;
        private static Font _fontImpuestosLocalesValues;

        private static Font _fontTotalesTitulos;
        private static Font _fontTotalesValues;

        private static Font _fontTotalLetra;

        private static Font _fontPagareTitulos;
        private static Font _fontPagareValues;

        private static int _borderTitulos;
        private static int _borderValues;

        private static float _paddingTopTitulos;
        private static float _paddingTopValues;

        private static float _paddingBottomTitulos;
        private static float _paddingBottomValues;

        private static float _paddingLeftTitulos;
        private static float _paddingLeftValues;

        private static float _paddingRightTitulos;
        private static float _paddingRightValues;

        private static BaseColor _backgroundColor;

        private static BaseColor _backgroundColorConceptos;

        private static float _conceptosCol1;
        private static float _conceptosCol2;
        private static float _conceptosCol3;
        private static float _conceptosCol4;
        private static float _conceptosCol5;
        private static float _conceptosCol6;
        private static float _conceptosCol7;

        private static float _conceptosHeight;

        private static string _moneda;

        private static Document _document;
        private static Rectangle _documentPageSize;

        private static float _pdfPTableTotalWidth;
        private static float _documentLeftMargin;
        private static float _documentRigthMargin;
        private static float _documentTopMargin;
        private static float _documentBottomMargin;
        private static FileInfo _documentFileInfo;

        public static FileInfo Get(Comprobante32 comprobante, FileInfo fileInfo, bool autoSize, bool pagare, bool reporteValidacion)
        {
            try {
                _comprobante = comprobante;

                _documentFileInfo = fileInfo;

                InicializarValores(autoSize, pagare);

                _document = new Document(_documentPageSize, _documentLeftMargin, _documentRigthMargin, _documentTopMargin, _documentBottomMargin);



                var pdfPTable = new PdfPTable(1) { TotalWidth = _pdfPTableTotalWidth };

                var pdfWriter = PdfWriter.GetInstance(_document, new FileStream(_documentFileInfo.FullName, FileMode.Create));
                pdfWriter.PageEvent = new PageEventHelper();
                pdfWriter.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfBoolean.PDFTRUE);
                pdfWriter.ViewerPreferences = PdfWriter.HideToolbar | PdfWriter.HideWindowUI | PdfWriter.CenterWindow;
                pdfWriter.SetEncryption(PdfWriter.STRENGTH128BITS, null, Program.RandomString(), PdfWriter.ALLOW_COPY | PdfWriter.ALLOW_PRINTING);

                _document.Open();

                AddPropietites();

                var pdfPCellWhiteHeight = _documentPageSize.Height - _documentTopMargin - _documentBottomMargin;

                var encabezado = Encabezado();
                var pdfPCellEncabezado = new PdfPCell(encabezado) { Border = 0 };
                pdfPCellWhiteHeight -= encabezado.TotalHeight;

                _backgroundColorConceptos = new BaseColor(251, 251, 251);

                var conceptos = PdfPTableConceptos();
                var pdfPCellConceptos = new PdfPCell(conceptos) { Border = 0, PaddingTop = _paddingTopTitulos };
                pdfPCellWhiteHeight -= conceptos.TotalHeight;

                var totales = PdfPTableTotales();
                var pdfPCellTotales = new PdfPCell(totales) { Border = 0 };
                pdfPCellWhiteHeight -= totales.TotalHeight;

                PdfPCell pdfPCellNomina = null;
                if (_comprobante.Complemento.Nomina != null)
                {
                    var pdfPTableNomina = PdfPTableNomina();
                    pdfPCellNomina = new PdfPCell(pdfPTableNomina) { Border = 0 };
                    pdfPCellWhiteHeight -= pdfPTableNomina.TotalHeight;
                }

                PdfPCell pdfPCellTimbreFiscalDigital = null;
                if (_comprobante.Complemento.TimbreFiscalDigital != null)
                {
                    var pdfPTableTimbreFiscalDigital = PdfPTableTimbreFiscalDigital();
                    pdfPCellTimbreFiscalDigital = new PdfPCell(pdfPTableTimbreFiscalDigital) { Border = 0 };
                    pdfPCellWhiteHeight -= pdfPTableTimbreFiscalDigital.TotalHeight;
                }

                PdfPCell pdfPCellPagare = null;
                if (pagare)
                {
                    var pdfPTablePagare = Pagare();
                    pdfPCellPagare = new PdfPCell(pdfPTablePagare) { Border = 0 };
                    pdfPCellWhiteHeight -= pdfPTablePagare.TotalHeight;
                }

                pdfPTable.AddCell(pdfPCellEncabezado);

                pdfPTable.AddCell(pdfPCellConceptos);

                var pdfPCellWhite = new PdfPCell { Border = 0, FixedHeight = pdfPCellWhiteHeight };
                pdfPCellWhite.FixedHeight = autoSize ? pdfPCellWhiteHeight : _conceptosHeight;
                pdfPTable.AddCell(pdfPCellWhite);

                pdfPTable.AddCell(pdfPCellTotales);

                if (pdfPCellNomina != null) pdfPTable.AddCell(pdfPCellNomina);

                if (pdfPCellTimbreFiscalDigital != null) pdfPTable.AddCell(pdfPCellTimbreFiscalDigital);

                if (pdfPCellPagare != null) pdfPTable.AddCell(pdfPCellPagare);

                pdfPTable.WriteSelectedRows(0, -1, _documentLeftMargin, _documentPageSize.Top - _documentTopMargin, pdfWriter.DirectContent);


               if (reporteValidacion)
                {
                    _document.NewPage();

                    pdfPTable = new PdfPTable(1) { TotalWidth = _pdfPTableTotalWidth };

                    PdfPCell pdfPCellValidacion = null;
                    var pdfPTableValidacion = PdfTableValidacion();
                    pdfPCellValidacion = new PdfPCell(pdfPTableValidacion) { Border = 0 };
                    pdfPTable.AddCell(pdfPCellValidacion);

                    pdfPTable.WriteSelectedRows(0, -1, _documentLeftMargin, _documentPageSize.Top - _documentTopMargin, pdfWriter.DirectContent);

                }

                pdfWriter.Flush();

                _document.Close();

                pdfWriter.Close();
                pdfWriter.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al crear el PDF");
            }
            return _documentFileInfo;
        
        }

        private class PageEventHelper : PdfPageEventHelper
        {

            public override void OnStartPage(PdfWriter writer, Document document)
            {


            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {

                //var rectangle = new Rectangle(document.PageSize);
                //rectangle.Left += document.LeftMargin;
                //rectangle.Right -= document.RightMargin;
                //rectangle.Top -= document.TopMargin;
                //rectangle.Bottom += document.BottomMargin;

                //var pdfContentByte = writer.DirectContent;
                //pdfContentByte.SetColorStroke(BaseColor.RED);
                //pdfContentByte.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                //pdfContentByte.SetLineWidth(0);
                //pdfContentByte.Stroke();

                //var pdfPTable = new PdfPTable(2)
                //{
                //    TotalWidth = document.PageSize.Width - document.Left - document.RightMargin,
                //    HorizontalAlignment = 2
                //};

                //var paragraph = new Paragraph(DateTime.Now.ToString("G"));
                //var pdfPCell = new PdfPCell(paragraph)
                //{
                //    BorderWidth = 0.07f,
                //    PaddingLeft = document.LeftMargin
                //};
                //pdfPTable.AddCell(pdfPCell);


                //paragraph = new Paragraph(writer.PageNumber.ToString());
                //pdfPCell = new PdfPCell(paragraph)
                //{
                //    HorizontalAlignment = Element.ALIGN_RIGHT,
                //    BorderWidth = 0.07f,
                //    PaddingRight = document.RightMargin
                //};
                //pdfPTable.AddCell(pdfPCell);

                //pdfPTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin + pdfPTable.TotalHeight, writer.DirectContent);

            }

        }

        private static void InicializarValores(bool autoSize, bool pagare)
        {

            _fontComprobanteTitulos = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
            _fontComprobanteValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);

            _fontEmisorTitulos = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
            _fontEmisorValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
            _fontEmisorValuesB = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.DARK_GRAY);

            _fontReceptorTitulos = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
            _fontReceptorValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
            _fontReceptorValuesB = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.DARK_GRAY);

            _fontConceptosTitulos = FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK);
            _fontConceptosValues = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);

            _fontTotalesTitulos = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
            _fontTotalesValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
            
            _fontTotalLetra = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);

            _fontTimbreFiscalDigitalTitulos = FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK);
            _fontTimbreFiscalDigitalValues = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);
            _fontTimbreFiscalDigitalValuesB = FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK);

            _fontNominaTitulos = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
            _fontNominaValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);

            _fontImpuestosLocalesTitulos = FontFactory.GetFont("Arial", 5, Font.BOLD, BaseColor.BLACK);
            _fontImpuestosLocalesValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);

            _fontPagareTitulos = FontFactory.GetFont("Arial", 5, Font.BOLD, BaseColor.BLACK);
            _fontPagareValues = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);

            _backgroundColor = new BaseColor(245, 245, 245);
            
            _documentPageSize = PageSize.LETTER;
            _documentLeftMargin = Utilities.MillimetersToPoints(10);
            _documentRigthMargin = Utilities.MillimetersToPoints(10);
            _documentTopMargin = Utilities.MillimetersToPoints(10);
            _documentBottomMargin = Utilities.MillimetersToPoints(10);

            _borderTitulos = 0;
            _borderValues = 0;

            _paddingTopTitulos = 2;
            _paddingTopValues = 0;

            _paddingBottomTitulos = 0;
            _paddingBottomValues = 0;

            _paddingLeftTitulos = 2;
            _paddingLeftValues = 4;

            _paddingRightTitulos = 2;
            _paddingRightValues = 4;

            _moneda = "$ ";

            _pdfPTableTotalWidth = _documentPageSize.Width - _documentLeftMargin - _documentRigthMargin;

            _conceptosCol1 = (float)(1.9 * _pdfPTableTotalWidth / 100);
            _conceptosCol2 = (float)(8.6 * _pdfPTableTotalWidth / 100);
            _conceptosCol3 = (float)(8.7 * _pdfPTableTotalWidth / 100);
            _conceptosCol4 = (float)(10.3 * _pdfPTableTotalWidth / 100);
            _conceptosCol5 = (float)(53.1 * _pdfPTableTotalWidth / 100);
            _conceptosCol6 = (float)(8.7 * _pdfPTableTotalWidth / 100);
            _conceptosCol7 = (float)(8.7 * _pdfPTableTotalWidth / 100);

        }

        private static void AddPropietites()
        {

            _document.AddTitle("Comprobante Fiscal Digital por Internet");

            _document.AddAuthor(System.Windows.Forms.Application.ProductName + " " + System.Windows.Forms.Application.ProductVersion);
            _document.AddCreator(System.Windows.Forms.Application.ProductName + " " + System.Windows.Forms.Application.ProductVersion);

            var subject = "Cfdi";

            if (_comprobante.Serie != null)
            {
                subject += " Serie: " + _comprobante.Serie;
                _document.AddHeader("Serie", _comprobante.Serie);
            }

            if (_comprobante.Folio != null)
            {
                subject += " Folio: " + _comprobante.Folio;
                _document.AddHeader("Folio", _comprobante.Folio);
            }

            subject += " Uuid: " + _comprobante.Complemento.TimbreFiscalDigital.Uuid;

            _document.AddSubject(subject);

            _document.AddKeywords("Cfdi Sat " + System.Windows.Forms.Application.ProductName);

            _document.AddHeader("Emisor", _comprobante.Emisor.Nombre);
            _document.AddHeader("EmisorRfc", _comprobante.Emisor.Rfc);
            _document.AddHeader("Fecha", _comprobante.FechaIso8601);
            _document.AddHeader("LugarExpedicion", _comprobante.LugarExpedicion);
            _document.AddHeader("Version", _comprobante.Version);
            _document.AddHeader("Receptor", _comprobante.Receptor.Nombre);
            _document.AddHeader("ReceptorRfc", _comprobante.Receptor.Rfc);

        }

        private static PdfPTable Encabezado()
        {

            //var logo = Image.GetInstance("LCA050628P99.PNG");
            //logo.ScalePercent(72f / logo.DpiX * 100);
            //var pdfPCellLogo = new PdfPCell(logo)
            //{
            //    MinimumHeight = logo.ScaledHeight,
            //    BorderWidth = 0.07f
            //};

            var pdfPTable = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.LeftMargin - _document.RightMargin
            };

            var pdfPCell = new PdfPCell(Titulo())
            {
                Border = 0,
                Colspan = 2
            };
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(PdfPTableEmisor())
            {
                Border = 0
            };
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(PdfPTableComprobanteReceptor())
            {
                Border = 0
            };
            pdfPTable.AddCell(pdfPCell);

            return pdfPTable;
        }

        private static PdfPTable Titulo()
        {

            var returnTable = new PdfPTable(1);

            var phraseTitulo = new Phrase("Comprobante fiscal digital a través de internet", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            var cell = new PdfPCell(phraseTitulo)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            returnTable.AddCell(cell);

            phraseTitulo = new Phrase("ESTE DOCUMENTO ES UNA REPRESENTACIÓN IMPRESA DE UN CFDI", FontFactory.GetFont("Arial", 5, Font.NORMAL, BaseColor.GRAY));
            cell = new PdfPCell(phraseTitulo)
            {
                Border = 0,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            returnTable.AddCell(cell);

            return returnTable;

        }

        private static PdfPTable PdfPTableEmisor(bool minimo = false)
        {
            var returnTable = new PdfPTable(1);

            var cell = new PdfPCell(new Phrase(new Chunk("EMISOR", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            returnTable.AddCell(cell);

            var chunks = new List<Chunk>();
            if (!string.IsNullOrEmpty(_comprobante.Emisor.Nombre)) chunks.Add(new Chunk(string.Format("{0} ", _comprobante.Emisor.Nombre.ToUpper()), _fontEmisorValues));
            chunks.Add(new Chunk(string.Format("({0})", _comprobante.Emisor.Rfc), _fontEmisorValues));
            var phrase = new Phrase();
            foreach (var a in chunks) { phrase.Add(a); }
            cell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues
            };
            returnTable.AddCell(cell);

            if (minimo) return returnTable;
            
            returnTable.AddCell(new PdfPCell(PdfPTableEmisorRegimenFiscal()) { Border = 0, Padding = 0 });

            if (_comprobante.Emisor.DomicilioFiscal != null)
                returnTable.AddCell(new PdfPCell(PdfPTableEmisorDomicilioFiscal()) { Border = 0, Padding = 0 });

            if (_comprobante.Emisor.ExpedidoEn != null)
                returnTable.AddCell(new PdfPCell(PdfPTableEmisorExpedidoEn()) { Border = 0, Padding = 0 });

            return returnTable;
        }

        private static PdfPTable PdfPTableEmisorRegimenFiscal()
        {
            var pdfPTable = new PdfPTable(1);

            var phrase = new Phrase
            {
                _comprobante.Emisor.RegimenFiscal.Count >= 2 ? new Chunk("RÉGIMENES FISCALES", _fontEmisorTitulos) : new Chunk("RÉGIMEN FISCAL", _fontEmisorTitulos)
            };

            var cell = new PdfPCell(phrase)
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);

            var chunks = _comprobante.Emisor.RegimenFiscal.Select(regimenFiscal => new Chunk(regimenFiscal.Regimen.ToUpper(), _fontEmisorValues)).ToList();

            phrase = new Phrase();
            for (var i = 0; i < chunks.Count; i++)
            {
                phrase.Add(chunks[i]);
                phrase.Add(chunks.Count - 1 != i ? new Chunk(", ", _fontEmisorValues) : new Chunk(".", _fontEmisorValues));
            }
            cell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            pdfPTable.AddCell(cell);

            return pdfPTable;
        }

        private static PdfPTable PdfPTableEmisorDomicilioFiscal()
        {
            var pdfPTable = new PdfPTable(1);

            var cell = new PdfPCell(new Phrase(new Chunk("DOMICILIO", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);

            var phrases = new List<Phrase>();

            var phrase = new Phrase
            {
                new Chunk("CALLE: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.DomicilioFiscal.Calle.ToUpper(), _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Emisor.DomicilioFiscal.NoExterior))
            {
                phrase = new Phrase
                {
                    new Chunk("NÚMERO: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Emisor.DomicilioFiscal.NoExterior.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.DomicilioFiscal.NoInterior))
            {
                phrase = new Phrase
                {
                    new Chunk("INTERIOR: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Emisor.DomicilioFiscal.NoInterior, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.DomicilioFiscal.Colonia))
            {
                phrase = new Phrase
                {
                    new Chunk("COLONIA: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Emisor.DomicilioFiscal.Colonia.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("CÓDIGO POSTAL: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.DomicilioFiscal.CodigoPostal, _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("MUNICIPIO: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.DomicilioFiscal.Municipio.ToUpper(), _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("ESTADO: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.DomicilioFiscal.Estado.ToUpper(), _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("PAÍS: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.DomicilioFiscal.Pais.ToUpper(), _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Emisor.DomicilioFiscal.Localidad))
            {
                phrase = new Phrase
                {
                    new Chunk("LOCALIDAD: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Emisor.DomicilioFiscal.Localidad.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.DomicilioFiscal.Referencia))
            {
                phrase = new Phrase
                {
                    new Chunk("REFERENCIA: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Emisor.DomicilioFiscal.Referencia.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase();
            for (var i = 0; i < phrases.Count; i++)
            {
                phrase.Add(phrases[i]);
                phrase.Add(phrases.Count - 1 != i ? new Chunk(", ", _fontEmisorValues) : new Chunk(".", _fontEmisorValues));
            }
            cell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            pdfPTable.AddCell(cell);

            return pdfPTable;
        }

        private static PdfPTable PdfPTableEmisorExpedidoEn()
        {
            var pdfPTable = new PdfPTable(1);

            if (_comprobante.Emisor.ExpedidoEn == null) return pdfPTable;

            Phrase phrase;

            var pdfPCell = new PdfPCell(new Phrase(new Chunk("EXPEDIDO EN", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(pdfPCell);

            var phrases = new List<Phrase>();

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Calle))
            {
                phrase = new Phrase
                {
                    new Chunk("CALLE: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Calle.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.NoExterior))
            {
                phrase = new Phrase
                {
                    new Chunk("NÚMERO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.NoExterior.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.NoInterior))
            {
                phrase = new Phrase
                {
                    new Chunk("INTERIOR: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.NoInterior.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Colonia))
            {
                phrase = new Phrase
                {
                    new Chunk("COLONIA: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Colonia.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.CodigoPostal))
            {
                phrase = new Phrase
                {
                    new Chunk("CÓDIGO POSTAL: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.CodigoPostal, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Municipio))
            {
                phrase = new Phrase
                {
                    new Chunk("MUNICIPIO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Municipio.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Estado))
            {
                phrase = new Phrase
                {
                    new Chunk("ESTADO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Estado.ToUpper(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("PAÍS: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Emisor.ExpedidoEn.Pais.ToUpper(), _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Localidad))
            {
                phrase = new Phrase
                {
                    new Chunk("LOCALIDAD: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Localidad, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Emisor.ExpedidoEn.Referencia))
            {
                phrase = new Phrase
                {
                    new Chunk("REFERENCIA: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Emisor.ExpedidoEn.Referencia, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase();
            for (var i = 0; i < phrases.Count; i++)
            {
                phrase.Add(phrases[i]);
                phrase.Add(phrases.Count - 1 != i ? new Chunk(", ", _fontEmisorValues) : new Chunk(".", _fontEmisorValues));
            }
            pdfPCell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };

            pdfPTable.AddCell(pdfPCell);

            return pdfPTable;
        }
        
        private static PdfPTable PdfPTableComprobanteReceptor()
        {
            var comprobanteReceptor = new PdfPTable(1);

            var pdfPCellComprobante = new PdfPCell(PdfPTableComprobante())
            {
                Border = 0,
                PaddingBottom = 0
            };
            comprobanteReceptor.AddCell(pdfPCellComprobante);

            var pdfPCellReceptor = new PdfPCell(new PdfPCell(PdfPTableReceptor("RECEPTOR")))
            {
                Border = 0,
            };
            comprobanteReceptor.AddCell(pdfPCellReceptor);

            return comprobanteReceptor;
        }

        private static PdfPTable PdfPTableComprobante()
        {
            var returnTable = new PdfPTable(2);

            var listTables = new List<PdfPTable>();

            var pdfPTable = new PdfPTable(1);

            Phrase phrase;

            var cell = new PdfPCell(new Phrase(new Chunk("FECHA Y HORA", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.Fecha.ToString("").ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("VERSIÓN", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.Version, _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("TIPO DE COMPROBANTE ", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.TipoDeComprobante.ToString().ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("CERTIFICADO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.NoCertificado, _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("FORMA DE PAGO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.FormaDePago.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("MÉTODO DE PAGO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MetodoDePago.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("LUGAR DE EXPEDICIÓN", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.LugarExpedicion.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            if (_comprobante.TipoCambio != null)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("TIPO DE CAMBIO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.TipoCambio, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.Serie))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase { new Chunk("SERIE", _fontComprobanteTitulos) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                phrase = new Phrase { new Chunk(_comprobante.Serie.ToUpper(), _fontComprobanteValues) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.Folio))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase { new Chunk("FOLIO", _fontComprobanteTitulos) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                phrase = new Phrase { new Chunk(_comprobante.Folio, _fontComprobanteValues) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.CondicionesDePago))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("CONDICIONES DE PAGO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.CondicionesDePago.ToUpper(), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.MotivoDescuento))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("MOTIVO DE DESCUENTO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MotivoDescuento.ToUpper(), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.NumCtaPago))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("NÚMERO DE CUENTA DE PAGO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.NumCtaPago, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            //VANTYC if (!string.IsNullOrEmpty(_comprobante.SerieFolioFiscalOrig) || !string.IsNullOrEmpty(_comprobante.FolioFiscalOrig))
            if (!string.IsNullOrEmpty(_comprobante.SerieFolioFiscalOrig) && !string.IsNullOrEmpty(_comprobante.FolioFiscalOrig))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase();
                if (!string.IsNullOrEmpty(_comprobante.Serie)) phrase.Add(new Chunk("SERIE", _fontComprobanteTitulos));
                if (!string.IsNullOrEmpty(_comprobante.Serie) || !string.IsNullOrEmpty(_comprobante.Folio)) phrase.Add(new Chunk(" Y ", _fontComprobanteTitulos));
                if (!string.IsNullOrEmpty(_comprobante.Serie)) phrase.Add(new Chunk("FOLIO", _fontComprobanteTitulos));
                phrase.Add(new Chunk(" FISCAL ORIGINAL", _fontComprobanteTitulos));
                cell = new PdfPCell(phrase)
                {
                      Border = _borderTitulos,
                      PaddingTop = _paddingTopTitulos,
                      PaddingBottom = _paddingBottomTitulos,
                      PaddingLeft = _paddingLeftTitulos,
                      PaddingRight = _paddingRightTitulos,
                };
                pdfPTable.AddCell(cell);
                
                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.SerieFolioFiscalOrig.ToUpper() + _comprobante.FolioFiscalOrig, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };

                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (_comprobante.FechaFolioFiscalOrigSpecified)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("FECHA FOLIO FISCAL ORIGINAL", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.FechaFolioFiscalOrig.ToString(""), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (_comprobante.MontoFolioFiscalOrigSpecified)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("MONTO FOLIO FISCAL ORIGINAL", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MontoFolioFiscalOrig.ToString(""), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            for (byte i = 0; i < listTables.Count; i++)
            {
                if (i != listTables.Count - 1)
                {
                    returnTable.AddCell(new PdfPCell(listTables[i]) { Border = 0 });
                }
                else
                {
                    returnTable.AddCell(!(i + 1).EsPar() ? 
                    new PdfPCell(listTables[i]) { Border = 0, Colspan = 2 } : 
                    new PdfPCell(listTables[i]) { Border = 0 });
                }
            }

            return returnTable;
        }

        private static PdfPTable PdfPTableReceptor(string v, bool minimo = false)
        {
            var returnTable = new PdfPTable(1);

            var tempPdfPCell = new PdfPCell(new Phrase(new Chunk(v, _fontReceptorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            returnTable.AddCell(tempPdfPCell);

            var chunks = new List<Chunk>();
            if (!string.IsNullOrEmpty(_comprobante.Receptor.Nombre)) chunks.Add(new Chunk(string.Format("{0} ", _comprobante.Receptor.Nombre.ToUpper()), _fontReceptorValues));
            chunks.Add(new Chunk(string.Format("({0})", _comprobante.Receptor.Rfc), _fontReceptorValues));
            var phrase = new Phrase();
            foreach (var a in chunks) { phrase.Add(a); }
            tempPdfPCell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues
            };
            returnTable.AddCell(tempPdfPCell);

            if (minimo) return returnTable;

            if (_comprobante.Receptor.Domicilio != null) 
                returnTable.AddCell(new PdfPCell(PdfPTableReceptorDomicilio()) { Border = 0, Padding = 0 });

            return returnTable;
        }

        private static PdfPTable PdfPTableReceptorDomicilio()
        {
            var pdfPTable = new PdfPTable(1);

            Phrase phrase;

            var cell = new PdfPCell(new Phrase(new Chunk("DOMICILIO", _fontReceptorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);

            var phrases = new List<Phrase>();

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Calle))
            {
                phrase = new Phrase
                {
                    new Chunk("CALLE: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Calle.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.NoExterior))
            {
                phrase = new Phrase
                {
                    new Chunk("NÚMERO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.NoExterior.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.NoInterior))
            {
                phrase = new Phrase
                {
                    new Chunk("INTERIOR: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.NoInterior.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Colonia))
            {

                phrase = new Phrase
                {
                    new Chunk("COLONIA: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Colonia.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.CodigoPostal))
            {
                phrase = new Phrase
                {
                    new Chunk("CÓDIGO POSTAL: ",_fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.CodigoPostal, _fontReceptorValues),
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Municipio))
            {
                phrase =new Phrase()
                {
                    new Chunk("MUNICIPIO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Municipio.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Estado))
            {
                phrase = new Phrase
                {
                    new Chunk("ESTADO: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Estado.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("PAÍS: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Receptor.Domicilio.Pais.ToUpper(), _fontReceptorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Localidad))
            {
                phrase = new Phrase
                {
                    new Chunk("LOCALIDAD: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Localidad.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Receptor.Domicilio.Referencia))
            {
                phrase = new Phrase
                {
                    new Chunk("REFERENCIA: ", _fontReceptorValuesB),
                    new Chunk(_comprobante.Receptor.Domicilio.Referencia.ToUpper(), _fontReceptorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase();
            for (var i = 0; i < phrases.Count; i++)
            {
                phrase.Add(phrases[i]);
                phrase.Add(phrases.Count - 1 != i ? new Chunk(", ", _fontReceptorValues) : new Chunk(".", _fontReceptorValues));
            }

            cell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            pdfPTable.AddCell(cell);

            return pdfPTable;
        }

        private static PdfPTable PdfPTableConceptos()
        {

            const float borderWidth = 0.007f;

            var returnPTable = new PdfPTable(7)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
                HorizontalAlignment = 2
            };

            returnPTable.SetWidthPercentage(new[] { _conceptosCol1, _conceptosCol2, _conceptosCol3, _conceptosCol4, _conceptosCol5, _conceptosCol6, _conceptosCol7 }, _document.PageSize);

            var pdfPTable = new PdfPTable(7);
            pdfPTable.SetWidthPercentage(new[] { _conceptosCol1, _conceptosCol2, _conceptosCol3, _conceptosCol4, _conceptosCol5, _conceptosCol6, _conceptosCol7 }, _document.PageSize);

            var cell = new PdfPCell(new Phrase("CANTIDAD", _fontConceptosTitulos))
            {
                Colspan = 2,
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = 2,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNIDAD", _fontConceptosTitulos))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = 1,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("CÓDIGO", _fontConceptosTitulos))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("DESCRIPCIÓN", _fontConceptosTitulos))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNITARIO", _fontConceptosTitulos))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = 2,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("IMPORTE", _fontConceptosTitulos))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = 2,
            };
            pdfPTable.AddCell(cell);

            cell = new PdfPCell(pdfPTable)
            {
                Colspan = 7,
                PaddingBottom = 5,
                PaddingTop = 5,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230)
            };
            returnPTable.AddCell(cell);

            for (var i = 0; i < _comprobante.Conceptos.Count; i++)
            {
               
                var borderColorBottom = new BaseColor(230, 230, 230);

                if (_comprobante.Conceptos.Count == 1) _backgroundColorConceptos = null;

                cell = new PdfPCell(new Phrase((i + 1).ToString(""), FontFactory.GetFont("Arial", 5, Font.NORMAL, BaseColor.BLACK)))
                {
                    Border = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null

                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].Cantidad.ToString(""), _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].Unidad.ToUpper(), _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].NoIdentificacion, _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].Descripcion.ToUpper(), _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].ValorUnitario.ToString("$ #,#.00####"), _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(_comprobante.Conceptos[i].Importe.ToString("$ #,#.00####"), _fontConceptosValues))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = borderWidth,
                    BorderColorBottom = borderColorBottom,
                    PaddingTop = _paddingTopValues,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = i.EsPar() ? _backgroundColorConceptos : null
                };
                returnPTable.AddCell(cell);

            }

            _conceptosHeight = returnPTable.GetRowHeight(1);

            return returnPTable;
        }

        private static PdfPTable PdfPTableTotales()
        {

            var returnPTable = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
            };

            returnPTable.SetWidthPercentage(new[] { _conceptosCol1 + _conceptosCol2 + _conceptosCol3 + _conceptosCol4 + _conceptosCol5, _conceptosCol6 + _conceptosCol7 }, _document.PageSize);

            var pdfPTable = new PdfPTable(4);

            var phrase = new Phrase
            {
                new Chunk( NumLet.Get(_comprobante.Total, _comprobante.Moneda), _fontTotalLetra)
            };
            var cell = new PdfPCell(phrase)
            {
                Colspan = 4,
                Border = 0,
                BackgroundColor = _backgroundColor,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            pdfPTable.AddCell(cell);

            pdfPTable.AddCell(new PdfPCell(new Phrase(new Chunk(""))) { Border = 0, Colspan = 2 });

            pdfPTable.AddCell(new PdfPCell(PdfPTableImpuestosTrasladados()) { Border = 0 });

            pdfPTable.AddCell(new PdfPCell(PdfPTableImpuestosRetenidos()) { Border = 0 });

            cell = new PdfPCell(pdfPTable)
            {
                Border = 0,
            };
            returnPTable.AddCell(cell);

            var pdfPTableTotales = new PdfPTable(2);

            pdfPTableTotales.AddCell(new PdfPCell(new Phrase("SUBTOTAL", _fontTotalesTitulos)) { BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });
            pdfPTableTotales.AddCell(new PdfPCell(new Phrase(_comprobante.SubTotal.ToString(_moneda + "0,0.00####"), _fontTotalesValues)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });

            if (_comprobante.DescuentoSpecified)
            {
                pdfPTableTotales.AddCell(new PdfPCell(new Phrase("DESCUENTO", _fontTotalesTitulos)) { BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });
                pdfPTableTotales.AddCell(new PdfPCell(new Phrase(_comprobante.Descuento.ToString("$ 0,0.00####"), _fontTotalesValues)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });
            }

            if (_comprobante.Impuestos.Traslados.Count != 0)
            {
                if (_comprobante.Impuestos.Traslados.Count == 1)
                {
                    cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", _comprobante.Impuestos.Traslados[0].Impuesto, _comprobante.Impuestos.Traslados[0].Tasa.ToString("0.##")), _fontTotalesTitulos))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    pdfPTableTotales.AddCell(cell);

                    cell = new PdfPCell(new Phrase(_comprobante.Impuestos.Traslados[0].Importe.ToString("$ 0,0.00####"), _fontTotalesValues))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    pdfPTableTotales.AddCell(cell);
                }
                else
                {

                    cell = new PdfPCell(new Phrase("IMPUESTOS\nTRASLADADOS", _fontTotalesTitulos))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    pdfPTableTotales.AddCell(cell);

                    cell = new PdfPCell(new Phrase(_comprobante.Impuestos.Traslados.Sum(x => x.Importe).ToString("$ 0,0.00####"), _fontTotalesValues))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    pdfPTableTotales.AddCell(cell);

                }
                
            }

            if (_comprobante.Impuestos.Retenciones.Count != 0)
            {

                if (_comprobante.Impuestos.Retenciones.Count == 1)
                {

                    cell = new PdfPCell(new Phrase(string.Format("{0} RET", _comprobante.Impuestos.Retenciones[0].Impuesto.ToString().ToUpper()), _fontTotalesTitulos))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    pdfPTableTotales.AddCell(cell);

                    cell = new PdfPCell(new Phrase(_comprobante.Impuestos.Retenciones[0].Importe.ToString("$ 0,0.00####"), _fontTotalesValues))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    pdfPTableTotales.AddCell(cell);

                }
                else
                {

                    cell = new PdfPCell(new Phrase("IMPUESTOS\nRETENIDOS", _fontTotalesTitulos))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    pdfPTableTotales.AddCell(cell);

                    cell = new PdfPCell(new Phrase(_comprobante.Impuestos.Retenciones.Sum(x => x.Importe).ToString("$ 0,0.00####"), _fontTotalesValues))
                    {
                        BackgroundColor = _backgroundColor,
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    pdfPTableTotales.AddCell(cell);

                }

            }

            if (_comprobante.Complemento.ImpuestosLocales != null)
            {
                if (_comprobante.Complemento.ImpuestosLocales.TrasladosLocales.Count != 0)
                {

                    if (_comprobante.Complemento.ImpuestosLocales.TrasladosLocales.Count == 1)
                    {
                        cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", _comprobante.Complemento.ImpuestosLocales.TrasladosLocales[0].ImpLocTrasladado, _comprobante.Complemento.ImpuestosLocales.TrasladosLocales[0].TasadeTraslado.ToString("0.##")), _fontTotalesTitulos))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        };
                        pdfPTableTotales.AddCell(cell);

                        cell = new PdfPCell(new Phrase(_comprobante.Complemento.ImpuestosLocales.TrasladosLocales[0].Importe.ToString("$ 0,0.00####"), _fontTotalesValues))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        };
                        pdfPTableTotales.AddCell(cell);

                    }
                    else
                    {

                        cell = new PdfPCell(new Phrase("IMPTO. LOCALES\nTRASLADADOS", _fontTotalesTitulos))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        pdfPTableTotales.AddCell(cell);

                        cell = new PdfPCell(new Phrase(_comprobante.Complemento.ImpuestosLocales.TrasladosLocales.Sum(x => x.Importe).ToString("$ 0,0.00####"), _fontTotalesValues))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        pdfPTableTotales.AddCell(cell);

                    }

                }
                if (_comprobante.Complemento.ImpuestosLocales.RetencionesLocales.Count != 0)
                {

                    if (_comprobante.Complemento.ImpuestosLocales.RetencionesLocales.Count == 1)
                    {

                        cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", _comprobante.Complemento.ImpuestosLocales.RetencionesLocales[0].ImpLocRetenido, _comprobante.Complemento.ImpuestosLocales.RetencionesLocales[0].TasadeRetencion.ToString("0.##")), _fontTotalesTitulos))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        };
                        pdfPTableTotales.AddCell(cell);

                        cell = new PdfPCell(new Phrase(_comprobante.Complemento.ImpuestosLocales.RetencionesLocales[0].Importe.ToString("$ 0,0.00####"), _fontTotalesValues))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        };
                        pdfPTableTotales.AddCell(cell);

                    }
                    else
                    {

                        cell = new PdfPCell(new Phrase("IMPTO. LOCALES\nRETENDOS", _fontTotalesTitulos))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        pdfPTableTotales.AddCell(cell);

                        cell = new PdfPCell(new Phrase(_comprobante.Complemento.ImpuestosLocales.RetencionesLocales.Sum(x => x.Importe).ToString("$ 0,0.00####"), _fontTotalesValues))
                        {
                            BackgroundColor = _backgroundColor,
                            Border = 0,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        pdfPTableTotales.AddCell(cell);

                    }

                }
            }

            pdfPTableTotales.AddCell(new PdfPCell(new Phrase("TOTAL", _fontTotalesTitulos)) { BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });
            pdfPTableTotales.AddCell(new PdfPCell(new Phrase(_comprobante.Total.ToString("$ 0,0.00####"), _fontTotalesValues)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = _backgroundColor, Border = 0, VerticalAlignment = Element.ALIGN_MIDDLE });

            cell = new PdfPCell(pdfPTableTotales)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            returnPTable.AddCell(cell);

            return returnPTable;
        }

        private static PdfPTable PdfPTableImpuestosRetenidos()
        {

            var pdfPTable = new PdfPTable(1);
            PdfPTable t;
            PdfPCell cell;


            if (_comprobante.Impuestos.Retenciones.Count > 1)
            {

                cell = new PdfPCell(new Phrase(new Chunk("DETALLE IMPUESTOS RETENIDOS", _fontTotalesTitulos)))
                {
                    Border = 0,
                    PaddingTop = _paddingTopTitulos,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
                };
                pdfPTable.AddCell(cell);

                foreach (var i in _comprobante.Impuestos.Retenciones)
                {
                    t = new PdfPTable(2);

                    cell = new PdfPCell(new Phrase(i.Impuesto.ToString(""), _fontTotalesValues))
                    {
                        Border = 0,
                        Padding = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };
                    t.AddCell(cell);

                    cell = new PdfPCell(new Phrase(i.Importe.ToString("$ 0,0.00####"), _fontTotalesValues))
                    {
                        Border = 0,
                        Padding = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    };
                    t.AddCell(cell);

                    cell = new PdfPCell(t)
                    {
                        Border = 0,
                        PaddingTop = _paddingTopValues,
                        PaddingBottom = _paddingBottomValues,
                        PaddingRight = _paddingRightValues,
                        PaddingLeft = _paddingLeftValues,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    };

                    pdfPTable.AddCell(cell);
                }

            }

            if (_comprobante.Complemento.ImpuestosLocales == null) return pdfPTable;

            if (_comprobante.Complemento.ImpuestosLocales.RetencionesLocales.Count < 1) return pdfPTable;

            cell = new PdfPCell(new Phrase(new Chunk("DETALLE IMPTO. LOC. RETENIDOS", _fontImpuestosLocalesTitulos)))
            {
                Border = 0,
                PaddingTop = _paddingTopTitulos,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            pdfPTable.AddCell(cell);

            foreach (var i in _comprobante.Complemento.ImpuestosLocales.RetencionesLocales)
            {
                t = new PdfPTable(2);

                cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", i.ImpLocRetenido, i.TasadeRetencion), _fontImpuestosLocalesValues))
                {
                    Border = 0,
                    Padding = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                };
                t.AddCell(cell);

                cell = new PdfPCell(new Phrase(i.Importe.ToString("$ 0,0.00####"), _fontImpuestosLocalesValues))
                {
                    Border = 0,
                    Padding = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                };
                t.AddCell(cell);

                cell = new PdfPCell(t)
                {
                    Border = 0,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingRight = _paddingRightValues,
                    PaddingLeft = _paddingLeftValues,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                };

                pdfPTable.AddCell(cell);
            }

            return pdfPTable;
        }

        private static PdfPTable PdfPTableImpuestosTrasladados()
        {
            var pdfPTable = new PdfPTable(1);
            PdfPTable t;
            PdfPCell cell;

            if (_comprobante.Impuestos.Traslados.Count > 1)
            {

                cell = new PdfPCell(new Phrase(new Chunk("DETALLE IMPTO. TRASLADADOS", _fontImpuestosLocalesTitulos)))
                {
                    Border = 0,
                    PaddingTop = _paddingTopTitulos,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
                };
                pdfPTable.AddCell(cell);

                foreach (var i in _comprobante.Impuestos.Traslados)
                {
                    t = new PdfPTable(2);

                    cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", i.Impuesto, i.Tasa), _fontImpuestosLocalesValues))
                    {
                        Border = 0,
                        Padding = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };
                    t.AddCell(cell);

                    cell = new PdfPCell(new Phrase(i.Importe.ToString("$ 0,0.00####"), _fontImpuestosLocalesValues))
                    {
                        Border = 0,
                        Padding = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    };
                    t.AddCell(cell);

                    cell = new PdfPCell(t)
                    {
                        Border = 0,
                        PaddingTop = _paddingTopValues,
                        PaddingBottom = _paddingBottomValues,
                        PaddingRight = _paddingRightValues,
                        PaddingLeft = _paddingLeftValues,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    };

                    pdfPTable.AddCell(cell);
                }

            }

            if (_comprobante.Complemento.ImpuestosLocales == null) return pdfPTable;

            if (_comprobante.Complemento.ImpuestosLocales.TrasladosLocales.Count < 1) return pdfPTable;

            cell = new PdfPCell(new Phrase(new Chunk("DETALLE IMPTO. LOC. TRASLADOS", _fontImpuestosLocalesTitulos)))
            {
                Border = 0,
                PaddingTop = _paddingTopTitulos,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            pdfPTable.AddCell(cell);

            foreach (var i in _comprobante.Complemento.ImpuestosLocales.TrasladosLocales)
            {
                t = new PdfPTable(2);

                cell = new PdfPCell(new Phrase(string.Format("{0} {1}%", i.ImpLocTrasladado, i.TasadeTraslado), _fontImpuestosLocalesValues))
                {
                    Border = 0,
                    Padding = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                };
                t.AddCell(cell);

                cell = new PdfPCell(new Phrase(i.Importe.ToString(""), _fontImpuestosLocalesValues))
                {
                    Border = 0,
                    Padding = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                };
                t.AddCell(cell);

                cell = new PdfPCell(t)
                {
                    Border = 0,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingRight = _paddingRightValues,
                    PaddingLeft = _paddingLeftValues,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                };

                pdfPTable.AddCell(cell);
            }


            return pdfPTable;

        }

        private static PdfPTable PdfPTableTimbreFiscalDigital()
        {

            var pdfPTable = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
            };

            pdfPTable.SetWidthPercentage(new[] { 96, _document.PageSize.Width - _document.LeftMargin - _document.RightMargin - 96 }, new Rectangle(12, 12));

            var hints = new Dictionary<EncodeHintType, object> { { EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.Q } };

            var barcodeQrCodeText = string.Format("?re={0}&rr={1}&tt={2}&id{3}", _comprobante.Emisor.Rfc,
                _comprobante.Receptor.Rfc, _comprobante.Total.ToString("0000000000.000000"),
                _comprobante.Complemento.TimbreFiscalDigital.Uuid);

            var barcodeQrCode = new BarcodeQRCode(barcodeQrCodeText, 1, 1, hints);

            var cell = new PdfPCell(barcodeQrCode.GetImage(), true)
            {
                FixedHeight = 96f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 0
            };
            pdfPTable.AddCell(cell);

            var cadenaOriginal = _comprobante.Complemento.TimbreFiscalDigital.CadenaOriginal;

            var comprobanteTimbreFiscalDigital = new PdfPTable(1);

            cell = new PdfPCell(new Phrase("SELLO DIGITAL DEL EMISOR", _fontTimbreFiscalDigitalTitulos))
            {
                Border = 0
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloCfdBase64.Substring(0, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL 
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloCfdBase64.Substring(86, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL 
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase("SELLO DIGITAL DEL SAT", _fontTimbreFiscalDigitalTitulos))
            {
                Border = 0
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloSatBase64.Substring(0, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloSatBase64.Substring(86, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase("CADENA ORIGINAL DEL COMPLEMENTO DE CERTIFICACIÓN DEL SAT", _fontTimbreFiscalDigitalTitulos))
            {
                Border = 0
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(cadenaOriginal.Substring(0, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(cadenaOriginal.Substring(86, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                PaddingBottom = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(new Phrase(cadenaOriginal.Substring(172, 86), _fontTimbreFiscalDigitalValues))
            {
                Border = 0,
                PaddingTop = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            var phrase = new Phrase
            {
                new Chunk("FECHA: ", _fontTimbreFiscalDigitalValuesB),
                new Chunk(_comprobante.Complemento.TimbreFiscalDigital.FechaTimbradoString, _fontTimbreFiscalDigitalValues),
                new Chunk(" UUID: ", _fontTimbreFiscalDigitalValuesB), 
                new Chunk(_comprobante.Complemento.TimbreFiscalDigital.Uuid, _fontTimbreFiscalDigitalValues), 
                new Chunk(" CERTIFICADO SAT: ", _fontTimbreFiscalDigitalValuesB), 
                new Chunk(_comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSat, _fontTimbreFiscalDigitalValues)
            };

            cell = new PdfPCell(phrase)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL
            };
            comprobanteTimbreFiscalDigital.AddCell(cell);

            cell = new PdfPCell(comprobanteTimbreFiscalDigital)
            {
                Border = 0
            };

            pdfPTable.AddCell(cell);

            return pdfPTable;
        }

        private static PdfPTable PdfPTableNomina()
        {

            var pdfPTable = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
                HorizontalAlignment = 2
            };
            
            pdfPTable.SetWidthPercentage(new float[] { 55, 55 }, PageSize.LETTER);

            var pdfPCellImporte = new PdfPCell(PdfTableTituloNomina())
            {
                BorderWidth = 0,
                HorizontalAlignment = 2,
                Colspan = 2
            };
            pdfPTable.AddCell(pdfPCellImporte);


            pdfPCellImporte = new PdfPCell(PdfTablePercepciones())
            {
                BorderWidth = 0,
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(PdfTableDeducciones())
            {
                BorderWidth = 0,
            };
            pdfPTable.AddCell(pdfPCellImporte);


            return pdfPTable;
        }

        private static PdfPTable PdfTableTituloNomina()
        {
            var pdfPTable = new PdfPTable(1);

            var phrases = new List<Phrase>();

            var phrase = new Phrase
            {
                new Chunk("Nomina: ", _fontReceptorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.Version, _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.RegistroPatronal))
            {
                phrase = new Phrase
                {
                    new Chunk("RegistroPatronal: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.RegistroPatronal, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("NumEmpleado: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.NumEmpleado, _fontEmisorValues)
            };
            phrases.Add(phrase);


            phrase = new Phrase
            {
                new Chunk("CURP: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.Curp, _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("TipoRegimen: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.TipoRegimen.ToString(), _fontEmisorValues)
            };
            phrases.Add(phrase);


            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.NumSeguridadSocial))
            {
                phrase = new Phrase
                {
                    new Chunk("NumSeguridadSocial: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.NumSeguridadSocial, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("FechaFinalPago: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.FechaFinalPago.ToString("yyyy-MM-dd"), _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("FechaInicialPago: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.FechaInicialPago.ToString("yyyy-MM-dd"), _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("FechaFinalPago: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.FechaFinalPago.ToString("yyyy-MM-dd"), _fontEmisorValues)
            };
            phrases.Add(phrase);

            phrase = new Phrase
            {
                new Chunk("NumDiasPagados: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.NumDiasPagados.ToString(""), _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.Departamento))
            {
                phrase = new Phrase
                {
                    new Chunk("Departamento: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.Departamento, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.Clabe))
            {
                phrase = new Phrase
                {
                    new Chunk("Clabe: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.Clabe, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.BancoSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("Banco: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.Banco.ToString(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.FechaInicioRelLaboralSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("FechaInicioRelLaboral: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.FechaInicioRelLaboral.ToString("yyyy-MM-dd"), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.AntiguedadSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("Antiguedad: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.Antiguedad.ToString(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.RiesgoPuestoSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("Puesto: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.Puesto, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.TipoContrato))
            {
                phrase = new Phrase
                {
                    new Chunk("TipoContrato: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.TipoContrato, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (!string.IsNullOrEmpty(_comprobante.Complemento.Nomina.TipoJornada))
            {
                phrase = new Phrase
                {
                    new Chunk("TipoJornada: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.TipoJornada, _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            phrase = new Phrase
            {
                new Chunk("PeriodicidadPago: ", _fontEmisorValuesB),
                new Chunk(_comprobante.Complemento.Nomina.PeriodicidadPago, _fontEmisorValues)
            };
            phrases.Add(phrase);

            if (_comprobante.Complemento.Nomina.SalarioBaseCotAporSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("SalarioBaseCotApor: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.SalarioBaseCotApor.ToString(""), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.RiesgoPuestoSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("RiesgoPuesto: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.RiesgoPuesto.ToString(), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }

            if (_comprobante.Complemento.Nomina.SalarioDiarioIntegradoSpecified)
            {
                phrase = new Phrase
                {
                    new Chunk("SalarioDiarioIntegrado: ", _fontEmisorValuesB),
                    new Chunk(_comprobante.Complemento.Nomina.SalarioDiarioIntegrado.ToString(""), _fontEmisorValues)
                };
                phrases.Add(phrase);
            }


            phrase = new Phrase();
            for (var i = 0; i < phrases.Count; i++)
            {
                phrase.Add(phrases[i]);
                phrase.Add(phrases.Count - 1 != i ? new Chunk(", ", _fontEmisorValues) : new Chunk(".", _fontEmisorValues));
            }
            var cell = new PdfPCell(phrase)
            {
                Border = _borderValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            pdfPTable.AddCell(cell);

            return pdfPTable;
        }

        private static PdfPTable PdfTablePercepciones()
        {

            var pdfPTable = new PdfPTable(5)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
                HorizontalAlignment = 2
            };

            pdfPTable.SetWidthPercentage(new float[] { 8, 8, 60, 14, 10 }, PageSize.LETTER);

            var pdfPCellImporte = new PdfPCell(new Phrase("Tipo", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0.07f,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),


                HorizontalAlignment = Element.ALIGN_CENTER
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Clave", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_CENTER
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Percepción Concepto", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_LEFT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Grabado", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Exento", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            foreach (var percepcion in _comprobante.Complemento.Nomina.Percepciones.Percepcion)
            {

                pdfPCellImporte = new PdfPCell(new Phrase(percepcion.TipoPercepcion.ToString(), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(percepcion.Clave, _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(percepcion.Concepto, _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(percepcion.ImporteGravado.ToString("$ 0,0.00####"), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(percepcion.ImporteExento.ToString("$ 0,0.00####"), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                pdfPTable.AddCell(pdfPCellImporte);

            }

            pdfPCellImporte = new PdfPCell(new Phrase("Totales", _fontNominaTitulos))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 3
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase(_comprobante.Complemento.Nomina.Percepciones.TotalGravado.ToString("$ 0,0.00####"), _fontNominaValues))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase(_comprobante.Complemento.Nomina.Percepciones.TotalExento.ToString("$ 0,0.00####"), _fontNominaValues))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            return pdfPTable;
        }

        private static PdfPTable PdfTableDeducciones()
        {


            var pdfPTable = new PdfPTable(5)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
                HorizontalAlignment = 2
            };


            pdfPTable.SetWidthPercentage(new float[] { 8, 8, 60, 14, 10 }, PageSize.LETTER);

            var pdfPCellImporte = new PdfPCell(new Phrase("Tipo", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0.07f,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_CENTER,
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Clave", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                HorizontalAlignment = Element.ALIGN_CENTER,
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Deducción Concepto", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),
                
                HorizontalAlignment = Element.ALIGN_LEFT,
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Grabado", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                BorderWidth = 0.07f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase("Exento", _fontNominaTitulos))
            {
                BorderWidthTop = 0.07f,
                BorderWidthBottom = 0.07f,
                BorderWidthLeft = 0,
                BorderWidthRight = 0.07f,
                BackgroundColor = _backgroundColor,
                BorderColor = new BaseColor(230, 230, 230),

                BorderWidth = 0.07f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            };
            pdfPTable.AddCell(pdfPCellImporte);





            foreach (var deduccion in _comprobante.Complemento.Nomina.Deducciones.Deduccion)
            {


                pdfPCellImporte = new PdfPCell(new Phrase(deduccion.TipoDeduccion.ToString(), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(deduccion.Clave, _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(deduccion.Concepto, _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(deduccion.ImporteGravado.ToString("$ 0,0.00####"), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                pdfPTable.AddCell(pdfPCellImporte);

                pdfPCellImporte = new PdfPCell(new Phrase(deduccion.ImporteExento.ToString("$ 0,0.00####"), _fontNominaValues))
                {
                    BorderWidthTop = 0,
                    BorderWidthBottom = 0.07f,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColor = new BaseColor(230, 230, 230),
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                pdfPTable.AddCell(pdfPCellImporte);




            }



            pdfPCellImporte = new PdfPCell(new Phrase("Totales", _fontNominaTitulos))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 3
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase(_comprobante.Complemento.Nomina.Deducciones.TotalGravado.ToString("$ 0,0.00####"), _fontNominaValues))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);

            pdfPCellImporte = new PdfPCell(new Phrase(_comprobante.Complemento.Nomina.Deducciones.TotalExento.ToString("$ 0,0.00####"), _fontNominaValues))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            pdfPTable.AddCell(pdfPCellImporte);




            return pdfPTable;
        }

        private static PdfPTable Pagare()
        {

            var pdfPTable = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.Left - _document.RightMargin,
                HorizontalAlignment = 2
            };

            pdfPTable.SetWidthPercentage(new float[] { 55, 55 }, PageSize.LETTER);

            var datosPagare = "PAGARÉ # %documento% EN %ciudad% %documentofecha% DEBE(MOS) Y PAGARÉ(MOS) INCONDICIONALMENTE A LA ORDEN DE %beneficiario% EN %ciudad%. EL DÍA %documentovence% LA CANTIDAD DE %total% %totalletra% INCLUSIVE SI EL PRESENTE PAGARÉ ES SUSCRITO POR UN FACTOR, EMPLEADO O DEPENDIENTE DEL DEUDOR Y/O SUSCRIPTOR PRINCIPAL EN LOS TÉRMINOS DE LOS ARTÍCULOS 315 Y 316 Y DEMÁS RELATIVOS Y APLICABLES DEL CÓDIGO DE COMERCIO. VALOR DE LA MERCANCÍA HE(MOS) RECIBIDO A MI(NUESTRA) ENTERA SATISFACCIÓN ESTE PAGARE ES MERCANTIL DE UNA SERIE NUMERADA DEL ____ AL ____ Y TODOS ESTÁN SUJETOS A LA CONDICIÓN DE QUE DE NO PAGARSE CUALQUIERA DE ELLOS A SU VENCIMIENTO SERÁN EXIGIBLES TODOS LOS QUE SE LE SIGUEN EN NÚMERO, ADEMÁS DE LOS YA VENCIDOS DE ACUERDO A LO ESTABLECIDO POR EL ARTÍCULO 79 DE LA LEY GENERAL DE TÍTULOS Y OPERACIONES DE CRÉDITO CAUSANDO UN INTERÉS MORATORIO DEL 10% POR CADA MES O FRACCIÓN PAGADERO JUNTAMENTE CON EL PRINCIPAL DICHOS INTERESES SE CAUSARAN SOBRE EL CAPITAL INSOLUTO CONFORME A LO DISPUESTO POR EL ARTÍCULO 152 INCISO I,II, III,IV DE LA LEY GENERAL DE TÍTULOS Y OPERACIONES DE CRÉDITO";

            if (datosPagare.IndexOf("%documento%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%documento%", _comprobante.Serie + _comprobante.Folio);
            if (datosPagare.IndexOf("%documentofecha%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%documentofecha%", _comprobante.Fecha.ToString("D").ToUpper());
            if (datosPagare.IndexOf("%documentovence%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%documentovence%", "=DOCUMENTOVENCE=");
            if (datosPagare.IndexOf("%ciudad%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%ciudad%", (_comprobante.Emisor.DomicilioFiscal.Municipio + " " + _comprobante.Emisor.DomicilioFiscal.Estado).ToUpper());
            if (datosPagare.IndexOf("%total%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%total%", _comprobante.Total.ToString("C"));
            if (datosPagare.IndexOf("%totalletra%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%totalletra%", NumLet.Get(_comprobante.Total, _comprobante.Moneda));
            if (datosPagare.IndexOf("%beneficiario%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%beneficiario%", _comprobante.Emisor.Nombre.ToUpper());
            if (datosPagare.IndexOf("%t%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%t%", "=T=");
            if (datosPagare.IndexOf("%fl%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%fl%", "=FL=");
            if (datosPagare.IndexOf("%f%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%f%", "=F=");
            if (datosPagare.IndexOf("%hl%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%hl%", "=HL=");
            if (datosPagare.IndexOf("%h%", StringComparison.Ordinal) != -1) datosPagare = datosPagare.Replace("%h%", "=H=");

            var pdfPCellPagare = new PdfPCell(new Phrase(new Chunk(datosPagare, _fontPagareValues)))
            {
                BorderWidth = 0,
                BorderWidthTop = 0.07f,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Colspan = 2,
                PaddingBottom = 6
            };
            pdfPTable.AddCell(pdfPCellPagare);

            var pdfPCellDatosDeudor = new PdfPCell(PdfPTableReceptor("DEUDOR: "))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.07f,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Colspan = 1,
                PaddingBottom = 6
            };
            pdfPTable.AddCell(pdfPCellDatosDeudor);

            var pdfPCellFirmaDeudor = new PdfPCell(new Phrase(new Chunk("FIRMA DEL DEUDOR:____________________", _fontPagareTitulos)))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.07f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                Colspan = 1,
                PaddingBottom = 6
                
            };
            pdfPTable.AddCell(pdfPCellFirmaDeudor);

            return pdfPTable;
        }


        private static PdfPTable PdfTableValidacion()
        {

            var returnTable = new PdfPTable(1);

            var tempTable = new PdfPTable(3);

            tempTable.SetWidthPercentage(new float[] { 20, 60, 20 }, PageSize.LETTER);

            var tempPhrase = new Phrase(DateTime.Now.ToShortDateString(), FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            var tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase("REPORTE DE VALIDACIÓN DE COMPROBANTES", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase(DateTime.Now.ToLongTimeString().ToUpper(), FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(tempTable) { Border = 0 };
            returnTable.AddCell(tempCell);


            tempTable = new PdfPTable(2);
            tempTable.SetWidthPercentage(new float[] { 50, 50 }, PageSize.LETTER);

            tempPhrase = new Phrase("Documento", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = _backgroundColor,
                Colspan = 2
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(PdfPTableComprobanteValidacion())
            {
                Border = 0,
                Colspan = 2
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(PdfPTableEmisor(true))
            {
                Border = 0
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(PdfPTableReceptor("RECEPTOR", true))
            {
                Border = 0
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(tempTable) { Border = 0 };
            returnTable.AddCell(tempCell);


            tempTable = new PdfPTable(1);

            tempPhrase = new Phrase("Sello del Emisor", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                PaddingTop = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = _backgroundColor
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Validación", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase("El sello del documento es " + (_comprobante.SelloValido ? "Valido" : "¡INVALIDO!"), FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Sello", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase(_comprobante.SelloBase64, FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Cadena Original", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase(_comprobante.CadenaOriginal, FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(tempTable) { Border = 0 };
            returnTable.AddCell(tempCell);


            tempTable = new PdfPTable(1);

            tempPhrase = new Phrase("Sello del timbre fiscal digital", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                PaddingTop = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = _backgroundColor
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Validación", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = _comprobante.Complemento.TimbreFiscalDigital.SelloValido == null ? 
                new Phrase("No fue posible validar el sello debido a la falta del certificado " + _comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSat , FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK)) : 
                new Phrase("Sello del timbre fiscal digital es " + ((bool) _comprobante.Complemento.TimbreFiscalDigital.SelloValido ? "valido" : "¡INVALIDO!"), FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Sello", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloSatBase64, FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Cadena Original", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            tempPhrase = new Phrase(_comprobante.Complemento.TimbreFiscalDigital.CadenaOriginal, FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(tempTable) { Border = 0 };
            returnTable.AddCell(tempCell);


            tempTable = new PdfPTable(1);

            tempPhrase = new Phrase("Estructura del XML", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY));
            tempCell = new PdfPCell(tempPhrase)
            {
                Border = 0,
                PaddingTop = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = _backgroundColor
            };
            tempTable.AddCell(tempCell);

            tempCell = new PdfPCell(new Phrase(new Chunk("Validación", _fontEmisorTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingRightTitulos
            };
            tempTable.AddCell(tempCell);

            var validar = new Validar(_comprobante);
            if (validar.ErroresEstructura.Count != 0)
            {
                foreach (var s in validar.ErroresEstructura)
                {

                    tempPhrase = new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
                    tempCell = new PdfPCell(tempPhrase)
                    {
                        Border = _borderValues,
                        PaddingTop = _paddingTopValues,
                        PaddingBottom = _paddingBottomValues,
                        PaddingLeft = _paddingLeftValues,
                        HorizontalAlignment = Element.ALIGN_JUSTIFIED
                    };
                    tempTable.AddCell(tempCell);
                }
            }
            else
            {
                tempPhrase = new Phrase("Estructura del XML valida", FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK));
                tempCell = new PdfPCell(tempPhrase)
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED
                };
                tempTable.AddCell(tempCell);
            }

            tempCell = new PdfPCell(tempTable) { Border = 0 };
            returnTable.AddCell(tempCell);

            



            return returnTable;

        }


        private static PdfPTable PdfPTableComprobanteValidacion()
        {
            var returnTable = new PdfPTable(4);

            var listTables = new List<PdfPTable>();

            var pdfPTable = new PdfPTable(1);

            Phrase phrase;

            var cell = new PdfPCell(new Phrase(new Chunk("FECHA Y HORA", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.Fecha.ToString("").ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("VERSIÓN", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.Version, _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("TIPO DE COMPROBANTE ", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.TipoDeComprobante.ToString().ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("CERTIFICADO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.NoCertificado, _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("FORMA DE PAGO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.FormaDePago.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("MÉTODO DE PAGO", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MetodoDePago.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            pdfPTable = new PdfPTable(1);
            cell = new PdfPCell(new Phrase(new Chunk("LUGAR DE EXPEDICIÓN", _fontComprobanteTitulos)))
            {
                Border = _borderTitulos,
                PaddingTop = _paddingTopTitulos,
                PaddingBottom = _paddingBottomTitulos,
                PaddingLeft = _paddingLeftTitulos,
                PaddingRight = _paddingTopTitulos
            };
            pdfPTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk(_comprobante.LugarExpedicion.ToUpper(), _fontComprobanteValues)))
            {
                Border = _borderValues,
                PaddingTop = _paddingTopValues,
                PaddingBottom = _paddingBottomValues,
                PaddingLeft = _paddingLeftValues,
                PaddingRight = _paddingRightValues,
            };
            pdfPTable.AddCell(cell);
            listTables.Add(pdfPTable);

            if (_comprobante.TipoCambio != null)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("TIPO DE CAMBIO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.TipoCambio, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.Serie))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase { new Chunk("SERIE", _fontComprobanteTitulos) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                phrase = new Phrase { new Chunk(_comprobante.Serie.ToUpper(), _fontComprobanteValues) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.Folio))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase { new Chunk("FOLIO", _fontComprobanteTitulos) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                phrase = new Phrase { new Chunk(_comprobante.Folio, _fontComprobanteValues) };
                cell = new PdfPCell(phrase)
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.CondicionesDePago))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("CONDICIONES DE PAGO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.CondicionesDePago.ToUpper(), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.MotivoDescuento))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("MOTIVO DE DESCUENTO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MotivoDescuento.ToUpper(), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.NumCtaPago))
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("NÚMERO DE CUENTA DE PAGO", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.NumCtaPago, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (!string.IsNullOrEmpty(_comprobante.SerieFolioFiscalOrig) || !string.IsNullOrEmpty(_comprobante.FolioFiscalOrig))
            {
                pdfPTable = new PdfPTable(1);
                phrase = new Phrase();
                if (!string.IsNullOrEmpty(_comprobante.Serie)) phrase.Add(new Chunk("SERIE", _fontComprobanteTitulos));
                if (!string.IsNullOrEmpty(_comprobante.Serie) || !string.IsNullOrEmpty(_comprobante.Folio)) phrase.Add(new Chunk(" Y ", _fontComprobanteTitulos));
                if (!string.IsNullOrEmpty(_comprobante.Serie)) phrase.Add(new Chunk("FOLIO", _fontComprobanteTitulos));
                phrase.Add(new Chunk(" FISCAL ORIGINAL", _fontComprobanteTitulos));
                cell = new PdfPCell(phrase)
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingTopTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.SerieFolioFiscalOrig.ToUpper() + _comprobante.FolioFiscalOrig, _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (_comprobante.FechaFolioFiscalOrigSpecified)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("FECHA FOLIO FISCAL ORIGINAL", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.FechaFolioFiscalOrig.ToString(""), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            if (_comprobante.MontoFolioFiscalOrigSpecified)
            {
                pdfPTable = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(new Chunk("MONTO FOLIO FISCAL ORIGINAL", _fontComprobanteTitulos)))
                {
                    Border = _borderTitulos,
                    PaddingTop = _paddingTopTitulos,
                    PaddingBottom = _paddingBottomTitulos,
                    PaddingLeft = _paddingLeftTitulos,
                    PaddingRight = _paddingTopTitulos
                };
                pdfPTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Chunk(_comprobante.MontoFolioFiscalOrig.ToString(""), _fontComprobanteValues)))
                {
                    Border = _borderValues,
                    PaddingTop = _paddingTopValues,
                    PaddingBottom = _paddingBottomValues,
                    PaddingLeft = _paddingLeftValues,
                    PaddingRight = _paddingRightValues,
                };
                pdfPTable.AddCell(cell);
                listTables.Add(pdfPTable);
            }

            for (byte i = 0; i < listTables.Count; i++)
            {
                if (i != listTables.Count - 1)
                {
                    returnTable.AddCell(new PdfPCell(listTables[i]) { Border = 0 });
                }
                else
                {
                    returnTable.AddCell(!(i + 1).EsPar() ?
                    new PdfPCell(listTables[i]) { Border = 0, Colspan = 2 } :
                    new PdfPCell(listTables[i]) { Border = 0 });
                }
            }

            return returnTable;
        }

    }

}
