namespace finanzauto_Back.Models.Requests
{
    public class UpdateTeacherRequestModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
