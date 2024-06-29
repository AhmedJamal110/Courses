using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public class CourseWithFilteration : BaseSpecification<Course>
    {
        public CourseWithFilteration(CourseSpec courseSpec):
         base( C => (!courseSpec.CategoryId.HasValue || C.CategoryId == courseSpec.CategoryId)&&
            (!string.IsNullOrEmpty(courseSpec.Search) || C.Title.ToLower().Contains(courseSpec.Search)))
        {
            
        }

    }
}
