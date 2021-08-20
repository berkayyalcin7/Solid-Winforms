using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities.Base
{
    //Ana Model Özellikleri -> Normal Veri Girişleri İçin Base Alınacak Model
    public class BaseEntity:IBaseEntity
    {
        //Buradaki Id'yi biz oluşturacağımız için long veri tipinde atadık.
        // Id Kolonu Kayıt ederken 0. indexe yerleştir ve ID 'yi otomatik atamayı None yapıyoruz.
        [Column(Order =0),Key,DatabaseGenerated(DatabaseGeneratedOption.None)]         
        public long Id { get; set; }


        //Kendimiz Attr oluşturup burada tanımlayacağız Validation işlemleri için -> Kod ve Zorunlu alan 
        [Column(Order = 1),Required,StringLength(20),Kod("Kod","txtKod"),ZorunluAlan("Kod","txtKod")]
        //Virtual ' ı override edip index uygulayacağıımız yaptık
        public virtual string Kod { get; set; }

    }
}
