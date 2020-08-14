using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class Employee
    {
        public Employee()
        {
        }

                public int EmployeeId {get;set;}
			    public string  FirstName {get;set;}
				public string MiddleName {get;set;}
				public string LastName {get;set;}
				public string EmpCode {get;set;}
			    public DateTime	DateOfBirth {get;set;}
				public string Gender {get;set;}
				public string MaritalStatus {get;set;}
				public string BloodGroup {get;set;}
				public string Nationality {get;set;}
				public string Cast {get;set;}
				public string Religion {get;set;}
				public string CorrespondanceAddress {get;set;}
				public string CorrespondanceAddressCity {get;set;}
				public string CorrespondanceAddressState {get;set;}
				public string CorrespondanceAddressPin {get;set;}
				public string PermanentAddress {get;set;}
				public string PermanentAddressCity {get;set;}
				public string PermanentAddressState {get;set;}
				public string PermanentAddressPin {get;set;}
				public string Country {get;set;}
				public string ContactNo1 {get;set;}
				public string ContactNo2 {get;set;}
				public string ContactEmail1 {get;set;}
				public string ContactEmail2 {get;set;}
				public string PassportNo {get;set;}
				public string PassportPlaceOfIssue {get;set;}
			    public DateTime? PassportIssueDate {get;set;}
				public DateTime? PassportExpiryDate {get;set;}
				public string PayMode {get;set;}
				public string BankName {get;set;}
				public string BankBranchAddress {get;set;}
				public string BankAcNo {get;set;}
				public string BankIFSCode {get;set;}
				public bool IsActive {get;set;}
                public int CreatedBy {get;set;}
                public int ModifiedBy { get; set; }
                public bool IsPermanent { get; set; }
                
                public string UserName { get; set; }
                public string Password { get; set; }
                public int CompanyId { get; set; }
                public int LeaveManagerId { get; set; }
                public int ClaimApproverId { get; set; }
                public string Photo { get; set; }
                public int ContractPeriod { get; set; }
                public string Roles { get; set; }
    }
}
