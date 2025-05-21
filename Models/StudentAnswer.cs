namespace Examination_System.Models
{
    public class StudentAnswer : BaseEntity
    {
        public int ExamAttemptId { get; set; }
        public ExamAttempt ExamAttempt { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public int? ChoiceId { get; set; } 
        public Choice? Choice { get; set; }
    }
}
