using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Choice : BaseEntity
    {
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public Question? Question { get; set; }
    }
}
