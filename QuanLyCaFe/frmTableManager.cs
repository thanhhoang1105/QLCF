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
    public partial class frmTableManager : Form
    {

        //TableBLL table = new TableBLL();
        DSMatHangBLL mathang = new DSMatHangBLL();

        dbDataContext db = new dbDataContext();

        private ListView lv;
        Panel pn;
        ImageList img1;

        public frmTableManager()
        {
            InitializeComponent();
        }

        private void frmTableManager_Load(object sender, EventArgs e)
        {
            foreach (var kv in db.tblKhuVucs)
            {
                TabPage tp = new TabPage(kv.TenKhuVuc);
                tp.Name = kv.Id.ToString();
                tbcDSB.Controls.Add(tp);
            }

            var minIdkv = db.tblKhuVucs.Min(x => x.Id);
            LoadBan(minIdkv.ToString(), 0);
            LoadMatHang();
        }

        private void tbcDSB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idkv = tbcDSB.SelectedTab.Name;
            var tabIndex = tbcDSB.SelectedIndex;
            LoadBan(idkv, tabIndex);
        }

        private void LoadBan(string idkv, int tabIndex)
        {
            lv = new ListView();
            var dsban = db.tblBans.Where(x => x.IdKV == int.Parse(idkv));
            if (lv.Items.Count > 0)
            {
                lv.Items.Clear();
            }

            img1 = new ImageList();
            img1.ImageSize = new Size(64, 64);

            DirectoryInfo dir = new DirectoryInfo(@"G:\TaiLieuHocTap\Ky_1_Nam_4\.NET\18103080\QuanLyCaFe\QuanLyCaFe\img");
            foreach (FileInfo fi in dir.GetFiles())
            {
                img1.Images.Add(Image.FromFile(fi.FullName));
            }

            pn = new Panel();
            pn.Dock = DockStyle.Fill;
            pn.BackColor = Color.SeaGreen;

            lv = new ListView();
            lv.Dock = DockStyle.Fill;
            lv.LargeImageList = img1;

            foreach (var ban in dsban)
            {
                if (ban.DangPhucVu == 1)
                {
                    lv.Items.Add(new ListViewItem { ImageIndex = 0, Text = ban.TenBan });
                }
                else
                {
                    lv.Items.Add(new ListViewItem { ImageIndex = 1, Text = ban.TenBan });
                }

            }

            pn.Controls.Add(lv);
            tbcDSB.TabPages[tabIndex].Controls.Add(pn);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatHang();
        }

        private void LoadMatHang()
        {
            cbDSProduct.DataSource = mathang.GetListMatHang();
            cbDSProduct.DisplayMember = "TenMatHang";
            cbDSProduct.ValueMember = "Id";
            //cbDSProduct.SelectedIndex = -1;
        }
    }
}
