using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public class CategoetWithCourseSpec : BaseSpecification<Category>
    {
        public CategoetWithCourseSpec(int id):base(C => C.Id == id)
        {
            InClude.Add(C => C.Courses);
        }


    }
}
