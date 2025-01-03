namespace finanzauto_Back.Models.Requests
{
    public class UpdateScoreRequestModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public decimal Score1 { get; set; }
    }
}
