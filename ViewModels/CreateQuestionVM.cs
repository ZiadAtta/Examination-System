using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class CreateQuestionVM
    {
        [Required(ErrorMessage = "Question text is required.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Difficulty level is required.")]
        public int Difficulty { get; set; }
        [Required(ErrorMessage ="The Is Required")]
        public int InstructorId { get; set; }
    }
}
