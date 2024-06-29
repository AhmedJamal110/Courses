using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public class CourseWithCategorySpec : BaseSpecification<Course>
    {
        public CourseWithCategorySpec( CourseSpec courseSpec)
             :base(C => !courseSpec.CategoryId.HasValue ||  C.CategoryId == courseSpec.CategoryId )
        {

            if (!string.IsNullOrEmpty(courseSpec.Sort))
            {
                switch (courseSpec.Sort)
                {
                    case "PriceAsc":
                        AddSortByAsc(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddSortByDesc(P => P.Price);
                        break;
                    default:
                        AddSortByAsc(P => P.Title);
                        break;

                }
            }

            AddPagination( courseSpec.PageSize *(courseSpec.PageIndex -1 ) , courseSpec.PageSize);

            InClude.Add(C => C.Requerments);
            InClude.Add(C => C.Learnings);
            InClude.Add(C => C.Category);
        }

        public CourseWithCategorySpec(Guid id):base(C => C.Id == id)
        {
            InClude.Add(C => C.Requerments);
            InClude.Add(C => C.Learnings);
            InClude.Add(C =>C.Category);
        }

    }
}
