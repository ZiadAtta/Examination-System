using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class UpdateCourseVM
    {
        [Required(ErrorMessage = "The CourseId Is Required .")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "The Name Of The Course IS Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The Description Of The Course IS Required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The Hours Of The Course IS Required")]
        [Range(1, 3, ErrorMessage = "The Hours Of The Course Must Be Between 1 And 3")]
        public int Hours { get; set; }
        [Required(ErrorMessage = "The Instructor ID IS Required")]
        public int InstructorId { get; set; }
    }
}
