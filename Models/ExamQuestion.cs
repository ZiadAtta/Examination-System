using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class ExamQuestion :BaseEntity
    {
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        public Exam? Exam { get; set; }
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
