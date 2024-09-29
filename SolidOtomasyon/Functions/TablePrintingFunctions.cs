using System;
using System.Drawing;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SolidOtomasyon.Forms.MainForms;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Functions
{
    public class TablePrintingFunctions
    {

        private static GridView _tablo;

        private static string _subeAdi;

        //
        private static PrintableComponentLink _link;
        //Verilerin gösterileceği yeri ifade ediyor
        private static PrintingSystem _ps;

        private static DokumParametreleri _dokumParametreleri;


        public static void Yazdir(GridView tablo , string raporBaslik , string subeAdi)
        {
            _link = new PrintableComponentLink();
            _ps = new PrintingSystem();
            _tablo = tablo;
            _subeAdi = subeAdi;
            //Açılacak olan formu kastediyor içerik dolacak
            _dokumParametreleri = ShowEditForms<TabloDokumParametreleri>.ShowDialogEditForm<DokumParametreleri>(raporBaslik);

            RaporDokumu();
        }

        private static void RaporDokumu()
        {
            BaslikEkle();
            RaporuKagidaSigdirma();

            _tablo.OptionsPrint.PrintHorzLines = _dokumParametreleri.YatayCizgileriGoster == EvetHayir.Evet;
            _tablo.OptionsPrint.PrintVertLines = _dokumParametreleri.DikeyCizgileriGoster == EvetHayir.Evet;
            _tablo.OptionsPrint.PrintHeader = _dokumParametreleri.SutunBasliklariniGoster == EvetHayir.Evet;
            //Başlığı başta gösterme
            _tablo.OptionsView.ShowViewCaption = false;

            //tablodakileri componente aktarıyozuz
            _link.Component = _tablo.GridControl;
            //Kağıt Tipi
            //_link.PaperKind = PaperKind.Letter;
            //Ayarlanmış rakamlardır 
            _link.Margins = new Margins(59, 59, 115, 48);    
            _link.CreateMarginalHeaderArea += Link_CreateMarginalHeaderArea;
            //En Son Dökümanı oluşturuyoruz ...
            _link.CreateDocument(_ps);

            switch (_dokumParametreleri.DokumSekli)
            {
                case DokumSekli.TabloBaskiOnizleme:
                    //Baskı önizleme için dialog olarak açılacak -> true veriyoruz
                    ShowRibbonForms<RaporOnizleme>.ShowForm(true,_ps,_dokumParametreleri.RaporBaslik);
                    break;
                case DokumSekli.TabloYazdir:
                    for (int i = 0; i < _dokumParametreleri.YazdirilacakAdet; i++)
                        _link.Print(_dokumParametreleri.YaziciAdi);
                    
                    break;                          
            }
            //Başlığı göster
            _tablo.OptionsView.ShowViewCaption = true;
        }

        private static void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            if (_dokumParametreleri.BaslikEkle == EvetHayir.Hayir)
                return;

            var boldFont = new Font("Tahoma",8,FontStyle.Bold);
            var regularFont = new Font("Tahoma",8, FontStyle.Regular);

            
            var sayfaBrick = new PageInfoBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = regularFont,
                //Bulunan sayfa ve toplam sayfayı göster
                PageInfo = PageInfo.NumberOfTotal,
                Format = "Sayfa : {0} / {1}",
                Alignment=BrickAlignment.Far,
                AutoWidth=true
            };

            // Sanal bir dikdörtgen oluşturduk ...
            _ps.Graph.DrawBrick(sayfaBrick,new RectangleF(200,25,40,15));

            var tarihBrick = new PageInfoBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = regularFont,
                //Bulunan sayfa ve toplam sayfayı göster
                PageInfo = PageInfo.DateTime,
                Format = "Tarih : {0:dd.MM.yyyy}",
                Alignment = BrickAlignment.Far,
                AutoWidth = true
            };
            //Denenmiş Değerler
            _ps.Graph.DrawBrick(tarihBrick, new RectangleF(0, 40, 50, 15));

            var subeBaslikBrick = new TextBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = boldFont,
                Text="Şube"
            };
            //Denenmiş Değerler
            _ps.Graph.DrawBrick(subeBaslikBrick, new RectangleF(0, 25, 40, 15));

            var subeDegerBrick = new TextBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = regularFont,
                Text = ": " + _subeAdi
            };
            //Denenmiş Değerler
            _ps.Graph.DrawBrick(subeDegerBrick, new RectangleF(55, 25, 500, 15));

            var donemBaslikBrick = new TextBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = boldFont,
                Text = "Dönem"
            };
            //Denenmiş Değerler
            _ps.Graph.DrawBrick(donemBaslikBrick, new RectangleF(0, 40, 40, 15));

            var donemDegerBrick = new TextBrick(BorderSide.None, 0, Color.Transparent, Color.Transparent, Color.Black)
            {
                Font = regularFont,
                Text = $": {AnaForm.DonemAdi}"
            };
            //Denenmiş Değerler
            _ps.Graph.DrawBrick(donemDegerBrick, new RectangleF(55, 40, 215, 15));

        }

        private static void RaporuKagidaSigdirma()
        {

            YazdirmaYonuAyarla();

            switch (_dokumParametreleri.RaporuKagidaSigdir)
            {
                case Takip.Common.Enums.RaporuKagidaSigdirma.SutunlariDaraltarakSigdir:
                    _tablo.OptionsPrint.AutoWidth = true;
                    break;
                case Takip.Common.Enums.RaporuKagidaSigdirma.YaziBoyutunuKuculterekSigdir:
                    _tablo.OptionsPrint.AutoWidth = false;
                    //Yazı boyutunu küçülterek sayfaya sığdırır
                    _ps.Document.AutoFitToPagesWidth = 1;
                    break;

                default:
                    _tablo.OptionsPrint.AutoWidth = false;
                    //sığdığı kadarını sığdır kalanı diğer sayfaya at ...
                    _ps.Document.ScaleFactor = 1.0f;
                    break;
            }

        }

        private static void YazdirmaYonuAyarla()
        {

            switch (_dokumParametreleri.YazdirmaYonu)
            {
                case YazdirmaYonu.Dikey:
                    //Yatay Yapma
                    _link.Landscape = false;
                    break;
                case YazdirmaYonu.Yatay:
                    _link.Landscape = true;
                    break;
                case YazdirmaYonu.Otomatik:
                    _link.Landscape = OtomatikYazdirmaYonu();
                    break;  
            }
        }

        private static bool OtomatikYazdirmaYonu()
        {
            const int sayfaGenisligi = 703;
            var tabloSutunGenislikleri = 0;

            for (int i = 0; i < _tablo.Columns.Count; i++)
                if (_tablo.Columns[i].Visible)
                {
                    tabloSutunGenislikleri += _tablo.Columns[i].Width;
                }
            //Sütun genişliği büyük ise yatay olarak çeviriyor return true
            return tabloSutunGenislikleri > sayfaGenisligi;
         
        }



        private static void BaslikEkle()
        {
            if (_dokumParametreleri.BaslikEkle ==EvetHayir.Hayir)
            {
                return;
            }
            //Rapor üzerindeki kısmı yazıyoruz 
            var headerArea = new PageHeaderArea();
            headerArea.Content.AddRange(new[] {"",_dokumParametreleri.RaporBaslik,"" });
            headerArea.Font = new Font("Arial Narrow", 10f, FontStyle.Bold);
            //Konumlandırma Ayarı Far : Altta
            headerArea.LineAlignment = BrickAlignment.Far;

            _link.PageHeaderFooter = new PageHeaderFooter(headerArea,null);
        }
    }
}
