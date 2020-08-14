using System;

namespace BusinessLayer.Accounts
{
	/// <summary>
	/// Summary description for clsGlobalVariable.
	/// Code Written By : Kuntal Banerjee
	/// Date			: 16-June-2007
	/// Modification	:
	/// Modified By		Date		Comment/Modification details
	/// 
	/// </summary>
	public class clsGlobalVariable
	{
		public static string gStrConnectionMsg;
		public static string gStrSystemErrorMsg;
		public static string gSstrUserErrorMsg;
		public static bool gBoolErrorMsg;
		public static string strConnString;
		public static string strBackupConnString;
		public static string strDataSource = "";
		public static string strUserID = "";
		public static string strPWD = "";

		public static string gcnstAppPath = "";

		public static bool gBoolSubItemEntry = false;
		//public static string strFileValue = "";


        //public static string sesCompanyID = "SesCompanyID"; // sesion variable for company
        //public static string sesBranchID = "SesBranchID";   // sesion variable for Branch
        //public static string sesUserID = "SesUserID";       // sesion variable for User
        //public static string sesFinYrID = "SesFinYrID";     // sesion variable for Financial year
        //public static string sesUserType = "SesUserType";   // sesion variable for User Type
        //public static string sesDataFlow = "SesDataFlow";   // sesion variable for Operation Type 1 No or 2 No


        public static string sesPageTitle = "SesPageTitle"; // session variable for Page Title
        public static string sesSearch = "SesSearch";   // sesion variable for Search Page
        public static string SesSummary = "SesSummary";   // sesion variable for Summary

        public static string sesCompanyID = "SesCompanyID"; // session variable for company
        public static string sesBranchID = "SesBranchID";   // session variable for Branch
        public static string sesUserID = "SesUserID";       // session variable for User ID
        public static string sesUserName = "SesUserName";   // session variable for User Name
        public static string sesFinYrID = "SesFinYrID";     // session variable for Financial year ID
        public static string sesUserType = "SesUserType";   // session variable for User Type
        //Variables for Report print formats
        public static string sesReportTitle = "SesRPTTitle";    // session variable for Report Page Title
        public static string sesReportHeader = "SesRPTHeader";  // session variable for Report Header
        public static string sesReportGrid = "SesRPTGrid";      // session variable for Report Page Grid
        public static string sesReportPageHeader = "SesRPTPageHeader";  // session variable for HTML Report Page Header
        public static string sesReportPageFooter = "SesRPTPageFooter";  // session variable for HTML Report Page Footer
        public static string sesReportPageBody = "SesRPTPageHTMLBody";  // session variable for HTML Report Page Body
        public static string sesReportTitle2 = "SesRPTTitle2";    // session variable for Report Page Title 2
        public static string sesReportHeader2 = "sesReportHeader2";  // session variable for Report Header 2
        public static string sesReportPageFooter2 = "SesRPTPageFooter2";  // session variable for HTML Report Page Footer 2
        public static string sesStuddedImageURL = "SesStuddedURL";
        public static string sesSketchImageURL = "SesSketchURL";

        public static string sesLAN = "sesLAN";
        public static string sesWAN = "sesWAN";
        //public static objConn conn;
        public static int intFlagImg ;
       
        public static int intFlagImgSktch;
		public clsGlobalVariable()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
