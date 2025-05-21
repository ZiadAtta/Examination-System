using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Enrollment : BaseEntity
    {
        public DateTime EnrollmentDate { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<ExamAttempt> ExamAttempts { get; set; }
    }
}
