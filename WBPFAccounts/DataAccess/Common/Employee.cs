using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Employee
    {
        public Employee()
        {
        }

        public static int Save(Entity.Common.Employee Employee)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, ParameterDirection.InputOutput, Employee.EmployeeId);
                oDm.Add("@pFirstName", SqlDbType.VarChar,100, Employee.FirstName);
                oDm.Add("@pMiddleName", SqlDbType.VarChar, 100, Employee.MiddleName);
                oDm.Add("@pLastName", SqlDbType.VarChar, 100, Employee.LastName);
                oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, Employee.EmpCode);
                oDm.Add("@pDateOfBirth", SqlDbType.DateTime, Employee.DateOfBirth);

                oDm.Add("@pGender", SqlDbType.VarChar, 10, Employee.Gender);
                oDm.Add("@pMaritalStatus", SqlDbType.VarChar, 15, Employee.MaritalStatus);
                oDm.Add("@pBloodGroup", SqlDbType.VarChar, 3, Employee.BloodGroup);
                oDm.Add("@pNationality", SqlDbType.VarChar, 20, Employee.Nationality);
                oDm.Add("@pCast", SqlDbType.VarChar, 10, Employee.Cast);
                oDm.Add("@pReligion", SqlDbType.VarChar, 10, Employee.Religion);
                oDm.Add("@pCorrespondanceAddress", SqlDbType.VarChar, 200, Employee.CorrespondanceAddress);
                oDm.Add("@pCorrespondanceAddressCity", SqlDbType.VarChar, 50, Employee.CorrespondanceAddressCity);
                oDm.Add("@pCorrespondanceAddressState", SqlDbType.VarChar, 50, Employee.CorrespondanceAddressState);
                oDm.Add("@pCorrespondanceAddressPin", SqlDbType.VarChar, 10, Employee.CorrespondanceAddressPin);

                oDm.Add("@pPermanentAddress", SqlDbType.VarChar, 200, Employee.PermanentAddress);
                oDm.Add("@pPermanentAddressCity", SqlDbType.VarChar, 50, Employee.PermanentAddressCity);
                oDm.Add("@pPermanentAddressState", SqlDbType.VarChar, 50, Employee.PermanentAddressState);
                oDm.Add("@pPermanentAddressPin", SqlDbType.VarChar, 10, Employee.PermanentAddressPin);

                oDm.Add("@pCountry", SqlDbType.VarChar,50, Employee.Country);
                oDm.Add("@pContactNo1", SqlDbType.VarChar, 15, Employee.ContactNo1);
                oDm.Add("@pContactNo2", SqlDbType.VarChar, 15, Employee.ContactNo2);
                oDm.Add("@pContactEmail1", SqlDbType.VarChar, 50, Employee.ContactEmail1);
                oDm.Add("@pContactEmail2", SqlDbType.VarChar, 50, Employee.ContactEmail2);

                oDm.Add("@pPassportNo", SqlDbType.VarChar, 30, Employee.PassportNo);
                oDm.Add("@pPassportPlaceOfIssue", SqlDbType.VarChar, 50, Employee.PassportPlaceOfIssue);
                if (Employee.PassportIssueDate == null)
                {
                     oDm.Add("@pPassportIssueDate", SqlDbType.DateTime, DBNull.Value);
                }
                else {oDm.Add("@pPassportIssueDate", SqlDbType.DateTime, Employee.PassportIssueDate);}

                if (Employee.PassportExpiryDate == null)
                {
                    oDm.Add("@pPassportExpiryDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pPassportExpiryDate", SqlDbType.DateTime, Employee.PassportExpiryDate); }

                
                oDm.Add("@pPayMode", SqlDbType.VarChar, 20, Employee.PayMode);
                oDm.Add("@pBankName", SqlDbType.VarChar, 50, Employee.BankName);
                oDm.Add("@pBankBranchAddress", SqlDbType.VarChar, 200, Employee.BankBranchAddress);
                oDm.Add("@pBankAcNo", SqlDbType.VarChar, 15, Employee.BankAcNo);
                oDm.Add("@pBankIFSCode", SqlDbType.VarChar, 15, Employee.BankIFSCode);

                oDm.Add("@pIsActive", SqlDbType.Bit, Employee.IsActive);
                oDm.Add("@pCreatedBy", SqlDbType.Int, Employee.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, Employee.ModifiedBy);
                oDm.Add("@pIsPermanent", SqlDbType.Bit, Employee.IsPermanent);
                oDm.Add("@pCompanyId", SqlDbType.Int, Employee.CompanyId);

                if (Employee.LeaveManagerId == 0)
                {
                    oDm.Add("@pLeaveManagerId", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pLeaveManagerId", SqlDbType.Int, Employee.LeaveManagerId); }

                if (Employee.ClaimApproverId == 0)
                {
                    oDm.Add("@pClaimApproverId", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pClaimApproverId", SqlDbType.Int, Employee.ClaimApproverId); }

                oDm.Add("@pPhoto", SqlDbType.VarChar, 50, Employee.Photo);
                oDm.Add("@pContractPeriod", SqlDbType.Int, Employee.ContractPeriod);
                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected=oDm.ExecuteNonQuery("usp_Employee_Save");
                Employee.EmployeeId = (int)oDm["@pEmployeeId"].Value;
                return RowsAffected;
            }
        }

        public static DataTable GetAll(string EmpCode, string FirstName)
        {
            using (DataManager oDm = new DataManager())
            {
                if (EmpCode.Trim().Length == 0)
                { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, EmpCode); }

                if (FirstName.Trim().Length == 0)
                { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Employee_GetAll");
            }
        }

        public static Entity.Common.Employee GetAllById(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeId", SqlDbType.Int, ParameterDirection.Input, EmployeeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Employee_GetAllById");
                Entity.Common.Employee Employee = new Entity.Common.Employee();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Employee.EmployeeId = EmployeeId;
                        Employee.FirstName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Employee.MiddleName = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Employee .LastName= (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        Employee.EmpCode = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        Employee.DateOfBirth = (dr[5] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[5].ToString());
                        Employee.Gender = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();
                        Employee.MaritalStatus = (dr[7] == DBNull.Value) ? "" : dr[7].ToString();
                        Employee.BloodGroup = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                        Employee.Nationality = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();
                        Employee.Cast = (dr[10] == DBNull.Value) ? "" : dr[10].ToString();
                        Employee.Religion = (dr[11] == DBNull.Value) ? "" : dr[11].ToString();
                        Employee.CorrespondanceAddress = (dr[12] == DBNull.Value) ? "" : dr[12].ToString();
                        Employee.CorrespondanceAddressCity = (dr[13] == DBNull.Value) ? "" : dr[13].ToString();
                        Employee.CorrespondanceAddressState = (dr[14] == DBNull.Value) ? "" : dr[14].ToString();
                        Employee.CorrespondanceAddressPin = (dr[15] == DBNull.Value) ? "" : dr[15].ToString();
                        Employee.PermanentAddress = (dr[16] == DBNull.Value) ? "" : dr[16].ToString();
                        Employee.PermanentAddressCity = (dr[17] == DBNull.Value) ? "" : dr[17].ToString();

                        Employee.PermanentAddressState = (dr[18] == DBNull.Value) ? "" : dr[18].ToString();
                        Employee.PermanentAddressPin = (dr[19] == DBNull.Value) ? "" : dr[19].ToString();
                        Employee.Country = (dr[20] == DBNull.Value) ? "" : dr[20].ToString();
                        Employee.ContactNo1 = (dr[21] == DBNull.Value) ? "" : dr[21].ToString();
                        Employee.ContactNo2 = (dr[22] == DBNull.Value) ? "" : dr[22].ToString();
                        Employee.ContactEmail1 = (dr[23] == DBNull.Value) ? "" : dr[23].ToString();
                        Employee.ContactEmail2 = (dr[24] == DBNull.Value) ? "" : dr[24].ToString();
                        Employee.PassportNo = (dr[25] == DBNull.Value) ? "" : dr[25].ToString();
                        Employee.PassportPlaceOfIssue = (dr[26] == DBNull.Value) ? "" : dr[26].ToString();

                        if (dr[27] == DBNull.Value) {Employee.PassportIssueDate=null;}
                        else {Employee.PassportIssueDate=DateTime.Parse(dr[27].ToString());}

                        if (dr[28] == DBNull.Value) {Employee.PassportExpiryDate=null;}
                        else {Employee.PassportExpiryDate=DateTime.Parse(dr[28].ToString());}
                        
                        Employee.PayMode = (dr[29] == DBNull.Value) ? "" : dr[29].ToString();
                        Employee.BankName = (dr[30] == DBNull.Value) ? "" : dr[30].ToString();
                        Employee.BankBranchAddress = (dr[31] == DBNull.Value) ? "" : dr[31].ToString();
                        Employee.BankAcNo = (dr[32] == DBNull.Value) ? "" : dr[32].ToString();
                        Employee.BankIFSCode = (dr[33] == DBNull.Value) ? "" : dr[33].ToString();
                        Employee.IsActive = (dr[34] == DBNull.Value) ? false : bool.Parse(dr[34].ToString());
                        Employee.IsPermanent = (dr[35] == DBNull.Value) ? false : bool.Parse(dr[35].ToString());
                        Employee.CompanyId = (dr[36] == DBNull.Value) ? 0 : int.Parse(dr[36].ToString());
                        Employee.LeaveManagerId = (dr[37] == DBNull.Value) ? 0 : int.Parse(dr[37].ToString());
                        Employee.ClaimApproverId = (dr[38] == DBNull.Value) ? 0 : int.Parse(dr[38].ToString());
                        Employee.Photo = (dr[39] == DBNull.Value) ? "" : dr[39].ToString();
                        Employee.ContractPeriod = (dr[40] == DBNull.Value) ? 0 : int.Parse(dr[40].ToString());
                        
                    }
                }
                return Employee;
            }
        }

        public static DataTable GetEmpByDesigAndDept(int DepartmentId,int DesignationId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@DepartmentId", SqlDbType.Int, DepartmentId);
                oDm.Add("@DesignationId", SqlDbType.Int, DesignationId); 

              
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeDetail");
            }
        }

        public static DataTable AuthenticateUser(string UserName, string Mode)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserName", SqlDbType.VarChar, 50, UserName);
                oDm.Add("@pMode", SqlDbType.VarChar, 50, Mode);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetUserNameAndPass");
            }
        }

        public static void ChangePassword(int userid, string password)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, ParameterDirection.Input, userid);
                oDm.Add("@pPassword", SqlDbType.VarChar, 50, password);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_ChangePassword");


            }
        }
    }
}
