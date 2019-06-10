
using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    //XtraDataLayout DLL ' i referanslara ekliyoruz.
    public class MyDataLayoutControl : DataLayoutControl
    {

        public MyDataLayoutControl()
        {

            // İstediğimiz index düzeninde hareket etmesini istiyoruz.
            OptionsFocus.EnableAutoTabOrder = false;

        }


        protected override LayoutControlImplementor CreateILayoutControlImplementorCore()
        {
            // MyLayoutControlImplementorumuzu oluşturacağız
            return new MyLayoutControlImplementor(this);
        }


    }


    internal class MyLayoutControlImplementor : LayoutControlImplementor
    {
        public MyLayoutControlImplementor(ILayoutControlOwner owner) : base(owner)
        {

        }

        //CreateLayout item ve Groupı override ediyoruz.
        public override BaseLayoutItem CreateLayoutItem(LayoutGroup parent)
        {
            var item = base.CreateLayoutItem(parent);
            item.AppearanceItemCaption.ForeColor = Color.Maroon;
            return item;
        }

        public override LayoutGroup CreateLayoutGroup(LayoutGroup parent)
        {
            var grp = base.CreateLayoutGroup(parent);
            grp.LayoutMode = LayoutMode.Table;
            //Tabloyu düzenleme -> Kolonları Sabitliyoruz > Absolute ile
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].SizeType = SizeType.Absolute;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].Width = 200;
            //2. sütunu yüzde olarak ayarlıyoruz
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].SizeType = SizeType.Percent;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].Width = 100;
            //3. Kolon için -> genelde toggle switch kullanırız burada -> yeni kolon sütun ekliyoruz
            grp.OptionsTableLayoutGroup.ColumnDefinitions.Add(new ColumnDefinition
            {
                SizeType = SizeType.Absolute,
                Width = 99
            });

            //Şimdi Row - Satır kullanacağız -> Otomatik oluşan Satırları temizledik
            grp.OptionsTableLayoutGroup.RowDefinitions.Clear();

            for (int i = 0; i < 9; i++)
            {
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    Height = 24,
                    SizeType = SizeType.Absolute
                });

                if (i + 1 != 9) continue;
                //i = 9 olduğunda yani son elemanın boyutunu yüzdeli ayarla
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    Height = 100,
                    SizeType = SizeType.Percent
                });
            }

            return grp;
        }
    }
}
