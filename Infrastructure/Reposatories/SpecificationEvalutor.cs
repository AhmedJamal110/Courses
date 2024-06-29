using Entity.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public static class SpecificationEvalutor<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InnerQuery , ISpecification<T> Spec)
        {
            var Query = InnerQuery;

            if (Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);

            if(Spec.IsPagination)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            if (Spec.Sort is not null)
                Query = Query.OrderBy(Spec.Sort);

            if (Spec.SortByDesc is not null)
                Query = Query.OrderByDescending(Spec.SortByDesc);

            Query = Spec.InClude.Aggregate(Query, (currentQuery, includeQuey) => currentQuery.Include(includeQuey));

            return Query;
        }


    }
}
