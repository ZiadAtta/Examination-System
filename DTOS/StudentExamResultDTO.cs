namespace Examination_System.DTOS
{
    public class StudentExamResultDTO
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public decimal Score { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage => TotalQuestions == 0 ? 0 : Math.Round(Score / TotalQuestions * 100, 2);
        public DateTime SubmissionTime { get; set; }
    }

}
