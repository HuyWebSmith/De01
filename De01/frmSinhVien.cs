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
    }
}
