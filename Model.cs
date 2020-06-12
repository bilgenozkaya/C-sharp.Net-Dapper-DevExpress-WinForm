using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CariBilgi
{
	public  class Model
	{
		public int ID { get; set; }
		public string CARIKOD { get; set; }
		public string CARIISIM { get; set; }
		public string ADRES { get; set; }
		public string IL { get; set; }
		public string ILCE { get; set; }
		public string ULKEKODU { get; set; }
		public string TELEFON { get; set; }
		public string FAX { get; set; }
		public string VERGIDAIRESI { get; set; }
		public string VERGINO { get; set; }
		public string TCNO { get; set; }
		public string POSTAKODU { get; set; }
		public int TIP { get; set; }
		public string TIPSTR { get { return TIP == (int)Tip.Alıcı ? "Alıcı" : (TIP == (int)Tip.Satıcı ? "Satıcı" : (TIP == (int)Tip.Toptancı ? "Toptancı" : (TIP == (int)Tip.Kefil ? "Kefil" : (TIP == (int)Tip.Müstahsil ? "Müstahsil" : (TIP == (int)Tip.Diğer ? "Diğer" : ""))))); } }
		public string EMAIL { get; set; }
		public string WEBADRESI { get; set; }

	
	}
	public enum Tip
	{
		Alıcı = 1,
		Satıcı = 2,
		Toptancı = 3,
		Kefil = 4,
		Müstahsil = 5,
		Diğer = 6
	}
}
