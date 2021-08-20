using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.Takip.Common.Message
{
    public class Messages
    {
        public static void HataMesaji(string hataMesaji)
        {
            //Data ve Utils kısmını ekliyoruz. Dll
            XtraMessageBox.Show(hataMesaji, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public static void UyariMesaji(string uyariMesaji)
        {
            //Data ve Utils kısmını ekliyoruz. Dll
            XtraMessageBox.Show(uyariMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static DialogResult EvetSeciliEvetHayir(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }

        public static DialogResult HayirSeciliEvetHayir(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        }



        public static DialogResult EvetSeciliEvetHayirIptal(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }


        public static DialogResult SilMesaj(string kartAdi)
        {
            return HayirSeciliEvetHayir($"Seçtiğiniz {kartAdi} Silinecektir. Onaylıyor musunuz?", "Silme Onayı?");
        }

        public static DialogResult KapanisMesaj()
        {
            return EvetSeciliEvetHayirIptal("Yapılan Değişiklikler Kayıt Edilsin mi ?", "Çıkış Onay");
        }

        public static DialogResult KayitMesaj()
        {
            //Kaydet Butonuna basıldığında gelecek mesaj
            return EvetSeciliEvetHayir("Yapılan Değişiklikler Kayıt Edilsin mi ?", "Kayıt Onay");
        }

        public static void KartSecUyariMesaji()
        {
            UyariMesaji("Lütfen Bir Kart Seçiniz .");
        }

        public static void VarOlanKodKayitHataMesaji(string alanAdi)
        {
            HataMesaji($"Girmiş olduğunuz {alanAdi} daha önce kullanılmıştır !");
        }

        public static void HataliVeriMesaji(string alanAdi)
        {
            HataMesaji($"{alanAdi} alanına geçerli bir değer girmelisiniz !");
        }

        public static DialogResult TabloExportMesaj(string dosyaFormati)
        {
            return EvetSeciliEvetHayir($"İlgili Tablo , {dosyaFormati} olarak dışa aktarılacaktır . Onaylıyor musunuz ?", "Aktarım Onay");

        }






    }
}
