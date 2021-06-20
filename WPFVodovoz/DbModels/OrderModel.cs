using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFVodovoz.DbModels
{
    public class OrderModel
    {
        [Key]
        public long Id { get; set; }
        public int Number { get; set; }
        public string Contractor { get; set; }
        public DateTime OrderDate { get; set; }
        public long AuthorId { get; set; }
        public OrderModel(int _number, string _contractor, DateTime _orderDate, long _authorId)
        {
            Number = _number;
            Contractor = _contractor;
            OrderDate = _orderDate;
            AuthorId = _authorId;
        }
        public OrderModel()
        {

        }
    }
}
