using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class UpdateChoiceVM
    {
        [Required(ErrorMessage = "The Id Is Required.")]
        [Range(1,int.MaxValue,ErrorMessage = "The Id Must Be Greater Than Zero.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Text Of  Is Required.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "The Correctness Of The Choice Is Required.")]
        public bool IsCorrect { get; set; }
        [Required(ErrorMessage = "The QuestionId Is Required.")]
        public int QuestionId { get; set; }
    }
}
