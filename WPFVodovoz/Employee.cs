using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFVodovoz.DBContext;
using WPFVodovoz.Helpers;

namespace WPFVodovoz
{
    public class Employee : INotifyPropertyChanged
	{
		public long Id { get; set; }

		private string lastName;
        private string firstName;
        private string patronymic;
        private DateTime birthDate;
		private GenderEnum gender;
        private long departmentId;

        public Employee(long id, string _lastName, string _firstName, string _patronymic,
            DateTime _birthDate, GenderEnum _gender, long _departmentId)
        {
			Id = id;
            lastName = _lastName;
            firstName = _firstName;
            patronymic = _patronymic;
            birthDate = _birthDate;
			gender = _gender;
			departmentId = _departmentId;
        }

		public string LastName
		{
			get
			{
				return this.lastName;
			}
            set
            {
				this.lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get
			{
				return this.firstName;
			}
            set
            {
				this.firstName = value;
				OnPropertyChanged("FirstName");
            }
		}

		public string Patronymic
		{
			get
			{
				return this.patronymic;
			}
			set
			{
				this.patronymic = value;
				OnPropertyChanged("Patronymic");
			}
		}

		public string BirthDate
		{
			get
			{
				return $"{this.birthDate.Day}.{this.birthDate.Month}.{this.birthDate.Year}";
			}
            set
            {
				var date = value;
				this.birthDate = DateTime.Parse(date);
				OnPropertyChanged("BirthDate");
            }
		}

		public string Gender
		{
			get
			{
				return this.gender.ToString();
			}
			set
			{
				this.gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), value, true);
				OnPropertyChanged("Gender");
			}
		}

		public string Department
		{
			get
			{
				DbContextActions context = new DbContextActions(new Context());
				var res = context.FindDepartment(this.departmentId);
				if (res != null)
				{
					return res.Title;
				}
				return "";
			}
			set
			{
				DbContextActions context = new DbContextActions(new Context());
				this.departmentId = context.FindDepartment(value).Id;
				OnPropertyChanged("Department");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged; 
		public void OnPropertyChanged([CallerMemberName] string prop = "") 
		{
			if (PropertyChanged != null)
            {
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
				using (Context db = new Context())
				{
					var item = db.Employees.Find(this.Id);

					switch (prop)
					{
						case "FirstName":
							item.FirstName = this.firstName;
							break;
						case "LastName":
							item.LastName = this.lastName;
							break;
						case "Patronymic":
							item.Patronymic = this.patronymic;
							break;
						case "BirthDate":
							item.BirthDate = this.birthDate;
							break;
						case "Gender":
							item.Gender = this.gender.ToString();
							break;
						case "Department":
							item.DepartmentId = this.departmentId;
							break;
					}
					db.SaveChanges();
				}
			}
		}
	}
}
