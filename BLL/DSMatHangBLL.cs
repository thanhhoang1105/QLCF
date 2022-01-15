using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using DAL;

namespace BLL
{
    public class DSMatHangBLL
    {
        dbDataContext db = new dbDataContext();
        public string NhanVien = "admin";

        public DSMatHangBLL()
        {

        }

        public List<tblDVT> GetListDVT()
        {
            return db.tblDVTs.ToList();
        }

        public List<tblMatHang> GetListMatHang()
        {
            return db.tblMatHangs.ToList();
        }

        public tblMatHang GetTenMatHang(int mathang)
        {
            return db.tblMatHangs.Where(mh => mh.Id == mathang).FirstOrDefault();
        }

        public DataTable GetDSMatHang()
        {
            var ketqua = from h in db.tblMatHangs
                         join d in db.tblDVTs on h.DonViTinh equals d.Id
                         select new
                         {
                             Id = h.Id,
                             TenMatHang = h.TenMatHang,
                             Ten = d.Ten,
                             giaban = h.GiaBan
                         };
            DataTable dt = new DataTable();
            DataColumn Id = new DataColumn("ID");
            DataColumn TenMatHang = new DataColumn("Tên Mặt Hàng");
            DataColumn Ten = new DataColumn("Tên");
            DataColumn GiaBan = new DataColumn("Giá Bán");

            dt.Columns.Add(Id);
            dt.Columns.Add(TenMatHang);
            dt.Columns.Add(Ten);
            dt.Columns.Add(GiaBan);

            foreach (var item in ketqua)
            {
                DataRow row = dt.NewRow();
                row["ID"] = item.Id;
                row["Tên Mặt Hàng"] = item.TenMatHang;
                row["Tên"] = item.Ten;
                row["Giá Bán"] = item.giaban;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void Add(string tenmathang, int ten, int giaban)

        {
            tblMatHang mh = new tblMatHang();
            mh.TenMatHang = tenmathang;
            mh.DonViTinh = ten;
            mh.GiaBan = giaban;

            mh.NgayTao = DateTime.Now;
            mh.NguoiTao = NhanVien;

            db.tblMatHangs.InsertOnSubmit(mh);
            db.SubmitChanges();
        }

        public void Edit(int ma, string tenmathang, int ten, int giaban)
        {
            tblMatHang mh = GetTenMatHang(ma);
            mh.TenMatHang = tenmathang;
            mh.DonViTinh = ten;
            mh.GiaBan = giaban;

            mh.NgayTao = DateTime.Now;
            mh.NguoiTao = NhanVien;

            db.SubmitChanges();
        }

        public void Delete(int ma)
        {
            tblMatHang mh = GetTenMatHang(ma);
            db.tblMatHangs.DeleteOnSubmit(mh);
            db.SubmitChanges();
        }
    }
}
