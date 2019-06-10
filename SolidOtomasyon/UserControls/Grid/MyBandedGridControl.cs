using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using SolidOtomasyon.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.UserControls.Grid
{
    [ToolboxItem(true)]
    public class MyBandedGridControl : GridControl
    {
        //View Oluşturuyoruz MyBandedGridView'a cast ediyoruz
        protected override BaseView CreateDefaultView()
        {
            var view = (MyBandedGridView)CreateView("MyBandedGridView");
            //Band Panel ayarları
            view.Appearance.BandPanel.ForeColor = Color.DarkBlue;
            view.Appearance.BandPanel.Font = new Font(new FontFamily("Tahoma"), 8.25f, FontStyle.Bold);
            view.Appearance.BandPanel.TextOptions.HAlignment = HorzAlignment.Center;
            //  Sabit bir değer 40px yani 2 kolonluk bir yer ayarı
            view.BandPanelRowHeight = 40;

            view.Appearance.HeaderPanel.ForeColor = Color.Maroon;
            view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            view.Appearance.ViewCaption.ForeColor = Color.Maroon;
            view.Appearance.FooterPanel.ForeColor = Color.Maroon;
            view.Appearance.FooterPanel.Font = new Font(new FontFamily("Tahoma"), 8.25f, FontStyle.Bold);


            view.OptionsMenu.EnableColumnMenu = false;
            view.OptionsMenu.EnableFooterMenu = false;
            view.OptionsMenu.EnableGroupPanelMenu = false;

            //Enter ile diğer colona geçsin
            view.OptionsNavigation.EnterMoveNextColumn = true;
            view.OptionsPrint.AutoWidth = false;
            view.OptionsPrint.PrintFooter = false;
            view.OptionsPrint.PrintGroupFooter = false;

            //Oto filtreleme açık gelsin
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowViewCaption = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.RowAutoHeight = true;
            view.OptionsView.HeaderFilterButtonShowMode = FilterButtonShowMode.Button;

            //2 yeni kolon ekleyeceğiz - Dizi Şeklinde' yapılabilirdi var column = new[] {} şeklinde

            var idColumn = new BandedGridColumn
            {
                Caption = "Id",
                FieldName = "Id",
                OptionsColumn = { AllowEdit = false, ShowInCustomizationForm = false }

            };
            // 2 .Kullanım
            //idColumn.OptionsColumn.AllowEdit = false;
            //idColumn.OptionsColumn.ShowInCustomizationForm = false;


            var kodColumn = new BandedGridColumn
            {
                Caption = "Kod",
                FieldName = "Kod",
                Visible = true,
                OptionsColumn = { AllowEdit = false, ShowInCustomizationForm = true },
                AppearanceCell = { TextOptions = { HAlignment = HorzAlignment.Center }, Options = { UseTextOptions = true } }

            };

            view.Columns.Add(idColumn);
            view.Columns.Add(kodColumn);


            return view;

        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);

            collection.Add(new MyBandedGridInfoRegistrator());
        }

        private class MyBandedGridInfoRegistrator : BandedGridInfoRegistrator
        {
            public override string ViewName => "MyBandedGridView";

            public override BaseView CreateView(GridControl grid) => new MyBandedGridView(grid);

        }
    }

    // Control haricinde View 'imiz olacka
    public class MyBandedGridView : BandedGridView, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarKisaYol { get; set; }
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }

        #endregion

        public MyBandedGridView() { }

        //Ctor CreateView kısmında oluşturduk.
        public MyBandedGridView(GridControl ownerGrid) : base(ownerGrid)
        {
        }

        protected override void OnColumnChangedCore(GridColumn column)
        {
            base.OnColumnChangedCore(column);
            //Tarih Alanlarını Ortalayacağız

            if (column.ColumnEdit == null) return;
            if (column.ColumnEdit.GetType() == typeof(RepositoryItemDateEdit))
            {
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                ((RepositoryItemDateEdit)column.ColumnEdit).Mask.MaskType = MaskType.DateTimeAdvancingCaret;

            }

        }

        protected override GridColumnCollection CreateColumnCollection()
        {
            return new MyGridColumnCollection(this);
        }

        //Çalıştığımız BandedGridColumnCollection olacak
        private class MyGridColumnCollection : BandedGridColumnCollection
        {
            public MyGridColumnCollection(ColumnView view) : base(view) { }
            protected override GridColumn CreateColumn()
            {
                //Column Oluşturacağız
                var column = new MyBandedGridColumn();
                column.OptionsColumn.AllowEdit = false;
                return column;
            }

        }

    }

    //View içerisinde Column olacak
    public class MyBandedGridColumn : BandedGridColumn, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarKisaYol { get; set; }
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }

        #endregion
    }




}
