using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SolidOtomasyon.Functions
{
    public static class FileFunctions
    {

        //Extension metot
        public static void FormSablonKaydet(this string sablonAdi, int solUzaklik, int ustUzaklik, int Genislik, int Yukseklik, FormWindowState windowState)
        {
            //Hata durumlarında
            try
            {
                //Şablon dosyaları yoksa ekle
                if (!Directory.Exists(Application.StartupPath + @"\SablonDosyalari"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\SablonDosyalari");
                }
                var settings = new XmlWriterSettings
                {
                    //Girinti oluşturmaya izin verdik
                    Indent = true,

                };
                var writer = XmlWriter.Create(Application.StartupPath + @"\SablonDosyalari\" + sablonAdi + "_location.xml", settings);
                //Xml dosyası yazma
                writer.WriteStartDocument();
                writer.WriteComment("Solid Otomasyon Tarafından Yazıldı");
                writer.WriteStartElement("Tablo");
                writer.WriteStartElement("Location");
                writer.WriteAttributeString("Left", solUzaklik.ToString());
                writer.WriteAttributeString("Top", ustUzaklik.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("FormSize");
                if (windowState == FormWindowState.Maximized)
                {
                    //Tam ekran ise alıp kaydetmemesi lazım çünkü Ekran çözünürlüğü ekrandan ekrana farklılık gösterebilir ...
                    writer.WriteAttributeString("Width", "-1");
                    writer.WriteAttributeString("Hight", "-1");
                }
                else
                {
                    writer.WriteAttributeString("Width", Genislik.ToString());
                    writer.WriteAttributeString("Hight", Yukseklik.ToString());
                }

                writer.WriteEndElement();
                //Tabloyu kapattık
                writer.WriteEndElement();
                //Document Kapatma
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
            }
        }

        public static void FormSablonYukle(this string sablonAdi, XtraForm frm)
        {
            var list = new List<string>();

            try
            {
                if (File.Exists(Application.StartupPath + $@"\SablonDosyalari\{sablonAdi}_location.xml"))
                {
                    var reader = XmlReader.Create(Application.StartupPath + $@"\SablonDosyalari\{sablonAdi}_location.xml");

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Location")
                        {
                            //Location'un Left ve Top eklemiş olduk
                            list.Add(reader.GetAttribute(0));
                            list.Add(reader.GetAttribute(1));
                        }
                        else if (reader.NodeType == XmlNodeType.Element && reader.Name == "FormSize")
                        {
                            list.Add(reader.GetAttribute(0));
                            list.Add(reader.GetAttribute(1));
                        }

                    }
                    reader.Close();
                    reader.Dispose();
                }

            }
            catch (Exception ex)
            {

                Messages.HataMesaji(ex.Message);
            }

            if (list.Count <= 0)
            {
                //Liste boş ise okuma
                return;
            }
            //Location ayarladık
            frm.Location = new System.Drawing.Point(int.Parse(list[0]), int.Parse(list[1]));
            if (list[2] == "-1" && list[3] == "-1")
            {
                frm.WindowState = FormWindowState.Maximized;
            }
            else
                //2 ve 3 Width ve hight
                frm.Size = new System.Drawing.Size(int.Parse(list[2]), int.Parse(list[3]));

        }

        public static void TabloSablonKaydet(this GridView tablo, string sablonAdi)
        {
            try
            {
                //Filtreleri kaldıracağız filtreli kaydetmesin diye
                tablo.ClearColumnsFilter();

                if (!Directory.Exists(Application.StartupPath + @"\SablonDosyalari"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\SablonDosyalari");
                }

                tablo.SaveLayoutToXml(Application.StartupPath + $@"\SablonDosyalari\{sablonAdi}.xml");

            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
            }
        }

        public static void TabloSablonYukle(this GridView tablo, string sablonAdi)
        {
            try
            {
                if (File.Exists(Application.StartupPath + $@"\SablonDosyalari\{sablonAdi}.xml"))
                    tablo.RestoreLayoutFromXml(Application.StartupPath + $@"\SablonDosyalari\{sablonAdi}.xml");


            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
            }
        }



        public static void TabloDisariAktar(this GridView tablo, DosyaTuru dosyaTuru, string dosyaFormati, string excelSayfaAdi = null)
        {
            if (Messages.TabloExportMesaj(dosyaFormati) != DialogResult.Yes)
            {
                return;
            }

            if (!Directory.Exists(Application.StartupPath + @"\Temp"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Temp");
            }

            var dosyaAdi = Guid.NewGuid().ToString();

            var filePath = $@"{Application.StartupPath}\Temp\{dosyaAdi}";

            switch (dosyaTuru)
            {
                case DosyaTuru.ExcelStandart:
                    {
                        var opt = new XlsxExportOptionsEx
                        {
                            ExportType = ExportType.Default,
                            SheetName = excelSayfaAdi,
                            TextExportMode = TextExportMode.Text
                        };

                        filePath = filePath + ".xlsx";
                        tablo.ExportToXlsx(filePath, opt);
                    }
                    break;

                case DosyaTuru.ExcelFormatli:
                    {
                        var opt = new XlsxExportOptionsEx
                        {
                            //Hazır filtrenebilir özellikler geliyor
                            ExportType = ExportType.WYSIWYG,
                            SheetName = excelSayfaAdi,
                            TextExportMode = TextExportMode.Text
                        };

                        filePath = filePath + ".xlsx";
                        tablo.ExportToXlsx(filePath, opt);
                    }
                    break;

                case DosyaTuru.ExcelFormatsiz:
                    {
                        var opt = new CsvExportOptionsEx
                        {
                            ExportType = ExportType.WYSIWYG,
                            TextExportMode = TextExportMode.Text
                        };

                        filePath = filePath + ".csv";
                        tablo.ExportToCsv(filePath, opt);
                    }
                    break;

                case DosyaTuru.WordDosyasi:
                    {
                        filePath = filePath + ".rtf";
                        tablo.ExportToRtf(filePath);
                    }
                    break;
                case DosyaTuru.PdfDosyasi:
                    {
                        filePath = filePath + ".pdf";
                        tablo.ExportToPdf(filePath);

                
                    }
                    break;

                case DosyaTuru.TxtDosyasi:
                    {
                        var opt = new TextExportOptions
                        {
                            TextExportMode = TextExportMode.Text

                        };
                        filePath = filePath + ".txt";
                        tablo.ExportToText(filePath);
                    }
                    break;
            }

            if (!File.Exists(filePath))
            {
                Messages.HataMesaji("Tablo Verileri Dosyaya Aktarılırken bir sorun oluştu");
                return;
            }
            //Oluşan dosyayı aç
            Process.Start(filePath);


        }


    }
}
