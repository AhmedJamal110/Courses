using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> InClude { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> Sort { get; set; }
        public Expression<Func<T, object>> SortByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T , bool>> expression ) 
        {
            Criteria = expression;
        }

        public void AddSortByAsc(Expression<Func<T , object>> expression)
        {
            Sort = expression;
        }

        public void AddSortByDesc(Expression<Func<T, object>> expression)
        {
            SortByDesc = expression;
        }
   
    
        public void AddPagination(int skip , int take)
        {
            Skip = skip;
            Take = take;
            IsPagination = true;
        }
    
    }
}
