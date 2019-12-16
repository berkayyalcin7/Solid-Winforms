using SolidOtomasyon.Forms.IlceForms;
using SolidOtomasyon.Forms.IlForms;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.UserControls.Controls;
using System;

namespace SolidOtomasyon.Functions
{
    public class SelectFunctions : IDisposable
    {
        #region Değişkenler
        //Tek Kullanım olacağı yerler -> İlçe
        private MyButtonEdit _btnEdit;
        //Çift -> İl prm Edit
        private MyButtonEdit _prmEdit;
        private KartTuru _kartTuru;

        #endregion
        public void Sec(MyButtonEdit btnEdit)
        {
            _btnEdit = btnEdit;
            SecimYap();
        }

        public void Sec(MyButtonEdit btnEdit , MyButtonEdit prmEdit)
        {
            _btnEdit = btnEdit;
            _prmEdit = prmEdit;
            SecimYap();
        }

        private void SecimYap()
        {
            switch (_btnEdit.Name)
            {
                case "txtIl":
                    {
                        var entity = (Il)ShowListForms<IlListForm>.ShowDialogListForm(_kartTuru, _btnEdit.Id);
                        if (entity!=null)
                        {
                            
                            _btnEdit.Id = entity.Id;
                            //IlAdi ni yakalamak için ShowListformu' Il'e cast ettik
                            _btnEdit.EditValue = entity.IlAdi;
                        }
                    }
                    break;

                case "txtIlce":
                    {   //prmEdit ile Il ' Id ve IlAdi gönderiyoruz
                        var entity = (Ilce)ShowListForms<IlceListForm>.ShowDialogListForm(_kartTuru, _btnEdit.Id,_prmEdit.Id,_prmEdit.Text);
                        if (entity != null)
                        {

                            _btnEdit.Id = entity.Id;
                            _btnEdit.EditValue = entity.IlceAdi;
                        }
                    }
                    break;
            }
        }

        public void Dispose()
        {
            //İşlem sonrası dispose ediyoruz
            GC.SuppressFinalize(this);
        }
    }
}
