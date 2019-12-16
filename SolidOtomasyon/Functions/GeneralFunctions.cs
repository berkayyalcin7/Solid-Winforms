using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Message;
using SolidOtomasyon.Takip.Model.Entities.Base;
using SolidOtomasyon.UserControls.Controls;
using System;
using System.Windows.Forms;

namespace SolidOtomasyon.Functions
{
    public static class GeneralFunctions
    {
        public static long GetRowId(this GridView tablo)
        {
            //Focuslanan index'in 0 veya büyük rakam olduğunu anlamak için kontrol yapacağız
            if (tablo.FocusedRowHandle > -1)
            {

                //O satırın Id değerini al longa cast et
                return (long)tablo.GetFocusedRowCellValue("Id");

            }
            Messages.KartSecUyariMesaji();
            return -1;
        }


        public static T GetRow<T>(this GridView tablo, bool mesajVer = true)
        {
            if (tablo.FocusedRowHandle > -1) return (T)tablo.GetRow(tablo.FocusedRowHandle);

            if (mesajVer)
                Messages.KartSecUyariMesaji();
            //null olan haliyle geri gönder
            return default(T);
        }

        //Değişimi karşılaştıracağız Enum'a göre değer döndüreceğiz
        private static VeriDegisimYeri VeriDegisimYeriGetir<T>(T oldEntity, T currentEntity)
        {
            //Bll'deki GeneralFunctions'dan DeğişimleriGetir kısmıyla aynı
            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                // ICollection Tipinde bi Property' sahip ise örnek ICollection<Ilce> Ilce tipinde ise işlem yapmadan devam et
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                // Gelen değer Null ise String.Empty yap -> Karşılaştırabilmesi için Emptye dönüştürdük.
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;

                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                //Örnek PRopertyler Byte Dizisi şeklindeyse -> Resim Dosyası
                if (prop.PropertyType == typeof(byte[]))
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        //Eğer oldValue null veya empty ise byte oluştur ve Default 0 ata
                        oldValue = new byte[] { 0 };
                    }

