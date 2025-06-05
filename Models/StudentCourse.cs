
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class StudentCourse : BaseEntity
    {
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; } = null!;
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
} 