using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFVodovoz.DBContext;

namespace WPFVodovoz.Helpers
{
    public class DbContextActions
    {
        public Context db;
        public DbContextActions(Context _db)
        {
            db = _db;
        }
        public Department FindDepartment(string title)
        {
            if (title == "")
            {
                return null;
            }
            var res = db.Departments.SingleOrDefault(z => z.Title == title);
            return ModelConvert.DepartmentModelToDepartment(res);

        }

        public Department FindDepartment(long depId)
        {
            if (depId == 0)
            {
                return null;
            }
            var res = db.Departments.SingleOrDefault(z => z.Id == depId);        
            return ModelConvert.DepartmentModelToDepartment(res);
           
        }
        public Employee FindEmployee(long empId)
        {
            var res = db.Employees.SingleOrDefault(z => z.Id == empId);
            return ModelConvert.EmployeeModelToEmployee(res);
        }
        public Employee FindEmployeeByLastName(string _name)
        {
            string[] name = _name.Split(' ');
            string lastname = name[1];
            var res = db.Employees.SingleOrDefault(z => z.LastName == lastname);
            return ModelConvert.EmployeeModelToEmployee(res);
        }
    }
    }

