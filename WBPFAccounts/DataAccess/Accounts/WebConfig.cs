using System;
using System.Configuration;

namespace DataAccess.Accounts
{
	/// <summary>
	/// Summary description for WebConfig.
	/// </summary>
	public sealed class WebConfig
	{
		public static string connectionstring
		{
			get
			{
                return ConfigurationSettings.AppSettings["ConnectionString"];
			}
		}
		public static string XMLpathstring
		{
			get
			{
				return ConfigurationSettings.AppSettings["XMLPath"];
			}
		}
		public static string imagePathstring
		{
			get
			{
				return ConfigurationSettings.AppSettings["imagePath"];
			}
		}
		public static string SMTPEmailIP
		{
			get
			{
				return ConfigurationSettings.AppSettings ["SMTPEmailIP"];
				//return "192.192.192.58";
			}
		}

        public static string ReportData
        {
            get
            {
                return ConfigurationSettings.AppSettings["ReportData"];
            }
        }
        public static string Mailfrm
        {
            get
            {
                return ConfigurationSettings.AppSettings["FromMail"];
            }
        }

        public static string ReportDataURL
        {
            get
            {
                return ConfigurationSettings.AppSettings["ReportDataURL"];
            }
        }
        public static int ReportDataRowCount
        {
            get
            {
                return Convert.ToInt32(ConfigurationSettings.AppSettings["ReportDataRowCount"]);
            }
        }

        public static string SessionFolder
        {
            get
            {
                return ConfigurationSettings.AppSettings["SessionPath"];
            }
        }

	}
}
