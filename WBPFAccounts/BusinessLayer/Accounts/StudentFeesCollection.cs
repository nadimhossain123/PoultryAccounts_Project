using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public  class StudentFeesCollection
    {
        public StudentFeesCollection()
        {
        }

        public void Save(Entity.Accounts.StudentFeesCollection Fees)
        {
            DataAccess.Accounts.StudentFeesCollection.Save(Fees);
        }
        public void SaveRefundableFees(Entity.Accounts.StudentFeesCollection Fees)
        {
            DataAccess.Accounts.StudentFeesCollection.SaveRefundableFees(Fees);
        }

        public Entity.Accounts.StudentFeesCollection GetAllById(int PaymentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetAllById(PaymentId);
        }
        public Entity.Accounts.StudentFeesCollection Refund_GetAllById(int PaymentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.Refund_GetAllById(PaymentId);
        }

        public DataSet GetStudentUnpaidTrans(int StudentId, int FinYrId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetStudentUnpaidTrans(StudentId, FinYrId);
        }
        public DataSet GetStudentUnpaidTransJV(int StudentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetStudentUnpaidTransJV(StudentId);
        }

        public DataSet GetStudentAdvanceTrans(int StudentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetStudentAdvanceTrans(StudentId);
        }

        public DataSet GetStudentOutstandingReport(int StudentId, string FromDate, string ToDate)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetStudentOutstandingReport(StudentId,FromDate,ToDate);
        }

        public DataSet GetMoneyReceipt(int PaymentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetMoneyReceipt(PaymentId);
        }
        public DataSet GetRefundMoneyReceipt(int PaymentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetRefundMoneyReceipt(PaymentId);
        }

        public DataSet GetAll(string MoneyReceiptNo, string FromDate, string ToDate)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetAll(MoneyReceiptNo, FromDate, ToDate);
        }

        public void Delete(int PaymentId)
        {
            DataAccess.Accounts.StudentFeesCollection.Delete(PaymentId);
        }
        public DataSet GetStudentRefundableTrans(int StudentId)
        {
            return DataAccess.Accounts.StudentFeesCollection.GetStudentRefundableTrans(StudentId);
        }
        public void DeleteCVB(int CBVHeaderId)
        {
            DataAccess.Accounts.StudentFeesCollection.DeleteCVB(CBVHeaderId);
        }
    }
}
