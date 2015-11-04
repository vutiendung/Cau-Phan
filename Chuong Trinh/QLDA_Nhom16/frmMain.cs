using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication2.APP;
using WindowsFormsApplication2.APP.HeThong;
using WindowsFormsApplication2.APP.Nhân_viên;
using WindowsFormsApplication2.APP.HoaDon;
using WindowsFormsApplication2.APP.KhachHang;
using WindowsFormsApplication2.APP.NhaCungCap;
using WindowsFormsApplication2.APP.DVT;

namespace WindowsFormsApplication2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public static string quyen;
        public static string maNV;

        private void frmMain_Load(object sender, EventArgs e)
        {
            UCThuoc thuoc = new UCThuoc();
            this._panelMain.Controls.Clear();
            thuoc.Dock = DockStyle.Fill;
            this._panelMain.Controls.Add(thuoc);

            if (quyen == "Nhân viên")
            {
                //vToolStripMenuItem.Enabled = false;
                //cToolStripMenuItem.Enabled = false;
            }
            string sql = "SELECT NHANVIEN.HOTEN, ChUCVU.MOTA FROM NHANVIEN,CHUCVU WHERE CHUCVU.MACHUCVU = NHANVIEN.MACHUCVU AND MANHANVIEN = '" + maNV + "' ";
            DataTable TenNhanVien = ConnectToServer.Instance.GetTable(sql);
            if (TenNhanVien.Rows.Count > 0)
            {
                _toolStripStatusLabel.Text = TenNhanVien.Rows[0][0].ToString();
                toolStripStatusLabel3.Text = TenNhanVien.Rows[0][1].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeColor(button1);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            if (button1.BackColor != Color.FromArgb(66, 131, 222))
            {
                button1.BackColor = Color.FromArgb(93, 93, 93);
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.FromArgb(93, 93, 93))
            {
                button1.BackColor = Color.FromArgb(103, 103, 103);
            }
        }

        private void ChangeColor(Button b)
        {
            foreach (Control c in panelLeft.Controls)
            {
                if (c.BackColor == Color.FromArgb(66, 131, 222))
                {
                    c.BackColor = Color.FromArgb(93, 93, 93);
                }
            }

            if (b != null)
            {
                b.BackColor = Color.FromArgb(66, 131, 222);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeColor(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeColor(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void saoLưuVàPhụcHồiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SLPHDuLieu frm = new SLPHDuLieu();
            frm.ShowDialog();
        }
    }
}
