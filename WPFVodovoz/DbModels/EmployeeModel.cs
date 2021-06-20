using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFVodovoz.DbModels
{
    public class EmployeeModel
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public long DepartmentId { get; set; }
        public EmployeeModel(string _lastName, string _firstName, string _patronymic,
           DateTime _birthDate, Enum _gender, long _departmentId)
        {
            LastName = _lastName;
            FirstName = _firstName;
            Patronymic = _patronymic;
            BirthDate = _birthDate;
            Gender = _gender.ToString();
            DepartmentId = _departmentId;
        }
        public EmployeeModel()
        {

        }
    }
}
