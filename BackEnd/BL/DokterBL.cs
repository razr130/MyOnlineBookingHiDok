using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.BL
{
    public interface IDokterBL
    {
        void Save(DokterModel dokter);
        void Delete(string id);
        void Clear();
        DokterModel GetById(string id);
        List<DokterModel> ListData();
        List<DokterModel> ListData(string kodeLayanan);
        bool IsExist(string id);
    }

    public class DokterBL : IDokterBL
    {
        IDokterDal _dokterDal;
        ILayananBL _layananBL;

        public DokterBL()
        {
            _dokterDal = new DokterDal();
            _layananBL = new LayananBL();
        }

        public DokterBL(IDokterDal injDokterDal,
                        ILayananBL injLayananBL)
        {
            _dokterDal = injDokterDal;
            _layananBL = injLayananBL;
        }

        public bool IsExist(string id)
        {
            DokterModel dummy = _dokterDal.GetById(id);
            if (dummy == null) return false;
            else return true;
        }

        public void Save(DokterModel dokter)
        {
            //  cek kode dokter
            if (dokter.Kode.Trim() == "")
            {
                throw new ArgumentException("KODE DOKTER kosong");
            }

            //  cek nama dokter
            if (dokter.Nama.Trim() == "")
            {
                throw new ArgumentException("NAMA DOKTER kosong");
            }

            //  composit: kode layanan
            if (dokter.KodeLayanan.Trim() != "")
            {
                LayananModel layanan = _layananBL.GetById(dokter.KodeLayanan);
                if(layanan == null)
                {
                    throw new ArgumentException("KODE LAYANAN invalid");
                }
                else if (_layananBL.GetById(layanan.Kode).Nama.Trim() == "")
                {
                    throw new ArgumentException("KODE LAYANAN invalid");
                }
            }

            //--save data
            if (!IsExist(dokter.Kode))
            {
                _dokterDal.Insert(dokter);
            }
            else
            {
                _dokterDal.Update(dokter);
            }
        }

        public void Delete(string id)
        {
            if (IsExist(id))
            {
                _dokterDal.Delete(id);
            }
        }

        public void Clear()
        {
            _dokterDal.Clear();
        }


        public DokterModel GetById(string id)
        {
            return _dokterDal.GetById(id);
        }

        public List<DokterModel> ListData()
        {
            return _dokterDal.ListData();
        }

        public List<DokterModel> ListData(string kodeLayanan)
        {
            return _dokterDal.ListData(kodeLayanan);
        }

    }
}