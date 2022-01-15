using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DAL;
using BLL;
using System.IO;

namespace QuanLyCaFe
{
    public partial class frmAdmin : Form
    {
        DSMatHangBLL dsMatHang = new DSMatHangBLL();
        DSKhuVucBLL dsKhuVuc = new DSKhuVucBLL();
        DSBanBLL dsBan = new DSBanBLL();
        DSTaiKhoanBLL dsTaiKhoan = new DSTaiKhoanBLL();

        BindingSource DSMHlist = new BindingSource();
        BindingSource DSKVlist = new BindingSource();
        BindingSource DSBanlist = new BindingSource();
        BindingSource DSTaiKhoanlist = new BindingSource();

        private DataGridViewRow r;

        public frmAdmin()
        {
            InitializeComponent();
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {

        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            dtgvProduct.DataSource = DSMHlist;
            dtgvArea.DataSource = DSKVlist;
            dtgvTable.DataSource = DSBanlist;
            dtgvAccount.DataSource = DSTaiKhoanlist;

            LoadDSMH();
            LoadDSKhuVuc();
            LoadDSBan();
            LoadDSTaiKhoan();
        }

        #region DSMH
        //CURD DSMatHang

        private void LoadDSMH()
        {
            DSMHlist.DataSource = dsMatHang.GetDSMatHang();

            cbProductDVT.DataSource = dsMatHang.GetListDVT();
            cbProductDVT.DisplayMember = "Ten";
            cbProductDVT.ValueMember = "Id";
            cbProductDVT.SelectedIndex = -1;

            dtgvProduct.Columns[0].Width = 50;
            dtgvProduct.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvProduct.Columns[2].Width = 70;
            dtgvProduct.Columns[3].Width = 70;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Select();
                return;
            }
            if (cbProductDVT.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(nmProductPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập giá tiền", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmProductPrice.Select();
                return;
            }
            dsMatHang.Add(txtProductName.Text, int.Parse(cbProductDVT.SelectedValue.ToString()), int.Parse(nmProductPrice.Text.ToString()));
            MessageBox.Show("Thêm mới sản phẩm thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSMH();
            nmProductPrice.Text = txtProductName.Text = null;
            cbProductDVT.SelectedIndex = -1;
        }

        private void dtgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dtgvProduct.Rows[e.RowIndex];
                txtProductID.Text = r.Cells["ID"].Value.ToString();
                txtProductName.Text = r.Cells["Tên Mặt Hàng"].Value.ToString();
                cbProductDVT.Text = r.Cells["Tên"].Value.ToString();
                nmProductPrice.Text = r.Cells["Giá Bán"].Value.ToString();
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng muốn xóa", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn thực sự muốn xóa mặt hàng " + r.Cells["Tên Mặt Hàng"].Value.ToString() + "?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                try
                {
                    dsMatHang.Delete(int.Parse(r.Cells["Id"].Value.ToString()));
                    MessageBox.Show("Xóa mặt hàng thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDSMH();

                    txtProductName.Text = "0";
                    cbProductDVT.SelectedIndex = -1;
                    nmProductPrice.Text = null;
                    r = null;
                }
                catch
                {
                    MessageBox.Show("Xóa mặt hàng thất bại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Select();
                return;
            }
            if (cbProductDVT.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(nmProductPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập giá tiền", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmProductPrice.Select();
                return;
            }
            dsMatHang.Edit(int.Parse(r.Cells["Id"].Value.ToString()), txtProductName.Text, int.Parse(cbProductDVT.SelectedValue.ToString()), int.Parse(nmProductPrice.Text.ToString()));
            MessageBox.Show("Sửa sản phẩm thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSMH();
            nmProductPrice.Text = txtProductName.Text = null;
            cbProductDVT.SelectedIndex = -1;
        }

        #endregion

        #region DSKV
        //CURD DSKhuVuc

        private void LoadDSKhuVuc()
        {
            DSKVlist.DataSource = dsKhuVuc.GetDSKhuVuc();

            dtgvArea.Columns[0].Width = 50;
            dtgvArea.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAddArea_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAreaName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khu vực", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAreaName.Select();
                return;
            }
            dsKhuVuc.Add(txtAreaName.Text);
            MessageBox.Show("Thêm mới sản phẩm thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSKhuVuc();
        }

        private void dtgvArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dtgvArea.Rows[e.RowIndex];
                txtAreaID.Text = r.Cells["ID"].Value.ToString();
                txtAreaName.Text = r.Cells["Tên Khu Vực"].Value.ToString();
            }
        }

        private void btnDeleteArea_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Vui lòng chọn khu vục muốn xóa", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn thực sự muốn xóa khu vực " + r.Cells["Tên Khu Vực"].Value.ToString() + "?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                try
                {
                    dsKhuVuc.Delete(int.Parse(r.Cells["Id"].Value.ToString()));
                    MessageBox.Show("Xóa khu vực thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDSKhuVuc();

                    txtAreaName.Text = "0";
                    r = null;
                }
                catch
                {
                    MessageBox.Show("Xóa khu vực thất bại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditArea_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAreaName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khu vực", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAreaName.Select();
                return;
            }
            dsKhuVuc.Edit(int.Parse(r.Cells["Id"].Value.ToString()), txtAreaName.Text);
            MessageBox.Show("Sửa sản phẩm thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSKhuVuc();
        }

        #endregion

        #region DSBan
        //CURD DSBan

        private void LoadDSBan()
        {
            DSBanlist.DataSource = dsBan.GetDSBan();

            cbTableKV.DataSource = dsBan.GetListKV();
            cbTableKV.DisplayMember = "TenKhuVuc";
            cbTableKV.ValueMember = "Id";
            cbTableKV.SelectedIndex = -1;

            dtgvTable.Columns[0].Width = 50;
            dtgvTable.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvTable.Columns[2].Width = 50;
            dtgvTable.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTableName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Bàn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Select();
                return;
            }
            if (cbTableKV.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khu vực", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(nmNumberChair.Text))
            {
                MessageBox.Show("Vui lòng nhập số ghế", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmNumberChair.Select();
                return;
            }
            dsBan.Add(txtTableName.Text, int.Parse(cbTableKV.SelectedValue.ToString()), int.Parse(nmNumberChair.Text.ToString()));
            MessageBox.Show("Thêm mới sản phẩm thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSBan();
            nmNumberChair.Text = txtTableName.Text = null;
            cbTableKV.SelectedIndex = -1;
        }

        private void dtgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dtgvTable.Rows[e.RowIndex];
                txtTableID.Text = r.Cells["ID"].Value.ToString();
                txtTableName.Text = r.Cells["Tên Bàn"].Value.ToString();
                nmNumberChair.Text = r.Cells["Số Ghế"].Value.ToString();
                cbTableKV.Text = r.Cells["Tên Khu Vực"].Value.ToString();
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if(r == null)
            {
                MessageBox.Show("Vui lòng chọn bàn muốn xóa", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn thực sự muốn xóa bàn " + r.Cells["Tên Bàn"].Value.ToString() + "?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                try
                {
                    dsBan.Delete(int.Parse(r.Cells["Id"].Value.ToString()));
                    MessageBox.Show("Xóa bàn thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDSBan();

                    txtTableName.Text = "0";
                    cbTableKV.SelectedIndex = -1;
                    nmNumberChair.Text = null;
                    r = null;
                }
                catch
                {
                    MessageBox.Show("Xóa bàn thất bại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTableName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bàn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Select();
                return;
            }
            if (cbTableKV.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khu vực", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(nmNumberChair.Text))
            {
                MessageBox.Show("Vui lòng nhập số ghế", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmNumberChair.Select();
                return;
            }
            dsBan.Edit(int.Parse(r.Cells["Id"].Value.ToString()), txtTableName.Text, int.Parse(cbTableKV.SelectedValue.ToString()), int.Parse(nmNumberChair.Text.ToString()));
            MessageBox.Show("Sửa bàn thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSBan();
            nmNumberChair.Text = txtTableName.Text = null;
            cbTableKV.SelectedIndex = -1;
        }

        #endregion

        #region DSTaiKhoan
        //CRUD DSTaiKhoan

        private void LoadDSTaiKhoan()
        {
            DSTaiKhoanlist.DataSource = dsTaiKhoan.GetDSTaiKhoan();

            dtgvAccount.Columns[0].Width = 30;
            dtgvAccount.Columns[1].Width = 50;
            dtgvAccount.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvAccount.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvAccount.Columns[4].Width = 80;
            dtgvAccount.Columns[5].Width = 30;
        }

        private void btnAddAcount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountMNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountMNV.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountDisplayName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountDisplayName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountPass.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountPass.Select();
                return;
            }
            if (string.IsNullOrEmpty(nmAccountType.Text))
            {
                MessageBox.Show("Vui lòng nhập loại tài khoản", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmNumberChair.Select();
                return;
            }
            dsTaiKhoan.Add(txtAccountMNV.Text, txtAccountName.Text, txtAccountDisplayName.Text, txtAccountPass.Text, int.Parse(nmNumberChair.Text.ToString()));
            MessageBox.Show("Thêm mới tài khoản thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSTaiKhoan();
            nmAccountType.Text = txtAccountMNV.Text = txtAccountName.Text = txtAccountDisplayName.Text = txtAccountPass.Text = null;
        }

        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dtgvAccount.Rows[e.RowIndex];
                txtAccountID.Text = r.Cells["ID"].Value.ToString();
                txtAccountMNV.Text = r.Cells["MNV"].Value.ToString();
                txtAccountName.Text = r.Cells["Tên Tài Khoản"].Value.ToString();
                txtAccountDisplayName.Text = r.Cells["Tên Hiển Thị"].Value.ToString();
                txtAccountPass.Text = r.Cells["Mật Khẩu"].Value.ToString();
                nmAccountType.Text = r.Cells["Loại"].Value.ToString();
            }
        }

        private void btnDeleteAcount_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản muốn xóa", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn thực sự muốn xóa tài khoản " + r.Cells["Tên Tài Khoản"].Value.ToString() + "?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                try
                {
                    dsTaiKhoan.Delete(int.Parse(r.Cells["Id"].Value.ToString()));
                    MessageBox.Show("Xóa tài khoản thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDSTaiKhoan();

                    txtAccountMNV.Text = txtAccountName.Text = txtAccountDisplayName.Text = txtAccountPass.Text = "0";
                    cbTableKV.SelectedIndex = -1;
                    nmAccountType.Text = null;
                    r = null;
                }
                catch
                {
                    MessageBox.Show("Xóa tài khoản thất bại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditAcount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountMNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountMNV.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountDisplayName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountDisplayName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountPass.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountPass.Select();
                return;
            }
            if (string.IsNullOrEmpty(nmAccountType.Text))
            {
                MessageBox.Show("Vui lòng nhập loại tài khoản", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmAccountType.Select();
                return;
            }
            dsTaiKhoan.Edit(int.Parse(r.Cells["Id"].Value.ToString()), txtAccountMNV.Text, txtAccountName.Text, txtAccountDisplayName.Text, txtAccountPass.Text, int.Parse(nmNumberChair.Text.ToString()));
            MessageBox.Show("Sửa tài khoản thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDSTaiKhoan();
            nmAccountType.Text = txtAccountMNV.Text = txtAccountName.Text = txtAccountDisplayName.Text = txtAccountPass.Text = null;
        }

        #endregion
    }
}
