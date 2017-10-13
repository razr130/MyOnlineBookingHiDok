using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEnd.Models;
using BackEnd.Dal;
namespace BackEnd.BL
{
    public interface ILayananBL
    {
        void Save(LayananModel layanan);
        void Delete(string id);
        bool IsExist(string id);
        LayananModel GetById(string id);
        List<LayananModel> ListData(LayananListDataType listType);

        void Clear();
    }

    public class LayananBL : ILayananBL
    {
        ILayananDal _layananDal;

        //  constructor
        public LayananBL()
        {
            _layananDal = new LayananDal();
        }

        //  constructor with injection
        public LayananBL(ILayananDal injLayananDal)
        {
            _layananDal = injLayananDal;
        }
        
        public bool IsExist(string id)
        {
            LayananModel dummy = _layananDal.GetById(id);
            if (dummy == null) return false;
            else return true;
        }

        public void Save(LayananModel layanan)
        {
            if (layanan.Kode.Trim() == "")
            {
                throw new ArgumentException("KODE LAYANAN kosong");
            }

            if (layanan.Nama.Trim() == "")
            {
                throw new ArgumentException("NAMA LAYANAN kosong");
            }

            if (IsExist(layanan.Kode))
            {
                _layananDal.Update(layanan); 
            }
            else
            {
                _layananDal.Insert(layanan);
            }
        }

        public void Delete(string id)
        {
            _layananDal.Delete(id);
        }

        public LayananModel GetById(string id)
        {
            return _layananDal.GetById(id);
        }

        public List<LayananModel> ListData(LayananListDataType listType)
        {
            return _layananDal.ListData(listType);
        }

        public void Clear()
        {
            _layananDal.Clear();
        }
    }
}