using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DAL;

namespace BLL
{
    public class DSBanBLL
    {
        dbDataContext db = new dbDataContext();
        public string NhanVien = "admin";

        public DSBanBLL()
        {

        }

        public List<tblKhuVuc> GetListKV()
        {
            return db.tblKhuVucs.ToList();
        }

        public tblBan GetTenBan(int ban)
        {
            return db.tblBans.Where(b => b.Id == ban).FirstOrDefault();
        }

        public DataTable GetDSBan()
        {
            var ketqua = (from h in db.tblBans
                          join d in db.tblKhuVucs on h.IdKV equals d.Id
                          select new
                          {
                              Id = h.Id,
                              TenBan = h.TenBan,
                              SoGhe = h.SoGhe,
                              TenKhuVuc = d.TenKhuVuc,
                          }).ToList().OrderBy(x => x.TenKhuVuc).ToList();
            DataTable dt = new DataTable();
            DataColumn Id = new DataColumn("ID");
            DataColumn TenBan = new DataColumn("Tên Bàn");
            DataColumn SoGhe = new DataColumn("Số Ghế");
            DataColumn TenKhuVuc = new DataColumn("Tên Khu Vực");

            dt.Columns.Add(Id);
            dt.Columns.Add(TenBan);
            dt.Columns.Add(SoGhe);
            dt.Columns.Add(TenKhuVuc);

            foreach (var item in ketqua)
            {
                DataRow row = dt.NewRow();
                row["ID"] = item.Id;
                row["Tên Bàn"] = item.TenBan;
                row["Số Ghế"] = item.SoGhe;
                row["Tên Khu Vực"] = item.TenKhuVuc;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void Add(string tenban, int soghe, int tenkhuvuc)

        {
            tblBan b = new tblBan();
            b.TenBan = tenban;
            b.SoGhe = soghe;
            b.IdKV = tenkhuvuc;

            b.NgayTao = DateTime.Now;
            b.NguoiTao = NhanVien;

            db.tblBans.InsertOnSubmit(b);
            db.SubmitChanges();
        }

        public void Edit(int ma, string tenban, int soghe, int tenkhuvuc)
        {
            tblBan b = GetTenBan(ma);
            b.TenBan = tenban;
            b.SoGhe = soghe;
            b.IdKV = tenkhuvuc;

            b.NgayTao = DateTime.Now;
            b.NguoiTao = NhanVien;

            db.SubmitChanges();
        }

        public void Delete(int ma)
        {
            tblBan b = GetTenBan(ma);
            db.tblBans.DeleteOnSubmit(b);
            db.SubmitChanges();
        }
    }
}
