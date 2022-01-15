using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DAL;

namespace BLL
{
    public class DSTaiKhoanBLL
    {
        dbDataContext db = new dbDataContext();
        public string NhanVien = "admin";

        public DSTaiKhoanBLL()
        {

        }

        public tblTaiKhoan GetTaiKhoan(int taikhoan)
        {
            return db.tblTaiKhoans.Where(tk => tk.Id == taikhoan).FirstOrDefault();
        }

        public DataTable GetDSTaiKhoan()
        {
            var ketqua = from h in db.tblTaiKhoans
                         select new
                         {
                             Id = h.Id,
                             MaNhanVien = h.MaNhanVien,
                             TenTaiKhoan = h.TenTaiKhoan,
                             TenHienThi = h.TenHienThi,
                             MatKhau = h.MatKhau,
                             IsAdmin = h.IsAdmin,
                         };
            DataTable dt = new DataTable();
            DataColumn Id = new DataColumn("ID");
            DataColumn MaNhanVien = new DataColumn("MNV");
            DataColumn TenTaiKhoan = new DataColumn("Tên Tài Khoản");
            DataColumn TenHienThi = new DataColumn("Tên Hiển Thị");
            DataColumn MatKhau = new DataColumn("Mật Khẩu");
            DataColumn IsAdmin = new DataColumn("Loại");

            dt.Columns.Add(Id);
            dt.Columns.Add(MaNhanVien);
            dt.Columns.Add(TenTaiKhoan);
            dt.Columns.Add(TenHienThi);
            dt.Columns.Add(MatKhau);
            dt.Columns.Add(IsAdmin);

            foreach (var item in ketqua)
            {
                DataRow row = dt.NewRow();
                row["ID"] = item.Id;
                row["MNV"] = item.MaNhanVien;
                row["Tên Tài Khoản"] = item.TenTaiKhoan;
                row["Tên Hiển Thị"] = item.TenHienThi;
                row["Mật Khẩu"] = item.MatKhau;
                row["Loại"] = item.IsAdmin;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void Add(string manhanvien, string tentaikhoan, string tenhienthi, string matkhau, int isadmin)

        {
            tblTaiKhoan tk = new tblTaiKhoan();
            tk.MaNhanVien = manhanvien;
            tk.TenTaiKhoan = tentaikhoan;
            tk.TenHienThi = tenhienthi;
            tk.MatKhau = matkhau;
            tk.IsAdmin = isadmin;

            tk.NgayTao = DateTime.Now;
            tk.NguoiTao = NhanVien;

            db.tblTaiKhoans.InsertOnSubmit(tk);
            db.SubmitChanges();
        }

        public void Edit(int ma, string manhanvien, string tentaikhoan, string tenhienthi, string matkhau, int isadmin)
        {
            tblTaiKhoan tk = GetTaiKhoan(ma);
            tk.MaNhanVien = manhanvien;
            tk.TenTaiKhoan = tentaikhoan;
            tk.TenHienThi = tenhienthi;
            tk.MatKhau = matkhau;
            tk.IsAdmin = isadmin;

            tk.NgayTao = DateTime.Now;
            tk.NguoiTao = NhanVien;

            db.SubmitChanges();
        }

        public void Delete(int ma)
        {
            tblTaiKhoan tk = GetTaiKhoan(ma);
            db.tblTaiKhoans.DeleteOnSubmit(tk);
            db.SubmitChanges();
        }
    }
}
