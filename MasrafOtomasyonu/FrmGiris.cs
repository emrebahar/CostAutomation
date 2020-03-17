using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Entities;

namespace MasrafOtomasyonu
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (onDenetleme() == true)
                return;
            PersonelIslemleri pi = new PersonelIslemleri();
            Personel personel = pi.PersonelLogin(txtKullaniciAdi.Text, txtSifre.Text);

            if (personel != null)
            {
                this.Hide();

                frmAnaForm frm = new frmAnaForm();
                frm.GirisYapanPersonel = personel;
                frm.ShowDialog();

                Application.Exit();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ya da Şifre Hatalı.","Hatalı Giriş",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private bool onDenetleme()
        {
            bool result = false;
            errorProvider1.Clear();
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text;
            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                errorProvider1.SetError(txtKullaniciAdi, "Kullanıcı Adı Boş Geçilemez..");
                result = true;
            }

            if (string.IsNullOrEmpty(sifre))
            {
                errorProvider1.SetError(txtSifre, "Şifre Boş Geçilemez..");
                result = true;
            }
            return result;
        }
    }
}
