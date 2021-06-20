using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFVodovoz.DbModels;

namespace WPFVodovoz.Helpers
{
    public class ModelConvert
    {
        #region EmployeeActions
        public static ObservableCollection<Employee> EmployeeModelToEmployees(List<EmployeeModel> model)
        {
            ObservableCollection<Employee> result = new ObservableCollection<Employee>();

            foreach (var item in model)
            {
               result.Add(new Employee(item.Id, item.LastName, item.FirstName, item.Patronymic,
                    item.BirthDate, (GenderEnum) Enum.Parse(typeof(GenderEnum), item.Gender, true), item.DepartmentId));
            }

            return result;
        }
       
        public static Employee EmployeeModelToEmployee(EmployeeModel model)
        {
            if (model == null) return null;
            return new Employee(model.Id, model.LastName, model.FirstName, model.Patronymic,
                    model.BirthDate, (GenderEnum)Enum.Parse(typeof(GenderEnum), model.Gender, true), model.DepartmentId);
        }
        #endregion

        #region DepartmentActions
        public static ObservableCollection<Department> DepartmentModelToDepartments(List<DepartmentModel> model)
        {
            ObservableCollection<Department> result = new ObservableCollection<Department>();

            foreach (var item in model)
            {
                result.Add(new Department(item.Id, item.Title, item.DirectorId));
            }
            return result;
        }
        public static Department DepartmentModelToDepartment(DepartmentModel model)
        {
            if (model == null) return null;
           return new Department(model.Id, model.Title, model.DirectorId);
            
        }
        public static Order OrderModelToOrder(OrderModel model)
        {
            if (model == null) return null;
            return new Order(model.Id, model.Number,model.Contractor,model.OrderDate,model.AuthorId);

        }
        #endregion

        #region OrderActions
        public static ObservableCollection<Order> OrderModelToOrders(List<OrderModel> model)
        {
            ObservableCollection<Order> result = new ObservableCollection<Order>();

            foreach (var item in model)
            {
                result.Add(new Order(item.Id, item.Number, item.Contractor, item.OrderDate, item.AuthorId));

            }

            return result;
        }
        #endregion
    }
}
