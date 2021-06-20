using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFVodovoz.DbModels
{
    public class DepartmentModel
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public long DirectorId { get; set; }
        public DepartmentModel(string _title, long _directorId)
        {
            Title = _title;
            DirectorId = _directorId;
        }
        public DepartmentModel()
        {

        }
    }
}
