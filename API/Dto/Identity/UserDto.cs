using Entity.Models;

namespace API.Dto.Identity
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public BasketDto BasketDto { get; set; }


        public List<Course> Courses { get; set; }
    }
}
