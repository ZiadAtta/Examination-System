using System.ComponentModel.DataAnnotations;
namespace Examination_System.ViewModels
{
    public class StudentExamVM
    {
        [Required(ErrorMessage = "Student ID is required.")]
        [Range(1,int.MaxValue,ErrorMessage = "Student ID must be a positive integer.")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Exam ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Exam ID must be a positive integer.")]
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Course ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a positive integer.")]
        public int CourseId { get; set; }
    }
}
