using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public  class StudentJournalTrn
    {
     public string CVoucherNo {get; set;}
	 public int CompanyID_FK {get; set;}
	 public int FinYearID_FK {get; set;}
	 public int BranchID_FK {get; set;}
	 public int DataFlow {get; set;}
	 public DateTime CVoucherDate {get; set;}
	 public int  DailySrlNo {get; set;}
	 public int LedgerId_FK {get; set;}
	 public string TransactionType {get; set;}
	 public string ModeOfTransaction {get; set;}
	 public string ChequeNo {get; set;}
	 public DateTime ChequeDate {get; set;}
	 public string DrawnOn {get; set;}
	 public int ParentLedgerId_FK {get; set;}
	 public decimal TotalAmount {get; set;}
	 public string CVNarration 	{get; set;}
	 public int OperationBy	{get; set;}
	 public int CompanyID_FK2 {get; set;}
     public string PaymentDetailsXML { get; set; }
     public int Semester { get; set; }
     public string XMLCashBankVoucherDetails { get; set; }
    }
}
