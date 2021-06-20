using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFVodovoz.DBContext;
using WPFVodovoz.DbModels;
using WPFVodovoz.Helpers;

namespace WPFVodovoz
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private Employee selectedEmployee;
        private ObservableCollection<Employee> employees;

		private Department selectedDepartment;
		private ObservableCollection<Department> departments;

		private Order selectedOrder;
		private ObservableCollection<Order> orders;
		public AppViewModel()
		{
			using (Context _db = new Context())
			{				
				if (_db.Employees.Count() == 0)
				{
					EmployeeModel emp1 = new EmployeeModel("Glazev", "Andrey", "Anatolyevich", new DateTime(1999, 6, 7), GenderEnum.Male, 2);
					EmployeeModel emp2 = new EmployeeModel("Perfilov", "Ivan", "Alexandrovich", new DateTime(2000, 5, 12), GenderEnum.Male, 1);
					EmployeeModel emp3 = new EmployeeModel("Stratila", "Vadim", "Georgievich", new DateTime(2000, 5, 12), GenderEnum.Male, 3);

					DepartmentModel dep1 = new DepartmentModel("IT", 3);
					DepartmentModel dep2 = new DepartmentModel("Business", 1);
					DepartmentModel dep3 = new DepartmentModel("Business", 2);


					OrderModel ord1 = new OrderModel(124, "OOO AutoRu", new DateTime(1999, 6, 7), 2);
					OrderModel ord2 = new OrderModel(635, "OAO IOGames", new DateTime(2012, 10, 4), 1);
					OrderModel ord3 = new OrderModel(635, "OAO RiotsWarGame", new DateTime(2011, 8, 3), 3);

					_db.Employees.Add(emp1);
					_db.Employees.Add(emp2);
					_db.Employees.Add(emp3);

					_db.Departments.Add(dep1);
					_db.Departments.Add(dep2);
					_db.Departments.Add(dep3);

					_db.Orders.Add(ord1);
					_db.Orders.Add(ord2);
					_db.Orders.Add(ord3);

					_db.SaveChanges();					
				}
				
			}
			Context db = new Context();

			employees = new ObservableCollection<Employee>();
			departments = new ObservableCollection<Department>();
			orders = new ObservableCollection<Order>();


			employees = ModelConvert.EmployeeModelToEmployees(db.Employees.ToList());

			departments = ModelConvert.DepartmentModelToDepartments(db.Departments.ToList());

			orders = ModelConvert.OrderModelToOrders(db.Orders.ToList());
		}

	
		public Employee SelectedEmployee
		{
			get
			{
				return this.selectedEmployee;
			}

			set
			{
				this.selectedEmployee = value;
				OnPropertyChanged("SelectedEmployee");
			}
		}
		public Department SelectedDepartment
		{
			get
			{
				return this.selectedDepartment;
			}

			set
			{
				this.selectedDepartment = value;
				OnPropertyChanged("SelectedDepartment");
			}
		}
		public Order SelectedOrder
		{
			get
			{
				return this.selectedOrder;
			}

			set
			{
				this.selectedOrder = value;
				OnPropertyChanged("SelectedOrder");
			}
		}

		public ObservableCollection<Employee> Employees
		{
			get
			{
				return this.employees;
			}
		}
		public ObservableCollection<Department> Departments
		{
			get
			{
				return this.departments;
			}      
		}
		public ObservableCollection<Order> Orders
		{
			get
			{
				return this.orders;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
