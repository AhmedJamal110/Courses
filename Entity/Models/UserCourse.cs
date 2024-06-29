using Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class UserCourse
    {
        public int CurrentLecture { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser{ get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

    }
}
