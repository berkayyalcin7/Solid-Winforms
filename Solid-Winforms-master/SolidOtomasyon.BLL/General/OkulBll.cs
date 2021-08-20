using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Dto;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    //Burada Okul Entity'si ve OgrenciTakipContext'ini kullanacağız.
    public class OkulBll : BaseGenelBll<Okul>,IBaseGenelBll,IBaseCommonBll
    {
        //Ctor 'leri oluşturuyoruz : İşlem Yapmayacağız ..
        
        //Kart Türünü Base olarak gönderiyoruz ...
        public OkulBll():base(KartTuru.Okul)
        {

        }

        public OkulBll(Control ctrl) : base(ctrl,KartTuru.Okul) { }

        //BaseBll'deki Single TResult Türünde ve TResult ise BaseEntity olacağı için geriye BaseEntity gönderecek.
        public override BaseEntity Single(Expression<Func<Okul, bool>> filter)
        {
            //Seçim işlemini selector burada devreye giricek fonksiyon parametresinde değil.
            //Burada Selector için Data Transfer Object Oluşturacağız DTO ..
            return BaseSingle(filter, x => new OkulS
            {
                //Property'leri böyle kullanacağız. DTO kullanmış olduk
                Id = x.Id,
                Kod = x.Kod,
                OkulAdi = x.OkulAdi,
                IlId = x.IlId,
                IlAdi = x.Il.IlAdi,
                IlceId = x.IlceId,
                IlceAdi = x.Ilce.IlceAdi,
                Aciklama = x.Aciklama,
                Durum = x.Durum
            });
        }

        public override IEnumerable<BaseEntity> List(Expression<Func<Okul, bool>> filter)
        {
            //BaseList IQueryable türünde döndüğü için bunu ekrana getirirken sıralama ve Listeleme yapıyoruz.
            //Burada ToList() döndürmemizin sebebi OrderBy ile filtre uygulamamız . Yani İlk Sıralama Sonra Listeleme işlemi
            return BaseList(filter, x => new OkulL
            {
                Id = x.Id,
                Kod = x.Kod,
                OkulAdi = x.OkulAdi,
                IlAdi = x.Il.IlAdi,
                IlceAdi = x.Ilce.IlceAdi,
                Aciklama = x.Aciklama,
            }).OrderBy(x=>x.Kod).ToList();
        }

       
    }
}
