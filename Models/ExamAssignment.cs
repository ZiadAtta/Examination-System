using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class ExamAssignment
    {
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        public Exam? Exam { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
