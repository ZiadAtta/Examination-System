using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class StudentAnswer : BaseEntity
    {
        [ForeignKey(nameof(StudentExam))]
        public int StudentExamId { get; set; }
        public StudentExam StudentExam { get; set; } = null!;
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        [ForeignKey(nameof(Choice))]
        public int? ChoiceId { get; set; } 
        public Choice? Choice { get; set; }
    }
}
