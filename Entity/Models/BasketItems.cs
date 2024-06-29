using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class BasketItems
    {
        public int Id { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
