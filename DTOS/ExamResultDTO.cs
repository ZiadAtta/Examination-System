namespace Examination_System.DTOS
{
    public class ExamResultDTO
    {
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage => TotalQuestions == 0 ? 0 : (decimal)CorrectAnswers / TotalQuestions * 100;
    }
}
