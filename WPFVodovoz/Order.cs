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
    public class Order : INotifyPropertyChanged
    {
        public long Id { get; set; }

        private int number;
        private string contractor;
        private DateTime orderDate;
        private long authorId;

        public Order(long id, int _number, string _contractor, DateTime _orderDate, long _authorId)
        {
            Id = id;
            number = _number;
            contractor = _contractor;
            orderDate = _orderDate;
            authorId = _authorId;
        }

        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                OnPropertyChanged("Number");
            }
        }
        public string Contractor
        {
            get
            {
                return this.contractor;
            }
            set
            {
                this.contractor = value;
                OnPropertyChanged("Contractor");
            }
        }
        public string OrderDate
        {
            get
            {
                return $"{this.orderDate.Day}.{this.orderDate.Month}.{this.orderDate.Year}";
            }
            set
            {
                var date = value;
                this.orderDate = DateTime.Parse(date);
                OnPropertyChanged("OrderDate");
            }
        }
        public string Author
        {
            get
            {
                DbContextActions context = new DbContextActions(new Context());
                var res = context.FindEmployee(this.authorId);
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
                this.authorId = context.FindEmployeeByLastName(newName).Id;
                OnPropertyChanged("Author");
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
                    var item = db.Orders.Find(this.Id);

                    switch (prop)
                    {
                        case "Number":
                            item.Number = this.number;
                            break;
                        case "Contractor":
                            item.Contractor = this.contractor;
                            break;
                        case "OrderDate":
                            item.OrderDate = this.orderDate;
                            break;
                        case "Author":
                            item.AuthorId = this.authorId;
                            break;
                    }
                    db.SaveChanges();
                }
            }

        }
    }
}
