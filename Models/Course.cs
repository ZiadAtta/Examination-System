using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Course
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Exam>? Exams { get; set; }
    }
}
