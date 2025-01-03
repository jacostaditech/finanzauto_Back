using finanzauto_Back.DTOs;

namespace finanzauto_Back.Models.Requests
{
    public class CreateStudentRequestModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public long Identification { get; set; }

        public string? Gender { get; set; }

        public Student CopyToDTO()
        {
            return new Student
            {
                FirstName = FirstName,
                LastName = LastName,
                Identification = Identification,
                Gender = Gender
            };
        }
    }
}
