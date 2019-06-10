using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;

namespace SolidOtomasyon.Takip.Model.Entities.Base
{
    //Ana Model Özellikleri -> Normal Veri Girişleri İçin Base Alınacak Model
    public class BaseEntity:IBaseEntity
    {
        //Buradaki Id'yi biz oluşturacağımız için long veri tipinde atadık.
        public long Id { get; set; }

        public string Kod { get; set; }

    }
}
