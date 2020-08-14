using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Company
    {
        public Company()
        {
        }

        public static int Save(Entity.Common.Company Company)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pCompanyId", SqlDbType.Int, Company.CompanyId);
                oDm.Add("@pCompanyName", SqlDbType.VarChar, 150, Company.CompanyName);
                oDm.Add("@pCompanyAliasName", SqlDbType.VarChar, 150, Company.CompanyAliasName);
                oDm.Add("@pCompanyAddress", SqlDbType.VarChar, 500, Company.CompanyAddress);
                oDm.Add("@pCompanyPhoneNo", SqlDbType.VarChar, 15, Company.CompanyPhoneNo);
                oDm.Add("@pCompanyPhoneFax", SqlDbType.VarChar, 15, Company.CompanyPhoneFax);
                oDm.Add("@pCompanyEmailId", SqlDbType.VarChar, 50, Company.CompanyEmailId);
                oDm.Add("@pCompanyPTax", SqlDbType.VarChar, 50, Company.CompanyPTax);
                oDm.Add("@pCompanyTanNo", SqlDbType.VarChar, 50, Company.CompanyTanNo);
                oDm.Add("@pCompanyPFNo", SqlDbType.VarChar, 50, Company.CompanyPFNo);
                oDm.Add("@pCompanyESINo", SqlDbType.VarChar, 50, Company.CompanyESINo);
                oDm.Add("@pCompanyCIN", SqlDbType.VarChar, 50, Company.CompanyCIN);
                oDm.Add("@pCompanyVATRegistrationNo", SqlDbType.VarChar, 50, Company.CompanyVATRegistrationNo);
                oDm.Add("@pCompanyCSTRegistrationNo", SqlDbType.VarChar, 50, Company.CompanyCSTRegistrationNo);
                oDm.Add("@pCompanySTRegistrationNo", SqlDbType.VarChar, 50, Company.CompanySTRegistrationNo);
                oDm.Add("@pCompanyWebsite", SqlDbType.VarChar, 50, Company.CompanyWebsite);
                oDm.Add("@pCompanyContactPersonName", SqlDbType.VarChar, 50, Company.CompanyContactPersonName);
                oDm.Add("@pCompanyContactPersonNo", SqlDbType.VarChar, 50, Company.CompanyContactPersonNo);
                oDm.Add("@pCompanyCUser_UserId", SqlDbType.Int, Company.CompanyCUser_UserId);
                oDm.Add("@pCompanyType", SqlDbType.VarChar, 50, Company.CompanyType);
                oDm.Add("@pCompanyPANNo", SqlDbType.VarChar, 50, Company.CompanyPANNo);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Company_Save");
            }
        }

        public static Entity.Common.Company GetAllById(int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pCompanyId", SqlDbType.Int, CompanyId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Company_GetAllById");
                Entity.Common.Company Company = new Entity.Common.Company();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Company.CompanyId = CompanyId;
                        Company.CompanyName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Company.CompanyAliasName = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Company.CompanyAddress = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        Company.CompanyPhoneNo = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        Company.CompanyPhoneFax = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                        Company.CompanyEmailId = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();
                        Company.CompanyPTax = (dr[7] == DBNull.Value) ? "" : dr[7].ToString();
                        Company.CompanyTanNo = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                        Company.CompanyPFNo = (dr[9] == DBNull.Value) ? "" : dr[9].ToString();
                        Company.CompanyESINo = (dr[10] == DBNull.Value) ? "" : dr[10].ToString();
                        Company.CompanyCIN = (dr[11] == DBNull.Value) ? "" : dr[11].ToString();
                        Company.CompanyVATRegistrationNo = (dr[12] == DBNull.Value) ? "" : dr[12].ToString();
                        Company.CompanyCSTRegistrationNo = (dr[13] == DBNull.Value) ? "" : dr[13].ToString();
                        Company.CompanySTRegistrationNo = (dr[14] == DBNull.Value) ? "" : dr[14].ToString();
                        Company.CompanyWebsite = (dr[15] == DBNull.Value) ? "" : dr[15].ToString();
                        Company.CompanyContactPersonName = (dr[16] == DBNull.Value) ? "" : dr[16].ToString();
                        Company.CompanyContactPersonNo = (dr[17] == DBNull.Value) ? "" : dr[17].ToString();
                        Company.CompanyType = (dr[18] == DBNull.Value) ? "None" : dr[18].ToString();
                        Company.CompanyPANNo = (dr[19] == DBNull.Value) ? "" : dr[19].ToString();

                    }
                }
                return Company;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Company_GetAll");
            }
        }

        public static int GetCompanyId(string CompanyType)
        {
            using (DataManager oDm = new DataManager())
            {
                int CompanyId = 0;
                string query = string.Format("Select CompanyId from Company where CompanyType='{0}'", CompanyType);
                oDm.CommandType = CommandType.Text;
                SqlDataReader dr = oDm.ExecuteReader(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CompanyId = int.Parse(dr[0].ToString());
                    }
                }

                return CompanyId;
            }
        }
    }
}
