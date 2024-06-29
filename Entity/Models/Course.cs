using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Instructor { get; set; }
        public string Image { get; set; }
        public decimal Rating { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Stuents { get; set; }
        public string Language { get; set; }

        public string Level { get; set; }

        public ICollection<Requerment> Requerments { get; set; }

        public ICollection<Learning> Learnings { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public DateTime LastUpdate { get; set; } =  DateTime.Now;

        public ICollection<UserCourse> UserCourses { get; set; }



        public ICollection<Section> Sections { get; set; }
    }

  
}
