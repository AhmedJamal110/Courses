using Entity.Models;

namespace API.Dto
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public decimal Rating { get; set; }
        public string Instructor { get; set; }
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Stuents { get; set; }
        public string Language { get; set; }

        public string Level { get; set; }

        public ICollection<RequermentDto> Requerments { get; set; }

        public ICollection<LearningDto> Learnings { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }



    }
}
