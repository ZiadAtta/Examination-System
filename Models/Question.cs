using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Question
    {
        public string? Text { get; set; }
        public QuestionDifficulty Difficulty { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(Instructor))]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Choice>? Choices { get; set; }
        public ICollection<ExamQuestion>? ExamQuestions { get; set; }
    }

    public enum QuestionDifficulty
    {
        Simple,
        Medium,
        Hard
    }
}
