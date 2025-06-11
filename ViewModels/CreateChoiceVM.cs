using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class CreateChoiceVM
    {
        [Required(ErrorMessage = "The Text Of  Is Required.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "The Correctness Of The Choice Is Required.")]
        public bool IsCorrect { get; set; }
        [Required(ErrorMessage = "The QuestionId Is Required.")]
        public int QuestionId { get; set; }
    }
}
