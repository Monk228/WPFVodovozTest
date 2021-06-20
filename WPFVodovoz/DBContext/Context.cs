using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFVodovoz.DbModels;

namespace WPFVodovoz.DBContext
{
   public class Context : System.Data.Entity.DbContext
    {
        public Context()
              : base("Server=localhost\\SQLEXPRESS05;Database=dbo;Trusted_Connection=True;")
        { }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<OrderModel> Orders { get; set; }


    }
}
