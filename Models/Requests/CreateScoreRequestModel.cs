using finanzauto_Back.DTOs;

namespace finanzauto_Back.Models.Requests
{
    public class CreateScoreRequestModel
    {

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public decimal Score1 { get; set; }


        public Score CopyToDTO()
        {
            return new Score
            {
                Score1 = Score1,
                StudentId = StudentId,
                CourseId = CourseId
            };
        }
    }
}
