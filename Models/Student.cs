namespace Examination_System.Models
{
    public class Student:User
    {
        public ICollection<Enrollment>? Enrollments { get; set; } 
    }
}
