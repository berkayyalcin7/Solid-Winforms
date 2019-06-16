namespace SolidOtomasyon.Forms.OkulForms
{
    partial class OkulEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition4 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition5 = new DevExpress.XtraLayout.RowDefinition();
            this.myDataLayoutControl = new SolidOtomasyon.UserControls.Controls.MyDataLayoutControl();
            this.txtAciklama = new SolidOtomasyon.UserControls.Controls.MyMemoEdit();
            this.tglDurum = new SolidOtomasyon.UserControls.Controls.MyToggleSwitchEdit();
            this.myButtonEdit2 = new SolidOtomasyon.UserControls.Controls.MyButtonEdit();
            this.myButtonEdit1 = new SolidOtomasyon.UserControls.Controls.MyButtonEdit();
            this.myTextEdit1 = new SolidOtomasyon.UserControls.Controls.MyTextEdit();
            this.myKodTextEdit1 = new SolidOtomasyon.UserControls.Controls.MyKodTextEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txtKod = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtOkulAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtIl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtIlce = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataLayoutControl)).BeginInit();
            this.myDataLayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myButtonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myButtonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myKodTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOkulAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIlce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(468, 102);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // myDataLayoutControl
            // 
            this.myDataLayoutControl.Controls.Add(this.txtAciklama);
            this.myDataLayoutControl.Controls.Add(this.tglDurum);
            this.myDataLayoutControl.Controls.Add(this.myButtonEdit2);
            this.myDataLayoutControl.Controls.Add(this.myButtonEdit1);
            this.myDataLayoutControl.Controls.Add(this.myTextEdit1);
            this.myDataLayoutControl.Controls.Add(this.myKodTextEdit1);
            this.myDataLayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataLayoutControl.Location = new System.Drawing.Point(0, 102);
            this.myDataLayoutControl.Name = "myDataLayoutControl";
            this.myDataLayoutControl.OptionsFocus.EnableAutoTabOrder = false;
            this.myDataLayoutControl.Root = this.layoutControlGroup;
            this.myDataLayoutControl.Size = new System.Drawing.Size(468, 262);
            this.myDataLayoutControl.TabIndex = 2;
            this.myDataLayoutControl.Text = "myDataLayoutControl1";
            // 
            // txtAciklama
            // 
            this.txtAciklama.EnterMoveNextControl = true;
            this.txtAciklama.Location = new System.Drawing.Point(63, 108);
            this.txtAciklama.MenuManager = this.ribbonControl;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.txtAciklama.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAciklama.Properties.MaxLength = 500;
            this.txtAciklama.Size = new System.Drawing.Size(393, 142);
            this.txtAciklama.StatusBarAciklama = "Açıklama Giriniz. ";
            this.txtAciklama.StyleController = this.myDataLayoutControl;
            this.txtAciklama.TabIndex = 9;
            // 
            // tglDurum
            // 
            this.tglDurum.EnterMoveNextControl = true;
            this.tglDurum.Location = new System.Drawing.Point(361, 12);
            this.tglDurum.MenuManager = this.ribbonControl;
            this.tglDurum.Name = "tglDurum";
            this.tglDurum.Properties.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.tglDurum.Properties.Appearance.Options.UseForeColor = true;
            this.tglDurum.Properties.AutoHeight = false;
            this.tglDurum.Properties.AutoWidth = true;
            this.tglDurum.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tglDurum.Properties.OffText = "Pasif";
            this.tglDurum.Properties.OnText = "Aktif";
            this.tglDurum.Size = new System.Drawing.Size(97, 20);
            this.tglDurum.StatusBarAciklama = "Kartın Kullanım Durumunu Seçiniz. ";
            this.tglDurum.StyleController = this.myDataLayoutControl;
            this.tglDurum.TabIndex = 8;
            // 
            // myButtonEdit2
            // 
            this.myButtonEdit2.EnterMoveNextControl = true;
            this.myButtonEdit2.Id = null;
            this.myButtonEdit2.Location = new System.Drawing.Point(63, 84);
            this.myButtonEdit2.MenuManager = this.ribbonControl;
            this.myButtonEdit2.Name = "myButtonEdit2";
            this.myButtonEdit2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.myButtonEdit2.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.myButtonEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.myButtonEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.myButtonEdit2.Size = new System.Drawing.Size(145, 20);
            this.myButtonEdit2.StatusBarAciklama = null;
            this.myButtonEdit2.StatusBarKisaYol = "F4 :";
            this.myButtonEdit2.StatusBarKisaYolAciklama = null;
            this.myButtonEdit2.StyleController = this.myDataLayoutControl;
            this.myButtonEdit2.TabIndex = 7;
            // 
            // myButtonEdit1
            // 
            this.myButtonEdit1.EnterMoveNextControl = true;
            this.myButtonEdit1.Id = null;
            this.myButtonEdit1.Location = new System.Drawing.Point(63, 60);
            this.myButtonEdit1.MenuManager = this.ribbonControl;
            this.myButtonEdit1.Name = "myButtonEdit1";
            this.myButtonEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.myButtonEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.myButtonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.myButtonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.myButtonEdit1.Size = new System.Drawing.Size(145, 20);
            this.myButtonEdit1.StatusBarAciklama = null;
            this.myButtonEdit1.StatusBarKisaYol = "F4 :";
            this.myButtonEdit1.StatusBarKisaYolAciklama = null;
            this.myButtonEdit1.StyleController = this.myDataLayoutControl;
            this.myButtonEdit1.TabIndex = 6;
            // 
            // myTextEdit1
            // 
            this.myTextEdit1.EnterMoveNextControl = true;
            this.myTextEdit1.Location = new System.Drawing.Point(63, 36);
            this.myTextEdit1.MenuManager = this.ribbonControl;
            this.myTextEdit1.Name = "myTextEdit1";
            this.myTextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.myTextEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.myTextEdit1.Properties.MaxLength = 45;
            this.myTextEdit1.Size = new System.Drawing.Size(393, 20);
            this.myTextEdit1.StatusBarAciklama = null;
            this.myTextEdit1.StyleController = this.myDataLayoutControl;
            this.myTextEdit1.TabIndex = 5;
            // 
            // myKodTextEdit1
            // 
            this.myKodTextEdit1.EnterMoveNextControl = true;
            this.myKodTextEdit1.Location = new System.Drawing.Point(63, 12);
            this.myKodTextEdit1.MenuManager = this.ribbonControl;
            this.myKodTextEdit1.Name = "myKodTextEdit1";
            this.myKodTextEdit1.Properties.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.myKodTextEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.myKodTextEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.myKodTextEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.myKodTextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.myKodTextEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.myKodTextEdit1.Properties.MaxLength = 20;
            this.myKodTextEdit1.Size = new System.Drawing.Size(145, 20);
            this.myKodTextEdit1.StatusBarAciklama = "Kod Giriniz.";
            this.myKodTextEdit1.StyleController = this.myDataLayoutControl;
            this.myKodTextEdit1.TabIndex = 4;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txtKod,
            this.txtOkulAdi,
            this.txtIl,
            this.layoutControlItem5,
            this.txtIlce,
            this.layoutControlItem6});
            this.layoutControlGroup.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            this.layoutControlGroup.Name = "layoutControlGroup";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
            columnDefinition1.Width = 200D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 100D;
            columnDefinition3.SizeType = System.Windows.Forms.SizeType.Absolute;
            columnDefinition3.Width = 99D;
            this.layoutControlGroup.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3});
            rowDefinition1.Height = 24D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition2.Height = 24D;
            rowDefinition2.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition3.Height = 24D;
            rowDefinition3.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition4.Height = 24D;
            rowDefinition4.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition5.Height = 100D;
            rowDefinition5.SizeType = System.Windows.Forms.SizeType.Percent;
            this.layoutControlGroup.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3,
            rowDefinition4,
            rowDefinition5});
            this.layoutControlGroup.Size = new System.Drawing.Size(468, 262);
            this.layoutControlGroup.TextVisible = false;
            // 
            // txtKod
            // 
            this.txtKod.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.txtKod.AppearanceItemCaption.Options.UseForeColor = true;
            this.txtKod.Control = this.myKodTextEdit1;
            this.txtKod.Location = new System.Drawing.Point(0, 0);
            this.txtKod.Name = "txtKod";
            this.txtKod.Size = new System.Drawing.Size(200, 24);
            this.txtKod.Text = "Kod";
            this.txtKod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.txtKod.TextSize = new System.Drawing.Size(41, 13);
            this.txtKod.TextToControlDistance = 10;
            // 
            // txtOkulAdi
            // 
            this.txtOkulAdi.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.txtOkulAdi.AppearanceItemCaption.Options.UseForeColor = true;
            this.txtOkulAdi.Control = this.myTextEdit1;
            this.txtOkulAdi.Location = new System.Drawing.Point(0, 24);
            this.txtOkulAdi.Name = "txtOkulAdi";
            this.txtOkulAdi.OptionsTableLayoutItem.ColumnSpan = 3;
            this.txtOkulAdi.OptionsTableLayoutItem.RowIndex = 1;
            this.txtOkulAdi.Size = new System.Drawing.Size(448, 24);
            this.txtOkulAdi.Text = "Okul Adı";
            this.txtOkulAdi.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.txtOkulAdi.TextSize = new System.Drawing.Size(41, 13);
            this.txtOkulAdi.TextToControlDistance = 10;
            // 
            // txtIl
            // 
            this.txtIl.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.txtIl.AppearanceItemCaption.Options.UseForeColor = true;
            this.txtIl.Control = this.myButtonEdit1;
            this.txtIl.Location = new System.Drawing.Point(0, 48);
            this.txtIl.Name = "txtIl";
            this.txtIl.OptionsTableLayoutItem.RowIndex = 2;
            this.txtIl.Size = new System.Drawing.Size(200, 24);
            this.txtIl.Text = "İl";
            this.txtIl.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.txtIl.TextSize = new System.Drawing.Size(41, 13);
            this.txtIl.TextToControlDistance = 10;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.Control = this.tglDurum;
            this.layoutControlItem5.Location = new System.Drawing.Point(349, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.OptionsTableLayoutItem.ColumnIndex = 2;
            this.layoutControlItem5.Size = new System.Drawing.Size(99, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // txtIlce
            // 
            this.txtIlce.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.txtIlce.AppearanceItemCaption.Options.UseForeColor = true;
            this.txtIlce.Control = this.myButtonEdit2;
            this.txtIlce.Location = new System.Drawing.Point(0, 72);
            this.txtIlce.Name = "txtIlce";
            this.txtIlce.OptionsTableLayoutItem.RowIndex = 3;
            this.txtIlce.Size = new System.Drawing.Size(200, 24);
            this.txtIlce.Text = "İlçe";
            this.txtIlce.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.txtIlce.TextSize = new System.Drawing.Size(41, 13);
            this.txtIlce.TextToControlDistance = 10;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem6.Control = this.txtAciklama;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.OptionsTableLayoutItem.ColumnSpan = 3;
            this.layoutControlItem6.OptionsTableLayoutItem.RowIndex = 4;
            this.layoutControlItem6.Size = new System.Drawing.Size(448, 146);
            this.layoutControlItem6.Text = "Açıklama";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(41, 13);
            this.layoutControlItem6.TextToControlDistance = 10;
            // 
            // OkulKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 395);
            this.Controls.Add(this.myDataLayoutControl);
            this.MinimumSize = new System.Drawing.Size(450, 350);
            this.Name = "OkulKarti";
            this.Text = "Okul Kartı";
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.myDataLayoutControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataLayoutControl)).EndInit();
            this.myDataLayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myButtonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myButtonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myKodTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOkulAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIlce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Controls.MyDataLayoutControl myDataLayoutControl;
        private UserControls.Controls.MyMemoEdit txtAciklama;
        private UserControls.Controls.MyToggleSwitchEdit tglDurum;
        private UserControls.Controls.MyButtonEdit myButtonEdit2;
        private UserControls.Controls.MyButtonEdit myButtonEdit1;
        private UserControls.Controls.MyTextEdit myTextEdit1;
        private UserControls.Controls.MyKodTextEdit myKodTextEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem txtKod;
        private DevExpress.XtraLayout.LayoutControlItem txtOkulAdi;
        private DevExpress.XtraLayout.LayoutControlItem txtIl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem txtIlce;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}