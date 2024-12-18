using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using De01.Models;

namespace De01
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<Lop> listLop = context.Lops.ToList();
                List<SinhVien> listSinhVien = context.SinhViens.ToList();
                DuaVaoCombobox(listLop);
                ThemGrid(listSinhVien, listLop);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DuaVaoCombobox(List<Lop> listLop)
        {
            this.cmbLop.DataSource = listLop;
            this.cmbLop.DisplayMember = "TenLop";
            this.cmbLop.ValueMember = "MaLop";
        }

        private void ThemGrid(List<SinhVien> listStudent, List<Lop> listLop)
        {
            dgvSinhVien.Rows.Clear();
            foreach (var item in listStudent)
            {
                var tenLop = listLop.FirstOrDefault(
                    f => f.MaLop == item.MaLop)?.TenLop ?? "N/A";

                int index = dgvSinhVien.Rows.Add();
                dgvSinhVien.Rows[index].Cells[0].Value = item.MaSV;
                dgvSinhVien.Rows[index].Cells[1].Value = item.HoTenSV;
                dgvSinhVien.Rows[index].Cells[2].Value = item.NgaySinh;
                dgvSinhVien.Rows[index].Cells[3].Value = tenLop;

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnKLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                SinhVien student = new SinhVien()
                {
                    MaSV = txtMSSV.Text,
                    HoTenSV = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    MaLop = cmbLop.SelectedValue.ToString(),

                };
                context.SinhViens.Add(student);
                context.SaveChanges();
                List<Lop> listLop = context.Lops.ToList();
                List<SinhVien> listSinhVien = context.SinhViens.ToList();
                ThemGrid(listSinhVien, listLop);
                MessageBox.Show("Thêm Sinh Viên Thành Công");
                btnLuu.Enabled = false;
                btnKLuu.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                string studentId = txtMSSV.Text;
                SinhVien dbupdate = context.SinhViens.FirstOrDefault(p => p.MaSV == studentId);

                if (dbupdate != null)
                {
                    DialogResult dialog = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa sinh viên có mã {studentId} không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        context.SinhViens.Remove(dbupdate);
                        context.SaveChanges();

                        List<Lop> listLop = context.Lops.ToList();
                        List<SinhVien> listSinhVien = context.SinhViens.ToList();
                        ThemGrid(listSinhVien, listLop);

                        MessageBox.Show("Xóa Sinh Viên Thành Công");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                btnLuu.Enabled = false;
                btnKLuu.Enabled = false;
                Model1 context = new Model1();
                string studentId = txtMSSV.Text;
                SinhVien dbupdate = context.SinhViens.FirstOrDefault(p => p.MaSV == studentId);
                if (dbupdate != null)
                {
                    dbupdate.MaSV = txtMSSV.Text;
                    dbupdate.HoTenSV = txtHoTen.Text;
                    dbupdate.NgaySinh = dtpNgaySinh.Value;
                    dbupdate.MaLop = cmbLop.SelectedValue.ToString();
                    context.SaveChanges();
                    List<Lop> listLop = context.Lops.ToList();
                    List<SinhVien> listSinhVien = context.SinhViens.ToList();
                    ThemGrid(listSinhVien, listLop);
                    MessageBox.Show("Cập Nhật Sinh Viên Thành Công");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để sửa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                txtMSSV.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                dtpNgaySinh.Text = row.Cells[2].Value.ToString();
                cmbLop.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnKLuu.Enabled = false;
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtTim.Text == "Tìm Kiếm Theo Tên") return;
            string timKiem = txtTim.Text.ToLower().Trim();
            Model1 context = new Model1();
            List<Lop> listLop = context.Lops.ToList();
            List<SinhVien> listSinhVien = context.SinhViens.ToList();
            if (string.IsNullOrEmpty(timKiem)
                || timKiem == "Tìm Kiếm Theo Tên")
            {
                ThemGrid(listSinhVien, listLop);
                return;
            }

            var ketQua = listSinhVien.Where(s => s.HoTenSV.ToLower().Contains(timKiem)).ToList();

            ThemGrid(ketQua, listLop);
        }
    }
}
