using Examination_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class UpdateQuestionDTO
    {
        [Required(ErrorMessage ="The Id Is Required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Question text is required.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Difficulty level is required.")]
        public QuestionDifficulty Difficulty { get; set; }
        [Required(ErrorMessage = "The Is Required")]
        public int InstructorId { get; set; }
    }
}
