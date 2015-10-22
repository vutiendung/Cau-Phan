using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2.APP.DVT
{
    public partial class FrmDVT : Form
    {
        private static int viTri;
        public FrmDVT()
        {
            InitializeComponent();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            LoadData();
        }
        void ClearRows()
        {
            while (dataDVT.Rows.Count > 0)
            {
                dataDVT.Rows.RemoveAt(0);
            }
        }
        void LoadData()
        {
            ClearRows();
            string querry = "select * from DONVITINH";
            DataTable donViTInh = ConnectToServer.Instance.GetTable(querry);
            dataDVT.DataSource = donViTInh;
        }

        private void btnXoaOnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (viTri >= 0 || viTri < dataDVT.Rows.Count)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa không?!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Question)
                                    == DialogResult.OK)
                {
                    string maDonVi = dataDVT.Rows[viTri].Cells[0].Value.ToString();
                    string qrXoa = "delete from DONVITINH where MADONVI='"+maDonVi+"'";
                    ConnectToServer.Instance.ExecuteQuerryNotReturn(qrXoa);
                    LoadData();
                    MessageBox.Show("Xóa thành công!"
                                    ,"Thông báo"
                                    ,MessageBoxButtons.OK
                                    ,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn đã chọn sai vị trí cần xóa!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Error);
            }

        }

        private void dataDVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            if (viTri >= 0 && viTri < dataDVT.Rows.Count)
            {
                txtTenDVT.Text = dataDVT.Rows[viTri].Cells[1].Value.ToString();
            }
        }

        private void btnSuaOnClick(object sender, EventArgs e)
        {
            if (viTri >= 0 || viTri < dataDVT.Rows.Count)
            {
                if (MessageBox.Show("Bạn có chắc muốn sửa không?!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Question)
                                    == DialogResult.OK)
                {
                    string maDonVi = dataDVT.Rows[viTri].Cells[0].Value.ToString();
                    string tenDonVi=txtTenDVT.Text;
                    string qrSua = "update DONVITINH set MOTA='"+tenDonVi+"' where MADONVI='" + maDonVi + "'";
                    ConnectToServer.Instance.ExecuteQuerryNotReturn(qrSua);
                    LoadData();
                    MessageBox.Show("Sửa thành công!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn đã chọn sai vị trí cần sửa!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Error);
            }
        }

        private void btnThemOnClick(object sender, EventArgs e)
        {
            string maDonVi = "DV"+dataDVT.Rows.Count+1;
            string tenDonViTinh=txtTenDVT.Text;
            if (tenDonViTinh.Length != 0)
            {
                if (KiemTraTonTaiDVT(tenDonViTinh) == false)
                {
                    string qr = "insert into DONVITINH VALUES('" + maDonVi + "',N'" + tenDonViTinh + "')";
                    ConnectToServer.Instance.ExecuteQuerryNotReturn(qr);
                    LoadData();
                    MessageBox.Show("Thêm thành công!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tên đơn vị tính đã tồn tại.\nVui lòng chọn tên khác.!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bạn không được để trống tên đơn vị tính!"
                                    , "Thông báo"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Error);
            }
        }
        bool KiemTraTonTaiDVT(string tenDVT)
        {
            string qr = "select MOTA from DONVITINH where MOTA='"+tenDVT+"'";
            DataTable donViTinh = ConnectToServer.Instance.GetTable(qr);
            if (donViTinh.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}
