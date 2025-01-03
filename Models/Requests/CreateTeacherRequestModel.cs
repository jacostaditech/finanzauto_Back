using finanzauto_Back.DTOs;

namespace finanzauto_Back.Models.Requests
{
    public class CreateTeacherRequestModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;


        public Teacher CopyToDTO()
        {
            return new Teacher
            {
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }

    public class AsignTeacherCourse()
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
