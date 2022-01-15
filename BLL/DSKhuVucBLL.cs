using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DAL;

namespace BLL
{
    public class DSKhuVucBLL
    {
        dbDataContext db = new dbDataContext();
        public string NhanVien = "admin";

        public DSKhuVucBLL()
        {

        }

        public DataTable GetDSKhuVuc()
        {
            var ketqua = from h in db.tblKhuVucs
                         select new
                         {
                             Id = h.Id,
                             TenKhuVuc = h.TenKhuVuc,
                         };
            DataTable dt = new DataTable();
            DataColumn Id = new DataColumn("ID");
            DataColumn TenKhuVuc = new DataColumn("Tên Khu Vực");

            dt.Columns.Add(Id);
            dt.Columns.Add(TenKhuVuc);

            foreach (var item in ketqua)
            {
                DataRow row = dt.NewRow();
                row["ID"] = item.Id;
                row["Tên Khu Vực"] = item.TenKhuVuc;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public tblKhuVuc GetTenKhuVuc(int khuvuc)
        {
            return db.tblKhuVucs.Where(kv => kv.Id == khuvuc).FirstOrDefault();
        }

        public void Add(string tenkhuvuc)

        {
            tblKhuVuc kv = new tblKhuVuc();
            kv.TenKhuVuc = tenkhuvuc;

            db.tblKhuVucs.InsertOnSubmit(kv);
            db.SubmitChanges();
        }

        public void Edit(int ma, string tenkhuvuc)
        {
            tblKhuVuc kv = GetTenKhuVuc(ma);
            kv.TenKhuVuc = tenkhuvuc;

            db.SubmitChanges();
        }

        public void Delete(int ma)
        {
            tblKhuVuc kv = GetTenKhuVuc(ma);
            db.tblKhuVucs.DeleteOnSubmit(kv);
            db.SubmitChanges();
        }
    }
}
