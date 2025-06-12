using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class CreateExamQuestionVM
    {
        [Required(ErrorMessage = "Question text is required.")]
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Question text is required.")]
        public int QuestionId { get; set; }
    }
}
