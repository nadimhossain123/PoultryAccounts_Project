using System;
//using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Data;

namespace BusinessLayer.Accounts
{
	/// <summary>
	/// Summary description for clsGlobalConstants.
	/// </summary>
	public class clsGlobalConstants
	{
		public clsGlobalConstants()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static string gCnstStrSelect = "Select";
		public static string gCnstStrSelectNone = "None";
		public static string gCnstStrFieldTypeField = "Field";
		public static string gCnstStrFieldTypeGroup = "Group";
		public static string gCnstStrDisplayTypeH = "Horizontal";
		public static string gCnstStrDisplayTypeV = "Vertical";
		public static bool gCnstboolGroupRepeatY = true;
		public static bool gCnstboolGroupRepeatN = true;
		public static bool gCnstboolFieldRepeatY = true;
		public static bool gCnstboolFieldRepeatN = true;
		public static bool gCnstboolFieldRequiredY = true;
		public static bool gCnstboolFieldRequiredN = true;

		//Class hierarchy names
		public static string gCnstStrTextBox = "System.Windows.Forms.TextBox";
		public static string gCnstStrComboBox = "System.Windows.Forms.ComboBox";
		public static string gCnstStrCheckListBox = "System.Windows.Forms.CheckedListBox";
		public static string gCnstStrPictureBox = "System.Windows.Forms.PictureBox";

		#region All tablename & fieldname constants for AutoID generation
		//All tablename & fieldname constants for AutoID generation
		public static string gCnstStrTableMstDesigner = "MstDesigner";
		public static string gCnstStrTableMstManufacturer = "MstManufacturer";
		public static string gCnstStrTableMstBuyer = "MstBuyer";
		public static string gCnstStrTableTrnSpecificationStatic = "TrnSpecificationStatic";

		public static string gCnstStrFieldMstDesigner = "DCode";
		public static string gCnstStrFieldMstManufacturer = "MCode";
		public static string gCnstStrFieldMstBuyer = "BCode";
		public static string gCnstStrFieldTrnSpecificationStatic = "SSDesignNo";

		#endregion


		#region All Messages as Constants
		public const string gcnstMSGSave = "Record(s) Saved Successfully!";
		public const string gcnstMSGUpdate = "Record(s) Updated Successfully!";
		public const string gcnstMSGDelete = "Record(s) Deleted Successfully!";
		public const string gcnstMSGNotSaved = "Record(s) Not Saved!";
		public const string gcnstMSGNotUpdated = "Record(s) Not Updated!";
		public const string gcnstMSGNotDeleted = "Record(s) Not Deleted!";
		public const string gcnstMSGFields = "One or more Compulsory Field(s) not Filled in!";
		public const string gcnstMSGPassword = "Password & Confirm Password fields do not match!";
		public const string gcnstMSGInvalidPassword = "Invalid Password ! Please Try Again.";
		//*** Duplicate record message ********
		public const string gcnstMsgDuplicateRecord = "The record You are going to save is already in database!" + " You can't save this duplicate record.";
		public const string gcnstMsgConfirmDelete = "Are you sure you want to delete this record(s) ?";
		//'*** Email Format message *************
		public const string gcnstMsgEmail = "Not a vaild Email Format !";
		public const string gcnstMsgPieChart = "Kindly Select a valid Search criteria for Pie-Chart Display!";
		#endregion


		#region All User Permission constants
		public const string gcnstADDEditDelete = "Add/Edit";
		public const string gcnstReportsOnly = "Reports";
		public const string gcnstSendEmailFax = "Send Email & Fax";
		public const string gcnstBackupRestore = "Backup/Restore";
		#endregion


		#region All Program STATIC VARIABLES
		public static string gcnstSection1 = "Section1";
		public static string gcnstSection2 = "Section2";
		public static string gcnstSection3 = "Section3";

		public static string gcnstSold = "SOLD";
		public static string gcnstNotSold = "NOT SOLD";

		public static string gcnstSketched = "Sketched Jewellery";
		public static string gcnstStudded = "Studded Jewellery";
		#endregion

		//Fixed path for Sketch & Studded photos
//		public static string gcnstSketchPhotoPath = Application.StartupPath + "\\SketchJewellery\\";
//		public static string gcnstStuddedPhotoPath = Application.StartupPath + "\\StuddedJewellery\\";

		public static string gcnstSketchPhotoPath = clsGlobalVariable.gcnstAppPath + "\\SketchJewellery\\";
		public static string gcnstStuddedPhotoPath = clsGlobalVariable.gcnstAppPath + "\\StuddedJewellery\\";

		//Fixed prefix for Sketch & Studded photos
		public static string gcnstSketchPrefix = "SKJ_";
		public static string gcnstStuddedPrefix = "STJ_";

        //User Type
        public static string gcnstUserAdmin = "ADMIN";
        public static string gcnstUserMaster = "MASTER";
        public static string gcnstUserOperator = "OPERATOR";

	}
}
