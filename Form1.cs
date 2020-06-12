using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using Microsoft.Win32;
using System.Diagnostics;

namespace CariBilgi
{
	public partial class Form1 : DevExpress.XtraEditors.XtraForm
	{
		//public List<Model> cariListe;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				btnDelete.Enabled = false;
				//
				ddVergiDairesi.Properties.Items.Add("Antalya VD.");
				ddVergiDairesi.Properties.Items.Add("Konyaaltı VD.");
				ddVergiDairesi.Properties.Items.Add("Muratpaşa VD.");
				ddVergiDairesi.Properties.Items.Add("Kepez VD.");
				//
				eUlkeKodu1.Text = "TR";
				eUlkeKodu2.Text = "TÜRKİYE";
				eUlkeKodu1.ReadOnly = eUlkeKodu2.ReadOnly = true;
				eUlkeKodu1.Enabled = eUlkeKodu2.Enabled = false;
				//
				eWebAdresi.Text = "http://";
				FillGrid();
				gridHazırlama();
			}
			catch ( Exception ex )
			{

				throw new Exception("Veriler yüklenemedi." + ex.Message.ToString());
			}
		}
		//
		BusinessLayer getBusineesLayer;
		Model getData;
		int _Id;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region Buton click olayları
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if ( !FormControl() )
				{
					return;
				}
				getData = new Model()
				{
					ID = _Id,
					CARIKOD = eCariKod.Text,
					CARIISIM = eCariIsim.Text,
					ADRES = eAdres.Text,
					IL = eIl.Text,
					ILCE = eIlce.Text,
					ULKEKODU = eUlkeKodu1.Text,
					TELEFON = eTelefon.Text,
					FAX = eFax.Text,
					VERGIDAIRESI = ddVergiDairesi.Text,
					VERGINO = eVergiNo.Text,
					TCNO = eTCno.Text,
					POSTAKODU = ePostaKodu.Text,
					TIP = rgTip.SelectedIndex == -1 ? 0 : rgTip.SelectedIndex + 1,
					EMAIL = eEmail.Text,
					WEBADRESI = eWebAdresi.Text
				};

				if ( _Id == 0 )
				{
					getBusineesLayer = new BusinessLayer();
					getBusineesLayer.Save(getData);
					MessageBox.Show("Kayıt başarılı");
				}
				else
				{
					getBusineesLayer = new BusinessLayer();
					getBusineesLayer.Update(getData);
					MessageBox.Show("Güncelleme başarılı");
				}
			}
			catch ( Exception ex )
			{

				throw new Exception(ex.Message.ToString());
			}

			FillGrid();
		}
		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				getData = new Model()//silinecek veri
				{
					ID = _Id
				};

				getBusineesLayer = new BusinessLayer();
				getBusineesLayer.Delete(getData);

				Temizle();
				MessageBox.Show("Silme işlemi başarılı");

			}
			catch ( Exception ex )
			{

				throw new Exception(ex.Message.ToString()); ;
			}

			FillGrid();//listeyi günceller
		}
		private void gvListe_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				//deger var mı kontrolu
				if ( gvListe.FocusedRowHandle > -1 )
				{
					_Id = Convert.ToInt32(gvListe.GetFocusedRowCellValue("ID"));

					eCariKod.Text = gvListe.GetFocusedRowCellValue("CARIKOD").ToString();
					eCariIsim.Text = gvListe.GetFocusedRowCellValue("CARIISIM").ToString();
					eAdres.Text = gvListe.GetFocusedRowCellValue("ADRES").ToString();
					eIl.Text = gvListe.GetFocusedRowCellValue("IL").ToString();
					eIlce.Text = gvListe.GetFocusedRowCellValue("ILCE").ToString();
					eUlkeKodu1.Text = gvListe.GetFocusedRowCellValue("ULKEKODU").ToString();
					eTelefon.Text = gvListe.GetFocusedRowCellValue("TELEFON").ToString();
					eFax.Text = gvListe.GetFocusedRowCellValue("FAX").ToString();
					ddVergiDairesi.Text = gvListe.GetFocusedRowCellValue("VERGIDAIRESI").ToString();
					eVergiNo.Text = gvListe.GetFocusedRowCellValue("VERGINO").ToString();
					eTCno.Text = gvListe.GetFocusedRowCellValue("TCNO").ToString();
					ePostaKodu.Text = gvListe.GetFocusedRowCellValue("POSTAKODU").ToString();
					rgTip.SelectedIndex = Convert.ToInt32(gvListe.GetFocusedRowCellValue("TIP")) - 1;
					eEmail.Text = gvListe.GetFocusedRowCellValue("EMAIL").ToString();
					eWebAdresi.Text = gvListe.GetFocusedRowCellValue("WEBADRESI").ToString();

					//secılı deger varsa true olsun.
					btnDelete.Enabled = true;
					btnSave.Text = "Güncelle";
				}
			}
			catch ( Exception )
			{

				throw new Exception("seçili id değeri yok");
			}
		}
		private void btnClearAll_Click(object sender, EventArgs e)
		{
			Temizle();
		}
		private void btnWebAdresi_Click(object sender, EventArgs e)
		{
			tarayıcı_Ac();
		}
		#endregion
		/// <summary>
		/// Boş girişleri engellemek için kontol.
		/// </summary>
		/// <returns></returns>
		#region Methodlar 
		// ctrl + k + s ile seçtiğimiz satırları regiona alırız
		private bool FormControl()
		{
			try
			{
				if ( eCariKod.Text == "" || eCariIsim.Text == "" )
				{
					MessageBox.Show("Cari kod veya cari isim alanları boş bırakılamaz.", "Uyarı");
					return false;
				}
				return true;
			}
			catch ( Exception )
			{
				throw new Exception("form kontrol edilemedi");
			}

		}
		private void FillGrid()
		{
			getBusineesLayer = new BusinessLayer();
			var list = getBusineesLayer.GetAll();

			gcListe.DataSource = list;
		}
		private void gridHazırlama()
		{
			#region Sütun genişlik ayarlama
			var width = 200;
			gvListe.Columns[0].Width = Convert.ToInt32(width * 0.15);
			gvListe.Columns[1].Width = Convert.ToInt32(width * 0.5);
			gvListe.Columns[2].Width = Convert.ToInt32(width * 0.7);
			gvListe.Columns[3].Width = Convert.ToInt32(width * 0.9);
			gvListe.Columns[9].Width = Convert.ToInt32(width * 0.7);
			gvListe.Columns[10].Width = Convert.ToInt32(width * 0.5);
			gvListe.Columns[13].Width = Convert.ToInt32(width * 0.15);
			gvListe.Columns[15].Width = Convert.ToInt32(width * 0.9);
			gvListe.Columns[16].Width = Convert.ToInt32(width * 0.9);
			#endregion

			#region Sütün başlık(Caption) ayarlama
			gvListe.Columns[0].Caption = "Id";
			gvListe.Columns[1].Caption = "Cari Kod";
			gvListe.Columns[2].Caption = "Cari İsim";
			gvListe.Columns[3].Caption = "Adres";
			gvListe.Columns[4].Caption = "İl";
			gvListe.Columns[5].Caption = "İlçe";
			gvListe.Columns[6].Caption = "Ülke Kodu";
			gvListe.Columns[7].Caption = "Telefon";
			gvListe.Columns[8].Caption = "Fax";
			gvListe.Columns[9].Caption = "Vergi Dairesi";
			gvListe.Columns[10].Caption = "Vergi No";
			gvListe.Columns[11].Caption = "TC No";
			gvListe.Columns[12].Caption = "Posta Kodu";
			gvListe.Columns[13].Caption = "Tip Id";
			gvListe.Columns[14].Caption = "Tip";
			gvListe.Columns[15].Caption = "Email";
			gvListe.Columns[16].Caption = "Web Adres";
			#endregion

			#region Sütün görünürlüğü(Visible) ayarlama
			gvListe.Columns[0].Visible = false;
			gvListe.Columns[13].Visible = false;

			#endregion
		}
		private void tarayıcı_Ac()
		{
			string key = @"http\shell\open\command";
			RegistryKey registryKey =
			Registry.ClassesRoot.OpenSubKey(key, false);
			string defaultbrowserpath =
			((string)registryKey.GetValue(null, null)).Split('"')[1];
			Process.Start(defaultbrowserpath, eWebAdresi.Text);
		}
		public void Temizle()
		{
			try
			{
				_Id = 0;
				eCariKod.Text = "";
				eCariIsim.Text = "";
				eAdres.Text = "";
				eIl.Text = "";
				eIlce.Text = "";
				eUlkeKodu1.Text = "";
				eTelefon.Text = "";
				eFax.Text = "";
				ddVergiDairesi.Text = "";
				eVergiNo.Text = "";
				eTCno.Text = "";
				ePostaKodu.Text = "";
				rgTip.SelectedIndex = 0;
				eEmail.Text = "";
				eWebAdresi.Text = "http://";

				btnSave.Text = "Kaydet";
			}
			catch ( Exception ex )
			{

				throw new Exception("Formlar temizlenemedi. " + ex.Message.ToString());
			}
		} 
		#endregion
	}
}
