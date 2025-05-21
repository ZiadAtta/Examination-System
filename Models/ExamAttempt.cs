using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class ExamAttempt
    {
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
        public decimal? Score { get; set; }
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }

        public Exam? Exam { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        [ForeignKey(nameof(Enrollment))]
        public int EnrollmentId { get; set; }
        public Enrollment? Enrollment { get; set; }
        public ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}
