using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CariBilgi
{
	/// <summary>
	/// 
	/// </summary>

	
	public class BusinessLayer : IBusinessLayer<Model, int>
	{
		SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-INRNTK1\MSSQLSERVER2019;Initial Catalog=CARI;Integrated Security=True");
		public bool Delete(Model item)
		{
			try
			{
				sqlOpen();
				sqlCon.Query<Model>(@"DELETE FROM [dbo].[CARI] WHERE ID = @ID", item);

				return true;
			}
			catch ( Exception ex)
			{

				throw new Exception(string.Format("Silme hatası:{1} ID:{0}",item, ex.Message.ToString()));
			}
			finally
			{
				sqlClose();
			}
		}

	
		public List<Model> GetAll()
		{
			try
			{
				sqlOpen();
				List<Model> list =sqlCon.Query<Model>(@"SELECT * FROM [dbo].[CARI]").ToList();


				return list;
			}
			catch ( Exception ex )
			{

				throw new Exception(string.Format("Listeleme hatası:{0}", ex.Message.ToString()));
			}
			finally
			{
				sqlClose();
			}
		}

		public bool Save(Model item)
		{
			try
			{
				sqlOpen();
				sqlCon.Query<Model>(@"INSERT INTO [dbo].[CARI]  ([CARIKOD]
           ,[CARIISIM]
           ,[ADRES]
           ,[IL]
           ,[ILCE]
           ,[ULKEKODU]
           ,[TELEFON]
           ,[FAX]
           ,[VERGIDAIRESI]
           ,[VERGINO]
           ,[TCNO]
           ,[POSTAKODU]
           ,[TIP]
           ,[EMAIL]
           ,[WEBADRESI]) VALUES (@CARIKOD,@CARIISIM,@ADRES,@IL,@ILCE,@ULKEKODU,@TELEFON,@FAX,@VERGIDAIRESI,@VERGINO,@TCNO,@POSTAKODU,@TIP,@EMAIL,@WEBADRESI)",item);

				return true;
			}
			catch ( Exception ex )
			{

				throw new Exception(string.Format("Ekleme hatası:{1} ID:{0}", item, ex.Message.ToString()));
			}
			finally
			{
				sqlClose();
			}
		}

		public bool Update(Model item)
		{
			try
			{
				sqlOpen();
				sqlCon.Query<Model>(@"UPDATE [dbo].[CARI] SET [CARIKOD] = @CARIKOD,
      [CARIISIM]     = @CARIISIM,
      [ADRES]        = @ADRES,
      [IL]           = @IL, 
      [ILCE]         = @ILCE,
      [ULKEKODU]     = @ULKEKODU, 
      [TELEFON]      = @TELEFON,
      [FAX]          = @FAX,
      [VERGIDAIRESI] = @VERGIDAIRESI,
      [VERGINO]      = @VERGINO,
      [TCNO]         = @TCNO,
      [POSTAKODU]    = @POSTAKODU,
      [TIP]          = @TIP,
      [EMAIL]        = @EMAIL,
      [WEBADRESI]    = @WEBADRESI WHERE ID = @ID", item);

				return true;
			}
			catch ( Exception ex )
			{

				throw new Exception(string.Format("Günclleme hatası:{1} Id:{0}", item, ex.Message.ToString()));
			}
			finally
			{
				sqlClose();
			}
		}

		//(ctrl + k +s ) region
		#region Connection Open Close method
		public void sqlOpen()
		{
			if ( sqlCon.State == System.Data.ConnectionState.Closed )
				sqlCon.Open();
		}

		private void sqlClose()
		{
			if ( sqlCon.State == System.Data.ConnectionState.Open )
				sqlCon.Close();
		} 
		#endregion

	}
}
