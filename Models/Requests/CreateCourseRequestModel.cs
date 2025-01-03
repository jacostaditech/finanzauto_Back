using finanzauto_Back.DTOs;

namespace finanzauto_Back.Models.Requests
{
    public class CreateCourseRequestModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;


        public Course CopyToDTO()
        {
            return new Course
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
