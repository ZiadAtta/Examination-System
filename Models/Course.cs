using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Course:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Exam>? Exams { get; set; }
        [InverseProperty("PreRequestCourse")]
        public ICollection<PreRequest>? PreRequests { get; set; }
        [InverseProperty("MainCourse")]
        public ICollection<PreRequest>? MainCourses { get; set; }
    }
}
