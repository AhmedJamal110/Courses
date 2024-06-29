using Entity.Models;

namespace API.Dto
{
    public class SectionDto
    {
        public string Name { get; set; }

        public ICollection<LectureDto> Lectures { get; set; }

     


    }
}
