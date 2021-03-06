﻿using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Data.Contexts;
using SolidOtomasyon.Takip.Model.Dto;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Linq;
using SolidOtomasyon.BLL.Interfaces;

namespace SolidOtomasyon.BLL.General
{
    //Burada Okul Entity'si ve OgrenciTakipContext'ini kullanacağız.
    public class OkulBll : BaseBll<Okul, OgrenciTakipContext>,IBaseGenelBll,IBaseCommonBll
    {
        //Ctor 'leri oluşturuyoruz : İşlem Yapmayacağız ...
        public OkulBll()
        {
        }

        public OkulBll(Control ctrl) : base(ctrl) { }

        //BaseBll'deki Single TResult Türünde ve TResult ise BaseEntity olacağı için geriye BaseEntity gönderecek.
        public BaseEntity Single(Expression<Func<Okul, bool>> filter)
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

        public IEnumerable<BaseEntity> List(Expression<Func<Okul, bool>> filter)
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

        public bool Insert(BaseEntity entity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseInsert(entity, x => x.Kod == entity.Kod);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseUpdate(oldEntity, currentEntity, x => x.Kod == currentEntity.Kod);
        }

        public bool Delete(BaseEntity entity)
        {

            return BaseDelete(entity, KartTuru.Okul);

        }

        public string YeniKodVer()
        {
            //tür ve filter verdik -> Kod alanınını getir
            return BaseYeniKodVer(KartTuru.Okul, x => x.Kod);
        }
    }
}
