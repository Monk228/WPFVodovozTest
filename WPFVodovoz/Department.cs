using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPFVodovoz.DBContext;
using WPFVodovoz.Helpers;

namespace WPFVodovoz
{
    public class Department : INotifyPropertyChanged
    {
        public long Id { get; set; }

        private string title;
        private long directorId;

        public Department(long id, string _title, long _directorId)
        {
            Id = id;
            title = _title;
            directorId = _directorId;
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Director
        {
            get
            {
                DbContextActions context = new DbContextActions(new Context());
                var res = context.FindEmployee(this.directorId);
                if (res != null)
                {
                    return res.FirstName + " " + res.LastName;
                }
                return "";
            }
            set
            {
                
                string newName = value;
                DbContextActions context = new DbContextActions(new Context());
                this.directorId = context.FindEmployeeByLastName(newName).Id;
                
                OnPropertyChanged("Director");
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
                    var item = db.Departments.Find(this.Id);

                    switch (prop)
                    {
                        case "Title":
                            item.Title = this.title;
                            break;
                        case "Director":
                            item.DirectorId = this.directorId;
                            break;
                    }
                    db.SaveChanges();
                }
            }
                
        }
    }
}