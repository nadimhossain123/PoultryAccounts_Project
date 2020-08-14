using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Employee
    {
        public Employee()
        {
        }

        public int Save(Entity.Common.Employee Employee)
        {
          return  DataAccess.Common.Employee.Save(Employee);
        }

        public DataTable GetAll(string EmpCode, string FirstName)
        {
            return DataAccess.Common.Employee.GetAll(EmpCode, FirstName);
        }

        public Entity.Common.Employee GetAllById(int EmployeeId)
        {
            return DataAccess.Common.Employee.GetAllById(EmployeeId);
        }

        public DataTable AuthenticateUser(string UserName, string Mode)
        {
            return DataAccess.Common.Employee.AuthenticateUser(UserName, Mode);
        }

        public void ChangePassword(int userid, string password)
        {
            DataAccess.Common.Employee.ChangePassword(userid,password);
        }
        public DataTable GetEmpByDesigAndDept(int DepartmentId, int DesignationId)
        {
            return DataAccess.Common.Employee.GetEmpByDesigAndDept(DepartmentId, DesignationId);
        }
    }
}
