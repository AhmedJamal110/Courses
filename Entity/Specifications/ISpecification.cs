using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public interface ISpecification<T> where T  : class
    {

        public Expression<Func<T , bool>> Criteria { get; set; }
        public List<Expression<Func<T , object>>> InClude { get; set; }

        public Expression<Func<T , object>> Sort { get; set; }
        public Expression<Func<T , object>> SortByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }


    }
}
