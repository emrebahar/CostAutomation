using BusinessLayer;
using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasrafOtomasyonu
{
    public partial class frmAnaForm : Form
    {
        public frmAnaForm()
        {
            InitializeComponent();
        }
        private MasrafIslemleri MI = new MasrafIslemleri();
        private PersonelIslemleri PI = new PersonelIslemleri();

        public Personel GirisYapanPersonel { get; internal set; }

        private void frmAnaForm_Load(object sender, EventArgs e)
        {
            lblPersonelAdSoyad.Text = string.Format("{0} {1} ({2})", GirisYapanPersonel.Adi, GirisYapanPersonel.Soyadi, PersonelTuruEnumHelper.GetText(GirisYapanPersonel.PersonelTurId));
            //GirisYapanPersonel.Adi + " " + GirisYapanPersonel.Soyadi + " (" + PersonelTuruEnumHelper.GetText(GirisYapanPersonel.PersonelTurId) + ")";

            GetirPersonelMasraflari();

            GetirPersonelSorumluListe();
        }

        private void GetirPersonelSorumluListe()
        {
            List<Personel> masrafSahipleri = new List<Personel>();
            if (GirisYapanPersonel.PersonelTurId != (byte)PersonelTuruEnum.Muhasebe)
            {
                masrafSahipleri.Add(GirisYapanPersonel);
                masrafSahipleri.AddRange(PI.GetirPersonelBySorumluId(GirisYapanPersonel.Id));
            }
            else
            {
                masrafSahipleri.AddRange(PI.GetirTumPersonel());
            }
            cmbMasrafSahibi.DataSource = masrafSahipleri;
        }

        private void GetirPersonelMasraflari()
        {
            if (cmbMasrafSahibi.SelectedIndex > -1)
            {
                Personel personel = cmbMasrafSahibi.SelectedItem as Personel;

                List<Masraf> masraflar = MI.GertirMasraflar(personel.Id);
                lstMasraflar.DataSource = masraflar;
            }

        }

        private void btnMasrafEkle_Click(object sender, EventArgs e)
        {
            Masraf masraf = new Masraf()
            {
                Id = Guid.NewGuid(),
                Baslik = txtBaslik.Text,
                Tarih = dtpTarih.Value,
                Tutar = nudTutar.Value,
                Aciklama = txtAciklama.Text,
                PersonelId = GirisYapanPersonel.Id,
                DurumId = (byte)DurumEnum.OnayBekliyor
            };

            if (GirisYapanPersonel.PersonelTurId == (byte)PersonelTuruEnum.Yonetici)
            {
                masraf.DurumId = (byte)DurumEnum.Onaylandi;
            }

            int sonuc = MI.MasrafEkle(masraf);

            if (sonuc > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf Eklenemedi.");
            }

        }

        private void lstMasraflar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
                return;
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;
            if (masraf != null)
            {
                txtBaslik.Text = masraf.Baslik;
                txtAciklama.Text = masraf.Aciklama;
                dtpTarih.Value = masraf.Tarih;
                nudTutar.Value = masraf.Tutar;
            }
            if (masraf.DurumId == (byte)DurumEnum.Reddedildi || masraf.DurumId == (byte)DurumEnum.Odendi)
            {
                btnMasrafKaydet.Enabled = false;
            }
            else
            {
                btnMasrafKaydet.Enabled = true;
            }
            if (masraf.DurumId == (byte)DurumEnum.Odendi)
            {
                btnMasrafSil.Enabled = false;
            }
            else
            {
                btnMasrafSil.Enabled = true;
            }
            if (masraf.DurumId == (byte)DurumEnum.Onaylandi)
            {
                cmnuOdendi.Enabled = true;
            }
            else
            {
                cmnuOdendi.Enabled = false;
            }
        }

        private void btnMasrafKaydet_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Güncelleme işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;

            masraf.Baslik = txtBaslik.Text;
            masraf.Aciklama = txtAciklama.Text;
            masraf.Tarih = dtpTarih.Value;
            masraf.Tutar = nudTutar.Value;
            masraf.DurumId = (byte)DurumEnum.OnayBekliyor;

            if (GirisYapanPersonel.PersonelTurId == (byte)PersonelTuruEnum.Yonetici)
            {
                masraf.DurumId = (byte)DurumEnum.Onaylandi;
            }
            if (MI.MasrafGuncelle(masraf) > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf Güncellenemedi.");
            }
        }

        private void lstMasraflar_Format(object sender, ListControlConvertEventArgs e)
        {
            Masraf masraf = e.ListItem as Masraf;

            e.Value = string.Format("{0} ({1})", masraf.Baslik, DurumEnumHelper.GetText(masraf.DurumId));
        }

        private void btnMasrafSil_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen silme işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;
            DialogResult result = MessageBox.Show(masraf.Baslik + " başlıklı masraf silinsin mi?", "Masraf Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (MI.MasrafSil(masraf) > 0)
                {
                    GetirPersonelMasraflari();
                }
                else
                {
                    MessageBox.Show("Masraf Silinemedi.");
                }
            }
        }

        private void cmbMasrafSahibi_Format(object sender, ListControlConvertEventArgs e)
        {
            Personel personel = e.ListItem as Personel;
            e.Value = string.Format("{0} {1}", personel.Adi, personel.Soyadi);
        }

        private void cmbMasrafSahibi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMasrafSahibi.SelectedIndex > -1)
            {
                Personel personel = cmbMasrafSahibi.SelectedItem as Personel;

                if (personel.Id == GirisYapanPersonel.Id)
                {
                    flpDugmeler.Enabled = true;
                    lstMasraflar.ContextMenuStrip = null;
                }
                else
                {
                    flpDugmeler.Enabled = false;
                    lstMasraflar.ContextMenuStrip = cmnuMasrafYonet;
                }
                if (GirisYapanPersonel.PersonelTurId == (byte)PersonelTuruEnum.Muhasebe)
                {
                    lstMasraflar.ContextMenuStrip = cmnuMuhasebe;
                }
                List<Masraf> masraflar = MI.GertirMasraflar(personel.Id);

                lstMasraflar.DataSource = masraflar;
            }
        }

        private void cmnuOnayla_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Onaylama işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;

            masraf.DurumId = (byte)DurumEnum.Onaylandi;

            if (MI.MasrafGuncelle(masraf) > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf Onaylanmadı.");
            }
        }

        private void cmnuGuncellenmeli_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Güncelleme işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;

            masraf.DurumId = (byte)DurumEnum.Duzeltilecek;

            if (MI.MasrafGuncelle(masraf) > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf durumu değiştirilemedi.");
            }
        }

        private void cmnuReddet_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen reddetme işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;

            masraf.DurumId = (byte)DurumEnum.Reddedildi;

            if (MI.MasrafGuncelle(masraf) > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf reddedilemedi.");
            }
        }

        private void cmnuOdendi_Click(object sender, EventArgs e)
        {
            if (lstMasraflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen ödeme onayı işlemi için bir masraf seçiniz.");
                return;
            }
            Masraf masraf = lstMasraflar.SelectedItem as Masraf;
            masraf.DurumId = (byte)DurumEnum.Odendi;

            if (MI.MasrafGuncelle(masraf) > 0)
            {
                GetirPersonelMasraflari();
            }
            else
            {
                MessageBox.Show("Masraf durumu değiştirilmedi.");
            }
        }
    }
}
