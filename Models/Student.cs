namespace Examination_System.Models
{
    public class Student:User
    {
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
