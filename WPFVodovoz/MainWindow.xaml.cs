using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFVodovoz.DBContext;
using WPFVodovoz.DbModels;
using WPFVodovoz.Helpers;

namespace WPFVodovoz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context db = new Context();
        DbContextActions dbActions;
        public MainWindow()
        {      
            DataContext = new AppViewModel();          
            InitializeComponent();
            FillCombo();
        }

        private void ClearCombo(EmployeeModel empl)
        {
            var match = empl.FirstName + " " + empl.LastName;

            createDirectorList.Items.Remove(match);
            directorList.Items.Remove(match);
            authorCombo.Items.Remove(match);
            createCombAuthor.Items.Remove(match);
        }
        private void ClearCombo(DepartmentModel dep)
        {
            depCombo.Items.Remove(dep.Title);
            createdepCombo.Items.Remove(dep.Title);
        }
        private void FillCombo()
        {
            var empls = db.Employees.ToList();
            var deps = db.Departments.ToList();
           
            foreach (var item in empls)
            {
                var match = item.FirstName + " " + item.LastName;
                if (createDirectorList.Items.Contains(match))
                {
                    continue;
                }
                else
                {
                    createDirectorList.Items.Add(match);

                }
                if (directorList.Items.Contains(match))
                {
                    continue;
                }
                else
                {
                    this.directorList.Items.Add(match);
                }
                if (authorCombo.Items.Contains(match))
                {
                    continue;
                }
                else
                {
                    authorCombo.Items.Add(match);
                }
                if (createCombAuthor.Items.Contains(match))
                {
                    continue;
                }
                else
                {
                    createCombAuthor.Items.Add(match);
                }
            }

            foreach (var item in deps)
            {
                if (depCombo.Items.Contains(item.Title))
                {
                    continue;
                }
                else
                {
                    this.depCombo.Items.Add(item.Title);
                }
                if (createdepCombo.Items.Contains(item.Title))
                {
                    continue;
                }
                else
                {
                    createdepCombo.Items.Add(item.Title);
                }
            }

            if (!creategenderCombo.Items.Contains(GenderEnum.Female))
            {
                this.creategenderCombo.Items.Add(GenderEnum.Female);
            }

            if (!creategenderCombo.Items.Contains(GenderEnum.Male))
            {
                this.creategenderCombo.Items.Add(GenderEnum.Male);
            }

            if (!genderCombo.Items.Contains(GenderEnum.Female))
            {
                this.genderCombo.Items.Add(GenderEnum.Female);
            }

            if (!genderCombo.Items.Contains(GenderEnum.Male))
            {
                this.genderCombo.Items.Add(GenderEnum.Male);
            }
        }

        #region DepartmentActions
        private void ChangeDep(object sender, RoutedEventArgs e)
        {
            EditPanelDep.IsEnabled = true;
            EditPanelDep2.IsEnabled = true;
        }
        private void SaveDep(object sender, RoutedEventArgs e)
        {

            dbActions = new DbContextActions(db);

            var currentName = this.createDirectorList.SelectedItem;
            var empl = dbActions.FindEmployeeByLastName((string)currentName);

            var dep = db.Departments.Add(new DepartmentModel()
            {
                DirectorId = empl.Id,
                Title = CreateTitleText.Text
            }) ;
          
            db.SaveChanges();

            var models = DepList.ItemsSource.Cast<Department>().ToList();

            models.Add(Helpers.ModelConvert.DepartmentModelToDepartment(dep));
            DepList.ItemsSource = models;
            FillCombo();
            DepList.Items.Refresh();
        }
        private void DeleteDep(object sender, RoutedEventArgs e)
        {
            var dep = TakeDep();
            
            db.Departments.Remove(dep);
            db.SaveChanges();

            var models = DepList.ItemsSource.Cast<Department>().ToList();
            var model = (Department)DepList.SelectedItem;

            models.Remove(model);
            
            DepList.ItemsSource = models;
            DepList.Items.Refresh();

            ClearCombo(dep);
        }
        private DepartmentModel TakeDep()
        {
            var item = (Department)this.DepList.SelectedItem;
            if (item == null)
            {
                return null;
            }

            var dep = db.Departments.Where(i => i.Id == item.Id).ToList();
            if (dep.Count == 0)
            {
                return null;
            }

            return dep[0];
        }

        #endregion

        #region EmployeeActions
        private void ChangeEmpl(object sender, RoutedEventArgs e)
        {
            BirthdatePanel.IsEnabled = true;
            firstNamePanel.IsEnabled = true;
            lastNamePanel.IsEnabled = true;
            patrPanel.IsEnabled = true;
            GenderPanel.IsEnabled = true;
            DepPanelEmployee.IsEnabled = true;
        }
        private void DeleteEmpl(object sender, RoutedEventArgs e)
        {
            var employee = TakeEmpl();

            db.Employees.Remove(employee);
            db.SaveChanges();

            var models = emplList.ItemsSource.Cast<Employee>().ToList();
            var model = (Employee)emplList.SelectedItem;

            models.Remove(model);

            emplList.ItemsSource = models;
            emplList.Items.Refresh();

            ClearCombo(employee);
        }        
        private void SaveEmpl(object sender, RoutedEventArgs e)
        {
            dbActions = new DbContextActions(db);

            var firstName = this.createfirstName.Text;
            var lastName = this.createlastName.Text;
            var patronymic = this.createpatronymic.Text;

            DateTime date = new DateTime();
            if (!DateTime.TryParse(this.createbirthDate.Text, out date))
            {
                this.Error.Text = "Неверная дата";
                this.Error.Visibility = Visibility.Visible;
                return;
            }

            if(firstName == "" || lastName == "")
            {
                this.Error.Text = "Поля не должны быть пустые";
                this.Error.Visibility = Visibility.Visible;
                return;
            }
            

            if(this.createdepCombo.SelectedItem == null)
            {
                this.Error.Text = "Выберите департамент";
                this.Error.Visibility = Visibility.Visible;

                return;
            }    
            var department = this.createdepCombo.SelectedItem.ToString();

            if (this.creategenderCombo.SelectedItem == null)
            {
                this.Error.Text = "Выберите пол";
                this.Error.Visibility = Visibility.Visible;
                return;
            }
            var gender = this.creategenderCombo.SelectedItem.ToString();


            var empl = new EmployeeModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                BirthDate = date,
                Gender = gender,
                DepartmentId = dbActions.FindDepartment(department).Id
            };

            db.Employees.Add(empl);
            db.SaveChanges();

            var models = emplList.ItemsSource.Cast<Employee>().ToList();

            models.Add(Helpers.ModelConvert.EmployeeModelToEmployee(empl));
            emplList.ItemsSource = models;
            FillCombo();
            emplList.Items.Refresh();
        }
        private EmployeeModel TakeEmpl()
        {
            var item = (Employee)this.emplList.SelectedItem;
            if (item == null)
            {
                return null;
            }

            var emlp = db.Employees.Where(i => i.Id == item.Id).ToList();
            if (emlp.Count == 0)
            {
                return null;
            }

            return emlp[0];
        }

        #endregion

        #region OrderActions
       
        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            var ord = TakeOrder();

            db.Orders.Remove(ord);
            db.SaveChanges();

            var models = orderList.ItemsSource.Cast<Order>().ToList();
            var model = (Order)orderList.SelectedItem;

            models.Remove(model);

            orderList.ItemsSource = models;

            FillCombo();
            orderList.Items.Refresh();
        }
        private void ChangeOrder(object sender, RoutedEventArgs e)
        {
            NumPanel.IsEnabled = true;
            ContractorPanel.IsEnabled = true;
            DatePanel.IsEnabled = true;
            AuthorPanel.IsEnabled = true;
        }
        private void SaveOrder(object sender, RoutedEventArgs e)
        {
            dbActions = new DbContextActions(db);

            if (this.createNum.Text == "" || this.createCombAuthor.SelectedItem == null)
            {
                Error3.Visibility = Visibility.Visible;
                Error3.Text = "Поля не должны быть пустыми";
                return;
            }
            var currentName = this.createCombAuthor.SelectedItem;
            var empl = dbActions.FindEmployeeByLastName((string)currentName);

            int id = 0;
            if (!Int32.TryParse(this.createNum.Text,out id))
            {
                Error3.Visibility = Visibility.Visible;
                Error3.Text = "Введите корректный номер";
                return;
            }
            
            DateTime date = new DateTime();

            if(!DateTime.TryParse(this.creatDateOrder.Text,out date))
            {
                Error3.Visibility = Visibility.Visible;
                Error3.Text = "Ввведите корректную дату";
                return;
            }
            var ord = new OrderModel()
            {
                Number = Convert.ToInt32(this.createNum.Text),
                AuthorId = empl.Id,
                Contractor = this.createContractor.Text,
                OrderDate = DateTime.Parse(this.creatDateOrder.Text)
            };

            db.Orders.Add(ord);
            db.SaveChanges();

            var models = orderList.ItemsSource.Cast<Order>().ToList();

            models.Add(Helpers.ModelConvert.OrderModelToOrder(ord));
            orderList.ItemsSource = models;
            FillCombo();
            orderList.Items.Refresh();
        }
        private OrderModel TakeOrder()
        {
            var item = (Order)this.orderList.SelectedItem;
            if (item == null)
            {
                return null;
            }

            var ord = db.Orders.Where(i => i.Id == item.Id).ToList();
            if (ord.Count == 0)
            {
                return null;
            }

            return ord[0];
        }

        #endregion
    }
}
