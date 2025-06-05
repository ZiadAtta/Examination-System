
namespace Examination_System.Models
{
    public class StudentExam : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public decimal Score { get; set; }

        public DateTime SubmissionTime { get; set; }

        public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();
    }
} 