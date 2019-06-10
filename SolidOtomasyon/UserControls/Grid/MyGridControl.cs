using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Grid
{
    [ToolboxItem(true)]
    public class MyGridControl : GridControl
    {
        //Override ile oluşma (Create) aşamasında müdahalede bulunuyoruz. İhtiyaca göre 

        protected override BaseView CreateDefaultView()
        {
            #region View Özellikleri
            //BaseView' dan GridView'a cast ediyoruz.
            var view = (GridView)CreateView("MyGridView");

            //Grid View'in başlık rengini değiştiriyoruz.
            view.Appearance.ViewCaption.ForeColor = Color.Maroon;
            //Panel yazı renkleri 
            view.Appearance.HeaderPanel.ForeColor = Color.Maroon;
            //Başlık kısımları ortalandı
            view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            //Rengi Maroon
            view.Appearance.FooterPanel.ForeColor = Color.Maroon;
            //Footer Kalın Tahoma , 8.25
            view.Appearance.FooterPanel.Font = new Font(new FontFamily("Tahoma"), 8.25f, FontStyle.Bold);

            //Kolon Ayarları -> Kendimiz ayarlayacağımız için değerleri false' çekiyoruz
            view.OptionsMenu.EnableColumnMenu = false;
            view.OptionsMenu.EnableFooterMenu = false;
            view.OptionsMenu.EnableGroupPanelMenu = false;

            //Veri Girişi Yapılacak Gridler için - enter ile ilerleme
            view.OptionsNavigation.EnterMoveNextColumn = true;
            // Yazıcıya gönderilen verinin AutoWidth özelliği false
            view.OptionsPrint.AutoWidth = false;
            // Footer alanları nı yazıcıya göndermiyoruz
            view.OptionsPrint.PrintFooter = false;
            view.OptionsPrint.PrintGroupFooter = false;

            //View Ayarlayacağız
            view.OptionsView.ShowViewCaption = true;
            //Filtreleme bölümü açma
            view.OptionsView.ShowAutoFilterRow = true;
            //Gruplama panelini kapatma
            view.OptionsView.ShowGroupPanel = false;
            //Kolonların uzunluklarının sabit kalması için
            view.OptionsView.ColumnAutoWidth = false;
            //Kolon yüksekliği otomatik ayarlansın
            view.OptionsView.RowAutoHeight = true;
            //Default değeri Smart Tag -> Header filtresine button getiriyor. 
            view.OptionsView.HeaderFilterButtonShowMode = FilterButtonShowMode.Button;

            #endregion

            #region 2 Sabit Kolonlarımız    

            // 2 Kolon Ekli Olarak gelsin
            var idColumn = new MyGridColumn();
            idColumn.Caption = "Id";
            idColumn.FieldName = "Id";
            idColumn.OptionsColumn.AllowEdit = false;
            //ID Kolonu Gözükmeyecek
            idColumn.Visible = false;
            // ID kolonu hiçbir yerde gözükmesin
            idColumn.OptionsColumn.ShowInCustomizationForm = false;
            //Oluşturduğumuz Kolonu Ekledik
            view.Columns.Add(idColumn);

            var kodColumn = new MyGridColumn();

            kodColumn.Caption = "Kod";
            kodColumn.FieldName = "Kod";
            kodColumn.OptionsColumn.AllowEdit = false;
            //Column'da gözükmesi için Visible özelliği true yapıyoruz.
            kodColumn.Visible = true;
            kodColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //Text Options uygulanması için true çekmemiz gerek
            kodColumn.AppearanceCell.Options.UseTextOptions = true;
            view.Columns.Add(kodColumn);

            #endregion

            //Geriye Değer Döndürme
            //Oluşturduğumuz view'i gönderiyoruz.
            return view;


        }

        //MygridControl Override -> Şu Grid View'i kullan diyeceğiz

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);

            collection.Add(new MyGridInfoRegistrator());
        }

        private class MyGridInfoRegistrator : GridInfoRegistrator
        {
            //Oluşacak olan Gridin İsmine müdahale edeceğiz -> Kendi View ismimimizi veriyrouz
            public override string ViewName => "MyGridView";

            //Create view ' ile oluşacak MyGridView'e müdahalede bulunmuş olduk.
            public override BaseView CreateView(GridControl grid) => new MyGridView(grid);

            //Diğer override CreateXXX ile diğer özelliklere ihtiyaca göre müdahale edebiliriz.

        }
    }

    //GridControl İçersinde GridView'lar olacak -> IStatusBarKisaYol GridView için özel açıklama Vs Olabilir.
    public class MyGridView : GridView, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarKisaYol { get; set; }
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }
        #endregion

        //Boş olarak Instance alabilmesi için (Designer.Cs 'de ) ctor oluşturacağız
        public MyGridView()
        {

        }


        //Constructoru oluşturuyoruz -> MyGridInfoRegistrator 'daki override CreateView fonksiyonundan gelmiştir
        public MyGridView(GridControl ownerGrid) : base(ownerGrid)
        {
            //Sürüklediğimizde forma devreye girecektir.
        }

        // GridColumn Türünden Bir değer alıyor -> Kolon değiştiğinde belli ayarlar yapacağız
        protected override void OnColumnChangedCore(GridColumn column)
        {
            base.OnColumnChangedCore(column);

            if (column.ColumnEdit == null)
            {
                return;
            }

            //Kolon'un tipi RepoItemDate -> Tarih türünde DevExpress
            if (column.ColumnEdit.GetType() == typeof(RepositoryItemDateEdit))
            {
                //Date türünde veriyi Ortala.
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //Maske ayarları -> RepositoryItemDateEdit 'e cast etmeseydik ColumnEdit'in Mask özelliğine erişemeyecektik.
                ((RepositoryItemDateEdit)column.ColumnEdit).Mask.MaskType = MaskType.DateTimeAdvancingCaret;

            }


        }

        //   Kolon Ayarlarının yapılacağı yer
        protected override GridColumnCollection CreateColumnCollection()
        {
            //Üzerinde çalıştığımız MyGridView'i gönderiyoruz (this)
            return new MyGridColumnCollection(this);

        }


    }

    internal class MyGridColumnCollection : GridColumnCollection
    {
        public MyGridColumnCollection(ColumnView view) : base(view)
        {

        }

        //Kendimiz kolon oluşturup AllowEdit özelliğini false yapacağız
        protected override GridColumn CreateColumn()
        {
            var column = new MyGridColumn();
            column.OptionsColumn.AllowEdit = false;
            return column;
        }

    }

    //GridView altında GridColumnlar bulunacak -> Kodlamaya ilk olarak Kolonlardan başlayacağız , IStatusBarKisaYol GridColumn için özel açıklama Vs Olabilir.
    public class MyGridColumn : GridColumn, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarKisaYol { get; set; }
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }
        #endregion
    }
}