                    if (string.IsNullOrEmpty(currentValue.ToString()))
                    {
                        //Eğer currentValue null veya empty ise byte oluştur ve Default 0 ata
                        currentValue = new byte[] { 0 };
                    }
                    //Byte'a cast edip Uzunluğunu alıyoruz.
                    if (((byte[])oldValue).Length != ((byte[])currentValue).Length)
                    {
                        return VeriDegisimYeri.Alan;
                    }

                }
                //Eşit değil ise
                else if (!currentValue.Equals(oldValue))
                {
                    return VeriDegisimYeri.Alan;
                }

            }

            return VeriDegisimYeri.VeriDegisimiYok;



        }

        //Buton durumlarını 
        public static void ButonEnabledDurumu<T>(BarButtonItem btnYeni, BarButtonItem btnKaydet, BarButtonItem btnGeriAl, BarButtonItem btnSil, T oldEntity, T currentEntity)
        {
            var veriDegisimYeri = VeriDegisimYeriGetir(oldEntity, currentEntity);
            //Eşitse true
            var buttonEnabledDurum = veriDegisimYeri == VeriDegisimYeri.Alan;
            //Veri Girilmişse Kaydet ve Geri Enabled Olacak
            btnKaydet.Enabled = buttonEnabledDurum;
            btnGeriAl.Enabled = buttonEnabledDurum;
            btnYeni.Enabled = !buttonEnabledDurum;
            btnSil.Enabled = !buttonEnabledDurum;
        }

        public static void ButonEnabledDurumu<T>(BarButtonItem btnKaydet, BarButtonItem btnFarkliKaydet, BarButtonItem btnSil,IslemTuru islemTuru, T oldEntity, T currentEntity)
        {
            var veriDegisimYeri = VeriDegisimYeriGetir(oldEntity, currentEntity);
            //Eşitse true
            var buttonEnabledDurum = veriDegisimYeri == VeriDegisimYeri.Alan;
            //Veri Girilmişse Kaydet ve Geri Enabled Olacak
            btnKaydet.Enabled = buttonEnabledDurum;
            //Yeni kayıt aşamasında Farklı Kaydet olmayacak
            btnFarkliKaydet.Enabled = islemTuru != IslemTuru.EntityInsert;
            btnSil.Enabled = !buttonEnabledDurum;
        }

        //Extension
        public static long IdOlustur(this IslemTuru islemTuru , BaseEntity selectedEntity)
        {
            //Yeni Bi Kayıt ise yeni ID oluşturacağız -> Günün Tarihinden saatinden yararlanacağız -> Çakışma olmaması için

            //Ay' 12 , 4 04 eklemesi için uzunluk 1 ise başa 0 ekliyoruz 
            //Milisaniye 3 değer olacak 1 ise yanına 2' tane 0 ekleyeceğiz
            string SifirEkle(string deger)
            {
                if (deger.Length == 1)
                    return "0" + deger;
                return deger;
            }

            string UcBasamakYap(string deger)
            {
                switch (deger.Length)
                {
                    case 1:
                        return "00" + deger;

                    case 2:
                        return "0" + deger;
                    
                    
                }
                return deger;
            }
            string Id()
            {
                //Yıl kısmının SıfırEklemesi mümkün değil.
                var yil = DateTime.Now.Date.Year.ToString();
                var ay= SifirEkle(DateTime.Now.Date.Month.ToString());
                var gun= SifirEkle(DateTime.Now.Date.Day.ToString());
                var saat = SifirEkle(DateTime.Now.Date.Hour.ToString());
                var dakika = SifirEkle(DateTime.Now.Date.Minute.ToString());
                var saniye = SifirEkle(DateTime.Now.Date.Second.ToString());
                var milisaniye = UcBasamakYap(DateTime.Now.Date.Millisecond.ToString());

                //En son random sayı ekliyoruz
                var random = SifirEkle(new Random().Next(0, 99).ToString());

                //Aynı Id'nin denk gelme ihtimali yok denecek kadar az.
                return yil + ay + gun + saat + dakika + saniye + milisaniye + random;
            }

            //Işlem Update'ise entity'nin id sini insert ise Oluşturduğumuz fonksiyon Id'sini Long parse edip göndereceğiz
            return islemTuru == IslemTuru.EntityUpdate ? selectedEntity.Id : long.Parse(Id());


        }

        //Buraya BaseEdit olarak il prm edit olarak İlçeyi göndereceğiz İlçenin enabled durumunu Id'de değer varsa 
        public static void ControlEnabledChange(this MyButtonEdit baseEdit,Control prmEdit)
        {
            switch (prmEdit)
            {
                case MyButtonEdit edt:
                    //Enabled durumunu BaseEdit'in id sinde değer varsa true yap
                    edt.Enabled = baseEdit.Id.HasValue && baseEdit.Id > 0;
                    edt.Id = null;
                    edt.EditValue = null;
                    break;
            }
        }

        public static void RowFocus(this GridView tablo,string aranacakKolon,object aranacakDeger)
        {
            var rowHandle = 0;

            //RowCount ve DataRowCount farklı şeylerdir ...
            for (int i = 0; i < tablo.RowCount; i++)
            {
                //aranacakKolon -> fieldName olarak geçicek
                var bulunanDeger = tablo.GetRowCellValue(i, aranacakKolon);
                if (aranacakDeger.Equals(bulunanDeger))
                {
                    rowHandle = i;
                }
                //En sonunda aranacak değer 1 ' den fazla bulunuyorsa en sonuncuya focuslanmış olacak ilk kişiye focuslanmasını istiyorsak for döngüsüne kontrol koymamız gerek kontrol
                //Id aramasında böyle bi kontrole gerek yok
                tablo.FocusedRowHandle = rowHandle;
            }
        }

        //Silinmesi durumundan sonra bir sonraki veya önceki kayda focus ol
        public static void RowFocus(this GridView tablo,int rowHandle)
        {
            //Kayıt kalmadığı durumda focuslanma
            if (rowHandle <=0)
            {
                return;
            }

            //En son kayda eşitse en son kayda focuslan
            if (rowHandle==tablo.RowCount-1)
            {
                tablo.FocusedRowHandle = rowHandle;
            }
            // bir eksiğine eşitle
            else
            {
                tablo.FocusedRowHandle = rowHandle - 1;
            }

        }

        public static void SagTikMenuGoster(this MouseEventArgs e,PopupMenu sagTikMenu)
        {
            //Basılmadığı durumda
            if (e.Button !=MouseButtons.Right)
            {
                return;
            }
            //Mouse pozisyonunda Popup menüyü aç
            sagTikMenu.ShowPopup(Control.MousePosition);
        }


    }
}
