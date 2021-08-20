using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Model.Entities.Base
{
    //BaseEntity'den implemente olacak
    public class BaseEntityDurum:BaseEntity
    {
        //Amaç : Bazı Tablolarda Aktif-Pasif durumlarını Modelleme
        public bool Durum { get; set; } = true;
    }
}
