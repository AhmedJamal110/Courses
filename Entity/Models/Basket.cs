using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string ClientId { get; set; }

        public ICollection<BasketItems> Items { get; set; } = new List<BasketItems>();

        public string? PaymentIntendId { get; set; }
        public string? ClientSecrit { get; set; }
        public void AddToBasket(Course course)
        {
          if(Items.All(I => I.CourseId != course.Id))
            {
                Items.Add( new BasketItems { Course = course});
            }
        }
    
    
        public void DeleteFromBasket(Guid courseId)
        {
           var course =  Items.FirstOrDefault(I => I.CourseId == courseId);

            Items.Remove(course);
        }
    
    
        public void ClearBasket()
        {
            PaymentIntendId = null;
            ClientSecrit = null;
            Items.Clear();
        }
    
    }
}
