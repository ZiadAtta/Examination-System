using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class SubmitExamVM
    {
        [Required(ErrorMessage= "Student ID is required.")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Course ID is required.")]
        public int ExamId { get; set; }
        public List<StudentAnswerVM> Answers { get; set; }
    }

    public class StudentAnswerVM
    {
        [Required(ErrorMessage = "Question ID is required.")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Choice ID is required.")]
        public int ChoiceId { get; set; }
    }

}
