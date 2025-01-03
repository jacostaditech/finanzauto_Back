namespace finanzauto_Back.Models.Requests
{
    public class UpdateStudentRequestModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public long Identification { get; set; }

        public string? Gender { get; set; }
    }
}
