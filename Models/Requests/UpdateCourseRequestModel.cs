namespace finanzauto_Back.Models.Requests
{
    public class UpdateCourseRequestModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
